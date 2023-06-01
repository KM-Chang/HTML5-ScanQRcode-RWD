using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace EtestSingQR.Services
{
    public class FunService
    {
        /// <summary>
        /// 取IP
        /// </summary>
        /// <param name="myPage">來源頁面</param>
        /// <returns>IP</returns>
        public string GetClientIP(HttpContext myPage)                  //取得IP
        {
            return (myPage.Connection.RemoteIpAddress == null) ? "" : myPage.Connection.RemoteIpAddress.ToString();
        }

        public struct JWTDates       //JWT資訊
        {
            public string TPID { get; set; }
            public string LoadID { get; set; }
            public string UserID { get; set; }
        }
        /// <summary>
        /// 加密產生JWT Token
        /// </summary>
        /// <param name="sHEADER">表頭</param>
        /// <param name="sPAYLOAD">內容</param>
        /// <returns>JWT憑證</returns>
        public string EncodedJWT(object sHEADER, object sPAYLOAD)
        {
            string sHeadBlock64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sHEADER)));
            string sPAYBlock64 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sPAYLOAD)));
            string sVerifBlock64 = WebEncoders.Base64UrlEncode(MyHMACSHA256(sHeadBlock64 + "." + sPAYBlock64));
            return string.Format("{0}.{1}.{2}", sHeadBlock64, sPAYBlock64, sVerifBlock64);
        }
        /// <summary>
        /// 驗證並回傳JWT的內容
        /// </summary>
        /// <param name="TicketJWT">待驗證解析憑證</param>
        /// <returns>JSON字串</returns>
        public string DecodedJWT(string TicketJWT)
        {
            string[] JWTtree = (TicketJWT ?? "").Split('.');
            if (JWTtree.Length != 3) return "";  //無效
            if (WebEncoders.Base64UrlEncode(MyHMACSHA256(JWTtree[0] + "." + JWTtree[1])) == JWTtree[2])
            {
                //有效
                return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(JWTtree[1]));
            }
            else
            {
                //無效
                return "";
            }

        }
        /// <summary>
        /// 驗證是否為合法JWT
        /// </summary>
        /// <param name="TicketJWT">待驗證憑證</param>
        /// <returns>驗證結果 T=true F=false</returns>
        public bool VerifJWT(string TicketJWT)
        {
            string[] JWTtree = TicketJWT.Split('.');
            if (WebEncoders.Base64UrlEncode(MyHMACSHA256(JWTtree[0] + "." + JWTtree[1])) == JWTtree[2])
            {
                //有效
                return true;
            }
            else
            {
                //無效
                return false;
            }
        }
        /// <summary>
        /// 加密JWT
        /// </summary>
        /// <param name="ToTicket">要編碼的JWT</param>
        /// <returns>加密演算後的憑證</returns>
        private byte[] MyHMACSHA256(string ToTicket)
        {
            string SecrectKey = "i2A44ho1" + DateTime.Now.ToString("yyyyMMdd"); //在Secrect裡面加入伺服器時間 達到當日有效效果
            using (System.Security.Cryptography.HMACSHA256 MySHA256 = new System.Security.Cryptography.HMACSHA256(Encoding.Default.GetBytes(SecrectKey)))
            {
                return MySHA256.ComputeHash(Encoding.Default.GetBytes(ToTicket));
            }
        }

        /// <summary>
        /// 判斷報到時間是否在時間內
        /// </summary>
        /// <param name="SingTime">報到時間</param>
        /// <param name="S_time">可報到時間(起)</param>
        /// <param name="E_time">可報到時間(迄)</param>
        /// <returns>是否在時間內 true=是</returns>
        public bool SingTimeOKYN(string SingTime, string S_time, string E_time) 
        {
            if (DateTime.Parse(S_time) <= DateTime.Parse(SingTime) && DateTime.Parse(SingTime) < DateTime.Parse(E_time)) return true;
            return false;
        }
    }

    /// <summary>
    /// JSon格式類別(API返回結構)
    /// </summary>
    public struct JSonRetobjdt<T>
    {
        public int successYN { get; set; }      //1=成功  0=失敗
        public string sTPID { get; set; }         //TPID
        public string msg { get; set; }         //錯誤訊息
        public IEnumerable<T>? jsondt { get; set; }           //內容
    }
}
