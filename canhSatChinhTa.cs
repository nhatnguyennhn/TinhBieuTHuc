using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaiTap1_Heuristic
{
    class canhSatChinhTa
    {
        //Trước khi bắt đầu tính toán biểu thức, chúng ta phải có 1 cảnh sát chính tả để phục vụ cho biểu thức
        public static int doUuTien(string toanTu)
        {
            //Cảnh sát chính tả sẽ rà soát các toán tử trong biểu thức để xem thử toán tử nào sẽ làm trước
            //Đề bài yêu cầu có các phép toán +,-,*,/. Tự thấy phép toán * và / sẽ có độ ưu tiên cao hơn phép toán + và -. 
            if (toanTu == "*" || toanTu == "/")
            {
                return 2;
            }
            if (toanTu == "+" || toanTu == "-")
            {
                return 1;
            }

            return 0;
        }
        public static void dinhDangBieuThuc(ref string bieuThuc)
        {

            // Vì khi nhập biểu thức người dùng có thể viết dư khoảng trống,các ký tự không phù hợp và viết sai nên  cảnh sát chính tả sẽ bắt lỗi người dùng

              bieuThuc = bieuThuc.Replace(" ", ""); // Cảnh sát chính tả bắt những đứa khoảng trống đem về đồn để giữ lại sự trong sạch cho biểu thức
            /*      bieuThuc = Regex.Replace(bieuThuc, @"\+|\-|\*|\/|\)|\(", delegate (Match match)   // Cảnh sát chính tả trấn áp các toán tử và đặt 2 đầu toán tử 1 khoảng trống
                 {
                     return " " + match.Value + " ";
                 });*/
            bieuThuc = Regex.Replace(bieuThuc, @"(\+|\-|\*|\/|\%){3,}", match => match.Value[0].ToString());
            bieuThuc = Regex.Replace(bieuThuc, @"(\+|\-|\*|\/|\%)(\+|\*|\/|\%)", match =>
         match.Value[0].ToString()
     );
            bieuThuc = Regex.Replace(bieuThuc, @"\+|\-|\*|\/|\%|\)|\(", match =>
                String.Format(" {0} ", match.Value)
            );
            
            // 1 số đối tượng toán tử gần nhau vd như 1+-2 thì khi trấn áp cảnh sát sẽ bắt thành 1 +  - 2, việc này sẽ tạo thêm 2 khoảng trống giữa 2 toán tử kề nhau
            //làm các toán tử rời xa nhau 2 khoảng trống làm nó buồn, vì thế cảnh sát chính tả sẽ để cho nó gần nhau lại để trao hơi ấm tình yêu
            bieuThuc = bieuThuc.Replace("  ", " ");
            // có nhiều đối tượng khoảng trống rất tinh vi, đứng ở 2 đầu biên giới bieuThuc,nhưng làm sao mà thoát khỏi bàn tay của cảnh sát chính tả
            // vì thế chúng ta phải tạo đội quân Trim để vây bắt các đối tượng này
            bieuThuc = bieuThuc.Trim();


        }
        //Trong đợi vây bắt trên chúng ta đã thành công khi bắt được các đối tượng khoảng trống, nhưng có các đối tượng còn gian dối hơn
        // có thể là chữ cái, các ký tự bậy bạ không liên quan làm ảnh hưởng tới biểu thức, vì thế phải rà soát và kiểm tra các đối tượng này
        public static bool ktrToanTu(string toanTu)
        {
            return Regex.Match(toanTu, @"\+|\-|\*|\/").Success;
        }
        public static bool ktrToanHang(string toanTu)
        {
            try
            {
                double i = double.Parse(toanTu);

                return true;
            }
            catch (Exception loi)
            {
                return false;
            }
        }
      
       

    }
}
