namespace EtestSingQR.Models
{
    public class DetListDayViewModel
    {
        public int TestYear { get; set; } = DateTime.Now.Year;
        public string TestPlaceID { get; set; } = "";
        public string SetTestID { get; set;  } = "";
        public int PopCut { get; set; } = 0;
        public int SingCut { get; set; } = 0;
        public int NotSingCut { get; set; } = 0;
    }
}
