using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LR3.Models
{
    // класс гипермедийной ссылки
    [DataContractAttribute]
    public class HateoasLink
    {
        [DataMemberAttribute]
        public string Href { get; set; } // на что ссылка
        [DataMemberAttribute]
        public string Rel { get; set; } // какая связь между ресурсами
        [DataMemberAttribute]
        public string Method { get; set; } // HTTP-метод, который можно использовать для взаимодействия с данным ресурсом

        public HateoasLink(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}