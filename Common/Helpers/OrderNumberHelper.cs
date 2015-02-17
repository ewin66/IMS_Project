using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public class OrderNumberHelper
    {
        public static string GetDateTimeID(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            string date=String.Format("{0:yyyyMMdd}", DateTime.Now);
            string time = String.Format("{0:HHmmss}", DateTime.Now);
            builder.Append(date);
            int digit;
            for (int i = 0; i < size; i++)
            {
                digit = random.Next(0, 9);
                builder.Append(digit.ToString());
            }

            builder.Append(time);
            
            return builder.ToString();
        }

        public static string GetOrderID(int size, string prefix)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            string date = String.Format("{0:yyyyMMdd}", DateTime.Now);
            string time = String.Format("{0:HHmmss}", DateTime.Now);
            builder.Append(date + "-");
            int digit;
            for (int i = 0; i < size; i++)
            {
                digit = random.Next(0, 9);
                builder.Append(digit.ToString());
            }

            builder.Insert(0, prefix);

            return builder.ToString();
        }

    }
}


