using MISA.WebFresher042023.Demo.Common.Enums;
using MISA.WebFresher042023.Demo.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Common.Commons
{
    public static class Helper
    {
        /// <summary>
        /// format tiền về dạng vi-VN
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatDecimal(decimal? number)
        {
            if (number == null)
            {
                return "0,00";
            }
            else
            {
                //decimal debt = number??0;
                var cultureInfo = new CultureInfo("vi-VN");
                // Kiểm tra xem số có âm hay không
                bool isNegative = number < 0;
                decimal absoluteNumber = Math.Abs(number.Value);

                string formatted = string.Format(cultureInfo, "{0:N2}", absoluteNumber);

                // Thêm ngoặc đơn nếu số là âm
                if (isNegative)
                {
                    formatted = "(" + formatted + ")";
                }

                return formatted;
            }
        }

        public static string ConvertDateTimeToDDMMYYYY(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                return "";
            }
        }
        public static string ConvertReasonType(ReasonType? reasonType)
        {
            if (reasonType.HasValue)
            {
                switch (reasonType.Value)
                {
                    case ReasonType.PaySuppliersNotBilled:
                        return ResourceVN.PaySuppliersNotBilled.ToString();
                        break;
                    case ReasonType.AdvanceForEmployees:
                        return ResourceVN.AdvanceForEmployees.ToString();
                        break;
                    case ReasonType.BuyOutsideWithInvoice:
                        return ResourceVN.BuyOutsideWithInvoice.ToString();
                        break;
                    case ReasonType.PaySalaryForEmployees:
                        return ResourceVN.PaySalaryForEmployees.ToString();
                        break;
                    case ReasonType.TransferMoneyToAnotherBranch:
                        return ResourceVN.TransferMoneyToAnotherBranch.ToString();
                        break;
                    case ReasonType.DepositMoneyInTheBank:
                        return ResourceVN.DepositMoneyInTheBank.ToString();
                        break;
                    case ReasonType.OtherSpending:
                        return ResourceVN.OtherSpending.ToString();
                        break;
                    default:
                        return "";
                        break;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
