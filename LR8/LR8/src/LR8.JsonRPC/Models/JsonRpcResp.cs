namespace LR8.Models
{
    #region Пояснение
    // Есть три конструктора: 
    // 1: произошла ошибка и при этом id запроса - null
    // 2: ошибка, и id запроса известно
    // 3: запрос не вызвал ошибок, id запроса известно
    // Когда нет id запроса => это уведомление, которое не требует ответа.
    // Однако если произошла ошибка при обработке данного уведомления, то мы все равно вернем ответ, и у него будет "id": "null".
    #endregion
    public class JsonRpcResp
    {
        public readonly string jsonrpc = "2.0";
        public readonly object result = null;
        public readonly JsonRpcError error = null;
        public readonly string id = null;

        public JsonRpcResp(JsonRpcError error) => this.error = error;
        public JsonRpcResp(JsonRpcError error, string id)
        {
            this.id = id;
            this.error = error;
        }
        public JsonRpcResp(object result, string id)
        {
            this.result = result;
            this.id = id;
        }
    }
}
