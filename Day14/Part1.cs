using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Part 1 Just split the mask into an AND and OR mask and apply both to the value when writing
///        Use a dictionary for the memory since we have gaps that are always 0 and don't need to be summed
/// </summary>
public static class Part1
{
   public static void Solve(string[] input)
   {
      var Memory = new Dictionary<uint, ulong>();
      Mask Mask = default;
      foreach (string line in input)
      {
         if (line.StartsWith("mask"))
         {
            Mask = GetMaskFromString(line);
         }
         else
         {
            var wc = Day14.GetWriteCommandFromString(line);
            WriteValueToMemory(ref Memory, Mask, wc.Value, wc.Address);
         }
      }
      var part1Answer = Memory.Select(x => Convert.ToInt64(x.Value)).Sum();
      Console.WriteLine($"Part 1: {part1Answer}");
   }


   private static void WriteValueToMemory(ref Dictionary<uint, ulong> memory, Mask mask, ulong value, uint address)
   {
      value = value & mask.AND | mask.OR;
      if (memory.ContainsKey(address))
      {
         memory[address] = value;
      }
      else
      {
         memory.Add(address, value);
      }
   }

   private static Mask GetMaskFromString(string input)
   {
      var strMask = input.Split(' ').Last().Trim();
      var orMask = strMask.Replace('X', '0');
      var andMask = strMask.Replace('X', '1');
      var mask = new Mask();
      mask.OR = Convert.ToUInt64(orMask, 2);
      mask.AND = Convert.ToUInt64(andMask, 2);
      return mask;
   }

   private struct Mask
   {
      public ulong OR;
      public ulong AND;
   }

}
