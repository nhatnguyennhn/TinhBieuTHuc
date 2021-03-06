﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiTap1_Heuristic
{
    class tenTromPostfix
    {

        /* tên trộm Postfix là tên trộm rất biến thái, lão ta ăn trộm các toán tử và toán hạng(ở đây mình ghi tắt thành babyCute để chỉ ra rõ sự biến thái của lão) của biểu thức
          và sắp xếp vào 1 ngăn xếp(stack) để lão dễ dàng tìm kiếm , lão ta làm  như sau :
         Lão đọc các babyCute trong biểu thức từ trái qua phải, với mỗi babyCute lão thực hiện như sau :
        - Nếu là toán hạng  thì cho ra output 
        - Nếu là dấu mở ngoặc "(" : cho vào stack
        - Nếu là dấu đóng ngoặc ")": lấy các toán tử  trong stack ra và cho vào output cho đến khi gặp dấu mở ngoặc (lưu ý: dấu mở ngoặc cũng phải được đưa ra khỏi stack)
        - Nếu là toán tử:
         Chừng nào mà đỉnh stack là toán tử và độ ưu tiên toán tử  đó ra khỏi stack và cho ra output. Đưa toán tử hiện tại vào stack
         */

        public static string anTrom(string bieuThuc)
        {
            // để dễ dàng trộm cắp phục vụ sở thích biến thái của lão thì lão đã nhờ 1 chú cảnh sát chính tả về giúp đỡ

            canhSatChinhTa.dinhDangBieuThuc(ref bieuThuc);
            string[] babyCutes = bieuThuc.Split(' ').ToArray();
            Stack<string> stack = new Stack<string>();
            StringBuilder postfix = new StringBuilder();
            for (int i = 0; i < babyCutes.Length; i++)
            {
                string babyCute = babyCutes[i];
                if (canhSatChinhTa.ktrToanTu(babyCute))
                {
                    if ((i == 0) || (i > 0 && (canhSatChinhTa.ktrToanTu((babyCutes[i - 1])) || babyCutes[i - 1] == "(")))
                    {
                        if (babyCute == "-")
                        {
                            postfix.Append(babyCute + babyCutes[i + 1]).Append(" ");
                            i++;
                        }
                      
                    }
                    else
                    {
                        while (stack.Count > 0 && canhSatChinhTa.doUuTien(babyCute) <= canhSatChinhTa.doUuTien(stack.Peek()))
                            postfix.Append(stack.Pop()).Append(" ");
                        stack.Push(babyCute);
                    }
                }

                else if (babyCute == "(")
                    stack.Push(babyCute);
                else if (babyCute == ")")
                {
                    string x = stack.Pop();
                    while (x != "(")
                    {
                        postfix.Append(x).Append(" ");
                        x = stack.Pop();
                    }
                }
                else
                {
                    postfix.Append(babyCute).Append(" ");
                }
            }

            while (stack.Count > 0)
                postfix.Append(stack.Pop()).Append(" ");

            return postfix.ToString();
        }
    }
}
