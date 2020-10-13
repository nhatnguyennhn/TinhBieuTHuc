using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Transactions;
using System.Xml.Serialization;

namespace BaiTap1_Heuristic
{
    class Heuristic
    {

        public static bool phanPhoi(ref string[] babyCutes, ref string postfix, ref int n)
        {
            string[] bieuThucTruocs = new string[n];
            string[] bieuThucSaus = new string[n];
            string[]    toanTuSaus = new string[n];
            string[] toanTuTruocs = new string[n];
            
            int nbieuThucTruocs = 0;
            int nbieuThucSaus = 0;
            int ntoanTuSaus = 0;
            int ntoanTuTruocs = 0;
            for (int i = 0; i < n; i++)
            {
                if (babyCutes[i] == "*")
                {
                    int soLuongToanTu = 0;
                    int soLuongToanHang = 0;
                    string[] str = new string[1000];
                    int nStr = 0;

                    for (int j = i; j >= 0; j--)
                    {
                        str[nStr] = babyCutes[j];
                        nStr++;
                        if (canhSatChinhTa.ktrToanHang(babyCutes[j])) soLuongToanHang = soLuongToanHang + 1;

                        if (canhSatChinhTa.ktrToanTu(babyCutes[j])) soLuongToanTu = soLuongToanTu + 1;
                        if (soLuongToanTu + 1 == soLuongToanHang)
                        {
                            string[] mangDaoNguoc = new string[nStr];
                            int nMangDaoNguoc = 0;
                            for (int k = nStr - 1; k >= 0; k--)
                            {
                                mangDaoNguoc[nMangDaoNguoc] = str[k];
                                nMangDaoNguoc++;
                            }

                            bool ktr = false;
                            try
                            {
                                string daoNguoc = mangDaoNguoc[0];
                                for (int zzzzi = 1; zzzzi < nMangDaoNguoc; zzzzi++)
                                {
                                    daoNguoc = daoNguoc + " " + mangDaoNguoc[zzzzi];
                                }

                                nhaToanHocTaiBa.postfixHoanluong(daoNguoc);
                                ktr = true;
                            }
                            catch (Exception loi)
                            {

                            }
                            if (ktr)
                            {
                                int viTriToanTu = 0;
                                int viTriBieuThucSau = 0;
                                int slToanTu = 0;
                                int slToanHang = 0;






                                for (int zz = mangDaoNguoc.Length - 2; zz >= 0; zz--)
                                {

                                    if (canhSatChinhTa.ktrToanHang(mangDaoNguoc[zz].ToString())) slToanHang = slToanHang + 1;

                                    if (canhSatChinhTa.ktrToanTu(mangDaoNguoc[zz].ToString())) slToanTu = slToanTu + 1;
                                    if (slToanHang == slToanTu + 1)
                                    {

                                        viTriBieuThucSau = zz;

                                        viTriToanTu = i + 1;
                                        break;
                                    }
                                }
                                string bieuThucSau = mangDaoNguoc[viTriBieuThucSau];
                                for (int zz = viTriBieuThucSau + 1; zz < mangDaoNguoc.Length - 1; zz++)
                                {

                                    bieuThucSau = bieuThucSau + " " + mangDaoNguoc[zz];
                                }

                                string bieuThucTruoc = mangDaoNguoc[0];
                                for (int zz = 1; zz < viTriBieuThucSau; zz++)
                                {

                                    bieuThucTruoc = bieuThucTruoc + " " + mangDaoNguoc[zz];
                                }


                                bieuThucSaus[nbieuThucSaus] = bieuThucSau;
                                nbieuThucSaus = nbieuThucSaus + 1;
                                bieuThucTruocs[nbieuThucTruocs] = bieuThucTruoc;
                                nbieuThucTruocs = nbieuThucTruocs + 1;

                                if (j != 0)
                                {

                                    toanTuSaus[ntoanTuSaus] = babyCutes[viTriToanTu];
                                    ntoanTuSaus = ntoanTuSaus + 1;
                                }
                                else
                                {
                                    toanTuSaus[ntoanTuSaus] = "+";
                                    ntoanTuSaus = ntoanTuSaus + 1;
                                }

                          

                            }

                        }
                    }
                }
            }


            for (int i = 0; i < nbieuThucSaus; i++)
            {
                for (int j = 0; j < nbieuThucTruocs; j++)
                {
                    if (i > j)
                    {
                        if (bieuThucSaus[i] == bieuThucTruocs[j])
                        {


                            string bieuThucTongQuatSau = bieuThucTruocs[i] + " " + bieuThucSaus[i] + " * " + toanTuSaus[i];

                            string bieuThucTongQuatTruoc = bieuThucTruocs[j] + " " + bieuThucSaus[j] + " *";

                            string bieuThucChuyen = "";

                            if (toanTuSaus[j] == "+" || toanTuSaus[j] == "-")
                            {
                                if (toanTuSaus[i] == "-")
                                {
                                    if (toanTuSaus[j] == "+") toanTuSaus[j] = "-";
                                    else if (toanTuSaus[j] == "-") toanTuSaus[j] = "+";

                                }
                                   

                                bieuThucChuyen = bieuThucTruocs[j] + " " + bieuThucSaus[j] + " " + bieuThucTruocs[i] + " " + toanTuSaus[i] + " *";
                                postfix = postfix.Replace(bieuThucTongQuatTruoc, bieuThucChuyen);
                                postfix = postfix.Replace(bieuThucTongQuatSau, "");
                                postfix = postfix.Replace("  ", " ");

                                return true;

                            }
                          







                        }
                    }
                    if (i < j)
                    {
                        if (bieuThucSaus[i] == bieuThucTruocs[j])
                        {


                            string bieuThucTongQuatSau = bieuThucTruocs[j] + " " + bieuThucSaus[j] + " * " + toanTuSaus[j];

                            string bieuThucTongQuatTruoc = bieuThucTruocs[i] + " " + bieuThucSaus[i] + " *";

                            string bieuThucChuyen = "";

                            if (toanTuSaus[j] == "+" || toanTuSaus[j] == "-")
                            {
                                if (toanTuSaus[i] == "-")
                                {
                                    if (toanTuSaus[j] == "+") toanTuSaus[j] = "-";
                                    else if (toanTuSaus[j] == "-") toanTuSaus[j] = "+";

                                }

                                bieuThucChuyen = bieuThucTruocs[j] + " " + bieuThucTruocs[i] + " " + bieuThucSaus[j] + " " + toanTuSaus[j] + " *";

                                postfix = postfix.Replace(bieuThucTongQuatTruoc, bieuThucChuyen);
                                postfix = postfix.Replace(bieuThucTongQuatSau, "");
                                postfix = postfix.Replace("  ", " ");

                                return true;

                            }
                            




                        }
                    }
                }

            }
            for (int i = 0; i < nbieuThucSaus - 1; i++)
            {
                for (int j = i + 1; j < nbieuThucTruocs; j++)
                {
                   
                    if (bieuThucTruocs[i] == bieuThucTruocs[j])
                    {


                        string bieuThucTongQuatSau = bieuThucTruocs[j] + " " + bieuThucSaus[j] + " * " + toanTuSaus[j];

                        string bieuThucTongQuatTruoc = bieuThucTruocs[i] + " " + bieuThucSaus[i] + " *";

                        string bieuThucChuyen = "";

                        if (toanTuSaus[j] == "+" || toanTuSaus[j] == "-")
                        {
                            if (toanTuSaus[i] == "-")
                            {
                                if (toanTuSaus[j] == "+") toanTuSaus[j] = "-";
                                else if (toanTuSaus[j] == "-") toanTuSaus[j] = "+";

                            }
                            bieuThucChuyen = bieuThucTruocs[j] + " " + bieuThucSaus[i] + " " + bieuThucSaus[j] + " " + toanTuSaus[j] + " *";

                            postfix = postfix.Replace(bieuThucTongQuatTruoc, bieuThucChuyen);
                            postfix = postfix.Replace(bieuThucTongQuatSau, "");
                            postfix = postfix.Replace("  ", " ");

                            return true;

                        }
                        

                    }
                    if (bieuThucSaus[i] == bieuThucSaus[j])
                    {


                        string bieuThucTongQuatSau = bieuThucTruocs[j] + " " + bieuThucSaus[j] + " * " + toanTuSaus[j];

                        string bieuThucTongQuatTruoc = bieuThucTruocs[i] + " " + bieuThucSaus[i] + " *";

                        string bieuThucChuyen = "";

                        if (toanTuSaus[j] == "+" || toanTuSaus[j] == "-")
                        {
                            if (toanTuSaus[i] == "-")
                            {
                                if (toanTuSaus[j] == "+") toanTuSaus[j] = "-";
                                else if (toanTuSaus[j] == "-") toanTuSaus[j] = "+";

                            }
                            bieuThucChuyen = bieuThucSaus[j] + " " + bieuThucTruocs[i] + " " + bieuThucTruocs[j] + " " + toanTuSaus[j] + " *";
                            postfix = postfix.Replace(bieuThucTongQuatTruoc, bieuThucChuyen);
                            postfix = postfix.Replace(bieuThucTongQuatSau, "");
                            postfix = postfix.Replace("  ", " ");

                            return true;

                        }
                  
                   }
                }

            }


            return false;

        }


        public static bool so0nhan(ref string[] babyCutes, ref string postfix, ref int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (babyCutes[i] == "0")
                {
                    string pp = babyCutes[i];
                    for (int kz = i + 1; kz < n; kz++)
                    {
                        pp = pp + " " + babyCutes[kz];
                        bool ktrz = false;
                        try
                        {
                            double a = nhaToanHocTaiBa.tinhFostfix(pp);
                            if (babyCutes[kz] == "*" && a == 0)
                            {

                                postfix = postfix.Replace(pp, "0");
                                return true;

                            }
                        }
                        catch (Exception loi)
                        {

                        }

                    }
                }
            }
            return false;
        }
        public static bool nhanCho0(ref string[] babyCutes, ref string postfix, ref int n)
        {


            for (int k = 0; k < n - 1; k++)
            {


                if (babyCutes[k] == "0" && babyCutes[k + 1] == "*")
                {


                    bool[] nhatBaby = new bool[1000];
                    for (int k2 = 0; k2 < n; k2++) nhatBaby[k2] = true;
                    int k1 = k + 1;

                    int soLuongToanTu = 0;
                    int soLuongToanHang = 0;


                    //  nhatBaby[k + 1] = false;
                    for (k1 = k + 1; k1 >= 0; k1--)
                    {

                        if (soLuongToanHang == soLuongToanTu + 1) break;

                        if (canhSatChinhTa.ktrToanHang(babyCutes[k1])) soLuongToanHang = soLuongToanHang + 1;

                        if (canhSatChinhTa.ktrToanTu(babyCutes[k1])) soLuongToanTu = soLuongToanTu + 1;
                        nhatBaby[k1] = false;

                    }



                    nhatBaby[k] = true;
                    int n1 = 0;
                    string[] chuyen = new string[1000];
                    for (k1 = 0; k1 < n; k1++)
                    {
                        if (nhatBaby[k1])
                        {
                            chuyen[n1] = babyCutes[k1];

                            n1++;
                        }

                    }

                    for (k1 = 0; k1 < n1; k1++)
                    {

                        babyCutes[k1] = chuyen[k1];


                    }

                    n = n1;

                    postfix = "";

                    for (int k8 = 0; k8 < n; k8++)
                    {

                        postfix = postfix + " " + babyCutes[k8];
                    }
                    so0nhan(ref babyCutes, ref postfix, ref n);
                    return true;

                }

            }

            return false;
        }

        public static bool chuyen(ref string[] babyCutes, int viTrisohangsau, int viTrisohangtruoc, int i, ref string postfix, ref int n)
        {

            int j = i + 1;

            if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[viTrisohangsau]), babyCutes[i], double.Parse(babyCutes[viTrisohangtruoc])) != 0)
            {
                while (j < babyCutes.Length && canhSatChinhTa.ktrToanHang(babyCutes[j]) && canhSatChinhTa.ktrToanTu(babyCutes[j + 1]) && canhSatChinhTa.doUuTien(babyCutes[j + 1]) == canhSatChinhTa.doUuTien(babyCutes[i]))
                {
                    bool nhatdz = false;
                    string toanTu = babyCutes[j + 1];
                    if (babyCutes[i] == "-")
                    {
                        if (toanTu == "+")
                        {
                            toanTu = "-";
                        }
                        else toanTu = "+";
                        nhatdz = true;
                    }
                    if (babyCutes[i] == "/")
                    {
                        if (toanTu == "*")
                        {
                            toanTu = "/";
                        }
                        else toanTu = "*";
                        nhatdz = true;

                    }

                    if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[viTrisohangsau]), toanTu, double.Parse(babyCutes[j])) == 0)
                    {

                        string[] chuyen = new string[1000];
                        int k = 0;

                        while (k < i)
                        {
                            chuyen[k] = babyCutes[k];
                            k++;

                        }

                        chuyen[k] = babyCutes[j];
                        chuyen[k + 1] = toanTu;
                        k = k + 2;
                        for (int z = i; z < j; z++)
                        {
                            chuyen[k] = babyCutes[z];
                            k = k + 1;
                        }
                        for (int z = j + 2; z < babyCutes.Length; z++)
                        {
                            chuyen[k] = babyCutes[z];
                            k = k + 1;
                        }
                        for (k = 0; k < babyCutes.Length; k++)
                        {


                            babyCutes[k] = chuyen[k];
                        }

                        postfix = "";
                        foreach (string i1 in babyCutes)
                        {
                            postfix = postfix + " " + i1;
                        }
                        if (nhatdz) Console.WriteLine(" Su dung phep ket hop ");
                        else Console.WriteLine(" Su dung phep giao hoan ");

                        Console.Write("=" + nhaToanHocTaiBa.postfixHoanluong(postfix));

                        return true;
                    }
                    else
                    {
                        if (nhatdz)
                        {
                            switch (toanTu)
                            {
                                case "+":
                                    toanTu = "-";
                                    break;
                                case "-":
                                    toanTu = "+";
                                    break;
                                case "*":
                                    toanTu = "/";
                                    break;
                                case "/":
                                    toanTu = "*";
                                    break;

                            }
                        }

                        if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[j]), toanTu, double.Parse(babyCutes[viTrisohangtruoc])) == 0)
                        {


                            string[] chuyen = new string[1000];
                            int k = 0;

                            while (k < i - 1)
                            {
                                chuyen[k] = babyCutes[k];
                                k++;

                            }

                            chuyen[k] = babyCutes[j];
                            chuyen[k + 1] = toanTu;
                            k = k + 2;
                            for (int z = i - 1; z < j; z++)
                            {
                                chuyen[k] = babyCutes[z];
                                k = k + 1;
                            }
                            for (int z = j + 2; z < babyCutes.Length; z++)
                            {
                                chuyen[k] = babyCutes[z];
                                k = k + 1;
                            }
                            for (k = 0; k < babyCutes.Length; k++)
                            {


                                babyCutes[k] = chuyen[k];
                            }

                            postfix = "";
                            foreach (string i1 in babyCutes)
                            {
                                postfix = postfix + " " + i1;
                            }
                            if (nhatdz) Console.WriteLine(" Su dung phep ket hop ");
                            else Console.WriteLine(" Su dung phep giao hoan ");

                            Console.Write("=" + nhaToanHocTaiBa.postfixHoanluong(postfix));

                            return true;
                        }

                    }


                    j = j + 2;
                }

            }
            j = i + 1;
            if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[viTrisohangsau]), babyCutes[i], double.Parse(babyCutes[viTrisohangtruoc])) % 10 != 0)
                while (j < babyCutes.Length && canhSatChinhTa.ktrToanHang(babyCutes[j]) && canhSatChinhTa.ktrToanTu(babyCutes[j + 1]) && canhSatChinhTa.doUuTien(babyCutes[j + 1]) == canhSatChinhTa.doUuTien(babyCutes[i]))
                {
                    bool nhatdz = false;
                    string toanTu = babyCutes[j + 1];
                    if (babyCutes[i] == "-")
                    {
                        if (toanTu == "+")
                        {
                            toanTu = "-";
                        }
                        else toanTu = "+";
                        nhatdz = true;
                    }
                    if (babyCutes[i] == "/")
                    {
                        if (toanTu == "*")
                        {
                            toanTu = "/";
                        }
                        else toanTu = "*";
                        nhatdz = true;

                    }

                    if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[viTrisohangsau]), toanTu, double.Parse(babyCutes[j])) % 10 == 0)
                    {

                        string[] chuyen = new string[1000];
                        int k = 0;

                        while (k < i)
                        {
                            chuyen[k] = babyCutes[k];
                            k++;

                        }

                        chuyen[k] = babyCutes[j];
                        chuyen[k + 1] = toanTu;
                        k = k + 2;
                        for (int z = i; z < j; z++)
                        {
                            chuyen[k] = babyCutes[z];
                            k = k + 1;
                        }
                        for (int z = j + 2; z < babyCutes.Length; z++)
                        {
                            chuyen[k] = babyCutes[z];
                            k = k + 1;
                        }
                        for (k = 0; k < babyCutes.Length; k++)
                        {


                            babyCutes[k] = chuyen[k];
                        }

                        postfix = "";
                        foreach (string i1 in babyCutes)
                        {
                            postfix = postfix + " " + i1;
                        }
                        if (nhatdz) Console.WriteLine(" Su dung phep ket hop ");
                        else Console.WriteLine(" Su dung phep giao hoan ");

                        Console.Write("=" + nhaToanHocTaiBa.postfixHoanluong(postfix));

                        return true;
                    }
                    else
                    {
                        if (nhatdz)
                        {
                            switch (toanTu)
                            {
                                case "+":
                                    toanTu = "-";
                                    break;
                                case "-":
                                    toanTu = "+";
                                    break;
                                case "*":
                                    toanTu = "/";
                                    break;
                                case "/":
                                    toanTu = "*";
                                    break;

                            }
                        }

                        if (nhaToanHocTaiBa.giaTri(double.Parse(babyCutes[j]), toanTu, double.Parse(babyCutes[viTrisohangtruoc])) % 10 == 0)
                        {


                            string[] chuyen = new string[1000];
                            int k = 0;

                            while (k < i - 1)
                            {
                                chuyen[k] = babyCutes[k];
                                k++;

                            }

                            chuyen[k] = babyCutes[j];
                            chuyen[k + 1] = toanTu;
                            k = k + 2;
                            for (int z = i - 1; z < j; z++)
                            {
                                chuyen[k] = babyCutes[z];
                                k = k + 1;
                            }
                            for (int z = j + 2; z < babyCutes.Length; z++)
                            {
                                chuyen[k] = babyCutes[z];
                                k = k + 1;
                            }
                            for (k = 0; k < babyCutes.Length; k++)
                            {


                                babyCutes[k] = chuyen[k];
                            }

                            postfix = "";
                            foreach (string i1 in babyCutes)
                            {
                                postfix = postfix + " " + i1;
                            }
                            if (nhatdz) Console.WriteLine(" Su dung phep ket hop ");
                            else Console.WriteLine(" Su dung phep giao hoan ");

                            Console.Write("=" + nhaToanHocTaiBa.postfixHoanluong(postfix));

                            return true;
                        }

                    }


                    j = j + 2;
                }

            return false;
        }
    }
}
