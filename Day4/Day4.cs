using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


/// <summary>
/// I got stuck here for a bit because my regex was bad
/// Overall not too difficult, just data validation
/// 
/// I expected part2 to be like it was so for my part1 I had already formatted everything into a dictionary
/// </summary>
namespace Day4
{
   class Day4
   {
      static readonly string[] ExpectedFields = new[]
      {
          "byr", // (Birth Year)
          "iyr", // (Issue Year)
          "eyr", // (Expiration Year)
          "hgt", // (Height)
          "hcl", // (Hair Color)
          "ecl", // (Eye Color)
          "pid", // (Passport ID)
          // Not required "cid", // (Country ID)
      };
      
      // "Global" variables because lazy
      static int numPassport = 0;
      static int numValid = 0;
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         List<string> buf = new List<string>();
         // Go through each line and fill up a buffer
         foreach (var line in input)
         {
            // When we find an empty line, process that buffer into a passport
            if (string.IsNullOrEmpty(line) && buf.Any())
            {
               CheckInput(buf);
            }
            else
            {
               buf.Add(line);
            }
         }
         // Make sure the buffer is empty before we are done
         if (buf.Any())
         {
            CheckInput(buf);
         }
         Console.WriteLine($"Part1: {numPassport}");
         Console.WriteLine($"Part2: {numValid}");
      }

      /// <summary>
      /// This takes in the buffer and checks if it is a passport, then checks if passport is valid
      /// </summary>
      /// <param name="buf">Buffer to parse</param>
      private static void CheckInput(List<string> buf)
      {
         if (IsPassport(buf, out Dictionary<string, string> passport))
         {
            numPassport++;
            if (IsValid(passport))
            {
               numValid++;
            }
         }
         buf.Clear();
         return;
      }

      /// <summary>
      /// Checks all the keys in passport to make sure they are within range
      /// </summary>
      /// <param name="passport"></param>
      /// <returns></returns>
      private static bool IsValid(Dictionary<string, string> passport)
      {
         bool heightValid = HeightValid(passport["hgt"]);

         return int.Parse(passport["byr"]) >= 1920 && int.Parse(passport["byr"]) <= 2002 &&
                int.Parse(passport["iyr"]) >= 2010 && int.Parse(passport["iyr"]) <= 2020 &&
                int.Parse(passport["eyr"]) >= 2020 && int.Parse(passport["eyr"]) <= 2030 &&
                heightValid &&
                Regex.IsMatch(passport["hcl"], @"^#[a-z0-9]{6}$") &&
                (
                     passport["ecl"] == "amb" || passport["ecl"] == "blu" || passport["ecl"] == "brn" ||
                     passport["ecl"] == "gry" || passport["ecl"] == "grn" || passport["ecl"] == "hzl" || passport["ecl"] == "oth"
                ) &&
                Regex.IsMatch(passport["pid"], @"^\d{9}$"); // < This messed me up. I originally had @"\d{9}" so 10 digits got passed sometimes
      }

      /// <summary>
      /// Longer function to do height checking since it is the most complicated
      /// </summary>
      /// <param name="entry">The height string</param>
      /// <returns>True if height is valid, otherwise false</returns>
      private static bool HeightValid(string entry)
      {
         int height;
         Match match = Regex.Match(entry, @"\d+");
         if (match.Success)
         {
            height = int.Parse(match.Value);
            if (entry.EndsWith("cm"))
            {
               if (height >= 150 && height <= 193)
               {
                  return true;
               }
            }
            else if (entry.EndsWith("in"))
            {
               if (height >= 59 && height <= 76)
               {
                  return true;
               }
            }
         }

         return false;
      }

      /// <summary>
      /// Parses a raw buffer and produces a passport dictionary
      /// </summary>
      /// <param name="buf">The raw input buffer</param>
      /// <param name="passport">out, the formatted passport dictionary</param>
      /// <returns>True if this is a passport</returns>
      private static bool IsPassport(List<string> buf, out Dictionary<string, string> passport)
      {
         // Join each line of the buffer with a space
         passport = string.Join(' ', buf)
            // Then split by space to get each key/value pair
            .Split(' ')
            // Split each key/value pair by : and make a dictionary
            .ToDictionary(
               x => x.Split(':')[0],
               x => x.Split(':')[1]
            );
         // If the intersections bewteens they keys and ExpectedFields have the same size, the contents are identical and this is a passport
         return passport.Keys.Intersect(ExpectedFields).Count() == ExpectedFields.Count();
      }
   }
}
