using System;
using System.IO;
/// <summary>
/// This one really got me thinking coordinates but got in in the end.
/// Part 2 had me drawing
/// </summary>
namespace Day12
{
   class Day12
   {
      static void Main(string[] args)
      {
         var inputs = File.ReadAllLines("Input.txt");
         var ship = new Ship();
         var ship2 = new Ship { Part1 = false };
         foreach (var line in inputs)
         {
            var direction = new Direction().FromString(line[0]);
            var value = Convert.ToInt32(line.Substring(1));
            ship.Move(direction, value);
            ship2.Move(direction, value);
         }
         Console.WriteLine($"Part 1: {Math.Abs(ship.Position.X) + Math.Abs(ship.Position.Y)}");
         Console.WriteLine($"Part 2: {Math.Abs(ship2.Position.X) + Math.Abs(ship2.Position.Y)}");
      }
   }
}
