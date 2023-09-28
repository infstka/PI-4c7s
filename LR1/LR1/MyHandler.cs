using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace LR1
{
    public class MyHandler : IHttpHandler, IRequiresSessionState
    {
        private int _result; 
        //может ли другой запрос использовать интерфейс
        public bool IsReusable => true;

        //обработка запроса
        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request; 
            var res = context.Response; 

            var session = HttpContext.Current.Session; 
            //получение стека из сеанса
            var stack = session["Stack"] as Stack<int>; 
            //id сесии
            Console.WriteLine(session.SessionID); 

            if (stack is null)
            {
                //если стек не существует,то создает новый
                session["Stack"] = new Stack<int>(); 
                //новое значение стека
                stack = session["Stack"] as Stack<int>; 
            }

            switch (req.HttpMethod)
            {
                case "GET":
                    //вычисление результата на основе стека и текущего значения _result
                    var result = (stack.Count > 0) ? (_result + stack.Peek()) : _result;
                    res.ContentType = "application/json"; 
                    res.Write("{\"result\": " + result + "}"); 
                    break;

                case "POST":
                    //обработка для установки значения 
                    if (!int.TryParse(req.QueryString["result"], out int resultParameter))
                    {
                        SendResponse(res, 400, "Error! Not integer");
                        break;
                    }
                    //установка нового значения 
                    _result = resultParameter; 
                    break;

                case "PUT":
                    //обработка добавления элемента в стек
                    if (!int.TryParse(req.QueryString["add"], out int addParameter))
                    {
                        SendResponse(res, 400, "Error! Not integer"); 
                        break;
                    }
                    stack.Push(addParameter);
                    break;

                case "DELETE":
                    //обработка удаления элемента из стека
                    if (stack.Count <= 0)
                    {
                        SendResponse(res, 400, "Error! Empty stack!"); 
                        break;
                    }
                    //удаление элемента из стека
                    stack.Pop(); 
                    break;

                default:
                    SendResponse(res, 405, "Error! Method is not exists!"); 
                    break;
            }
        }

        //ответ
        private void SendResponse(HttpResponse res, int code, string message)
        {
            res.StatusCode = code; 
            res.Write(message);
        }
    }
}
