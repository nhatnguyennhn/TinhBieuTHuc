using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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

        public static double tinhFostfix(string postfix)
        {


            Stack<double> stack = new Stack<double>();

            postfix = postfix.Trim();

            IEnumerable<string> babyCutes = postfix.Split(' '); // như đã nói ở trên bạn hãy đọc sự tích của tên trộm postfix để hiểu 

            foreach (string babyCute in babyCutes)
            {

                if (canhSatChinhTa.ktrToanHang(babyCute))
                {

                    stack.Push(double.Parse(babyCute)); // dù là nhà toán học tài ba nhưng ông ấy vẫn cần sự giúp đỡ của cảnh sát chính tả để biết đâu là toán hạng(nãy khen xong giờ thấy ổng hơi ngu rồi)

                }
                else
                {
                    double sohangsau = stack.Pop();
                    double sohangtruoc = stack.Pop();

                    string[] mang = postfix.Split(sohangtruoc.ToString() + " " + sohangsau.ToString() + " " + babyCute);

                    double t = sohangtruoc;
                    switch (babyCute)
                    {
                        case "+": sohangtruoc += sohangsau; break;
                        case "-": sohangtruoc -= sohangsau; break;
                        case "*": sohangtruoc *= sohangsau; break;
                        case "/": sohangtruoc /= sohangsau; break;
                    }

                    bool nhatDz = true;
                    postfix = mang[0];
                    foreach (string i in mang)
                    {
                        if (nhatDz) postfix = postfix + sohangtruoc.ToString();
                        else postfix = postfix + i;
                        nhatDz = false;

                    }



                    stack.Push(sohangtruoc);

                }



            }

            return stack.Pop();


        }
        public static string tinhNhanh(string postfix)
        {

            if (canhSatChinhTa.ktrToanHang(postfix)) return postfix;
            else
            {

                Stack<int> viTri = new Stack<int>();
                postfix = postfix.Trim();
                string[] babyCutes = postfix.Split(' '); // như đã nói ở trên bạn hãy đọc sự tích của tên trộm postfix để hiểu 
                int n = babyCutes.Length;
                string a = postfix;
                if (Heuristic.phanPhoi(ref babyCutes, ref postfix, ref n))
                {
                    if (tinhFostfix(a) == tinhFostfix(postfix))
                    {
                        Console.WriteLine("   Dat nhan tu chung");

                        Console.Write("= " + postfixHoanluong(postfix));
                        return nhaToanHocTaiBa.tinhNhanh(postfix);

                    }
                    else postfix = a;
               
                        


                  
                }
                if (Heuristic.so0nhan(ref babyCutes, ref postfix, ref n))
                {
                    Console.WriteLine("    Su dung tinh chat nhan cho 0");

                    Console.Write("= " + postfixHoanluong(postfix));
                    return nhaToanHocTaiBa.tinhNhanh(postfix);
                }
                if (Heuristic.nhanCho0(ref babyCutes, ref postfix, ref n))
                {
                    Console.WriteLine("    Su dung tinh chat nhan cho 0");

                    Console.Write("= " + postfixHoanluong(postfix));
                    return nhaToanHocTaiBa.tinhNhanh(postfix);
                }/*
                 if (Heuristic.so0nhan(ref babyCutes, ref postfix, ref n))
                  {
                      Console.WriteLine("    Su dung tinh chat nhan cho 0");

                      Console.Write("= " + postfixHoanluong(postfix));
                      return nhaToanHocTaiBa.tinhNhanh(postfix);
                  }*/


                for (int i = 0; i < n; i++)
                {


                    // string  babyCute = babyCutes[i];

                    if (canhSatChinhTa.ktrToanHang(babyCutes[i]))
                    {

                        // dù là nhà toán học tài ba nhưng ông ấy vẫn cần sự giúp đỡ của cảnh sát chính tả để biết đâu là toán hạng(nãy khen xong giờ thấy ổng hơi ngu rồi)
                        viTri.Push(i);
                    }
                    else
                    {

                        int viTrisohangsau = viTri.Pop();

                        int viTrisohangtruoc = viTri.Pop();


                        if (Heuristic.chuyen(ref babyCutes, viTrisohangsau, viTrisohangtruoc, i, ref postfix, ref n))
                        {

                            return nhaToanHocTaiBa.tinhNhanh(postfix);
                        }



                        //
                        postfix = "";
                        for (int i1 = 0; i1 < n; i1++)
                        {
                            postfix = postfix + " " + babyCutes[i1];
                        }


                        //
                        string[] mang = postfix.Split(babyCutes[viTrisohangtruoc] + " " + double.Parse(babyCutes[viTrisohangsau]).ToString() + " " + babyCutes[i]);


                        Console.WriteLine("   Lay : " + babyCutes[viTrisohangtruoc] + babyCutes[i] + double.Parse(babyCutes[viTrisohangsau]) + "=" + giaTri(double.Parse(babyCutes[viTrisohangtruoc]), babyCutes[i], double.Parse(babyCutes[viTrisohangsau])));
                        bool nhatDz = true;
                        postfix = mang[0];
                        foreach (string i1 in mang)
                        {
                            if (nhatDz) postfix = postfix + giaTri(double.Parse(babyCutes[viTrisohangtruoc]), babyCutes[i], double.Parse(babyCutes[viTrisohangsau])).ToString();
                            else postfix = postfix + i1;
                            nhatDz = false;

                        }

                        Console.Write("=" + postfixHoanluong(postfix));
                        return tinhNhanh(postfix);


                    }



                }

                return postfix;





            }
        }

        public static double giaTri(double so1, string dau, double so2)

        {

            switch (dau)
            {
                case "+": so1 += so2; break;
                case "-": so1 -= so2; break;
                case "*": so1 *= so2; break;
                case "/": so1 /= so2; break;
            }
            return so1;
        }
        /* Sau khi cảnh sát bắt được tên trộm postfix,để sửa những sai lầm của tên trộm kia, nhà toán học tài ba đã được mời 
         để sửa chữa lại biểu thức của tên trộm postfix, nhà toán học tài ba đã đưa ra công thức để chuyển từ postfix sang infix
        Công thức đó như sau :
        Duyệt từ đầu đến cuối biểu thức, nếu nó là toán hạng thì push nó vào stack
        Nếu nó là toán tử, lôi đầu 2 giá trị đầu của stack, đặt toán tử và các giá trị là đối số tạo thành 1 chuỗi. Đẩy chuỗi vào lại stack
        Nếu còn 1 giá trị thì nó chính là giá trị biểu thức infix
         */
        public static string postfixHoanluong(string postfix)
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
                    string String2 = "(" + op2.ToString() + ")" + babyCute + op1.ToString();
                    string String3 = op2.ToString() + babyCute + "(" + op1.ToString() + ")";
                    string String4 = "(" + op2.ToString() + ")" + babyCute + "(" + op1.ToString() + ")";
                    double giaTriDung = tinhFostfix(tenTromPostfix.anTrom(String4));
                    if (babyCute == "*" || babyCute == "/")
                    {
                        if (!canhSatChinhTa.ktrToanHang(op1)) op1 = "(" + op1 + ")";
                        if (!canhSatChinhTa.ktrToanHang(op2)) op2 = "(" + op2 + ")";

                        stack.Push(  op2   + babyCute +   op1  );

                    }
                    else
                    {
                        if (tinhFostfix(tenTromPostfix.anTrom(String1)) == giaTriDung) stack.Push(String1);
                        else if (tinhFostfix(tenTromPostfix.anTrom(String2)) == giaTriDung) stack.Push(String2);
                        else if (tinhFostfix(tenTromPostfix.anTrom(String3)) == giaTriDung) stack.Push(String3);
                        else stack.Push(String4);
                    }

                }



            }
            return stack.Pop();

        }




    }
}
