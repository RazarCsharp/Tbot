using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TbotForHW.Utilities
{
    public static class Calc
    {
        public static string Sum(string input)
        {
            int b = 0;
            string a = "";
            foreach (var symbol in input)
            {
                if (char.IsNumber(symbol))
                {
                    try
                    {
                        int[] arr = (input).Split(' ').Select(n => int.Parse(n)).ToArray();
                        for (int i = 0; i <= arr.Length - 1; i++)
                        {
                            b += arr[i];
                        }
                        a = b.ToString();
                        break;
                    }
                    catch (Exception e) { a = e.Message; }
                }

                else if (!char.IsNumber(symbol))
                {
                    a = "Not Correct!!";
                }
                else { a = "WTF was that? : " + input; }
            }
            return "Сумма чисел : " + a;
        }
    }
}
