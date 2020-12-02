using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
   /// <summary>
   /// Part1:
   ///  Parse an input and find the string that has between min and max occurences of the character
   /// Part2:
   ///  Find a string that contains the character at (position min XOR position max)
   /// </summary>
   class Program
   {
      // Regex to match $"{min}-{max} {character}: {password}"
      private const string pattern = @"(?<min>\d+)-(?<max>\d+) (?<character>[a-zA-Z]): (?<password>[a-zA-Z]+)";
      private static int part1Valid = 0;
      private static int part2Valid = 0;
      static void Main(string[] args)
      {
         var input = File.ReadAllText("Input.txt");
         foreach (Match m in Regex.Matches(input, pattern, RegexOptions.Multiline))
         {
            // Make variables for the regex matches
            var min = int.Parse(m.Groups["min"].Value);
            var max = int.Parse(m.Groups["max"].Value);
            var character = char.Parse(m.Groups["character"].Value);
            var password = m.Groups["password"].Value;

            var characterCount = password.Count(x => x == character);

            if (characterCount >= min && characterCount <= max)
            {
               part1Valid++;
            }
            // != acts an XOR
            if ((password.ElementAt(min - 1) == character) !=
                (password.ElementAt(max - 1) == character))
            {
               part2Valid++;
            }
         }
         Console.WriteLine($"Part1: {part1Valid}");
         Console.WriteLine($"Part2: {part2Valid}");
      }
   }
}
