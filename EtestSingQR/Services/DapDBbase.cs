using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace EtestSingQR.Services
{
    public class DapDBbase : IDisposable
    {
        private readonly IDbConnection _dapdb;
        private readonly IConfiguration _config;

        /// <summary>
        /// 類別建構元
        /// </summary>
        public DapDBbase(IConfiguration c)
        {
            _config = c;
            _dapdb = new SqlConnection(c.GetValue<string>("ConnectionStrings:TrainsConnection"));   //DI注入IConfiguration appsettings.json連線字串
        }
        ~DapDBbase()
        {
            if (_dapdb != null) _dapdb.Dispose();
        }
        /// <summary>
        /// 共用查詢方法
        /// </summary>
        /// <typeparam name="T">類別模組</typeparam>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>回傳類別模組</returns>
        public IEnumerable<T> Query<T>(string sql, object param)
        {
            return _dapdb.Query<T>(sql, param);
        }

        /// <summary>
        /// 共用查詢方法(不使用參數)
        /// </summary>
        /// <typeparam name="T">類別模組</typeparam>
        /// <param name="sql">SQL子句</param>
        /// <returns>回傳類別模組</returns>
        public IEnumerable<T> Query<T>(string sql)
        {
            return _dapdb.Query<T>(sql);
        }

        /// <summary>
        /// 共用非同步查詢方法
        /// </summary>
        /// <typeparam name="T">類別模組</typeparam>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數 null=不使用參數</param>
        /// <returns>回傳類別模組</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param)
        {
            return await _dapdb.QueryAsync<T>(sql, param);
        }

        /// <summary>
        /// 共用非同步查詢方法(不使用參數)
        /// </summary>
        /// <typeparam name="T">類別模組</typeparam>
        /// <param name="sql">SQL子句</param>
        /// <returns>回傳類別模組</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            return await _dapdb.QueryAsync<T>(sql);
        }

        /// <summary>
        /// 共用查詢方法 (查詢是否有資料)
        /// </summary>
        /// <typeparam name="T">類別模組</typeparam>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>回傳單一類別模組</returns>
        public T QueryFirstOrDefault<T>(string sql, object param)
        {
            return _dapdb.QueryFirstOrDefault<T>(sql, param);
        }


        /// <summary>
        /// 共用新增刪除修改方法 並傳回搜尋結果
        /// </summary>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>傳回首筆查詢結果</returns>
        public string ExecuteScalar(string sql, object param)
        {
            return _dapdb.ExecuteScalar(sql, param).ToString() ?? "";
        }

        /// <summary>
        /// 共用新增刪除修改方法
        /// </summary>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>影響筆數</returns>
        public int Execute(string sql, object param)
        {
            return _dapdb.Execute(sql, param);
        }

        /// <summary>
        /// 共用新增刪除修改方法(不使用參數)
        /// </summary>
        /// <param name="sql">SQL子句</param>
        /// <returns>影響筆數</returns>
        public int Execute(string sql)
        {
            return _dapdb.Execute(sql);
        }

        /// <summary>
        /// 共用非同步新增刪除修改方法
        /// </summary>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>影響筆數</returns>
        public async Task<int> ExecuteAsync(string sql, object param)
        {
            return await _dapdb.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 共用非同步新增刪除修改方法(不使用參數)
        /// </summary>
        /// <param name="sql">SQL子句</param>
        /// <param name="param">參數</param>
        /// <returns>影響筆數</returns>
        public async Task<int> ExecuteAsync(string sql)
        {
            return await _dapdb.ExecuteAsync(sql);
        }

        /// <summary>
        /// 啟用鎖定交易的
        /// </summary>
        /// <returns>有例外傳出錯誤，空字串則無錯誤</returns>
        public int BeginTransaction(string sql, object param)
        {
            int ReInt = 0;
            _dapdb.Open();
            using (var Trandb = _dapdb.BeginTransaction())
            {
                try
                {
                    ReInt = _dapdb.Execute(sql, param);
                    Trandb.Commit();
                }
                catch
                {
                    Trandb.Rollback();
                }
            }
            _dapdb.Close();
            return ReInt;
        }

        /// <summary>
        /// 釋放連接
        /// </summary>
        public void Dispose()
        {
            _dapdb.Dispose();
        }

        #region 自寫DbString擴充函式 string條件參數必須指定好類型 效能才會好
        /// <summary>
        /// 指定為SQL中的VarChar
        /// </summary>
        /// <param name="StrVal">值</param>
        public DbString ToSqlVarChar(string StrVal)
        {
            return new DbString { Value = StrVal, IsFixedLength = false, IsAnsi = true };
        }

        /// <summary>
        /// 指定為SQL中的NVarChar
        /// </summary>
        /// <param name="StrVal">值</param>
        public DbString ToSqlNVarChar(string StrVal)
        {
            return new DbString { Value = StrVal, IsFixedLength = false, IsAnsi = false };
        }

        /// <summary>
        /// 指定為SQL中的Char
        /// </summary>
        /// <param name="StrVal">值</param>
        /// <param name="ValLen">固定的長度</param>
        public DbString ToSqlChar(string StrVal, int ValLen)
        {
            //IsFixedLength 是否固定長度、Length 字元長度(最大4000，預設-1即為4000)、IsAnsi=T為char IsAnsi=F為Nchar
            return new DbString { Value = StrVal, IsFixedLength = true, Length = ValLen, IsAnsi = true };
        }

        /// <summary>
        /// 指定為SQL中的NChar 固定長度字元
        /// </summary>
        /// <param name="StrVal">值</param>
        /// <param name="ValLen">固定的長度</param>
        public DbString ToSqlNChar(string StrVal, int ValLen)
        {
            return new DbString { Value = StrVal, IsFixedLength = true, Length = ValLen, IsAnsi = false };
        }
        #endregion
    }
}
