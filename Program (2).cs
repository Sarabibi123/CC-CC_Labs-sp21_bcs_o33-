using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab12Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string userInput = @"
                int x = 10;
                float y = 3.14;
                if(x > y) { 
                    print(x); 
                }
            ";

            // Initialize symbol table
            List<List<string>> symbolTable = new List<List<string>>();

            // Initialize regular expressions
            Regex variableRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
            Regex constantRegex = new Regex(@"^[0-9]+([.][0-9]+)?([e][+-]?[0-9]+)?$");
            Regex operatorRegex = new Regex(@"^[-*+/><&|=!]+$");
            Regex specialCharRegex = new Regex(@"^[.,'[\]{}();:?]+$");

            // Split input into lines
            string[] lines = userInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Loop through lines
            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                // Split line into tokens
                string[] tokens = lines[lineNumber].Split(new char[] { ' ', ';', '{', '}', '(', ')', '[', ']', '.', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);

                // Loop through tokens
                foreach (string token in tokens)
                {
                    // Check if token matches variable pattern
                    if (variableRegex.IsMatch(token))
                    {
                        AddToSymbolTable(symbolTable, token, "variable", lineNumber + 1);
                    }
                    // Check if token matches constant pattern
                    else if (constantRegex.IsMatch(token))
                    {
                        AddToSymbolTable(symbolTable, token, "constant", lineNumber + 1);
                    }
                    // Check if token matches operator pattern
                    else if (operatorRegex.IsMatch(token))
                    {
                        AddToSymbolTable(symbolTable, token, "operator", lineNumber + 1);
                    }
                    // Check if token matches special character pattern
                    else if (specialCharRegex.IsMatch(token))
                    {
                        AddToSymbolTable(symbolTable, token, "special character", lineNumber + 1);
                    }
                    else
                    {
                        // Token doesn't match any recognized pattern
                        Console.WriteLine($"Unrecognized token '{token}' at line {lineNumber + 1}");
                    }
                }
            }

            // Display symbol table
            Console.WriteLine("\nSymbol Table:");
            Console.WriteLine("Lexeme\tType\tLine Number");
            foreach (var entry in symbolTable)
            {
                Console.WriteLine($"{entry[0]}\t{entry[1]}\t{entry[2]}");
            }
        }

        // Function to add an entry to the symbol table
        static void AddToSymbolTable(List<List<string>> symbolTable, string lexeme, string type, int lineNumber)
        {
            List<string> entry = new List<string> { lexeme, type, lineNumber.ToString() };
            symbolTable.Add(entry);
        }
    }
}
