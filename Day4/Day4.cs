using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

      static int numPassport = 0;
      static int numValid = 0;
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         List<string> buf = new List<string>();
         foreach (var line in input)
         {
            if (string.IsNullOrEmpty(line) && buf.Any())
            {
               CheckInput(buf);
            }
            else
            {
               buf.Add(line);
            }
         }
         if (buf.Any())
         {
            CheckInput(buf);
         }
         Console.WriteLine($"Part1: {numPassport}");
         Console.WriteLine($"Part2: {numValid}");
      }

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
                Regex.IsMatch(passport["pid"], @"^\d{9}$");
      }

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

      private static bool IsPassport(List<string> buf, out Dictionary<string, string> passport)
      {
         passport = string.Join(' ', buf)
            .Split(' ')
            .ToDictionary(
               x => x.Split(':')[0],
               x => x.Split(':')[1]
            );
         return passport.Keys.Intersect(ExpectedFields).Count() == ExpectedFields.Count();
      }
   }
}
