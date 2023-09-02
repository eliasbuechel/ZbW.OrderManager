using System.Text.RegularExpressions;

namespace DataLayer.Base.DataValidators
{
    public static class StringValidator
    {
        public static bool Validate(string value, string pattern)
        {
            if (string.IsNullOrEmpty(value)) return false;

            return Regex.Match(value, pattern).Success;
        }
    }
}
