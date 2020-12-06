using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Used the same batch parsing as day4, needed 2 Just 2 simple count functions
/// </summary>
namespace Day6
{
   class Day6
   {
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         List<string> buf = new List<string>();

         int numberOfYes = 0;
         int numberOfGroupYes = 0;
         // Go through each line and fill up a buffer
         foreach (var line in input)
         {
            // When we find an empty line, process that buffer into a passport
            if (string.IsNullOrEmpty(line) && buf.Any())
            {
               numberOfYes += CountYes(buf);
               numberOfGroupYes += CountGroupYes(buf);
               buf.Clear();
            }
            else
            {
               buf.Add(line);
            }
         }
         // Make sure the buffer is empty before we are done
         if (buf.Any())
         {
            numberOfYes += CountYes(buf);
            numberOfGroupYes += CountGroupYes(buf);
            buf.Clear();
         }
         Console.WriteLine($"Part1: {numberOfYes}");
         Console.WriteLine($"Part2: {numberOfGroupYes}");
      }

      /// <summary>
      /// This counts the number of unique characters in the buffer by joining into a single string
      /// converting the string to a hash set (to get only unique characters) and returning the size
      /// </summary>
      /// <param name="buf">Question inputs one user per line</param>
      /// <returns>Number of questions answered yes to</returns>
      private static int CountYes(List<string> buf)
      {
         var combined = string.Join("", buf).ToHashSet();
         return combined.Count;
      }

      /// <summary>
      /// This counts the number of "yes" the whole group answered
      ///   1. Get a string of all characters and a string of all repeats
      ///   2. Check each unique character if it appears buf.Count in the string
      ///      a. This will return true if all people answered yes to the question
      ///   3. incerement a count varaible and return it at the end
      /// </summary>
      /// <param name="buf">Question inputs one user per line</param>
      /// <returns>Number of questions all people answered yes to</returns>
      private static int CountGroupYes(List<string> buf)
      {
         var count = 0;
         var combined = string.Join("", buf);

         foreach (var question in combined.ToHashSet())
         {
            if (combined.Where(x => x == question).Count() == buf.Count)
            {
               count++;
            }
         }
         return count;
      }



   }
}
