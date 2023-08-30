using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.DTO.Department
{
    public class DepartmentDTO
    {
        [Required(ErrorMessage = "Id phòng ban không được để trống")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Mã phòng ban không được để trống")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string DepartmentName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
