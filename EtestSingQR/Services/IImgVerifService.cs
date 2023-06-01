namespace EtestSingQR.Services
{
    public struct ImgVerifDate
    {
        public string? CheckCode { get; set; }
        public string? ImgBase64 { get; set; }
    }
    public interface IImgVerifService
    {
        ImgVerifDate GetImgVerif();
    }
}