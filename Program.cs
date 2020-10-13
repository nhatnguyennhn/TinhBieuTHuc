using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
namespace BaiTap1_Heuristic
{
    class Program
    {



        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Nhap bai toan : "); 
                string bieuThuc = Console.ReadLine();  // Lấy biểu thức người dùng nhập vào 
                Console.Write("Bai toan : " + bieuThuc);
              

                string ketQua = nhaToanHocTaiBa.tinhNhanh(tenTromPostfix.anTrom(bieuThuc)); 
                Console.WriteLine();
                Console.WriteLine("Dap an bai toan : " + ketQua);
            }
            catch( Exception loi)
            {
                Console.WriteLine("Error");
            }

        }
    }
}
