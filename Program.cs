using System;
using Microsoft.Win32; // Registro de Windows, no disponible en Linux

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Calculadora Legacy .NET Framework =====");
            Console.WriteLine("Ingresa operaciones en formato: número operador número");
            Console.WriteLine("Operadores soportados: +, -, *, /");
            Console.WriteLine("Escribe 'salir' para terminar");
            Console.WriteLine("============================================");

            // Intentar acceder al registro de Windows (fallará en Linux)
            try {
                Console.WriteLine("Verificando registro de Windows...");
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion");
                Console.WriteLine($"Versión de Windows: {key?.GetValue("ProductName")}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al acceder al registro: {ex.Message}");
            }    

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                
                if (input.ToLower() == "salir")
                    break;
                
                try
                {
                    // Parsear la entrada
                    string[] parts = input.Split(' ');
                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Formato incorrecto. Usa: número operador número");
                        continue;
                    }
                    
                    double num1 = double.Parse(parts[0]);
                    string op = parts[1];
                    double num2 = double.Parse(parts[2]);
                    double result = 0;
                    
                    // Realizar la operación
                    switch (op)
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "*":
                            result = num1 * num2;
                            break;
                        case "/":
                            if (num2 == 0)
                            {
                                Console.WriteLine("Error: No se puede dividir por cero");
                                continue;
                            }
                            result = num1 / num2;
                            break;
                        default:
                            Console.WriteLine($"Operador no soportado: {op}");
                            continue;
                    }
                    
                    Console.WriteLine($"Resultado: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
