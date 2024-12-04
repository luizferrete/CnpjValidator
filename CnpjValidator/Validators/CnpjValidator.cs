using System.Text.RegularExpressions;

namespace Validators
{
    public static class CnpjValidator
    {

        private static readonly int noVerifyingDigitCnpjSize = 12;
        private static readonly Regex noVerifyingDigitCnpj  = new Regex(@"^([A-Z\d]){12}$", RegexOptions.IgnoreCase);
        private static readonly Regex regexCNPJ = new Regex(@"^([A-Z\d]){12}(\d){2}$", RegexOptions.IgnoreCase);
        private static readonly Regex cnpjMaskCharacters = new Regex(@"[./-]", RegexOptions.Compiled);
        private static readonly Regex notAllowedCharacters = new Regex(@"[^A-Z\d./-]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly int baseValue = (int)'0';
        private static readonly int[] weigthsVD = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly string zeroedCnpj = "00000000000000";

        public static bool IsValid(string cnpj)
        {
            //99.999.999/9999-99
            if (cnpj.Length != 14 && cnpj.Length != 18)
                return false;

            if (notAllowedCharacters.IsMatch(cnpj))
                return false;

            string unmaskedCnpj = RemoveCNPJMask(cnpj).ToUpperInvariant();

            if(!regexCNPJ.IsMatch(unmaskedCnpj) || unmaskedCnpj == zeroedCnpj)
                return false;

            string verifyingDigit = unmaskedCnpj.Substring(noVerifyingDigitCnpjSize);
            string calculatedDigit = CalculateDigit(unmaskedCnpj.Substring(0, noVerifyingDigitCnpjSize));

            return verifyingDigit == calculatedDigit;
        }

        private static string CalculateDigit(string cnpj)
        {
            if (notAllowedCharacters.IsMatch(cnpj))
                return string.Empty;

            if (!noVerifyingDigitCnpj.IsMatch(cnpj))
                return string.Empty;

            int addedVD1 = 0;
            int addedVD2 = 0;

            for (int i = 0; i < noVerifyingDigitCnpjSize; i++)
            {
                int asciiDigit = (int)cnpj[i] - baseValue;
                addedVD1 += asciiDigit * weigthsVD[i + 1];
                addedVD2 += asciiDigit * weigthsVD[i];
            }

            int vd1 = addedVD1 % 11 < 2 ? 0 : 11 - (addedVD1 % 11);
            addedVD2 += vd1 * weigthsVD[noVerifyingDigitCnpjSize];
            int vd2 = addedVD2 % 11 < 2 ? 0 : 11 - (addedVD2 % 11);

            return $"{vd1}{vd2}";
        }

        private static string RemoveCNPJMask(string cnpj)
        {
            return cnpjMaskCharacters.Replace(cnpj, "");
        }
    }
}
