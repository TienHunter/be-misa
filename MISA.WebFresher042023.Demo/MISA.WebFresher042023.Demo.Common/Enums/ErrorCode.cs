namespace MISA.WebFresher042023.Demo.Common.Enums
{
    /// <summary>
    /// Enum sử dụng mô tả các mã lỗi xảy ra khi gọi API
    /// </summary>
    /// Created by: vdtien
    /// Create by: 13/6/2023
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi gặp Exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi dữ liệu đầu vào không hợp lệ
        /// </summary>
        InValidData = 3,

        /// <summary>
        /// Loi khong tim thay
        /// </summary>
        NotFound=4,

        /// <summary>
        /// Lỗi server
        /// </summary>
        InteralException = 5,

        /// <summary>
        /// Lỗi trùng mã nhưng hỏi người dùng lấy mã tự tăng
        /// </summary>
        DuplicateCodeHasFix = 6,

        /// <summary>
        /// lỗi bad request
        /// </summary>
        BadRequest=7
    }
}
