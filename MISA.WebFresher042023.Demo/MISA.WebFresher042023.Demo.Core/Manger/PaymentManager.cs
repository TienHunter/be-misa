using MISA.WebFresher042023.Demo.Common.DTO.Payment;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manger
{
    /// <summary>
    /// class thực thi validate payment manager
    /// </summary>
    /// created by: vdtien (15/08/2023)
    public class PaymentManager : IPaymentManager
    {
        #region Field
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISupplierRepository _supplierRepository;
        #endregion

        #region Constructor
        public PaymentManager(IPaymentRepository paymentRepository, IAccountRepository accountRepository, ISupplierRepository supplierRepository)
        {
            _paymentRepository = paymentRepository;
            _accountRepository = accountRepository;
            _supplierRepository = supplierRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// kiem tra trung ma phieu chi
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by: vdtien (15/08/2023)
        public async Task IsDupPaymentCode(string code, Guid? id)
        {
            // check dup accountcode
            var dupCode = await _paymentRepository.IsDupCodeAsync(code, id);
            if (dupCode != null)
            {
                var errMsg = String.Format(ResourceVN.UserMsg_DupPaymentCode, dupCode);
                var errsMsgs = new List<string>
                {
                    errMsg
                };
                var errsMore = new Dictionary<string, List<string>>
                {
                    { "PaymentCode", errsMsgs }
                };
                throw new DupCodeException(errsMsgs, errsMore, ErrorCode.DuplicateCodeHasFix);
            }
        }

        /// <summary>
        /// kiểm tra ngày hạch toán phải lớn hơn hoặc bằng ngày phiếu chi bỏ qua giờ phút giây
        /// </summary>
        /// <param name="accountingDate"></param>
        /// <param name="paymentDate"></param>
        /// <returns></returns>
        /// created by: vdtien (16/08/2023)
        public void CheckAccountingDateLessPaymentDate(DateTime? accountingDate, DateTime? paymentDate)
        {

            if (accountingDate < paymentDate)
            {
                var formatDate = paymentDate?.ToString("dd/MM/yyyy") ?? "N/A";
                var userMsg = new List<string>(){
                   String.Format(ResourceVN.UserMsg_DateAccountingNeedMoreDatePayment,formatDate)
                };
                var errMore = new Dictionary<string, List<string>>()
                {
                    { "AccountingDate",userMsg }
                };
                throw new ValidateException(userMsg, errMore);
            }
        }

        /// <summary>
        /// kiểm tra danh sách hạch toán null hoặc rỗng
        /// </summary>
        /// <param name="accountings"></param>
        /// <returns></returns>
        /// created by: vdtien (16/08/2023)
        public void AccountingsNullOrEmpty(List<Accounting>? accountings)
        {
            if (accountings == null || accountings?.Count == 0)
            {
                var userMsg = new List<string>(){
                    ResourceVN.UserMsg_NeedTypingAccountingDetail
                };
                var errMore = new Dictionary<string, List<string>>()
                {
                    { "Accountings",userMsg }
                };
                throw new ValidateException(userMsg, errMore);
            }

        }
        /// <summary>
        /// check trong list id có id empty
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="message"></param>
        /// <exception cref="ValidateException"></exception>
        /// created by: vdtien (15/08/2023)
        public void HasIdNullOrEmptyInListIds(List<Guid> listId, string message)
        {
            // validate listAccountingEditId không là guid rỗng
            var hasIdNullOrEmpty = listId.Any(e => e == Guid.Empty);
            if (hasIdNullOrEmpty) throw new ValidateException(message);
        }

        /// <summary>
        /// validate ghi sô trả về danh sách lỗi 
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (15/08/2023)
        public List<string> ValidateWrittenPayment(bool isSupplierNullOrEmpt, List<string> listAccountCode)
        {

            // validate ghi sổ

            // danh sách lỗi khi ghi sổ không thành công
            var userMsgs = new List<string>();
            if (isSupplierNullOrEmpt == true)
            {
                var length = listAccountCode.Count;
                for (var i = 0; i < length; i++)
                    userMsgs.Add(String.Format(ResourceVN.UserMsg_AccountSupplierAndPaymentLackSupplier, listAccountCode[i]));
            }

            return userMsgs;
        }

        /// <summary>
        /// check phiếu chi tồn tại theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (15/08/2023)
        public async Task<Payment> CheckPaymentExistByIdAsync(Guid id)
        {
            var payment = await _paymentRepository.GetAsync(id) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundPayment);

            return payment;
        } 
        #endregion
    }
}
