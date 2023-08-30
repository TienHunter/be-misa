using MISA.WebFresher042023.Demo.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher042023.Demo.Common.Entity
{
    /// <summary>
    /// Thuc the base
    /// </summary>
    /// Created by: vdtien (19/6/2023)
    public abstract class BaseEntity
    {
        /// <summary>
        /// nnguoi tao
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [NotUpdateProperty]
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// ngay tao
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [Display(Name = "Ngày tạo")]
        [NotUpdateProperty]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// nguoi sua
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// ngay sua
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }

    }
}
