using MISA.WebFresher042023.Demo.Common.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Interface.Excels
{
    /// <summary>
    /// interface thao tac voi excel
    /// </summary>
    /// Created by: vdtien (27/6/2023)
    public interface IExcelInfra
    {
        /// <summary>
        /// xuat ra file excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// Created by: vdtien (27/6/2023)
        public Task<byte[]> ExportToExcelAsync(DataTable data, string title, List<OptionsExcel>? optionsRow, List<OptionsExcel>? optionsCol, List<OptionsExcel>? optionsCell);
    }
}
