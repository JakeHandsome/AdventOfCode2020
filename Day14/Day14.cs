using System.IO;
using System.Text.RegularExpressions;
using System;

public static class Day14
{
   public static void Main()
   {
      var input = File.ReadAllLines("Input.txt");
      Part1.Solve(input);
      Part2.Solve(input);

   }

   public static WriteCommand GetWriteCommandFromString(string input)
   {
      var match = Regex.Match(input, @"mem\[(\d+)\] = (\d+)");
      WriteCommand wc = default;
      if (match.Success)
      {
         wc.Address = Convert.ToUInt32(match.Groups[1].Value);
         wc.Value = Convert.ToUInt64(match.Groups[2].Value);
      }
      return wc;
   }

   public struct WriteCommand
   {
      public uint Address;
      public ulong Value;
   }
}
