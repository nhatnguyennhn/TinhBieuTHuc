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
          
            string bieuThuc = "2*3+3*4";  Console.Write("bai toan : "+bieuThuc);
            bieuThuc = tenTromPostfix.anTrom(bieuThuc);
            string ketQua = nhaToanHocTaiBa.tinhNhanh(bieuThuc);








        }
    }
}
