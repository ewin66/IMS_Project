using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public class DataHelper
    {
        public static int? GetNullableInt(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            int i;
            if (Int32.TryParse(obj.ToString(), out i)) return i;
            return null;
        }
        public static bool? GetNullableBool(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            bool i;
            if (Boolean.TryParse(obj.ToString(), out i)) return i;
            return null;
        }
        public static decimal? GetNullableDecimal(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            decimal i;
            if (decimal.TryParse(obj.ToString(), out i)) return i;
            return null;
        }
        protected static bool TryParse(string input, out int? value)
        {
            int outValue;
            bool result = Int32.TryParse(input, out outValue);
            value = outValue;
            return result;
        }

        protected static bool TryParse(string input, out short? value)
        {
            short outValue;
            bool result = Int16.TryParse(input, out outValue);
            value = outValue;
            return result;
        }

        protected static bool TryParse(string input, out long? value)
        {
            long outValue;
            bool result = Int64.TryParse(input, out outValue);
            value = outValue;
            return result;
        }
    }
}
