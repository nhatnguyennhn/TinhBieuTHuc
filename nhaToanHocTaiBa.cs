using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiTap1_Heuristic
{
    class nhaToanHocTaiBa
    {
        /*Trong khi tên trộm Postfix đang trộm các toán tử và toán hạng của biểu thức để biểu diễn thành công thức của hấn ta thì 1 nhà toán học
         đã đứng lên để tính toán giá trị biểu thức biểu thức của tên trộm đó, nhằm đưa ra kết quả chính xác.Nhà toán học đó trình bày như sau: 
        Lặp các babyCute(nếu không hiểu babyCute là gì thì hãy đọc sự tích của tên trộm postfix ở tenTromPostfix.cs ) của Postfix từ
        trái qua phải.
        Nếu  là toán hạng thì đem vào stack
        Nếu là toán tử thì đem 2 toán hạng trong stack ra và  tính giá trị của chúng dựa vào toán tử này sau đó ném lại vô stack
        Giá trị còn lại cuối cùng của stack chính là kết quả của biểu thức Postfix ( rất dễ hiểu đúng không? đúng là nhà toán học tài ba ^^ giải thích cái là xong vấn đề)
        */
     
        public static double tinhFostfix(string postfix,bool ktr)
        {
          

            Stack<double> stack = new Stack<double>();
           
            postfix = postfix.Trim();
    
            IEnumerable<string> babyCutes = postfix.Split(' '); // như đã nói ở trên bạn hãy đọc sự tích của tên trộm postfix để hiểu 
          if(ktr)   Console.Write( "Bai toan : "+postfixHoanluong(postfix));
            foreach (string babyCute in babyCutes)
            {
              
                if (canhSatChinhTa.ktrToanHang(babyCute))
                {
                    
                    stack.Push(double.Parse(babyCute)); // dù là nhà toán học tài ba nhưng ông ấy vẫn cần sự giúp đỡ của cảnh sát chính tả để biết đâu là toán hạng(nãy khen xong giờ thấy ổng hơi ngu rồi)
                
                }
                else
                {
                    double soHangTruoc = stack.Pop();
                    double soHangSau = stack.Pop();
                   
                    string[] mang = postfix.Split(soHangSau.ToString() + " " + soHangTruoc.ToString() + " " + babyCute);

                    double t = soHangSau;
                    switch (babyCute)
                    {
                        case "+": soHangSau += soHangTruoc; break;
                        case "-": soHangSau -= soHangTruoc; break;
                        case "*": soHangSau *= soHangTruoc; break;
                        case "/": soHangSau /= soHangTruoc; break;
                    }
                 if(ktr)   Console.WriteLine("   Lay : " + t + babyCute + soHangTruoc + "="+ soHangSau);
                    bool nhatDz = true;
                    postfix = mang[0];
                    foreach (string i in mang)
                    {
                        if (nhatDz) postfix = postfix + soHangSau.ToString();
                        else postfix = postfix + i;
                        nhatDz = false;

                    }

            if(ktr)     Console.Write("="+postfixHoanluong(postfix));
    
                    stack.Push(soHangSau);

                }
         
           
          
            }

            return stack.Pop();
         
        }
        /* Sau khi cảnh sát bắt được tên trộm postfix,để sửa những sai lầm của tên trộm kia, nhà toán học tài ba đã được mời 
         để sửa chữa lại biểu thức của tên trộm postfix, nhà toán học tài ba đã đưa ra công thức để chuyển từ postfix sang infix
        Công thức đó như sau :
        Duyệt từ đầu đến cuối biểu thức, nếu nó là toán hạng thì push nó vào stack
        Nếu nó là toán tử, lôi đầu 2 giá trị đầu của stack, đặt toán tử và các giá trị là đối số tạo thành 1 chuỗi. Đẩy chuỗi vào lại stack
        Nếu còn 1 giá trị thì nó chính là giá trị biểu thức infix
         */
        public static string postfixHoanluong (string postfix)
        {

          
            Stack<string> stack = new Stack<string>();
            postfix = postfix.Trim();
            
            IEnumerable<string> babyCutes = postfix.Split(' ');
    
            foreach (string babyCute in babyCutes)
            {
                if (canhSatChinhTa.ktrToanHang(babyCute)) stack.Push(babyCute); 
                else
                {
             
                    string op1 = stack.Peek().ToString();
                    string op2;
                    stack.Pop();
                    op2 = stack.Peek().ToString();
                    stack.Pop();
                    
                  
                   
                
                    string String1 = op2.ToString() + babyCute + op1.ToString();
                    string String2 = "("+op2.ToString()+")" + babyCute + op1.ToString();
                    string String3 = op2.ToString()+ babyCute + "(" + op1.ToString() + ")";
                    string String4 = "(" + op2.ToString() + ")" + babyCute + "(" + op1.ToString() + ")";
                    double giaTriDung = tinhFostfix(tenTromPostfix.anTrom(String4), false);

                    
                    if (tinhFostfix(tenTromPostfix.anTrom(String1), false) == giaTriDung) stack.Push(String1);
                    else if (tinhFostfix(tenTromPostfix.anTrom(String2), false) == giaTriDung) stack.Push(String2);
                    else if (tinhFostfix(tenTromPostfix.anTrom(String3), false) == giaTriDung) stack.Push(String3);
                    else stack.Push(String4);
                }
        


            }
            return stack.Pop();

        }
    
       
        
        
    }
}
