using EtestSingQR.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace EtestSingQR.Services
{
    public class ScanQRTestService : IScanQRService
    {
        public ScanQRTestService()
        {

        }

        //查詢現在可報到資料
        public async Task<IEnumerable<ScanHomeViewModel>> SelSingSetDet(string TestPlaceID, string TestDate)
        {
            await Task.Delay(100); // 模擬一些非同步操作的延遲
            IEnumerable<ScanHomeViewModel> MyScanHomeViewModel = new ScanHomeViewModel[]
                    {
                        new ScanHomeViewModel {TestPlaceID = "TPE", TestPlaceInit = "台北考場", SingToday = TestDate,TestLotID = "FF",SetTestID="01",SingStime="05:00:00",SingEtime="12:37:00" },
                        new ScanHomeViewModel {TestPlaceID = "TPE", TestPlaceInit = "台北考場", SingToday = TestDate,TestLotID = "FF",SetTestID="02",SingStime="12:50:00",SingEtime="22:30:00" },
                        new ScanHomeViewModel {TestPlaceID = "KHH", TestPlaceInit = "高雄考場", SingToday = TestDate,TestLotID = "FF",SetTestID="01",SingStime="05:00:00",SingEtime="12:37:00" },
                        new ScanHomeViewModel {TestPlaceID = "KHH", TestPlaceInit = "高雄考場", SingToday = TestDate,TestLotID = "FF",SetTestID="02",SingStime="12:50:00",SingEtime="22:30:00" },
                        new ScanHomeViewModel {TestPlaceID = "HUN", TestPlaceInit = "花蓮考場", SingToday = TestDate,TestLotID = "FF",SetTestID="01",SingStime="05:00:00",SingEtime="12:37:00" },
                        new ScanHomeViewModel {TestPlaceID = "HUN", TestPlaceInit = "花蓮考場", SingToday = TestDate,TestLotID = "FF",SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00" },
                        new ScanHomeViewModel {TestPlaceID = "TSA", TestPlaceInit = "松山考場", SingToday = TestDate,TestLotID = "FF",SetTestID="01",SingStime="05:00:00",SingEtime="12:37:00" },
                        new ScanHomeViewModel {TestPlaceID = "TSA", TestPlaceInit = "松山考場", SingToday = TestDate,TestLotID = "FF",SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00" },
                        new ScanHomeViewModel {TestPlaceID = "TXG", TestPlaceInit = "台中考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01",SingStime="05:00:00",SingEtime="12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "TXG", TestPlaceInit = "台中考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "NRT", TestPlaceInit = "東京考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "NRT", TestPlaceInit = "東京考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "HND", TestPlaceInit = "關東考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "HND", TestPlaceInit = "關東考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "KIX", TestPlaceInit = "關西考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "KIX", TestPlaceInit = "關西考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "CTS", TestPlaceInit = "北海道考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "CTS", TestPlaceInit = "北海道考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "NGO", TestPlaceInit = "中部考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "NGO", TestPlaceInit = "中部考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "FUK", TestPlaceInit = "九州考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "FUK", TestPlaceInit = "九州考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "OKA", TestPlaceInit = "沖繩考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "OKA", TestPlaceInit = "沖繩考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "SDJ", TestPlaceInit = "東北考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "SDJ", TestPlaceInit = "東北考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"},
                        new ScanHomeViewModel {TestPlaceID = "HIJ", TestPlaceInit = "山陽考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "01", SingStime = "05:00:00", SingEtime = "12:37:00"},
                        new ScanHomeViewModel {TestPlaceID = "HIJ", TestPlaceInit = "山陽考場", SingToday = TestDate, TestLotID = "FF", SetTestID = "02", SingStime = "12:50:00", SingEtime = "22:30:00"}
                    };
            return MyScanHomeViewModel.Where(x => x.TestPlaceID == TestPlaceID && x.SingToday == TestDate);
        }

        //查詢報到應檢人資料
        public async Task<IEnumerable<SingStuerViewModel>> SelSingStuerDt(string TestPlaceID, string TestLotID, string[] AENO)
        {
            await Task.Delay(100);
            if (AENO.Length > 6)
            {
                AENO[0] = AENO[0].Replace("#", "") + AENO[3] + AENO[4] + AENO[5] + AENO[6];
                //大於6為掃描准考證碼 反之視為身分證
                if (AENO[0] == "G00100200710002B") 
                {
                    AENO[0] = "A234567890";
                }
                else if (AENO[0] == "G00100332510023B")  {
                    AENO[0] = "A123456789";
                }
            }
            IEnumerable<SingStuerViewModel> MySingStuerViewModel = new SingStuerViewModel[]
            {
                    new SingStuerViewModel {PermiNo = "112N999AW020104", SetTestID = "01", CName  = "王○一",EName="Peter",PID="A234567890",Birthday ="1988-09-21",JobLevel="丙"
                    ,JobKindCSF="00100冷凍空調裝修",JobKindSkill="00100冷凍空調裝修",TestType="免術",SignType="1",SingStime="05:00:00",SingEtime="12:37:00"
                    ,FirstSignTime="05:01:01",SignErrTypeMsg="生日錯誤,級別錯誤",SignErrNote="忘記帶證件無法核對姓名"},
                    new SingStuerViewModel {PermiNo = "112N999AW020202", SetTestID = "02", CName  = "佐藤○○",EName="sato marumaru",PID="A123456789",Birthday ="1999-01-09",JobLevel="乙"
                    ,JobKindCSF="00100冷凍空調裝修",JobKindSkill="00100冷凍空調裝修",TestType="免術",SignType="0",SingStime="12:50:00",SingEtime="22:30:00"
                    ,FirstSignTime="12:55:01",SignErrTypeMsg="",SignErrNote=""}
             };
            return MySingStuerViewModel.Where(x=> x.PID == AENO[0]);
        }

        //寫入報到
        public async Task<int> EditSingStuer(string PermiNo, int SignType, string SignErrTypeMsg, string SignErrNote, string TestPlaceID)
        {
            await Task.Delay(100);
            return (PermiNo == "112N999AW020104" || PermiNo=="112N999AW020202") ? 1 : 0;
        }

        //查詢今日場次明細
        public async Task<IEnumerable<DetListDayViewModel>> SelSingToDaySet(string TestPlaceID, string TestLotID)
        {
            await Task.Delay(150);
            int ThisYear = DateTime.Now.Year;
            return new DetListDayViewModel[]
                    {
                        new DetListDayViewModel {TestYear = ThisYear, TestPlaceID = TestPlaceID, SetTestID  = "01",PopCut = 5,SingCut=2,NotSingCut =3 },
                        new DetListDayViewModel {TestYear = ThisYear, TestPlaceID = TestPlaceID, SetTestID  = "02",PopCut = 3,SingCut=1,NotSingCut =1 }                    
                    };
        }

        //查詢本場次未報到
        public async Task<IEnumerable<StudentViewModel>> SelSingData(string TestPlaceID, string TestLotID, string SetTestID)
        {
            await Task.Delay(150);
            return new StudentViewModel[]
                    {
                        new StudentViewModel {TestRoomID = "01",TestRoomName="第一教室", CName = "陳○華", SeatNo = "01",PermiNo = "112N999AW020101",Mobile="0911-716-XXXX",DayTel="(02)2755-8XXX",FirstSignTime="",SignErrTypeMsg="" },
                        new StudentViewModel {TestRoomID = "01",TestRoomName="第一教室", CName = "林○明", SeatNo = "02",PermiNo = "112N999AW020102",Mobile="0911-926-XXXX",DayTel="(02)1755-7XXX",FirstSignTime="",SignErrTypeMsg="" },
                        new StudentViewModel {TestRoomID = "01",TestRoomName="第一教室", CName = "李○川", SeatNo = "03",PermiNo = "112N999AW020103",Mobile="0911-716-XXXX",DayTel="(04)2845-6XXX",FirstSignTime="",SignErrTypeMsg="" },
                        new StudentViewModel {TestRoomID = "02",TestRoomName="第二教室", CName = "王○勝", SeatNo = "01",PermiNo = "112N999AW020201",Mobile="0911-946-XXXX",DayTel="(02)1725-5XXX",FirstSignTime="",SignErrTypeMsg="" },
                        new StudentViewModel {TestRoomID = "02",TestRoomName="第二教室", CName = "孫○海", SeatNo = "02",PermiNo = "112N999AW020202",Mobile="0911-916-XXXX",DayTel="(02)2441-5XXX",FirstSignTime="",SignErrTypeMsg="" }
                    };
        }
    }
}
