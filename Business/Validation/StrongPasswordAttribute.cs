using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace AlloyAdvance.Business.Validation
{

    public class StrongPasswordAttribute : ValidationAttribute
    {
        public string SpecialCharacters { get; set; } = "!@#$&*";
        // best practice is to have high minimum length
        // but no special character requirements
        public int MinimumTotalCharacters { get; set; } = 20;
        public int MinimumUppercaseLetters { get; set; } = 0;
        public int MinimumLowercaseLetters { get; set; } = 0;
        public int MinimumDigitCharacters { get; set; } = 0;
        public int MinimumSpecialCharacters { get; set; } = 0;
        public override bool IsValid(object value)
        {
            ErrorMessage = $"Password must have: {MinimumDigitCharacters} digits, { MinimumSpecialCharacters} special characters, { MinimumUppercaseLetters} upper case, { MinimumUppercaseLetters} upper case, { MinimumLowercaseLetters} lower case, and { MinimumTotalCharacters} total length.";
            string input = value as string;

            if (string.IsNullOrWhiteSpace(input)) return true;
            var builder = new StringBuilder();
            builder.Append("^");
            if (MinimumUppercaseLetters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumUppercaseLetters; i++)
                {
                    builder.Append(".*[A-Z]");
                }
                builder.Append(")");
            }
            if (MinimumLowercaseLetters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumLowercaseLetters; i++)
                {
                    builder.Append(".*[a-z]");
                }
                builder.Append(")");
            }
            if (MinimumSpecialCharacters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumSpecialCharacters; i++)
                {
                    builder.Append(".*[");
                    builder.Append(SpecialCharacters);
                    builder.Append("]");
                }
                builder.Append(")");
            }
            if (MinimumDigitCharacters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumDigitCharacters; i++)
                {
                    builder.Append(".*[0-9]");
                }
                builder.Append(")");
            }
            builder.Append(".{");
            builder.Append(MinimumTotalCharacters);
            builder.Append(",}$");
            return Regex.IsMatch(input, builder.ToString());
        }
    }  
}