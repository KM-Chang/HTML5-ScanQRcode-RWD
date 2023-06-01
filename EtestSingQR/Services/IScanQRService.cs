using EtestSingQR.Models;

namespace EtestSingQR.Services
{
    public interface IScanQRService
    {
        public struct InAPIobj
        {
            public string FmErrItem { get; set; }
            public string FmErrNote { get; set; }
            public string FmSelKry { get; set; }
            public int FmSingType { get; set; }
        }

        Task<IEnumerable<ScanHomeViewModel>> SelSingSetDet(string TestPlaceID, string TestDate);
        Task<IEnumerable<SingStuerViewModel>> SelSingStuerDt(string TestPlaceID, string TestLotID, string[] AENO);
        Task<IEnumerable<DetListDayViewModel>> SelSingToDaySet(string TestPlaceID, string TestLotID);
        Task<int> EditSingStuer(string PermiNo, int SignType, string SignErrTypeMsg, string SignErrNote, string TestPlaceID);
        Task<IEnumerable<StudentViewModel>> SelSingData(string TestPlaceID, string TestLotID, string SetTestID);
    }
}