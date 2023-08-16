using System;
using Microsoft.Win32.SafeHandles;

class Program
{
  static void Main(string[] args)
  {
    // initial
    bool isValid = true;

    // input and calling function
    string input = Console.ReadLine();
    input = formatting(input, ref isValid);

    if (isValid)
    {
      Console.WriteLine(IEEE754(input));
    }
    else
    {
      Console.WriteLine("Invalid");
    }
  }
  static string IEEE754(string input)
  {
    // sign
    string sign = ("-".Contains(input[0])) ? "1" : "0";

    // split
    string[] num = input.Split('.');
    int decimal_front_num = Math.Abs(int.Parse(num[0]));
    int decimal_back_num = int.Parse(num[1]);

    // Mantissa and decimal Exponent
    string front_num = ToBinary(decimal_front_num, 23);
    int decimal_exponent = front_num.ToString().Length;
    string back_num = ToBinaryFraction(decimal_back_num, 24 - decimal_exponent);

    // Exponent
    string exponent = ToBinary(126 + decimal_exponent, 8);

    // combine and return
    return sign + exponent + front_num.Remove(0, 1) + back_num;
  }

  static string ToBinary(int num, int k)
  {
    string binary = "";
    while (num > 0 && k > 0)
    {
      int i = num % 2;
      num = num / 2;
      binary = i.ToString() + binary;
      k--;
    }
    return binary;
  }
  static string ToBinaryFraction(int num, int k)
  {
    int overdigit = Convert.ToInt32(Math.Pow(10, num.ToString().Length));
    string binary = "";
    while (k > 0)
    {
      if (num > 0)
      {
        num = num * 2;
        int i = num / overdigit;
        if (num > overdigit) { num = num - overdigit; }
        binary = binary + i.ToString();
      }
      else
      {
        binary = binary + 0;
      }
      k--;
    }
    return binary;
  }

  // check invalid input type
  static string formatting(string text, ref bool isValid)
  {
    int dotCount = 0;
    if (!"-0123456789".Contains(text[0])) //ถ้าอยากให้ใส่เลขทศนิยมที่ไม่มีจำนวนเต็มนำหน้าได้ (เช่น ".33") ให้เติม "." ในฟันหนูด้วย
    {
      isValid = false;
      return "0.0";
    }
    string temp_text = text.Remove(0, 1);

    foreach (char digit in text.Remove(0, 1))
    {
      if (!"0123456789.".Contains(digit) || dotCount > 1)
      {
        isValid = false;
        return "0.0";
      }

      if (".".Contains(digit))
      {
        dotCount++;
      }
    }
    if (dotCount == 0)
    {
      text = text + ".0";
    }
    return text;
  }
}