using Dapper;
using MISA.WebFresher042023.Demo.Common.Commons;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.DTO.Payments;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repositories
{
    /// <summary>
    /// class thực thi PaymentRepository
    /// </summary>
    /// created by: vdtien (13/8/2023)
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// lấy danh sách phiếu chi theo phân trang và lọc
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (13/8/2023)
        public override async Task<ListRecords<Payment>> GetListAsync(int limit, int offset, string keySearch)
        {

            string procName = String.Format(Procedures.GET_LIST, typeof(Payment).Name);
            // chuan bi tham so 
            var parameters = new DynamicParameters();
            parameters.Add("@v_Limit", limit);
            parameters.Add("@v_Offset", offset);
            parameters.Add("@v_KeySearch", keySearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            // Khởi tạo kết nối tới DB MariaDB
            var paymentDictionary = new Dictionary<Guid, Payment>();
            var results = await _uow.Connection.QueryAsync<Payment, Accounting, Payment>(procName, (payment, accounting) =>
            {
                if (!paymentDictionary.TryGetValue(payment.PaymentId, out var currentPayment))
                {
                    currentPayment = payment;
                    currentPayment.Accountings = new List<Accounting>();
                    paymentDictionary.Add(payment.PaymentId, currentPayment);
                }

                if (accounting != null)
                    currentPayment.Accountings.Add(accounting);

                return currentPayment;
            },
                splitOn: "AccountingId", param: parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            var TotalRecord = parameters.Get<int>("@TotalRecord");
            return new ListRecords<Payment>()
            {
                TotalRecord = TotalRecord,
                Data = results.Distinct().ToList()
            };

        }

        /// <summary>
        /// lấy tất cả danh sách phiếu chi
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public override async Task<List<Payment>> GetAllAsync()
        {
            var paymentDictionary = new Dictionary<Guid, Payment>();

            var usersWithPosts = await _uow.Connection.QueryAsync<Payment, Accounting, Payment>(
                "Proc_Payment_GetAll",
                (payment, accounting) =>
                {
                    if (!paymentDictionary.TryGetValue(payment.PaymentId, out var currentPayment))
                    {
                        currentPayment = payment;
                        currentPayment.Accountings = new List<Accounting>();
                        paymentDictionary.Add(payment.PaymentId, currentPayment);
                    }

                    if (accounting != null)
                        currentPayment.Accountings.Add(accounting);

                    return currentPayment;
                },
                splitOn: "AccountingId", transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return usersWithPosts.Distinct().ToList();
        }

        /// <summary>
        /// lấy 1 phiếu chi theo id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public override async Task<Payment?> GetAsync(Guid? paymentId)
        {
            var paymentDictionary = new Dictionary<Guid, Payment>();
            var parameters = new DynamicParameters();
            parameters.Add("@v_PaymentId", paymentId);
            var usersWithPosts = await _uow.Connection.QueryAsync<Payment, Accounting, Payment>(
                "Proc_Payment_GetById",
                (payment, accounting) =>
                {
                    if (!paymentDictionary.TryGetValue(payment.PaymentId, out var currentPayment))
                    {
                        currentPayment = payment;
                        currentPayment.Accountings = new List<Accounting>();
                        paymentDictionary.Add(payment.PaymentId, currentPayment);
                    }

                    if (accounting != null)
                        currentPayment.Accountings.Add(accounting);

                    return currentPayment;
                },
                splitOn: "AccountingId", param: parameters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);

            return usersWithPosts.FirstOrDefault(); // Trả về khoản thanh toán đầu tiên hoặc null
        }

        //public Task<ListRecordFailure<Payment>> GetListPaymentAsync(int limit, int offset, string keySearch);

        /// <summary>
        /// cập nhật trạng thái của phiếu chi
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public async Task UpdatePaymentStatusAsync(Guid paymentId, PaymentStatus paymentStatus)
        {
            var sql = "UPDATE payment SET PaymentStatus = @paymentStatus WHERE PaymentId = @paymentId";
            var res = await _uow.Connection.ExecuteAsync(sql, new { paymentStatus, paymentId }, transaction: _uow.Transaction);
        }

        /// <summary>
        /// lấy danh sách phiếu chi theo danh sách id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public override async Task<List<Payment>> GetListByListIdAsync(List<Guid> listId)
        {
            string listIdStr = String.Join(" , ", listId.Select(id => $"'{id}'"));
            var paymentDictionary = new Dictionary<Guid, Payment>();
            // Khởi tạo kết nối tới DB MariaDB
            var paramerters = new DynamicParameters();
            paramerters.Add("@v_ListId", listIdStr);
            var result = await _uow.Connection.QueryAsync<Payment, Accounting, Payment>("Proc_Payment_GetAllByListId",
                (payment, accounting) =>
            {
                if (!paymentDictionary.TryGetValue(payment.PaymentId, out var currentPayment))
                {
                    currentPayment = payment;
                    currentPayment.Accountings = new List<Accounting>();
                    paymentDictionary.Add(payment.PaymentId, currentPayment);
                }

                if (accounting != null)
                    currentPayment.Accountings.Add(accounting);

                return currentPayment;
            },
                splitOn: "AccountingId", param: paramerters, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);



            return result.Distinct().ToList();
        }

        /// <summary>
        /// cập nhật trạng thái phiếu chi theo danh sách id
        /// </summary>
        /// <param name="listPaymentId"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public async Task BulkUpdatePaymentStatusAsync(List<Guid> listPaymentId, PaymentStatus paymentStatus)
        {
            var sql = "UPDATE payment SET PaymentStatus = @paymentStatus WHERE PaymentId IN @listPaymentId ; ";

            await _uow.Connection.ExecuteAsync(sql, new { paymentStatus, listPaymentId }, transaction: _uow.Transaction);

        }

        /// <summary>
        /// Lấy danh sách phiếu chi xuất excel theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public async Task<List<PaymentExcelDTO>> GetAllPaymentExcelAsync(string keySearch)
        {
            var result = await _uow.Connection.QueryAsync<PaymentExcelDTO>("Proc_Payment_GetAllExcel", new { v_KeySearch = keySearch }, transaction: _uow.Transaction, commandType: System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }

        /// <summary>
        /// lấy phiếu chi theo id nhà cung cấp
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (14/8/2023)
        public async Task<bool> CheckSupplierHasPaymentBySupplierIdAsync(Guid supplierId)
        {
            var sql = "SELECT PaymentId FROM payment WHERE SupplierId = @supplierId AND PaymentStatus = @paymentStatus ;";

            var res = await _uow.Connection.QueryFirstOrDefaultAsync<Guid>(sql, new { supplierId,paymentStatus = PaymentStatus.Writted });
            if (res == Guid.Empty) return false;
            return true;
        }

        /// <summary>
        /// lấy tất cả supplierId theo list supplierId
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023)
        public async Task<List<Guid>> GetAllSupplierIdBySupplierIdAsync(List<Guid> supplierId)
        {
            var sql = "SELECT SupplierId FROM payment WHERE SupplierId In @supplierId";

            var res = await _uow.Connection.QueryAsync<Guid>(sql, new { supplierId });
            return res.ToList();
        }
    }
}
