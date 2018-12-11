using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Digi.Test
{
  static class Program
  {
    static void Main(string[] args)
    {
      int option;
      do
      {
        Clear();

        WriteLine("Menu de Opções:");
        WriteLine();
        WriteLine("1. Recebe três números inteiros e imprime o menor deles");
        WriteLine("2. Recebe três números inteiros diferentes e os ordena em ordem decrescente");
        WriteLine("3. Imprime uma sequência de 0 a 100, apresentando se o número é par ou ímpar");
        WriteLine("4. Recebe um valor, e valida se o mesmo está entre a faixa permitida, que é entre 1 e 9");
        WriteLine("5. Recebe um valor em Celsius e imprime o correspondente em Fahrenheit");
        WriteLine("0. Sair");
        WriteLine();

        Write("Escolha um item enumerado acima: ");
        option = ValidateOption(ReadLine().Trim());

        WriteLine();
        Init(option);

        WriteLine("Pressione qualquer tecla para continuar...");
        ReadKey();
      } while (option != 0);
    }

    static int ValidateOption(string input)
    {
      int option;

      if (!int.TryParse(input, out option))
      {
        WriteLine();
        WriteLine(nameof(input) + " deve ser numérico");
        return -1; // just reset
      }

      if (option < 0 || option > 5)
      {
        WriteLine();
        WriteLine(nameof(option) + " deve estar entre 1 e 5");
        return -1;
      }

      return option;
    }

    static float ConvertCelsiusToFahrenheit(float celsius)
    { // formula: C/5 = (F-32)/9
      // which gets into F = (9C + 160)/5
      return (9 * celsius + 160) / 5;
    }

    static IEnumerable<int> ConvertArgsToInt(this IEnumerable<string> args)
    {
      // this seems ugly and unpure, but I think it's the fastest way to get round it
      var i = 0;
      return args
        .Where(arg => int.TryParse(arg.Trim(), out i))
        .Select(x => i);
    }

    // This is the heart of this program <3
    static void Init(int code)
    {
      int n;
      string input;
      IEnumerable<string> args;
      IEnumerable<int> ints;

      switch (code)
      {
        case 1:
          Write("Digite três números inteiros separados por espaço: ");
          input = ReadLine().Trim();
          args = input.Split(' ');

          if (args.Count() != 3)
          {
            WriteLine($"Esperado 3 números, porém recebido {args.Count()} parâmetro(s)");
            break;
          }

          ints = args.ConvertArgsToInt();

          if (args.Count() != ints.Count()) // if any data gets lost it wasn't a number
          {
            WriteLine("Esperado input numérico, porém recebido texto");
            break;
          }

          WriteLine($"O menor número digitado foi: {ints.Min()}");
          break;
        case 2:
          Write("Digite três números inteiros diferentes separados por espaço: ");
          input = ReadLine().Trim();
          args = input.Split(' ');

          if (args.Count() != 3)
          {
            WriteLine($"Esperado 3 números, porém recebido {args.Count()} parâmetro(s)");
            break;
          }

          ints = args.ConvertArgsToInt();

          if (args.Count() != ints.Count()) // same as before
          {
            WriteLine("Esperado input numérico, porém recebido texto");
            break;
          }

          ints = ints.Distinct();

          if (ints.Count() != args.Count()) // now if data gets lost it was repeated
          {
            WriteLine("O input deve ser de 3 números distintos");
            break;
          }

          ints = ints.OrderByDescending(i => i);

          WriteLine("Ordem decrescente dos números: " + string.Join(", ", ints));
          break;
        case 3:
          for (var i = 0; i <= 100; i++)
          {
            WriteLine("N = {0} ({1})", i, (i % 2 == 0 ? "par" : "ímpar"));
          }

          break;
        case 4:
          Write("Digite um número: ");
          input = ReadLine().Trim();

          if (!int.TryParse(input, out n))
          {
            WriteLine("Esperado input numérico, porém recebido texto");
            break;
          }

          WriteLine(n < 1 || n > 9
            ? "O valor informado não está na faixa permitida"
            : "O valor informado está na faixa permitida");

          break;
        case 5:
          Write("Digite um valor em grau Celsius: ");
          input = ReadLine().Trim();

          if (!float.TryParse(input, out var celsius))
          {
            WriteLine("Esperado input numérico, porém recebido texto");
            break;
          }

          var fahrenheit = ConvertCelsiusToFahrenheit(celsius);
          WriteLine($"{celsius}°C em Fahrenheit é igual a {fahrenheit}°F");

          break;
      }

      WriteLine();
    }
  }
}
