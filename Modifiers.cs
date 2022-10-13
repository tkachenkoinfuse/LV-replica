using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceWithHangfire
{
    public static class Modifiers
    {
        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        static public string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
        public static string shielding(string inputStr)
        {
            var newString = inputStr.Replace(@"\", @"\\");
            newString = newString.Replace(@"'", @"\'");
            newString = newString.Replace(@"’", @"\’");
            newString = newString.Replace(@"(", @"\(");
            newString = newString.Replace(@")", @"\)");
            newString = Regex.Replace(newString, @"\p{Cs}", "");
            byte[] UTF8encodes = UTF8Encoding.UTF8.GetBytes(newString);
            string resText = UTF8Encoding.UTF8.GetString(UTF8encodes);
            return resText;
        }

        public static string strToDateStr(string date = "")
        {
            string[] str = date.Split('-');
            string newDate = "";
            for (int i = 0; i < 3; i++)
            {
                if (!String.IsNullOrEmpty(str[i]))
                {
                    if (i > 0) { newDate += "-"; }
                    newDate += str[i];
                }
            }
            return newDate;
        }

        public static string strToDateOffsetStr(string date = "")
        {
            string[] str = date.Split('-');
            string newDate = "";
            string sign = "";

            if (str.Length > 3)
            {
                newDate += str[3];
                sign = "-";
            }
            else
            {
                str = date.Split('+');
                if (str.Length > 1)
                {
                    newDate += str[1];
                    sign = "+";
                }

            }
            return sign + newDate;
        }

        public static string systemStringToStr(System.String[] arr)
        {
            string s = "";
            string s1 = "{";
            string s2 = "";
            string s3 = "}";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i > 0) { s2 += ","; }
                s2 += arr[i];
            }
            s = s1 + s2 + s3;
            return s;
        }

    }
}
