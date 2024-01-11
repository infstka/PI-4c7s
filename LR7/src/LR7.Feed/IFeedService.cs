using Microsoft.Samples.JsonFeeds;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace LR7_feed
{
    [ServiceContract]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    [ServiceKnownType(typeof(JsonFeedFormatter))]
    //JsonFeedFormatter - реализация SyndicationFeedFormatter
    public interface IFeedService
    {
        [OperationContract]
        [WebGet(UriTemplate = "*", BodyStyle = WebMessageBodyStyle.Bare)]
        SyndicationFeedFormatter CreateFeed(); // создание канала синдикации
    }
}
