using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using System.IO;

namespace LR4
{
    /// <summary>
    /// Summary description for Simplex
    /// </summary>
    // Атрибут [WebService] используется для настройки и описания веб-службы 
    // namespace - XML-пространство имен, которое будет использоваться для методов и типов данных, связанных с данной веб-службой
    // description - краткое описание веб-службы
    [WebService(Namespace = "http://BGS/", Description = "Simplex Service")]
    // соответствие службы профилю веб-служб BasicProfile1_1 
    // WsiProfiles.BasicProfile1_1 - это профиль соответствия, который определяет стандарты и правила, которым должна соответствовать служба
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class Simplex : System.Web.Services.WebService
    {
        // WebMethod - для метода, который может быть вызван удаленно через протокол SOAP
        // т.е. метод службы доступный для удаленного вызова
        // MessageName - псевдоним метода веб-службы; полезен при наличии одноименных методов
        [WebMethod(MessageName = "Sum_1", Description = "Sum of 2 int")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [WebMethod(MessageName = "Sum_2", Description = "Сoncatenation of string and double")]
        public string Concat(string s, double d)
        {
            return s + " " + d.ToString();
        }

        [WebMethod(MessageName = "Sum_3", Description = "Sum of fields of two [A] objects. Return [A] object")]
        public A Sum(A a1, A a2)
        {
            // Получаем необходимые данные из объекта HttpContext.Current.Request
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            string userAgent = HttpContext.Current.Request.UserAgent;

            // Записываем данные в файл "LR4.txt"
            string requestData = $"User Host Address: {userHostAddress}, User Agent: {userAgent}";
            File.WriteAllText(@"D:\BSTU\PI-4c7s\LR4\LR4.txt", requestData);

            return new A(a1.s + a2.s, a1.k + a2.k, a1.f + a2.f);
        }

        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(MessageName = "Sum_4", Description = "Sum of 2 int. Response JSON")]
        public int Adds(int x, int y)
        {
            return x + y;
        }
    }
}