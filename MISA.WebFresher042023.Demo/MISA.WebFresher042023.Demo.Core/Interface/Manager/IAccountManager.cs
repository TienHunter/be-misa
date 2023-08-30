using MISA.WebFresher042023.Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface account manager validate nghiep vu
    /// </summary>
    /// created by: vdtien (17/7/2023)
    public interface IAccountManager
    {
        /// <summary>
        /// kiem tra trung ma tai khoan
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        Task IsDupAccountCodeAsync(string code, Guid? id);

        /// <summary>
        /// check số tài khoản con > 3 thì cần tài khoản tổng hơp
        /// </summary>
        /// <param name="code"></param>
        /// created by: vdtien (17/7/2023)
        void CheckNeedAccountParent(string code,Guid? parentId);

        /// <summary>
        /// check số tài khoản con là tiền tố của tài khoản cha
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="accountParentCode"></param>
        /// created by: vdtien (17/7/2023)
        void CheckAccountCodeIsPrefixAccountParentCode(string accountCode, string accountParentCode);

        /// <summary>
        /// check trạng thái sử dụng của tài khoản con và tài khoản cha
        /// </summary>
        /// <param name="accountStatus"></param>
        /// <param name="accountParentStatus"></param>
        /// created by: vdtien (17/7/2023)
        void CheckStatusAccountAndParentAccount(Status accountStatus, Status accountParentStatus);

        /// <summary>
        /// check số tài khoản có liện quan đến hạch toán kinh tế không
        /// </summary>
        /// <param name="oldAccountCode"></param>
        /// <param name="newAccountCode"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        Task ValidateAccountCodeChangeAsync(string oldAccountCode, string newAccountCode,Guid accountId);

        /// <summary>
        /// validate xóa tài khoản là khóa ngoại 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// created by: vdtien (17/7/2023)
        Task ValidateDeleteAccountForeignKeyAsync(Guid accountId);
    }
}
