using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


/// <summary>
/// Trees and recursion. I made a shitty tree that is probs slow
/// </summary>
namespace Day7
{
   class Program
   {
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         foreach (var line in input)
         {
            ProcessLine(line);
         }
         var shinyGoldBag = Bag.allBag.Where(bag => bag.Color == "shiny gold").Single();
         var bagsWithShinyGold = Bag.allBag.Where(bag => bag.Contains(shinyGoldBag)).Count();
         Console.WriteLine($"Part1: {bagsWithShinyGold}");
         Console.WriteLine($"Part2: {shinyGoldBag.Count()}");
      }

      /// <summary>
      /// Convert string to bag object
      /// </summary>
      /// <param name="line">The line to parse</param>
      static void ProcessLine(string line)
      {
         var split1 = line
            .Substring(0, line.Length - 1) // Remove the .
            .Replace("bags", "")
            .Replace("bag", "")
            .Split("contain");

         var bag = Bag.GetBagWithColor(split1[0].Trim());
         var contents = split1[1].Split(',');

         foreach (var otherBag in contents)
         {
            if (otherBag.Contains("no other"))
            {
               continue;
            }
            // HACK my puzzle input only has signle digit numbers so I can cheat here a bit
            var count = Convert.ToUInt32(otherBag.Trim().Substring(0, 1));
            var bagName = otherBag.Trim().Substring(2);
            bag.AddBag(Bag.GetBagWithColor(bagName), count);
         }
      }
   }

   class Bag
   {
      /// <summary>
      /// Each bag is a singleton, here is a list of all the bags
      /// </summary>
      public static readonly HashSet<Bag> allBag = new HashSet<Bag>();

      public string Color;
      public Dictionary<Bag, uint> Contents;

      /// <summary>
      /// Get bag from list of singletons or make a new one
      /// </summary>
      /// <param name="color">Color of the bag</param>
      /// <returns>The bag matching this color</returns>
      public static Bag GetBagWithColor(string color)
      {
         var existingBag = allBag.Where(bag => bag.Color == color).SingleOrDefault();
         if (existingBag == null)
         {
            return new Bag(color);
         }
         else
         {
            return existingBag;
         }
      }

      private Bag(string color)
      {
         Color = color;
         Contents = new Dictionary<Bag, uint>();
         allBag.Add(this);
      }

      /// <summary>
      /// Adds a bag to this bag
      /// </summary>
      /// <param name="bag">the bag to add</param>
      /// <param name="quantity">number to add</param>
      public void AddBag(Bag bag, uint quantity)
      {
         Contents.Add(bag, quantity);
      }

      /// <summary>
      /// Determines if this bag contains bag
      /// </summary>
      /// <param name="bag">bag to check for</param>
      /// <returns>true if contains false otherwise</returns>
      public bool Contains(Bag bag)
      {
         if (Contents.Keys.Any(x => x == bag))
         {
            return true;
         }
         foreach (var content in Contents)
         {
            if (content.Key.Contains(bag))
            {
               return true;
            }
         }
         return false;
      }

      /// <summary>
      /// Counts the number of bags contained in this bag
      /// </summary>
      /// <returns></returns>
      public uint Count()
      {
         uint sum = 0;
         foreach(var content in Contents)
         {
            sum += (content.Key.Count() + 1) * content.Value;
         }
         return sum;
      }
   }


}
