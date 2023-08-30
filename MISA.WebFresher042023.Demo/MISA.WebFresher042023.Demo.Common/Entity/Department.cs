using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Entity
{
    /// <summary>
    /// Thực thể phòng ban
    /// </summary>
    /// Created by: vdtien (18/6/2023)
    public class Department:BaseEntity
    {
        /// <summary>
        /// id phong ban
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [Required(ErrorMessage = "Id phòng ban không được để trống")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// ma phong ban
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [Required(ErrorMessage = "Mã phòng ban không được để trống")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// ten phong ban
        /// </summary>
        /// Created by: vdtien (19/6/2023)
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string DepartmentName { get; set;}

    }
}
