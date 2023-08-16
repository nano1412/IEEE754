using System;
using Microsoft.Win32.SafeHandles;

class Program
{
  static void Main(string[] args)
  {
    // input and calling function
    string input = Console.ReadLine();
    Console.WriteLine(IEEE754(input));
  }
  static int IEEE754(string input)
  {
    // split
    string[] num = input.Split('.');
    int front_num = Math.Abs(int.Parse(num[0]));
    int back_num = int.Parse(num[1]);

    // sign
    int sign = 0;
    if (int.Parse(num[0]) <= 0) { sign = 1; }

    // Mantissa and decimal Exponent
    string binary_front_num = ToBinary(front_num, 23);
    int decimal_exponent = binary_front_num.ToString().Length;
    string binary_back_num = ToBinaryFraction(back_num, 24 - decimal_exponent);

    // Exponent
    string exponent = ToBinary(126 + decimal_exponent, 8);

    // combine and return
    return Convert.ToInt32(sign + exponent + binary_front_num.Remove(0, 1) + binary_back_num);
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
    while (num > 0 && k > 0)
    {
      num = num * 2;
      int i = num / overdigit;
      if (num > overdigit) { num = num - overdigit; }
      binary = binary + i.ToString();
      k--;
    }
    return binary;
  }

  // check invalid input type (ถ้าที่ใส่มาเป็น)
  static bool IsValid(string text)
  {
    foreach (char digit in text)
    {
      if (!"-0123456789.".Contains(digit))
      {
        return false;
      }
    }
    return true;

  }
}