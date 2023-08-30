namespace MISA.WebFresher042023.Demo
{
    /// <summary>
    /// Lớp cộng trừ nhân chia
    /// </summary>
    /// Created by: vdtien(09/06/2023)
    public class Caculator
    {
        /// <summary>
        /// Hàm cộng 2 số 
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Tổng 2 số</returns>
        /// Created by: vdtien(09/06/2023)
        public long Add(int a, int b)
        {
            return (long)a + b;
        }

        /// <summary>
        /// Hàm trừ 2 số 
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Hiệu 2 số  </returns>
        /// Created by: vdtien(09/06/2023)

        public long Sub(int a, int b)
        {
            return (long)a - b;
        }
        /// <summary>
        /// Hàm nhân 2 số 
        /// </summary>
        /// <param name="a">Số hạng 1</param>
        /// <param name="b">Số hạng 2</param>
        /// <returns>Tích 2 số </returns>
        /// Created by: vdtien(09/06/2023)
        public long Mul(int a, int b)
        {
            return (long)a * b;
        }
        /// <summary>
        /// Hàm chia 2 số
        /// </summary>
        /// <param name="a">Số hạng a</param>
        /// <param name="b">Số hạng b</param>
        /// <returns>Thương 2 số</returns>
        /// Created by: vdtien(09/06/2023)
        public double Div(int a, int b)
        {
            if (b == 0)
            {
                throw new Exception("Không được chia cho 0");
            }
            return (double)a / b;
        }
    }
}