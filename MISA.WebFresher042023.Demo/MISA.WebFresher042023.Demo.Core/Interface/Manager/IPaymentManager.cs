using MISA.WebFresher042023.Demo.Common.DTO.Acccounting;
using MISA.WebFresher042023.Demo.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Manager
{
    /// <summary>
    /// interface validate nghiep vu phieu chi
    /// </summary>
    /// created by: vdtien (14/8/2023) 
    public interface IPaymentManager
    {
        /// <summary>
        /// kiem tra trung ma phieu chi
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023) 
        Task IsDupPaymentCode(string code, Guid? id);

        /// <summary>
        /// check phiếu chi tồn tại theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Payment> CheckPaymentExistByIdAsync(Guid id);

        /// <summary>
        /// kiểm tra ngày hạch toán phải lớn hơn hoặc bằng ngày phiếu chi bỏ qua giờ phút giây
        /// </summary>
        /// <param name="accountingDate"></param>
        /// <param name="paymentDate"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023) 
        void CheckAccountingDateLessPaymentDate(DateTime? accountingDate, DateTime? paymentDate);

        /// <summary>
        /// kiểm tra danh sách hạch toán null hoặc rỗng
        /// </summary>
        /// <param name="accountings"></param>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023) 
        void AccountingsNullOrEmpty(List<Accounting>? accountings);

        /// <summary>
        /// validate ghi sô trả về danh sách lỗi 
        /// </summary>
        /// <returns></returns>
        /// created by: vdtien (14/8/2023) 
        List<string> ValidateWrittenPayment( bool isSupplierNullOrEmpt,  List<string> listAccountCode);

        /// <summary>
        /// kiểm tra trong danh sách id có id null or empty không
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="message"></param>
        /// created by: vdtien (14/8/2023) 
        void HasIdNullOrEmptyInListIds(List<Guid> listId, string message);
    }
}
