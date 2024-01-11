using Microsoft.Samples.JsonFeeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using WSBGSModel;

namespace LR7_feed
{
    //реализация контракта
    public class FeedService : IFeedService
    {
        public SyndicationFeedFormatter CreateFeed()
        {
            var serviceUrl = new Uri("http://localhost:58061/StudentNotes.svc");
            //создание канала синдиакции с основными свойствами
            #region
            // создание канала синдикации с основными свойствами (title, ...)
            #endregion
            var feed = new SyndicationFeed(
                title: "Student Notes Feed",
                description: "A WCF Syndication Student Notes Feed",
                id: "id",
                lastUpdatedTime: DateTime.Now,
                feedAlternateLink: serviceUrl
            );
            //контекст службы для взаимодействия с источником данных
            #region
            // контекст службы для взаимодействия с источником данных
            #endregion
            var service = new WSBGSEntites(serviceUrl);
            //запросы к службе (получение студентов и их оценок)
            #region
            // запросы к службе для получения списков студентов и баллов
            #endregion
            var students = service.Students.Execute().ToList();
            var notes = service.Notes.Execute().ToList();
            //item канала синдикации
            #region
            // элемент <item> канала синдикации
            #endregion
            var syndicationItemsStudents = new List<SyndicationItem>();

            foreach (var student in students)
            {
                var studentNotes = string.Join(", ", notes
                    .Where(note => note.StudentId == student.Id)
                    .Select(note => $"{note.Subject} - {note.Note1}\t\n\n"));

                syndicationItemsStudents.Add(
                    new SyndicationItem(
                        title: student.Id.ToString() + " - " + student.Name,
                        content: studentNotes,
                        itemAlternateLink: null)
                );
            }
            //заполнение канала синдикации (в фид добавляются все созданные айтемы)
            #region
            // заполнение канала синдикации, в <feed> добавляются все созданные <item>
            #endregion
            feed.Items = syndicationItemsStudents;
            //извлечение формата из параметров юри запроса
            #region
            // извлекаем формат из параметров URI запроса
            #endregion
            var query = WebOperationContext.Current?.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            //форматтеры
            #region
            // в зависимости от значения query, создается и возвращается соответствующий форматтер синдикации
            #endregion
            if (query?.ToLower() == "atom")
                return new Atom10FeedFormatter(feed);
            else if (query?.ToLower() == "json")
                return new JsonFeedFormatter(feed);
            else
                return new Rss20FeedFormatter(feed);
        }
    }
}
