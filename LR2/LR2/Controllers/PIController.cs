using System.Collections.Generic;
using System.Web.Http;

namespace LR2.Controllers
{
    //localhost:PORT/api/pi
    public class PIController : ApiController
    {

        //существует в единственном экземпляре для всего приложения
        private static int _result = 0;
        private static readonly Stack<int> _stack = new Stack<int>();

        [HttpGet]
        public IHttpActionResult Get()
        {
            int result = (_stack.Count > 0) ? (_result + _stack.Peek()) : _result;
            return Ok(new { result });
        }

        [HttpPost]
        public IHttpActionResult Post([FromUri] string result)
        {
            if (!int.TryParse(result, out int resultParameter))
                return BadRequest("Введите значение типа int");
            _result = resultParameter;
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put([FromUri] string add)
        {
            if (!int.TryParse(add, out int addParameter))
                return BadRequest("Введите значение типа int");
            _stack.Push(addParameter);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            if (_stack.Count <= 0)
                return BadRequest("Введите значение типа int");
            _stack.Pop();
            return Ok();
        }
    }
}