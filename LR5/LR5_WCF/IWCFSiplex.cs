using LR5_WCF.Models;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LR5_WCF
{
    [ServiceContract]
    public interface IWCFSiplex
    {
        [OperationContract]
        [WebGet(UriTemplate = "Add?x={x}&y={y}", ResponseFormat = WebMessageFormat.Json)]
        int Add(int x, int y);

        [OperationContract]
        [WebGet(UriTemplate = "Concat?s={s}&d={d}", ResponseFormat = WebMessageFormat.Json)]
        string Concat(string s, double d);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Sum", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        A Sum(A a1, A a2);
    }
}
