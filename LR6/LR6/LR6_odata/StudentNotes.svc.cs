using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;

namespace LR6_odata
{
    //объ€вление класса, который представл€ет службу одата
    //WSBGSEntites - контекст сущностей ef
    public class StudentNotes : EntityFrameworkDataService<WSBGSEntities>
    {
        //настройка службы
        public static void InitializeService(DataServiceConfiguration config)
        {
            //правило доступа сущности (предоставление всех прав на доступ)
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            //то же самое но службе
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            //верси€ одата
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
