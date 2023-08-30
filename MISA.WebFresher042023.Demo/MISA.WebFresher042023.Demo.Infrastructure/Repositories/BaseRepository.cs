

using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Demo.Common.Attributes;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {

        #region Field
        protected readonly IUnitOfWork _uow;
        private static string tableName = typeof(TEntity).Name;
        #endregion

        #region Constructor
        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lay danh sach tat ca ban ghi
        /// </summary>
        /// <returns>danh sach tat ca ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public virtual async Task<List<TEntity>> GetAllAsync()
        {

            string procName = String.Format(Procedures.GET_ALL, tableName);

            var results = await _uow.Connection.QueryAsync<TEntity>(procName, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return (List<TEntity>)results;

        }

        /// <summary>
        /// lay danh sach ban ghi theo paging va filter
        /// </summary>
        /// <returns>danh sach ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public virtual async Task<ListRecords<TEntity>> GetListAsync(int limit, int offset, string keySearch)
        {

            string procName = String.Format(Procedures.GET_LIST, tableName);
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add("@v_Limit", limit);
            parameters.Add("@v_Offset", offset);
            parameters.Add("@v_KeySearch", keySearch);
            // Khởi tạo kết nối tới DB MariaDB

            var results = await _uow.Connection.QueryMultipleAsync(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            var totalRecords = results.Read<int>().FirstOrDefault();
            var records = results.Read<TEntity>().ToList();
            return new ListRecords<TEntity>()
            {
                TotalRecord = totalRecords,

                Data = records
            };


        }

        /// <summary>
        /// lay 1ban ghi theo id
        /// </summary>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public virtual async Task<TEntity?> GetAsync(Guid? recordId)
        {
            string procName = String.Format(Procedures.GET_BY_ID, tableName);
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add($"@v_{tableName}Id", recordId);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }

        /// <summary>
        /// them moi 1 ban ghi
        /// </summary>
        /// <returns>ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public async Task<TEntity?> InsertAsync(TEntity record)
        {
            // chuan bi cau lenh
            string procName = String.Format(Procedures.INSERT, tableName);

            // chuan bi tham so 
            var parameters = new DynamicParameters();
            var recordId = (Guid)record.GetType().GetProperty($"{tableName}Id").GetValue(record);

            // map property của employee với tham số truyền vào database
            foreach (var prop in record.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)))
                {
                    // Bỏ qua xử lý cho thuộc tính bị đánh dấu bằng IgnorePropertyAttribute
                    continue;
                }
                parameters.Add("@v_" + prop.Name, prop.GetValue(record, null));
            }
            await _uow.Connection.ExecuteAsync(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            var result = await GetAsync(recordId);
            return result;

        }

        /// <summary>
        /// cap nhat 1 ban ghi
        /// </summary>
        /// <returns> ban ghi</returns>
        /// Created by: vdtien (19/6/2023)
        public async Task<TEntity?> UpdateAsync(TEntity record)
        {
            // chuan bi cau lenh
            string procName = String.Format(Procedures.UPDATE, tableName);

            // chuan bi tham so 
            var parameters = new DynamicParameters();
            var recordId = (Guid)record.GetType().GetProperty($"{tableName}Id").GetValue(record);

            // map property của employee với tham số truyền vào database
            foreach (var prop in record.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)))
                {
                    // Bỏ qua xử lý cho thuộc tính bị đánh dấu bằng IgnorePropertyAttribute
                    continue;
                }
                parameters.Add("@v_" + prop.Name, prop.GetValue(record, null));
            }


            await _uow.Connection.ExecuteAsync(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            var result = await GetAsync(recordId);
            return result;

        }

        /// <summary>
        /// xoa 1 ban ghi
        /// </summary>
        /// <returns>so ban ghi anh huong</returns>
        /// Created by: vdtien (19/6/2023)
        public virtual async Task<int> DeleteAsync(Guid recordId)
        {
            //// chuan bi cau lenh
            //var tableName = typeof(TEntity).Name;
            //string procName = String.Format(Procedures.DELETE, tableName);

            //// chuan bi tham so 
            //var parameters = new DynamicParameters();
            //parameters.Add($"@v_{tableName}Id", recordId);

            //var result = await _uow.Connection.ExecuteAsync(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            var sql = $"DELETE FROM {tableName} WHERE {tableName}Id = @id ;";

            var result = await _uow.Connection.ExecuteAsync(sql, new { id = recordId }, transaction: _uow.Transaction);

            return result;

        }

        /// <summary>
        /// xoa nhieu ban ghi
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>so ban ghi bi anh huong</returns>
        /// Created by: vdtien (20/6/2023)
        public virtual async Task<int> DeleteMultiAsync(List<Guid> listId)
        {
            //string listIdStr = string.Join(",", listId.Select(id => $"'{id.ToString()}'"));

            // chuan bi cau lenh
            //var tableName = typeof(TEntity).Name;
            //string procName = String.Format(Procedures.DELETE_MULTI, tableName);

            // chuan bi tham so 
            //var parameters = new DynamicParameters();
            //parameters.Add("@v_ListId", listIdStr, DbType.String); // Sử dụng DbType.String cho kiểu text trong proc

            var sql = $"DELETE FROM {tableName} WHERE {tableName}Id IN @Ids";

            var result = await _uow.Connection.ExecuteAsync(sql, new { Ids = listId }, transaction: _uow.Transaction);

            return result;

        }

        /// <summary>
        /// Lay danh sach ban ghi theo filter
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TEntity>> GetListByFilterAsync(string keySearch)
        {
            // chuan bi cau lenh
            string procName = String.Format(Procedures.GET_LIST_BY_FILTER, tableName);
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add("@v_KeySearch", keySearch);

            // thuc hien ket noi database

            var results = await _uow.Connection.QueryAsync<TEntity>(procName, parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return (List<TEntity>)results;

        }

        /// <summary>
        /// check ma trung
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> IsDupCodeAsync(string code, Guid? id)
        {
            // chuan bi cau lenh

            var parameters = new DynamicParameters();
            parameters.Add($"@v_{tableName}Code", code);
            parameters.Add($"@v_{tableName}Id", id);

            // Khởi tạo kết nối tới DB MariaDB
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<string>($"Proc_{tableName}_CheckDupCode", parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return result;

        }

        /// <summary>
        /// lay danh sach ban ghi theo danh sach id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<List<TEntity>> GetListByListIdAsync(List<Guid> listId)
        {
            var tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối tới DB MariaDB

            var query = $"SELECT * FROM {tableName} WHERE {tableName}Id IN @Ids";
            var result = await _uow.Connection.QueryAsync<TEntity>(query, new { Ids = listId }, transaction: _uow.Transaction);

            return (List<TEntity>)result;

        }

        /// <summary>
        /// Generate ma entity moi
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNewCode()
        {
            var tableName = typeof(TEntity).Name;

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<string>($"Proc_{tableName}_GetNewCode", transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return result;

        }

        /// <summary>
        /// insert nhieu ban ghi
        /// </summary>
        /// <param name="listEmtity"></param>
        /// <returns></returns>
        public virtual async Task BulkInsertAsync(List<TEntity> listEmtity)
        {
            if (listEmtity.Count == 0) return;

            var dynamicParams = new DynamicParameters();

            var sql = "";

            var index = 0;
            // tạo lệnh sql và add dynamic param
            foreach (var entity in listEmtity)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null && !Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)));
                sql += $"INSERT INTO {tableName} (";
                sql += string.Join(", ", notNullProps.Select(prop => prop.Name));
                sql += ") Values (";
                sql += string.Join(", ", notNullProps.Select(prop => $"@{prop.Name}_{index}"));
                sql += ");";

                foreach (var prop in notNullProps)
                {
                    dynamicParams.Add($"@{prop.Name}_{index}", prop.GetValue(entity));
                }
                index++;
            }
            await _uow.Connection.ExecuteAsync(sql, dynamicParams, transaction: _uow.Transaction);

        }

        /// <summary>
        /// update nhiều bản ghi
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        public virtual async Task BulkUpdateAsync(List<TEntity> listEntity)
        {
            // lấy ra trường update
            var propsNotIgnoreAndNotKey = typeof(TEntity).GetProperties().Where(prop => (!Attribute.IsDefined(prop, typeof(IgnorePropertyAttribute)) && !Attribute.IsDefined(prop, typeof(KeyProperty)) && !Attribute.IsDefined(prop, typeof(NotUpdateProperty))));

            // lấy ra trường key
            var propsKey = typeof(TEntity).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyProperty)));

            var sql = "";

            var parameters = new DynamicParameters();
            for (int index = 0; index < listEntity.Count; index++)
            {
                sql += $"UPDATE {tableName} SET ";

                sql += string.Join(", ", propsNotIgnoreAndNotKey.Select(prop => $"{prop.Name} = @{prop.Name}_{index}"));
                sql += " WHERE ";

                sql += string.Join(" AND ", propsKey.Select(prop => $"{prop.Name} = @{prop.Name}_{index}"));
                sql += "; ";

                // add động parameters
                foreach (var prop in propsNotIgnoreAndNotKey)
                {
                    parameters.Add($"@{prop.Name}_{index}", prop.GetValue(listEntity[index]));
                }
                foreach (var prop in propsKey)
                {
                    parameters.Add($"@{prop.Name}_{index}", prop.GetValue(listEntity[index]));
                }

            }

            //exceute query in db
            await _uow.Connection.ExecuteAsync(sql, parameters, transaction: _uow.Transaction);

        }
        #endregion
    }
}
