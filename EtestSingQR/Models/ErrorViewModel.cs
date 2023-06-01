namespace EtestSingQR.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? ErrType { get; set; }

        public string? ErrMsg { get; set; }

        public string? butUrl { get; set; }
    }
}