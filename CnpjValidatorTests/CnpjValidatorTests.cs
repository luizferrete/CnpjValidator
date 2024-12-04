using Validators;


namespace CnpjValidatorTests
{
    public class CnpjValidatorTests
    {
        [Theory]
        [InlineData("", false)] // Menos de 14 caracteres
        [InlineData("abc", false)] // Menos de 14 caracteres
        [InlineData("abc123", false)] // Menos de 14 caracteres
        [InlineData("abc12345", false)] // Menos de 14 caracteres
        [InlineData("50.060.010/0001-90", true)]
        [InlineData("00.249.292/0001-09", true)]
        [InlineData("12345678901234", false)] // DVs incorretos
        [InlineData("ABCDEF12345680", true)]
        [InlineData("00!249@292/0001-09", false)] // Caracteres inválidos
        [InlineData("12.ABC.345/01DE-35", true)]
        [InlineData("1A2B3C4D5E6F34", true)]
        [InlineData("Z9Y8X7W6V5U429", true)]
        [InlineData("A1B2C3D4E5F699", false)] // DVs incorretos
        [InlineData("1234ABCD5678EF", false)] // DVs finais não numéricos
        [InlineData("11444777000161", true)]
        [InlineData("12345678000195", true)]
        [InlineData("11444777000162", false)] // DVs incorretos
        [InlineData("00000000000000", false)] // CNPJ inválido
        [InlineData("!@#$%^&*()_+00", false)] // Caracteres inválidos
        [InlineData("A1B2C3D4E5F6", false)]   // Menos de 14 caracteres
        [InlineData("A1B2C3D4E5F6G7H8", false)] // Mais de 14 caracteres
        [InlineData("1144477700016A", false)] // DV contendo letra, inválido
        [InlineData("2233445500010X", false)] // DV contendo letra, inválido
        public void ShouldValidateCnpjWithNumbersAndLetters(string cnpj, bool isValid)
        {
            var result = CnpjValidator.IsValid(cnpj);

            Assert.Equal(isValid, result);
        }
    }
}