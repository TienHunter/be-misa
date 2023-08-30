using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface supplier manager validate nghiep vu
    /// </summary>
    /// creatd by: vdtien (25/7/2023)
    public interface ISupplierManager
    {
        /// <summary>
        /// kiem tra trung ma nha cung cap
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// creatd by: vdtien (25/7/2023)
        Task IsDupSupplierCodeAsync(string code, Guid? id);

        /// <summary>
        /// kiem tra ton tai tai khoan theo id;
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        ///  creatd by: vdtien (25/7/2023)
        Task<Supplier> CheckSupplierExistByIdAsync(Guid supplierId);

        /// <summary>
        /// check ton tai danh sach nhom nha cung cap theo listId
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        ///  creatd by: vdtien (25/7/2023)
        Task CheckListGroupSupplierExistByListIdAsync(List<Guid>? listId);

        /// <summary>
        /// check tai khoan nha cung cap tồn tại theo id tài khoản
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        ///  creatd by: vdtien (25/7/2023)
        Task CheckAccountSupplierExistByAccountIdAsync(Guid? accountId, string message);

        /// <summary>
        /// check nhà cung cáp có hạch toán kinh tế
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        ///  creatd by: vdtien (25/7/2023)
        Task<bool> CheckSupplierHasPaymentAsync(Guid supplierId);

        /// <summary>
        /// check trùng số tài khoản ngân hàng
        /// </summary>
        /// <param name="banksAccount"></param>
        /// created by: vdtien (24/8/2023)
        void CheckDuplicateBankAccountNumber(List<BankAccount>? banksAccount);

    }
}
