using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EtestSingQR.Services
{
    public class LoginUserTestService : ILoginUserService
    {
        public LoginUserTestService() { 

        }
        IEnumerable<LoginUserDate> MyLoginUserDate = new LoginUserDate[]
        {
            new LoginUserDate {LaborID = "0001", TestPlaceID = "TPE",TestPlaceName="台北國際考場",TestPlaceInit="台北考場",UserID="TestUser",CName="管理員",Permission="Admin" },
            new LoginUserDate {LaborID = "0002", TestPlaceID = "KHH",TestPlaceName="高雄小港考場",TestPlaceInit="高雄考場",UserID="TestUser",CName="管理員",Permission="Admin" },
            new LoginUserDate {LaborID = "0003", TestPlaceID = "HUN",TestPlaceName="東部花蓮考場",TestPlaceInit="花蓮考場",UserID="TestUser",CName="管理員",Permission="Admin" },
            new LoginUserDate {LaborID = "0004", TestPlaceID = "TSA",TestPlaceName="台北松山考場",TestPlaceInit="松山考場",UserID="TestUser",CName="管理員",Permission="Admin" },
            new LoginUserDate {LaborID = "0005", TestPlaceID = "TXG", TestPlaceName = "台中清泉崗考場", TestPlaceInit = "台中考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0006", TestPlaceID = "NRT", TestPlaceName = "東京成田考場", TestPlaceInit = "東京考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0007", TestPlaceID = "HND", TestPlaceName = "東京羽田考場", TestPlaceInit = "關東考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0008", TestPlaceID = "KIX", TestPlaceName = "大阪關西考場", TestPlaceInit = "關西考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0009", TestPlaceID = "CTS", TestPlaceName = "北海道新千歲考場", TestPlaceInit = "北海道考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0010", TestPlaceID = "NGO", TestPlaceName = "名古屋中部考場", TestPlaceInit = "中部考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "0011", TestPlaceID = "FUK", TestPlaceName = "九州福岡考場", TestPlaceInit = "九州考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "1012", TestPlaceID = "OKA", TestPlaceName = "沖繩那霸考場", TestPlaceInit = "沖繩考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "2013", TestPlaceID = "SDJ", TestPlaceName = "東北仙台考場", TestPlaceInit = "東北考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"},
            new LoginUserDate {LaborID = "3014", TestPlaceID = "HIJ", TestPlaceName = "山陽廣島考場", TestPlaceInit = "山陽考場", UserID = "TestUser", CName = "管理員", Permission = "Admin"}
        };
        public async Task<IEnumerable<T>> TestAsyn<T>(string KeyStr = "")
        {
            await Task.Delay(100); // 模擬一些非同步操作的延遲
            return (IEnumerable<T>)MyLoginUserDate.Where(x => Regex.IsMatch(x.LaborID, KeyStr, RegexOptions.IgnoreCase)
                                           || Regex.IsMatch(x.TestPlaceID, KeyStr, RegexOptions.IgnoreCase)
                                           || Regex.IsMatch(x.TestPlaceName, KeyStr, RegexOptions.IgnoreCase));
        }

        public async Task<IEnumerable<LoginUserDate>> SelListTP(string KeyStr = "")
        {
            return await TestAsyn<LoginUserDate>(KeyStr);
        }

        public async Task<IEnumerable<LoginUserDate>> SelUserLogin(string KeyStr, string UserID, string UserPas)
        {
            await Task.Delay(100); // 模擬一些非同步操作的延遲
            if (KeyStr.Length == 3)
            {
                return MyLoginUserDate.Where(x => x.TestPlaceID == KeyStr);
            }
            else
            {
                return MyLoginUserDate.Where(x => x.LaborID == KeyStr);
            }
            
        }
    }
}
