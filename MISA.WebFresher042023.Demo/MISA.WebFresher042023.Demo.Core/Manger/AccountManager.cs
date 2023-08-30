using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Common.Resources;
using MISA.WebFresher042023.Demo.Core.Interface.Manager;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manger
{
    /// <summary>
    /// validate tài khoản
    /// </summary>
    /// created by: vdtien (17/7/2023)
    public class AccountManager : IAccountManager
    {
        #region Field
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountingRepository _accountingRepository;
        private readonly ISupplierRepository _supplierRepository;
        #endregion

        #region Constructor
        public AccountManager(IAccountRepository accountRepository, IAccountingRepository accountingRepository, ISupplierRepository supplierRepository)
        {
            _accountRepository = accountRepository;
            _accountingRepository = accountingRepository;
            _supplierRepository = supplierRepository;
        }

        #endregion

        #region Methods
        /// <summary>
        /// check tài khoản con là tiền tố của tài khoản tổng hợp
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="accountParentCode"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (17/7/2023)
        public void CheckAccountCodeIsPrefixAccountParentCode(string accountCode, string accountParentCode)
        {
            // check tiền tố 
            bool isPrefix = accountCode.StartsWith(accountParentCode);
            if (isPrefix == false)
            {
                // mã tài khoản không hợp lệ
                var errsMsgs = new List<string>
                    {
                        ResourceVN.UserMsg_AccountCodeNeedPrefixAccountParentCode
                    };
                var errsMore = new Dictionary<string, List<string>>
                    {
                        { "AccountCode", errsMsgs }
                    };
                throw new DupCodeException(errsMsgs, errsMore);

            }
        }

        /// <summary>
        /// check số tài khoản con > 3 thì cần tài khoản tổng hơp
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parentId"></param>
        /// <exception cref="ValidateException"></exception>
        /// created by: vdtien (17/7/2023)
        public void CheckNeedAccountParent(string code, Guid? parentId)
        {
            if (code.Length > 3 && (parentId == null || parentId == Guid.Empty))
            {
                var errsMsgs = new List<string>
                {
                  ResourceVN.UserMsg_NeedParentAccount
                };
                var errsMore = new Dictionary<string, List<string>>
                {
                    { "AccountCode", errsMsgs }
                };
                throw new ValidateException(errsMsgs, errsMore);
            }
        }

        /// <summary>
        /// check trạng thái sử dụng của tài khoản con và tài khoản cha
        /// nếu cha ngừng sử dụng thì con phải là ngưng sử dụng
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <param name="accountParentStatus"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (17/7/2023)
        public void CheckStatusAccountAndParentAccount(Status accountStatus, Status accountParentStatus)
        {
            if (accountStatus == Status.Using && accountParentStatus == Status.StopUsing)
            {


                var userMsg = new List<string>()
                        {
                           ResourceVN.UserMsg_AccountUsingAndParentNotUsing
                        };
                var errMore = new Dictionary<string, List<string>>() {
                    { "ParentId",userMsg}
                };
                throw new BadRequestException(userMsg, errMore);
            }
        }

        /// <summary>
        /// check trùng số tài khoản
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DupCodeException"></exception>
        /// created by: vdtien (17/7/2023)
        public async Task IsDupAccountCodeAsync(string code, Guid? id)
        {
            // check dup accountcode
            var dupCode = await _accountRepository.IsDupCodeAsync(code, id);
            if (dupCode != null)
            {
                var errMsg = String.Format(ResourceVN.UserMsg_DupAccountCode, dupCode);
                var errsMsgs = new List<string>
                {
                    errMsg
                };
                var errsMore = new Dictionary<string, List<string>>
                {
                    { "AccountCode", errsMsgs }
                };
                throw new DupCodeException(errsMsgs, errsMore);
            }

        }

        /// <summary>
        /// check số tài khoản có liện quan đến hạch toán kinh tế không
        /// </summary>
        /// <param name="oldAccountCode"></param>
        /// <param name="newAccountCode"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="ValidateException"></exception>
        /// created by: vdtien (17/7/2023)
        public async Task ValidateAccountCodeChangeAsync(string oldAccountCode, string newAccountCode, Guid accountId)
        {
            if (oldAccountCode != newAccountCode)
            {
                // không được thay đổi số tài khoản khi nó liên quan đến hạch toán kinh tế
                var accountingId = await _accountingRepository.CheckAccountingByAccountId(accountId);
                if (accountingId != Guid.Empty)
                {
                    var errsMsgs = new List<string>
                    {
                        ResourceVN.UserMsg_UpdateAccountArisingAccounting
                    };

                    var errsMore = new Dictionary<string, List<string>>
                    {
                        { "AccountCode", errsMsgs }
                    };
                    throw new BadRequestException(errsMsgs, errsMore);
                }
            }

        }

        /// <summary>
        /// validate xóa tài khoản có khóa ngoại
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by: vdtien (17/7/2023)
        public async Task ValidateDeleteAccountForeignKeyAsync(Guid accountId)
        {
            var accountingId = await _accountingRepository.CheckAccountingByAccountId(accountId);
            if (accountingId != Guid.Empty)
            {
                var errsMsgs = new List<string>
                    {
                        ResourceVN.UserMsg_DeleteAccountArisingAccounting
                    };

                var errsMore = new Dictionary<string, List<string>>
                    {
                        { "AccountCode", errsMsgs }
                    };
                throw new BadRequestException(errsMsgs, errsMore);
            }

            var supplierId = await _supplierRepository.CheckSupplierByAccountId(accountId);
            if (supplierId != Guid.Empty)
            {
                var errsMsgs = new List<string>
                    {
                        ResourceVN.UserMsg_DeleteAccountSupplier
                    };

                var errsMore = new Dictionary<string, List<string>>
                    {
                        { "AccountCode", errsMsgs }
                    };
                throw new BadRequestException(errsMsgs, errsMore);
            }
        } 
        #endregion
    }
}
