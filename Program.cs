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
                string bieuThuc = "1+2*3+9";
                bieuThuc = tenTromPostfix.anTrom(bieuThuc);
             double ketQua = nhaToanHocTaiBa.tinhFostfix(bieuThuc, true);
            
            }
            catch (Exception loi)
            {
                Console.WriteLine("Bieu thuc ghi sai vui long nhap lai");
            }








        }
    }
}
