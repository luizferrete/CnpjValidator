# CnpjValidator

EN: This library contains the validation of the new version of the brazilian documentation CNPJ, which will have numbers and letters starting in July 2026, as announced by the brazilian government.

PT-BR: Essa biblioteca contém a validação nova de CNPJ, utilizando números e letras, que será utilizada a partir de julho de 2026, como foi anunciado pelo governo.

Exemplo de uso / Usage example:
 ```
string cnpj = "12.ABC.345/01DE-35";
bool isValid = CnpjValidator.IsValid(cnpj);
```
