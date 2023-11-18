using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LR3.Models
{
    // Этот класс также представляет студента, но его структура данных не привязана к базе данных и используется для предоставления данных через веб-сервис
    // StudentApi позволяет отправить клиенту информацию из класса Student + дополнительно гипермедиа ссылки
    public class StudentApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [XmlIgnore]
        public HateoasLink[] HateoasLinks { get; set; }

        public StudentApi()
        {

        }

        public StudentApi(Student student, HateoasLink[] hateoasLinks)
        {
            Id = student.id;
            Name = student.name;
            Phone = student.phone;
            HateoasLinks = hateoasLinks;
        }
    }
}