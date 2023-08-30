using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.DTO.Supplier;
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
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Core.Manger
{
    /// <summary>
    /// validate supplier manager
    /// </summary>
    /// created by: vdtien (25/7/2023)
    public class SupplierManager : ISupplierManager
    {
        #region Field
        private readonly ISupplierRepository _supplierRepository;
        private readonly IGroupSupplierRepository _groupSupplierRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPaymentRepository _paymentRepository;
        #endregion

        #region Constructor
        public SupplierManager(ISupplierRepository supplierRepository, IGroupSupplierRepository groupSupplierRepository, IAccountRepository accountRepository, IPaymentRepository paymentRepository)
        {
            _supplierRepository = supplierRepository;
            _groupSupplierRepository = groupSupplierRepository;
            _accountRepository = accountRepository;
            _paymentRepository = paymentRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// check trùng mã nhà cung cấp
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DupCodeException"></exception>
        /// created by: vdtien (25/7/2023)
        public async Task IsDupSupplierCodeAsync(string code, Guid? id)
        {
            // check dup accountcode
            var dupCode = await _supplierRepository.IsDupCodeAsync(code, id);
            if (dupCode != null)
            {
                var errMsg = String.Format(ResourceVN.UserMsg_DupCode, dupCode);
                var errsMsgs = new List<string>
                {
                    errMsg
                };
                var errsMore = new Dictionary<string, List<string>>
                {
                    { "SupplierCode", errsMsgs }
                };
                throw new DupCodeException(errsMsgs, errsMore);
            }

        }

        /// <summary>
        /// check tồn tại nhà cung cấp theo id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (25/7/2023)
        public async Task<Supplier> CheckSupplierExistByIdAsync(Guid supplierId)
        {

            var account = await _supplierRepository.GetAsync(supplierId) ?? throw new NotFoundException(ResourceVN.UserMsg_NotFoundSupplier);

            return account;
        }

        /// <summary>
        /// check tồn tại nhóm nhà cung cấp theo danh sách id nhóm nhà cung cấp
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (25/7/2023)
        public async Task CheckListGroupSupplierExistByListIdAsync(List<Guid>? listId)
        {
            if (listId == null || listId.Count == 0)
                return;
            var listGroupSupplier = await _groupSupplierRepository.GetListByListIdAsync(listId);
            if (listGroupSupplier.Count != listId.Count)
            {
                throw new NotFoundException(ResourceVN.UserMsg_NotFoundGroupSupplier);
            }
        }

        /// <summary>
        /// check tồn tại tài khoản nhà cung cấp theo id
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// created by: vdtien (25/7/2023)
        public async Task CheckAccountSupplierExistByAccountIdAsync(Guid? accountId, string message)
        {
            if (accountId == null) return;

            var account = await _accountRepository.GetAsync(accountId) ?? throw new NotFoundException(message);

            if (!(account.UserObject == UserObject.Supplier || account.UserObject == UserObject.All))
                throw new NotFoundException(message);
        }

        /// <summary>
        /// check nhà cung cấp có phát sinh chứng từ
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// created by: vdtien (25/7/2023)
        public async Task<bool> CheckSupplierHasPaymentAsync(Guid supplierId)
        {
            return await _paymentRepository.CheckSupplierHasPaymentBySupplierIdAsync(supplierId);
        }

        /// <summary>
        /// check trùng số tài khoản ngân hàng
        /// </summary>
        /// <param name="banksAccount"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void CheckDuplicateBankAccountNumber(List<BankAccount>? banksAccount)
        {
            if (banksAccount == null || banksAccount.Count == 0) return;

           var seenAccountNumbers = new HashSet<string>();
           var duplicateAccountNumbers = new List<string>();
            var userMsg = new List<string>();
            foreach ( var bank in banksAccount)
            {
                var accountNumber = bank.BankAccountNumber;
                if (accountNumber != null && accountNumber != "")
                {
                   if (!seenAccountNumbers.Add(accountNumber))
                    {
                        duplicateAccountNumbers.Add(accountNumber);
                    }
                }
            }
            foreach (var item in duplicateAccountNumbers)
            {
                userMsg.Add(String.Format(ResourceVN.UserMsg_DuplicateBankAccountNumber, item));
            }
            if (userMsg.Count > 0) throw new ValidateException(userMsg,null);
        }
        #endregion
    }
}
