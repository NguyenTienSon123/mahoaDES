// See https://aka.ms/new-console-template for more information
string k = "123457799BBCDFF1";
string m = "0123456789ABCDEF";
//ban ma "85E813540F0AB405"
//hàm chuyển nhị phân sang hex : truyền vào chuỗi 4 bit trả về chuỗi hex tương ứng
string chuyenSangHEX(string s)
{
    switch (s)
    {
        case "0000":
            return "0";
        case "0001":
            return "1";
        case "0010":
            return "2";
        case "0011":
            return "3";
        case "0100":
            return "4";
        case "0101":
            return "5";
        case "0110":
            return "6";
        case "0111":
            return "7";
        case "1000":
            return "8";
        case "1001":
            return "9";
        case "1010":
            return "A";
        case "1011":
            return "B";
        case "1100":
            return "C";
        case "1101":
            return "D";
        case "1110":
            return "E";
        case "1111":
            return "F";
        default: return "0";
    }

}
//hàm chuyển từ hex sang nhị phân : truyền vào số từ 0-15 trả về list 4 bit kiểu int tương ứng
List<int> ChuyenSangNhiPhan(int a)
{
    List<int> s = new List<int>();
    int c;
    while (a > 0)
    {
        c = a % 2;
        a = a / 2;
        s.Add(c);
    }
    List<int> s2 = new List<int>();
    switch (s.Count)
    {
        case 0:
            s2.Add(0);
            s2.Add(0);
            s2.Add(0);
            s2.Add(0); 
            break;
        case 1:
            s2.Add(0);
            s2.Add(0);
            s2.Add(0);
            break;
        case 2:
            s2.Add(0);
            s2.Add(0);
            break;
        case 3:
            s2.Add(0);
            break;
    }
    for (int i = s.Count - 1; i >= 0; i--)
    {
        s2.Add(s[i]);
    }
    return s2;
}
//hàm hoán vị pc1 cho khóa k : truyền vào khóa k dạng string,
//biến đổi sang nhị phân lưu lại các bit bằng một list int, sau đó hoán vị pc1 theo bảng hoán vị pc1.
//hàm trả về một list kiểu int lưu các bit đã hoán vị
List<int> hoanViPC1(string k)
{
    List<int> kpc1 = new List<int>();
    string[] mang = k.Select(c => c.ToString()).ToArray();
    List<string> mangk = mang.ToList();
    List<int> klist = new List<int>();
    for (int i = 0; i < mangk.Count; i++)
    {
        switch(mangk[i])
        {
            case "a":
                mangk[i] = "10";
                break;
            case "A":
                mangk[i] = "10";
                break;

            case "B":
                mangk[i] = "11";
                break;
            case "b":
                mangk[i] = "11";
                break;

            case "c":
                mangk[i] = "12";
                break;
            case "C":
                mangk[i] = "12";
                break;

            case "d":
                mangk[i] = "13";
                break;
            case "D":
                mangk[i] = "13";
                break;

            case "e":
                mangk[i] = "14";
                break;
            case "E":
                mangk[i] = "14";
                break;

            case "f":
                mangk[i] = "15";
                break;
            case "F":
                mangk[i] = "15";
                break;
            default:
                break;
        }
        List<int> tam = new List<int>();
        tam = ChuyenSangNhiPhan(int.Parse(mangk[i]));
        for(int j=0; j<tam.Count; j++)
        {
            klist.Add(tam[j]);
        }
    }
    int[] vtbit = { 57 ,49,41,33,25,17,9,1 ,58,50 ,42 ,34 ,26 ,18,10,2 ,59 ,51 ,43,35,27 ,19 ,11,3 ,60,52,44 ,36,63 ,
                55 ,1,39,31 ,23,15 ,7 ,62 ,54 ,46 ,38, 30,22,14 ,6 ,61,53 ,45,37,29 ,21 ,13 ,5 ,28 ,20,12 ,4 };
    for(int i = 0; i < vtbit.Length; i++)
    {
        kpc1.Add(klist[vtbit[i]-1]);
    }
    return kpc1;
}
//hàm dịch vòng sau khi hoán vị pc1 :
//truyền vào list các bit đã hoán vị pc1 và một số nguyên thể hiện đang ở vòng lặp mã hóa thứ mấy 
//tách khóa làm 2 nửa (2 list nhỏ) nếu đang ở vòng mã hóa thứ 1,2, 9, 16 thì dịch 1 bit, còn lại thì sẽ dịch 2 bit 
//dịch bằng cách thêm 1 hoặc 2 bit đầu list vào cuối list, sau đó xóa 1 hoặc 2 bít đầu list
//hàm trả về một list các bit đã dịch vòng tương ứng với mỗi lần mã hóa
List<int> dichVong(List<int> pc1, int index)
{
    List<int> nuatrai = new List<int>();
    for (int i = 0; i < pc1.Count / 2; i++)
    {
        nuatrai.Add(pc1[i]);
    }
    List<int> nuaphai = new List<int>();
    for (int i = pc1.Count / 2; i < pc1.Count; i++)
    {
        nuaphai.Add(pc1[i]);
    }
    if (index == 0||index == 1||index == 8|| index == 15)
    {
        int tam = nuaphai[0];
        nuaphai.Add(tam);
        nuaphai.Remove(nuaphai[0]);
        int tam2 = nuatrai[0];
        nuatrai.Add(tam2);
        nuatrai.Remove(nuatrai[0]);
    }
    else
    {
        for (int i = 0; i < 2; i++)
        {
            int tam = nuaphai[0];
            nuaphai.Add(tam);
            nuaphai.Remove(nuaphai[0]);
            int tam2 = nuatrai[0];
            nuatrai.Add(tam2);
            nuatrai.Remove(nuatrai[0]);
        }
    }
    List<int> k = new List<int>();
    for (int i = 0; i < nuatrai.Count; i++)
    {
        k.Add(nuatrai[i]);
    }
    for (int i = 0; i < nuaphai.Count; i++)
    {
        k.Add(nuaphai[i]);
    }
    return k;
}
//hàm hoán vị pc2 : nhận vào một list các bit khoa k đã dịch vòng 
//hoán vị pc2 theo bảng hoán vị pc2 trả về list bit đã hoán vị
List<int> hoanViPC2(List<int> dadichvong)
{
    List<int> pc2 = new List<int>();
    int[] vthv = {14, 17, 11, 24,  1,  5,
        3, 28, 15,  6, 21, 10,
        23, 19, 12,  4, 26,  8,
        16,  7, 27, 20, 13,  2,
        41, 52, 31, 37, 47, 55,
        30, 40, 51, 45, 33, 48,
        44, 49, 39, 56, 34, 53,
        46, 42, 50, 36, 29, 32};
    for (int i = 0; i < vthv.Length; i++)
    {
        pc2.Add(dadichvong[vthv[i]-1]);
    }
    return pc2;
}
//Hoán vị ip1 :
//truyền vào 1 chuỗi là bản rõ cần được mã hóa 
//tách chuỗi lưu các kí tự vào một mảng chuỗi sau đó chuyển sang nhị phân
//các bít được lưu vào list int, sau đó hoán vị ip theo bảng hoán vị
//hàm trả về một lít các bit đã được hoán vị
List<int> hoanviIP1(string M)
{
    List<int> mip1 = new List<int>();
    string[] mangm = M.Select(c => c.ToString()).ToArray();
    List<int> mlist = new List<int>();
    for (int i = 0; i < mangm.Length; i++)
    {
        switch (mangm[i])
        {
            case "a":
                mangm[i] = "10";
                break;
            case "A":
                mangm[i] = "10";
                break;

            case "B":
                mangm[i] = "11";
                break;
            case "b":
                mangm[i] = "11";
                break;

            case "c":
                mangm[i] = "12";
                break;
            case "C":
                mangm[i] = "12";
                break;

            case "d":
                mangm[i] = "13";
                break;
            case "D":
                mangm[i] = "13";
                break;

            case "e":
                mangm[i] = "14";
                break;
            case "E":
                mangm[i] = "14";
                break;

            case "f":
                mangm[i] = "15";
                break;
            case "F":
                mangm[i] = "15";
                break;
            default:
                break;
        }
        List<int> tam = new List<int>();
        tam = ChuyenSangNhiPhan(int.Parse(mangm[i]));
        for (int j = 0; j < tam.Count; j++)
        {
            mlist.Add(tam[j]);
        }
    }
    int[] vthv = {58, 50, 42, 34, 26, 18, 10, 2,
                60, 52, 44, 36, 28, 20, 12, 4,
                62, 54, 46, 38, 30, 22, 14, 6,
                64, 56, 48, 40, 32, 24, 16, 8,
                57, 49, 41, 33, 25, 17,  9, 1,
                59, 51, 43, 35, 27, 19, 11, 3,
                61, 53, 45, 37, 29, 21, 13, 5,
                63, 55, 47, 39, 31, 23, 15, 7};
    for (int i = 0; i < vthv.Length; i++)
    {
        mip1.Add(mlist[vthv[i] - 1]);
    }
    return mip1;
}
// hàm mở rộng nửa phải 
// nhận vào list lưu các bit nửa phải bản rõ, sau đó mở rộng theo bảng
//hàm trả về nửa phải đã được mở rộng
List<int> moRongNuaPhai(List<int> r1)
{
    List<int> r1mr = new List<int>();
    int[] ints = { 32,1,2,3,4,5,4,5,6,7,8,9,8,9,10,11,12,13,12,13,14,15,16,17,16,17,18,19,20,21,20,21,22,23,24,25,
    24,25,26,27,28,29,28,29,30,31,32,1};
    for (int i = 0; i < ints.Length; i++)
    {
        r1mr.Add(r1[ints[i] - 1]);
    }
    return r1mr;
}
//hàm thực hiện phép XOR hàm nhận vào 2 list có độ lớn bằng nhau lưu các bít nhị phân
//thực hiện so các bít ở 2 list ở các vị trí tương ứng
//hàm trả về list kết quả
List<int> so(List<int> r1, List<int> k)
{
    List<int> rtam = new List<int>();
    for (int i = 0; i < r1.Count; i++)
    {
        rtam.Add(r1[i] ^ k[i]);
    }
    return rtam;
}
//hàm thế s-box 1 
//hàm nhận vào list 6 bít nhị phân,
//trả về list 4 bit đã được thục hiện phép thế s box
List<int> hops1(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 };
    int[] zron = {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8};
    int[] onzr = {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0};
    int[] onon = {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13};
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 2
//quy trình tương tự hop1()
List<int> hops2(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 };
    int[] zron = { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 };
    int[] onzr = { 0 ,14 ,7 ,11 ,10 ,4 ,13, 1, 5, 8 ,12, 6, 9, 3, 2 ,15 };
    int[] onon = { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 3
//quy trình tương tự hop1()
List<int> hops3(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7 ,11, 4, 2, 8 };
    int[] zron = { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 };
    int[] onzr = { 13, 6, 4, 9, 8, 15, 3, 0, 11 ,1, 2, 12, 15, 10, 14, 7 };
    int[] onon = { 1, 10 ,13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 4
//quy trình tương tự hop1()
List<int> hops4(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 7, 13, 14, 3, 0, 6, 9 ,10 ,1 ,2, 8, 5, 11, 12, 4, 15 };
    int[] zron = { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 };
    int[] onzr = { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 };
    int[] onon = { 3, 15, 0, 6, 10, 1, 13, 8 ,9, 4, 5, 11, 12, 7, 2 ,14 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 5
//quy trình tương tự hop1()
List<int> hops5(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 };
    int[] zron = { 14 ,11, 2, 12, 4, 7 ,13, 1, 5, 0, 15, 10, 3, 9, 8, 6 };
    int[] onzr = { 4, 2, 1, 11, 10, 13, 7 ,8 ,15, 9, 12, 5, 6, 3, 0, 14 };
    int[] onon = { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 6
//quy trình tương tự hop1()
List<int> hops6(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 };
    int[] zron = { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 };
    int[] onzr = { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 };
    int[] onon = { 4, 3, 2, 12, 9, 5, 15, 10 ,11, 14, 1, 7, 6, 0, 8, 13 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 7
//quy trình tương tự hop1()
List<int> hops7(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 4 ,11, 2 ,14, 15, 0, 8, 13, 3, 12, 9 ,7 ,5 ,10 ,6 ,1};
    int[] zron = { 13 ,0 ,11 ,7 ,4 ,9 ,1 ,10, 14, 3, 5, 12 ,2 ,15, 8 ,6 };
    int[] onzr = { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8 ,0 ,5 ,9 ,2 };
    int[] onon = { 6 ,11 ,13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm thế s-box 8
//quy trình tương tự hop1()
List<int> hops8(List<int> chuoi6)
{
    string saubit = "";
    foreach (var item in chuoi6)
    {
        saubit += item;
    }
    string haibit = saubit.Remove(1, 4);
    string bonbit = saubit.Remove(0, 1).Remove(4, 1);
    int[] zrzr = { 13 ,2 ,8 ,4 ,6 ,15 ,11 ,1 ,10 ,9, 3, 14, 5, 0, 12, 7 };
    int[] zron = { 1 ,15 ,13 ,8 ,10 ,3 ,7 ,4 ,12, 5 ,6 ,11, 0, 14, 9, 2 };
    int[] onzr = { 7, 11, 4, 1, 9 ,12, 14, 2 ,0 ,6 ,10, 13, 15, 3, 5, 8 };
    int[] onon = { 2, 1 ,14, 7, 4, 10, 8, 13, 15, 12 ,9, 0, 3, 5, 6, 11 };
    List<int> a = new List<int>();
    a.Add(0);
    switch (bonbit)
    {
        case "0000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[0]);
                case "01":
                    return ChuyenSangNhiPhan(zron[0]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[0]);
                case "11":
                    return ChuyenSangNhiPhan(onon[0]);
                default:
                    return a;
            }
        case "0001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[1]);
                case "01":
                    return ChuyenSangNhiPhan(zron[1]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[1]);
                case "11":
                    return ChuyenSangNhiPhan(onon[1]);
                default:
                    return a;
            }
        case "0010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[2]);
                case "01":
                    return ChuyenSangNhiPhan(zron[2]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[2]);
                case "11":
                    return ChuyenSangNhiPhan(onon[2]);
                default:
                    return a;
            }
        case "0011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[3]);
                case "01":
                    return ChuyenSangNhiPhan(zron[3]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[3]);
                case "11":
                    return ChuyenSangNhiPhan(onon[3]);
                default:
                    return a;
            }
        case "0100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[4]);
                case "01":
                    return ChuyenSangNhiPhan(zron[4]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[4]);
                case "11":
                    return ChuyenSangNhiPhan(onon[4]);
                default:
                    return a;
            }
        case "0101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[5]);
                case "01":
                    return ChuyenSangNhiPhan(zron[5]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[5]);
                case "11":
                    return ChuyenSangNhiPhan(onon[5]);
                default:
                    return a;
            }
        case "0110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[6]);
                case "01":
                    return ChuyenSangNhiPhan(zron[6]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[6]);
                case "11":
                    return ChuyenSangNhiPhan(onon[6]);
                default:
                    return a;
            }
        case "0111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[7]);
                case "01":
                    return ChuyenSangNhiPhan(zron[7]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[7]);
                case "11":
                    return ChuyenSangNhiPhan(onon[7]);
                default:
                    return a;
            }
        case "1000":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[8]);
                case "01":
                    return ChuyenSangNhiPhan(zron[8]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[8]);
                case "11":
                    return ChuyenSangNhiPhan(onon[8]);
                default:
                    return a;
            }
        case "1001":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[9]);
                case "01":
                    return ChuyenSangNhiPhan(zron[9]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[9]);
                case "11":
                    return ChuyenSangNhiPhan(onon[9]);
                default:
                    return a;
            }
        case "1010":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[10]);
                case "01":
                    return ChuyenSangNhiPhan(zron[10]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[10]);
                case "11":
                    return ChuyenSangNhiPhan(onon[10]);
                default:
                    return a;
            }
        case "1011":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[11]);
                case "01":
                    return ChuyenSangNhiPhan(zron[11]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[11]);
                case "11":
                    return ChuyenSangNhiPhan(onon[11]);
                default:
                    return a;
            }
        case "1100":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[12]);
                case "01":
                    return ChuyenSangNhiPhan(zron[12]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[12]);
                case "11":
                    return ChuyenSangNhiPhan(onon[12]);
                default:
                    return a;
            }
        case "1101":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[13]);
                case "01":
                    return ChuyenSangNhiPhan(zron[13]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[13]);
                case "11":
                    return ChuyenSangNhiPhan(onon[13]);
                default:
                    return a;
            }
        case "1110":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[14]);
                case "01":
                    return ChuyenSangNhiPhan(zron[14]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[14]);
                case "11":
                    return ChuyenSangNhiPhan(onon[14]);
                default:
                    return a;
            }
        case "1111":
            switch (haibit)
            {
                case "00":
                    return ChuyenSangNhiPhan(zrzr[15]);
                case "01":
                    return ChuyenSangNhiPhan(zron[15]);
                case "10":
                    return ChuyenSangNhiPhan(onzr[15]);
                case "11":
                    return ChuyenSangNhiPhan(onon[15]);
                default:
                    return a;
            }
        default:
            return a;
    }
}
//hàm hoán vị ip - 1:
//nhận vào một list lưu các bit bản mã đã được thực hiện 16 vòng mã hóa
//hoán vị theo bảng hoán vị ip - 1
//trả về list các bit đã được hoán vị
List<int> hoanViIPTruMot(List<int> hopx)
{
    int[] vthv = {40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25};
    List<int> hvp = new List<int>();
    for(int i = 0; i < vthv.Length; i++)
    {
        hvp.Add(hopx[vthv[i]-1]);
    }
    return hvp;
}
//hàm hoán vị p:
//nhận vào một list lưu các bit nửa phải bản rõ đã được thực hiện phép thế s-box
//hoán vị theo bảng hoán vị p
//trả về list các bit đã được hoán vị
List<int> hoanViP(List<int> xong)
{
    int[] vthv = {16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6,
        22, 11, 4, 25};
    List<int> hvip = new List<int>();
    for (int i = 0; i < vthv.Length; i++)
    {
        hvip.Add(xong[vthv[i] - 1]);
    }
    return hvip;
}
//hàm thực hiện toàn bộ quá trình mã hóa
//hàm nhận vào list các bit bản rõ đã được thực hiện hoán vị ip, list các bit khóa k đã thực hiện hoán vị pc1
//hàm trả về list các bit nhị phân bản mã
List<int> mahoa(List<int> mip, List<int> kpc1)
{
    //lưu khóa sau khi pc1 vào một list ktam để dùng sau, đảm bảo kpc1 không bị thay đổi
    List<int> ktam = kpc1;
    //tach nửa trái phải ban ro da hoan vi ip
    List<int> l1 = new List<int>();
    List<int> r1 = new List<int>();
    for (int i = 0; i < 32; i++)
    {
        l1.Add(mip[i]);
    }
    for (int i = 32; i < 64; i++)
    {
        r1.Add(mip[i]);
    }
    //16 vòng lặp ma hóa
    for (int d=0; d < 16; d++)
    {
        Console.WriteLine("vong" + (d+1));
        //dịch vòng khóa k 
        List<int> kdicvong = dichVong(ktam, d);
        //hoán vị khóa k
        List<int> k = hoanViPC2(kdicvong);//48 bit
        //mở rộng nửa phải bản rõ 
        List<int> r1mr = moRongNuaPhai(r1);
        //XOR nửa phải với khóa k
        List<int> r1soki = so(r1mr, k);
        //thế s-box
        List<int> hop = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            List<int> saubit = new List<int>();
            saubit.Add(r1soki[0]);
            saubit.Add(r1soki[1]);
            saubit.Add(r1soki[2]);
            saubit.Add(r1soki[3]);
            saubit.Add(r1soki[4]);
            saubit.Add(r1soki[5]);
            List<int> the6bit = new List<int>();
            if (i == 0)
            {
                the6bit = hops1(saubit);
            }
            if (i == 1)
            {
                the6bit = hops2(saubit);
            }
            if (i == 2)
            {
                the6bit = hops3(saubit);
            }
            if (i == 3)
            {
                the6bit = hops4(saubit);
            }
            if (i == 4)
            {
                the6bit = hops5(saubit);
            }
            if (i == 5)
            {
                the6bit = hops6(saubit);
            }
            if (i == 6)
            {
                the6bit = hops7(saubit);
            }
            if (i == 7)
            {
                the6bit = hops8(saubit);
            }
            foreach (var item in the6bit)
            {
                hop.Add(item);
            }
            r1soki.Remove(r1soki[0]);
            r1soki.Remove(r1soki[0]);
            r1soki.Remove(r1soki[0]);
            r1soki.Remove(r1soki[0]);
            r1soki.Remove(r1soki[0]);
            r1soki.Remove(r1soki[0]);
        }
        //hoán vị p 
        List<int> hvp = hoanViP(hop);
        //XOR nửa trái với nửa phải 
        List<int> r1tam = so(hvp, l1);
        //gán lại các bit nửa trái bằng với các bít nửa phải
        l1.Clear();
        Console.WriteLine("2 nua sau khi ma hoa");
        foreach (var item in r1)
        {
            l1.Add(item);
            Console.Write(item);
        }
        //gán lại nửa phải bằng với các bit sau khi thực hiện phép XOR với nửa trái
        r1.Clear();
        foreach (var item in r1tam)
        {
            r1.Add(item);
            Console.Write(item);
        }
        Console.WriteLine();
        Console.WriteLine("k sinh moi");
        //gán lại khóa k bằng với các bit khi thực hiện phép dịch vòng ban đầu vòng lặp
        ktam.Clear();
        foreach (var item in kdicvong)
        {
            ktam.Add(item);
            Console.Write(item);
        }
        Console.WriteLine();
    }
    //tạo một list c lưu các bit bản mã
    List<int> c = new List<int>();
    //lưu các bit phải trước vào c sau đó lưu các bit bên trái sau (hoán đổi 2 nửa cho nhau)
    for (int i = 0; i < r1.Count; i++)
    {
        c.Add(r1[i]);
    }
    for (int i = 0; i < l1.Count; i++)
    {
        c.Add(l1[i]);
    }
    return c;
}

List<int> ctnp = hoanViIPTruMot(mahoa(hoanviIP1(m), hoanViPC1(k)));
List<int> ctnptam = ctnp;
string ct = "";
//chuyển list nhị phân bản mã thành hệ hex
for (int i = 0; i < 16; i++)
{
    string bonbit = "";
    for (int j = 0; j < 4; j++)
    {
        bonbit = bonbit + ctnptam[0].ToString();
        ctnptam.Remove(ctnptam[0]);
    }
    ct = ct + chuyenSangHEX(bonbit);
}

//in ra kết quả 
Console.WriteLine("ban ro");
Console.WriteLine(m);
Console.WriteLine("key");
Console.WriteLine(k);
Console.WriteLine("ban ma");
Console.WriteLine(ct);