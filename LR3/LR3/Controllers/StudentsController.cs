using System.Web.Http;
using System.Linq;
using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Net.Http;
using LR3.DataContext;
using LR3.Models;
using System.Data.Entity.Infrastructure;

namespace LR3.Controllers
{
    public class StudentsController : ApiController
    {
        private StudentsContext DB = new StudentsContext();

        [HttpGet]
        [Route("api/students{format:regex([.](json)|[.](xml))}")]
        public object GetStudents(string format, [FromUri] string[] parameters)
        {
            var students = DB.Students.ToList();
            var localhost = Request.RequestUri.GetLeftPart(UriPartial.Authority);

            var requestParams = Request.GetQueryNameValuePairs();

            // преобразуем значение к списку, потому что может быть несколько значений для параметра запроса
            var limit = requestParams.Where(p => p.Key.Equals("limit")).ToList(); 
            var sort = requestParams.Where(p => p.Key.Equals("sort")).ToList(); 
            var offset = requestParams.Where(p => p.Key.Equals("offset")).ToList(); 
            var minid = requestParams.Where(p => p.Key.Equals("minid")).ToList(); 
            var maxid = requestParams.Where(p => p.Key.Equals("maxid")).ToList();
            var like = requestParams.Where(p => p.Key.Equals("like")).ToList(); 
            var columns = requestParams.Where(p => p.Key.Equals("columns")).ToList(); 
            var globalike = requestParams.Where(p => p.Key.Equals("globalike")).ToList();

            var resultList = students;

            if (limit.Count == 1 || offset.Count == 1)
            {
                if (limit.Count == 1 && offset.Count == 1)
                {
                    resultList = students.Skip(int.Parse(offset[0].Value)).Take(int.Parse(limit[0].Value)).ToList();
                }
                else if (limit.Count == 1 && offset.Count < 1)
                {
                    resultList = students.Take(int.Parse(limit[0].Value)).ToList();
                }
                else if (offset.Count == 1 && limit.Count < 1)
                {
                    resultList = students.Skip(int.Parse(offset[0].Value)).ToList();
                }
            }

            if (sort.Count == 1)
            {
                resultList = resultList.OrderBy(s => s.name).ToList();
            }

            if (minid.Count == 1)
            {
                int min = int.Parse(minid[0].Value);
                resultList = resultList.Where(s => s.id >= min).ToList();
            }

            if (maxid.Count == 1)
            {
                int max = int.Parse(maxid[0].Value);
                resultList = resultList.Where(s => s.id <= max).ToList();
            }

            if (like.Count == 1)
            {
                string name = like[0].Value;
                resultList = resultList.Where(s => s.name.Contains(name)).ToList();
            }

            // ответ возвращаем в соответствии со структурой класса StudentApi
            var studentApi = new List<StudentApi>();

            foreach (var student in resultList)
            {
                studentApi.Add(new StudentApi(
                    student,
                    new HateoasLink[]{
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "_self", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}", "read", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "update", "PUT"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "delete", "DELETE"),
                    new HateoasLink($"{localhost}/api/students{format}/", "create", "POST")})
               );
            }

            // совпадение шаблона с ид, именем или номером
            if (globalike.Count() == 1)
            {
                string columnValue = globalike[0].Value;
                studentApi = studentApi.Where(s => string.Concat(s.Id.ToString(), s.Name, s.Phone).Contains(columnValue)).ToList();
            }

            // строка формата: "id,name,phone"
            if (columns.Count() == 1)
            {
                string columnValue = columns[0].Value;
                string[] arr = columnValue.Split(",".ToCharArray());
                bool id = false, name = false, phone = false;

                foreach (string val in arr)
                {
                    if (val == "id")
                        id = true;

                    if (val == "name")
                        name = true;

                    if (val == "phone")
                        phone = true;
                }

                if (!id)
                {
                    foreach (var field in studentApi)
                    {
                        field.Id = -1;
                    }
                }
                if (!name)
                {
                    foreach (var field in studentApi)
                    {
                        field.Name = "";
                    }
                }
                if (!phone)
                {
                    foreach (var field in studentApi)
                    {
                        field.Phone = "";
                    }
                }
            }

            if (format.Equals(".xml"))
            {
                return Ok(studentApi);
            }

            return Json(studentApi);
        }

        [HttpGet]
        [Route("api/students{format:regex([.](json)|[.](xml))}/{id}")]
        public object GetStudent(string format, int id)
        {
            var localhost = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            Student student = DB.Students.FirstOrDefault(s => s.id == id);
            if (student == null)
                // Функция Content используется для установки содержимого HTTP-ответа
                return Content(HttpStatusCode.NotFound,
                    new CustomError(404, Request.RequestUri.GetLeftPart(UriPartial.Authority)));

            var studentApi = new List<StudentApi>();
            studentApi.Add(new StudentApi(
                    student,
                    new HateoasLink[]{
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "_self", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}", "read", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "update", "PUT"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "delete", "DELETE"),
                    new HateoasLink($"{localhost}/api/students{format}/", "create", "POST")})
               );

            if (format.Equals(".xml"))
            {
                return Ok(studentApi);
            }

            return Json(studentApi);
        }

        [HttpPost]
        [ResponseType(typeof(Student))]
        [Route("api/students{format:regex([.](json)|[.](xml))}")]
        public object AddStudent(string format, Student student)
        {
            var localhost = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest,
                    new CustomError(444, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            DB.Students.Add(student);
            try { DB.SaveChanges(); }
            catch (DbUpdateConcurrencyException)
            {
                student.id = DB.Students.ToList().OrderByDescending(s => s.id).First().id;
                if (!StudentExists(student.id))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        new CustomError(500, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
                }
                else { throw; }
            }

            return Content(HttpStatusCode.Created, new HateoasLink[]{
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "_self", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}", "read", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "update", "PUT"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "delete", "DELETE"),
                    new HateoasLink($"{localhost}/api/students{format}/", "create", "POST")});
        }

        [HttpPut]
        [ResponseType(typeof(Student))]
        [Route("api/students{format:regex([.](json)|[.](xml))}/{id}")]
        public object UpdateStudent(string format, Student student)
        {
            var localhost = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest,
                    new CustomError(444, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            Student new_student = DB.Students.FirstOrDefault(s => s.id == student.id);
            if (new_student != null)
            {
                new_student.name = student.name;
                new_student.phone = student.phone;

                try { DB.SaveChanges(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(new_student.id))
                    {
                        return Content(HttpStatusCode.InternalServerError,
                            new CustomError(500, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
                    }
                    else { throw; }
                }
            }
            else
            {
                return Content(HttpStatusCode.NotFound,
                    new CustomError(404, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            student.id = DB.Students.ToList().OrderByDescending(s => s.id).First().id;

            return Content(HttpStatusCode.OK, new StudentApi(
                    student,
                    new HateoasLink[]{
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "_self", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}", "read", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "update", "PUT"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "delete", "DELETE"),
                    new HateoasLink($"{localhost}/api/students{format}/", "create", "POST")}));
        }

        private bool StudentExists(int id)
        {
            return DB.Students.Count(stud => stud.id == id) > 0;
        }

        [HttpDelete]
        [ResponseType(typeof(Student))]
        [Route("api/students{format:regex([.](json)|[.](xml))}/{id}")]
        public object DeleteStudent(string format, int id)
        {
            var localhost = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            Student student = DB.Students.FirstOrDefault(s => s.id == id);
            if (student == null)
            {
                return Content(HttpStatusCode.NotFound,
                    new CustomError(404, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest,
                    new CustomError(400, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            DB.Students.Remove(student);
            try { DB.SaveChanges(); }
            catch (DbUpdateConcurrencyException)
            {
                if (StudentExists(id))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        new CustomError(500, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
                }
                else { throw; }
            }

            return Content(HttpStatusCode.OK, new HateoasLink[]{
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "_self", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}", "read", "GET"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "update", "PUT"),
                    new HateoasLink($"{localhost}/api/students{format}/" + student.id, "delete", "DELETE"),
                    new HateoasLink($"{localhost}/api/students{format}/", "create", "POST")});
        }
    }
}
