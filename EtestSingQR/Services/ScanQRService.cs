using EtestSingQR.Models;
using System.Text;

namespace EtestSingQR.Services
{
    public class ScanQRService : DapDBbase, IScanQRService
    {
        public ScanQRService(IConfiguration c) : base(c)
        {

        }

        //查詢現在可報到資料
        public async Task<IEnumerable<ScanHomeViewModel>> SelSingSetDet(string TestPlaceID, string TestDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select a.TestPlaceID,a.TestPlaceInit,Convert(varchar(10),b.TestDate,120) as SingToday,b.TestLotID,c.SetTestID");
            sb.Append(",Convert(varchar(5),DATEADD(minute,-30,[StartTime]),108) as SingStime,Convert(varchar(5),DATEADD(minute,+15,[StartTime]),108) as SingEtime");
            sb.Append(" from TestPlace a WITH (NOLOCK) join TestLot b WITH (NOLOCK) on a.TestPlaceID=b.TestPlaceID");
            sb.Append(" left join (select * from TestLotDetail WITH (NOLOCK) where TestYear=@TestYear and CONVERT (time, DATEADD(minute,+16,[StartTime]))>CONVERT (time,GETDATE())) c ");
            sb.Append(" on a.TestPlaceID=c.TestPlaceID and b.TestLotID=c.TestLotID and b.TestYear=c.TestYear where a.TestPlaceID=@TestPlaceID and b.TestDate=@TestDate order by StartTime;");
            return await QueryAsync<ScanHomeViewModel>(sb.ToString(), new { TestPlaceID= ToSqlChar(TestPlaceID,3), TestYear = ToSqlChar(TestDate.Substring(0, 4), 4), TestDate = ToSqlVarChar($"{TestDate} 00:00:00.000") });
        }

        //查詢報到應檢人資料
        public async Task<IEnumerable<SingStuerViewModel>> SelSingStuerDt(string TestPlaceID, string TestLotID,string[] AENO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select a.PermiNo,a.SetTestID,a.CName,b.EName,a.PID,CONVERT(varchar(10),b.Birthday,120) as Birthday,case a.joblevel when '1' then '甲' when '2' then '乙' when '4' then '單一' else '丙' end as JobLevel");
            sb.Append(",JobKindID + JobKindName as JobKindCSF,a.JobKindTechID + c.JobKindTechName as JobKindSkill,case TestType when 'B' then '免術' else '全測' end as TestType,CONVERT(varchar(8),isnull(d.FirstSignTime,getdate()),108) as FirstSignTime,d.SignType");
            sb.Append(",Convert(varchar(8),DATEADD(minute,-30,[StartTime]),108) as SingStime,Convert(varchar(8),DATEADD(minute,+16,[StartTime]),108) as SingEtime,d.SignErrTypeMsg,d.SignErrNote");
            sb.Append(" from TestStudentDetail a join Student b on a.PID=b.PID and a.CName=b.CName");
            sb.Append(" join TestLotDetail e on a.TestYear=e.TestYear and a.TestPlaceID=e.TestPlaceID and a.TestLotID=e.TestLotID and a.SetTestID=e.SetTestID");
            sb.Append(" left join csf.dbo.JobKindTech c on a.JobKindTechID=c.JobKindTechID and a.JobLevel=c.JobLevel ");
            sb.Append(" left join TestStudentSign d on a.PermiNo=d.PermiNo and a.AENO=d.AENO");
            sb.Append(" where a.TestYear=YEAR(GETDATE()) and a.TestPlaceID=@TestPlaceID and a.TestLotID=@TestLotID and (a.AENO=@AENO or a.PID=@AENO)");
            if (AENO.Length > 6)
            {
                AENO[0] = AENO[0].Replace("#", "") + AENO[3] + AENO[4] + AENO[5] + AENO[6];
                //大於6為掃描准考證碼 反之視為身分證
            }
            return await QueryAsync<SingStuerViewModel>(sb.ToString(), new { TestPlaceID = ToSqlChar(TestPlaceID, 3), TestLotID = ToSqlChar(TestLotID, 2), AENO= ToSqlVarChar(AENO[0]) });
        }

        //寫入報到
        public async Task<int> EditSingStuer(string PermiNo, int SignType, string SignErrTypeMsg, string SignErrNote, string TestPlaceID)
        {
            if (SignType == 0) { SignErrTypeMsg = ""; SignErrNote = ""; }  //若為資料正確 則清除錯誤欄位
            StringBuilder sb = new StringBuilder();
            sb.Append("if exists(select AENO from TestStudentSign Where PermiNo=@PermiNo) ");
            sb.Append(" update TestStudentSign set SignType=@SignType,LastSignTime=getdate(),SignCountUp=SignCountUp+1,SignErrTypeMsg=@SignErrTypeMsg,SignErrNote=@SignErrNote where PermiNo=@PermiNo; else");
            sb.Append(" insert into TestStudentSign values ((select AENO from TestStudentDetail Where PermiNo=@PermiNo),@PermiNo,getdate(),@SignType,getdate(),1,@SignErrTypeMsg,@SignErrNote,@TestPlaceID);");
            return await ExecuteAsync(sb.ToString(), new { PermiNo = ToSqlChar(PermiNo, 15), SignType = SignType, SignErrTypeMsg = ToSqlNVarChar(SignErrTypeMsg), SignErrNote = ToSqlNVarChar(SignErrNote), TestPlaceID = ToSqlChar(TestPlaceID, 3) });
        }

        //查詢今日場次明細
        public async Task<IEnumerable<DetListDayViewModel>> SelSingToDaySet(string TestPlaceID, string TestLotID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select aa.*,isnull(bb.SingCut,0) as SingCut,aa.PopCut-isnull(bb.SingCut,0) as NotSingCut from");
            sb.Append(" (select TestYear,TestPlaceID,TestLotID,SetTestID,count(PermiNo) as PopCut from TestStudentDetail where TestPlaceID=@TestPlaceID and TestYear=Year(GETDATE()) and TestLotID=@TestLotID group by TestYear,TestPlaceID,TestLotID,SetTestID) aa");
            sb.Append(" left join ");
            sb.Append(" (select a.TestYear,a.TestPlaceID,a.TestLotID,a.SetTestID,count(b.PermiNo) as SingCut from TestStudentDetail a left join TestStudentSign b on a.AENO=b.AENO and a.PermiNo=b.PermiNo where a.TestPlaceID=@TestPlaceID and a.TestYear=Year(GETDATE()) and a.TestLotID=@TestLotID and b.PermiNo is not null group by a.TestYear,a.TestPlaceID,a.TestLotID,a.SetTestID) bb");
            sb.Append(" on aa.TestYear=bb.TestYear and aa.TestPlaceID=bb.TestPlaceID and aa.TestLotID=bb.TestLotID and aa.SetTestID=bb.SetTestID;");
            return await QueryAsync<DetListDayViewModel>(sb.ToString(), new { TestPlaceID = ToSqlChar(TestPlaceID, 3), TestLotID = ToSqlChar(TestLotID,2) });
        }

        //查詢本場次未報到
        public async Task<IEnumerable<StudentViewModel>> SelSingData(string TestPlaceID, string TestLotID, string SetTestID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select distinct b.TestRoomID,a.TestRoomName,b.SeatNo,b.PermiNo,b.CName,c.Mobile,c.DayTel,d.FirstSignTime,d.SignErrTypeMsg from TestRoom a join TestStudentDetail b on a.TestPlaceID=b.TestPlaceID and a.TestRoomID=b.TestRoomID");
            sb.Append(" join Student c on b.PID=c.PID and b.CName=c.CName");
            sb.Append(" left join TestStudentSign d on b.AENO=d.AENO and b.PermiNo=d.PermiNo ");
            sb.Append(" where b.TestPlaceID=@TestPlaceID and b.TestLotID=@TestLotID and b.SetTestID=@SetTestID and b.TestYear=Year(GETDATE()) and d.FirstSignTime is null");
            sb.Append(" order by TestRoomID,SeatNo");
            return await QueryAsync<StudentViewModel>(sb.ToString(), new { TestPlaceID = ToSqlChar(TestPlaceID, 3), TestLotID = ToSqlChar(TestLotID, 2), SetTestID = ToSqlChar(SetTestID, 2) });
        }
    }
}
