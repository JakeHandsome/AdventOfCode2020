using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// This was the easiest part2 ever, no code changes really needed
/// </summary>
namespace Day15
{
   class Program
   {
      static List<int> Input = new List<int>() { 6, 13, 1, 15, 2, 0 };
      static int PreviousNumber;
      static int currentTurn = 1;
      static Dictionary<int, List<int>> History = new Dictionary<int, List<int>>();

      static void Main(string[] args)
      {
         Setup();
         Console.WriteLine($"Part 1: {RunUntil(2020)}");
         // I realized this after submitting, I never reset any of the static variables, but it works because it keeps counting from 2020
         Console.WriteLine($"Part 2: {RunUntil(30000000)}");
      }

      static int RunUntil(int turn)
      {
         var previousNumberNew = true;
         while (currentTurn != turn + 1)
         {
            // If the previous number was entered for the first time
            if (previousNumberNew)
            {
               // Try adding a new list
               if (History.TryAdd(0, new List<int>(new[] { currentTurn })))
               {
                  previousNumberNew = true;
               }
               // If it fails just append the current list
               else
               {
                  History[0].Add(currentTurn);
                  previousNumberNew = false;
               }
               PreviousNumber = 0;
            }
            else
            {
               // Calculate the new number by looking at the 2 last numbers in the history
               var turnHistory = History[PreviousNumber];
               var newNumber = turnHistory.Last() - turnHistory[turnHistory.Count - 2];

               if (History.TryAdd(newNumber, new List<int>(new[] { currentTurn })))
               {
                  previousNumberNew = true;
               }
               else
               {
                  History[newNumber].Add(currentTurn);
                  previousNumberNew = false;
               }
               PreviousNumber = newNumber;
            }
            currentTurn++;
         }
         return PreviousNumber;
      }

      // Setup with the first numbers
      // Not sure this is needed but it works so 
      static void Setup()
      {
         for (int i = 0; i < Input.Count; i++)
         {
            History.Add(Input[i], new List<int>(new[] { currentTurn }));
            PreviousNumber = Input[i];
            currentTurn++;
         }
      }
   }
}
