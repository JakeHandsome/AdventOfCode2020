using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Bit tricky, my bit masks messed be up since I was using 1 instead of `1ul` for the ulong outputs 
/// </summary>
public static class Part2
{

   public static void Solve(string[] input)
   {
      var memory = new Dictionary<ulong, ulong>();
      Mask Mask = default;
      foreach (string line in input)
      {
         if (line.StartsWith("mask"))
         {
            Mask = GetMaskFromString(line);
         }
         else
         {
            // Calculate all the addresses needed and write to all of them
            var wc = Day14.GetWriteCommandFromString(line);
            var addresses = CalculateAddresses((ulong)wc.Address, Mask);
            foreach (var address in addresses)
            {
               if (memory.ContainsKey(address))
               {
                  memory[address] = wc.Value;
               }
               else
               {
                  memory.Add(address, wc.Value);
               }
            }
         }
      }
      var part2Answer = memory.Select(x => Convert.ToInt64(x.Value)).Sum();
      Console.WriteLine($"Part 2: {part2Answer}");
   }

   private static IEnumerable<ulong> CalculateAddresses(ulong address, Mask mask)
   {
      // Start with the same masking as part 1
      address = address | mask.OR;

      // Find all the positions that need to change
      var positions = mask.Original.Reverse()
                     .Select((value, offset) => (value, offset))
                     .Where(x => x.value == 'X')
                     .Select(x => x.offset)
                     .ToList();
      var Addresses = new List<ulong>();
      // This for loop will create all permutations of the true/false.
      foreach (var p in Enumerable.Range(0, (int)Math.Pow(2, positions.Count())))
      {
         var tempAddress = address;
         // Bool array of bits
         var bits = Convert.ToString(p, 2)
                     .PadLeft(positions.Count(), '0')
                     .Select(x => x == '1')
                     .ToList();
         // For each bit, that position to 0 or 1 based on the curret permutation
         for (int i = 0; i < bits.Count; i++)
         {
            tempAddress = SetBit(tempAddress, positions[i], bits[i]);
         }
         Addresses.Add(tempAddress);
      }
      return Addresses;
   }

   private static ulong SetBit(ulong target, int position, bool toOne)
   {
      if (toOne)
      {
         target |= (1ul << position);
      }
      else
      {
         target &= ~(1ul << position);
      }
      return target;
   }

   private static Mask GetMaskFromString(string input)
   {
      var strMask = input.Split(' ').Last().Trim();
      var orMask = strMask.Replace('X', '0');
      var andMask = strMask.Replace('X', '1');
      var mask = new Mask();
      mask.Original = strMask;
      mask.OR = Convert.ToUInt64(orMask, 2);
      mask.AND = Convert.ToUInt64(andMask, 2);
      return mask;
   }

   private struct Mask
   {
      public string Original;
      public ulong AND;
      public ulong OR;
   }

}
