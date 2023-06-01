using EtestSingQR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EtestSingQR.Services
{
    public class LoginUserService : DapDBbase, ILoginUserService
    {
        public LoginUserService(IConfiguration c) : base(c)
        {
        }

        public async Task<IEnumerable<LoginUserDate>> SelListTP(string KeyStr = "")
        {

            return await QueryAsync<LoginUserDate>(
                    "select LaborID,TestPlaceID,TestPlaceName,TestPlaceInit from TestPlace WITH (NOLOCK) "
                      + ((KeyStr == "") ? "" : ("where IsSchool=1 and  (LaborID like @KeyStr + '%' or TestPlaceID like @KeyStr + '%' or TestPlaceName like '%' + @KeyStr + '%' )"))
                      + " order by TestPlaceID;"
                    , new { KeyStr = ToSqlNVarChar(KeyStr) });
        }

        public async Task<IEnumerable<LoginUserDate>> SelUserLogin(string KeyStr, string UserID, string UserPas)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select a.LaborID,a.TestPlaceID,a.TestPlaceName,a.TestPlaceInit,b.UserID,b.CName,b.Permission from TestPlace a join TFMUsers b on a.TestPlaceID=b.TestPlaceID ");
            sb.Append("where UserID=@UserID and cast(decryptbypassphrase(a.TestPlaceID, [Password]) AS VARCHAR(50)) COLLATE Chinese_Taiwan_Stroke_CS_AI= @UserPas");
            if (KeyStr.Length == 3)
            {
                sb.Append(" and a.TestPlaceID=@TestPlaceID;");
            }
            else
            {
                sb.Append(" and a.LaborID=@TestPlaceID;");
            }
            return await QueryAsync<LoginUserDate>(sb.ToString(), new { UserID = ToSqlVarChar(UserID), UserPas = ToSqlVarChar(UserPas), TestPlaceID = ToSqlVarChar(KeyStr) });
        }
    }
}
