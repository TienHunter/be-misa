using Dapper;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// class thực thi Supplier_GroupSupplierRepository
    /// </summary>
    /// created by: vdtien (27/7/2023)
    public class Supplier_GroupSupplierRepository : BaseRepository<Supplier_GroupSupplier>, ISupplier_GroupSupplierRepository
    {
        #region Constructor
        public Supplier_GroupSupplierRepository(IUnitOfWork uow) : base(uow)
        {
        }
        #endregion


        #region Methods
        /// <summary>
        /// xóa quan hê theo supplierId
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (27/7/2023)
        public async Task<int> DeleteBySupplierIdAsync(Guid supplierId)
        {
            var sql = "DELETE FROM supplier_groupsupplier WHERE SupplierId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", supplierId);
            return await _uow.Connection.ExecuteAsync(sql, parameters, transaction: _uow.Transaction);
        }

        /// <summary>
        /// xóa quan hệ theo danh sách id truyền vào
        /// </summary>
        /// <param name="listSupplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (27/7/2023)
        public async Task DeleteByListSupplierIdAsync(List<Guid> listSupplierId)
        {
            var sql = "DELETE FROM supplier_groupsupplier WHERE SupplierId IN @Ids";
            await _uow.Connection.ExecuteAsync(sql, new { Ids = listSupplierId }, transaction: _uow.Transaction);
        }
        public async Task InsertIgnoreAsync(List<Supplier_GroupSupplier> listSupplierGroupSupplier)
        {
            var tableName = typeof(Supplier_GroupSupplier).Name;
            var properties = typeof(Supplier_GroupSupplier).GetProperties();
            var dynamicParams = new DynamicParameters();
            var sql = $"INSERT IGNORE  INTO {tableName} (";
            sql += string.Join(", ", properties.Select(prop => prop.Name));
            sql += ") Values ";

            for (var index = 0; index < listSupplierGroupSupplier.Count; index++)
            {

                sql += "(" + string.Join(", ", properties.Select(prop => $"@{prop.Name}_{index}")) + "),";
                foreach (var prop in properties)
                {
                    dynamicParams.Add($"@{prop.Name}_{index}", prop.GetValue(listSupplierGroupSupplier[index]));
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ";";
            //sql += " ON DUPLICATE KEY UPDATE ";
            //sql += string.Join(", ", properties.Select(prop => $"{prop.Name}= values({prop.Name})")) + ";";

            await _uow.Connection.ExecuteAsync(sql, dynamicParams, transaction: _uow.Transaction);
        }


        /// <summary>
        /// xóa quan hệ theo supplierId nhưng không có trong list groundSupplier
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="groupSupplierId"></param>
        /// <returns></returns>
        /// groupSupplierId
        public async Task DeleteBySupplierIdDifferentGroupSupplierId(Guid supplierId, List<Guid>? groupSupplierId)
        {
            string sql = @"
            DELETE FROM Supplier_GroupSupplier
            WHERE SupplierId = @SupplierIds";
            if (groupSupplierId != null && groupSupplierId.Count > 0)
            {
                sql += " AND GroupSupplierId NOT IN @GroupSupplierIds;";
            }
            else
            {
                sql += ";";
            }
            await _uow.Connection.ExecuteAsync(sql, new
            {
                SupplierIds = supplierId,
                GroupSupplierIds = groupSupplierId
            }, transaction: _uow.Transaction);
        }
        #endregion

    }
}