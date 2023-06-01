
namespace EtestSingQR.Services
{
    public struct LoginUserDate
    {
        public string LaborID { get; set; }
        public string TestPlaceID { get; set; }
        public string? TestPlaceName { get; set; }
        public string? TestPlaceInit { get; set; }
        public string? UserID { get; set; }
        public string? CName { get; set; }
        public string? Permission { get; set; }
    }

    public interface ILoginUserService
    {
        Task<IEnumerable<LoginUserDate>> SelListTP(string KeyStr="");
        Task<IEnumerable<LoginUserDate>> SelUserLogin(string KeyStr, string UserID, string UserPas);
    }
}