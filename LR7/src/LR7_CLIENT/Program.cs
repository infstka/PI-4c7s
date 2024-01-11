using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace LR7_CLIENT
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            // Создание XmlReader, который будет использоваться для чтения XML данных из веб-сервиса.
            // Адрес "http://localhost:8733/SyndicationService/FeedService/" предполагает,
            // что на локальном хосте работает веб-сервис с каналом синдикации.
            #endregion
            XmlReader xmlReader = XmlReader.Create("http://localhost:8733/SyndicationService/FeedService/");
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            foreach (SyndicationItem item in feed.Items)
            {
                TextSyndicationContent content = item.Content as TextSyndicationContent;
                if (content != null)
                {
                    Console.WriteLine(item.Title.Text + ":");
                    string[] subjects = content.Text.Split(',');
                    foreach (string subject in subjects)
                    {
                        Console.WriteLine(subject.Trim());
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
