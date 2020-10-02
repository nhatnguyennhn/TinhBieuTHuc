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
                string bieuThuc = "(3*4+5*((2-1)*5)/9)";

                bieuThuc = tenTromPostfix.anTrom(bieuThuc);

                float ketQua = nhaToanHocTaiBa.tinhFostfix(bieuThuc, true);
            }
            catch (Exception loi)
            {
                Console.WriteLine("Bieu thuc sai hoac co loi");
            }







        }
    }
}
