using AutoMapper.Execution;
using MISA.WebFresher042023.Demo.Common.DTO;
using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Core.Interface.Excels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Infrastructure.Excels
{
    public class ExcelInfra : IExcelInfra
    {
        public async Task<byte[]> ExportToExcelAsync(DataTable data, string title, List<OptionsExcel>? optionsRow, List<OptionsExcel>? optionsCol, List<OptionsExcel>? optionsCell)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc LicenseContext.Commercial

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(title);

                // Lấy số lượng cột hiện tại trong DataTable
                int columnCount = data.Columns.Count;

                // Xác định ô cuối cùng trong title header
                string lastCell = GetExcelColumnName(columnCount) + "1";

                // Gộp các ô từ A1 đến ô cuối cùng của title header
                worksheet.Cells["A1:" + lastCell].Merge = true;

                // Thiết lập các thuộc tính cho title header
                var titleRange = worksheet.Cells["A1:" + lastCell];
                titleRange.Value = title;
                titleRange.Style.Font.Size = 16;
                titleRange.Style.Font.Name = "Arial";
                titleRange.Style.Font.Bold = true;
                titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                titleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Ghi dữ liệu vào worksheet từ DataTable
                worksheet.Cells["A3"].LoadFromDataTable(data, true);

                // Tạo style cho header (dòng đầu tiên)
                using (var headerRange = worksheet.Cells[3, 1, 3, data.Columns.Count])
                {
                    headerRange.Style.Font.Size = 10;
                    headerRange.Style.Font.Name = "Arial";
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    headerRange.Style.Border.Top.Color.SetColor(Color.Black);
                    headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    headerRange.Style.Border.Bottom.Color.SetColor(Color.Black);
                    headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    headerRange.Style.Border.Left.Color.SetColor(Color.Black);
                    headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    headerRange.Style.Border.Right.Color.SetColor(Color.Black);
                }

                // tạo style cho hàng
                if (worksheet.Dimension.End.Row >= 4)
                {
                    // Tạo style cho các hàng cột
                    int startRow = 4;
                    for (int rowIndex = startRow; rowIndex <= worksheet.Dimension.End.Row; rowIndex++)
                    {
                        var row = worksheet.Cells[rowIndex, 1, rowIndex, data.Columns.Count];

                        row.Style.Font.Size = 10;
                        row.Style.Font.Name = "Time New Roman";
                        row.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        row.Style.Border.Bottom.Color.SetColor(Color.Black);
                        row.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        row.Style.Border.Left.Color.SetColor(Color.Black);
                        row.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        row.Style.Border.Right.Color.SetColor(Color.Black);
                        row.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        row.Style.Border.Top.Color.SetColor(Color.Black);
                        row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        row.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                        int rowNumber = rowIndex - startRow;
                        if (optionsRow != null)
                        {
                            // kiểm tra xem có syle dòng đó không
                            var optionRow = optionsRow.FirstOrDefault(o => o.Row == rowNumber);
                            if (optionRow != null)
                            {
                                if (optionRow?.FontBold == true)
                                {
                                    row.Style.Font.Bold = true;
                                }
                                if (optionRow?.BackgroundColorHex != null)
                                {
                                    row.Style.Fill.PatternType = ExcelFillStyle.Solid; // Đặt kiểu mẫu là Solid
                                    row.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(optionRow?.BackgroundColorHex ?? "#fff"));
                                }
                            }
                        }
                    }

                    if (optionsCol != null && optionsCol.Count > 0)
                    {
                        for (int colIndex = 1; colIndex <= worksheet.Dimension.End.Column; colIndex++)
                        {
                            var optionCol = optionsCol.FirstOrDefault(o => o?.Col == colIndex);
                            if (optionCol != null)
                            {

                                var col = worksheet.Cells[FromRow: startRow, colIndex, worksheet.Dimension.End.Row, colIndex];
                                if (optionCol?.Horizontal != null)
                                {
                                    switch (optionCol.Horizontal)
                                    {
                                        case StyleAlignment.Center:
                                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            break;

                                        case StyleAlignment.Right:
                                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }

                        }
                    }


                    // AutoFit các cột
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }

                if (optionsCell != null && optionsCell.Count > 0)
                {
                    foreach (var optionCell in optionsCell)
                    {
                        if (optionCell.Row.HasValue && optionCell.Col.HasValue)
                        {
                            var cell = worksheet?.Cells[(optionCell?.Row ?? 0) + 4, optionCell?.Col ?? 0];
                            if (cell != null)
                            {
                                //cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //cell.Style.Fill.PatternColor.SetColor(ColorTranslator.FromHtml(optionCell?.ColorHex));
                                cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(optionCell?.ColorHex));

                            }
                        }
                    }

                }

                // Tạo một MemoryStream để lưu trữ file Excel
                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);

                return stream.ToArray();
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}
