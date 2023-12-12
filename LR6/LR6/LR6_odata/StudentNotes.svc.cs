using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;

namespace LR6_odata
{
    //���������� ������, ������� ������������ ������ �����
    //WSBGSEntites - �������� ��������� ef
    public class StudentNotes : EntityFrameworkDataService<WSBGSEntities>
    {
        //��������� ������
        public static void InitializeService(DataServiceConfiguration config)
        {
            //������� ������� �������� (�������������� ���� ���� �� ������)
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            //�� �� ����� �� ������
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            //������ �����
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
