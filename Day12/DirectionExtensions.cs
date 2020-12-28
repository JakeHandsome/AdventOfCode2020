using System;

namespace Day12
{
   public static class DirectionExtensions
   {
      public static Direction FromString(this Direction direction, char s)
      {
         switch (char.ToUpper(s))
         {
            case 'N':
               return Direction.North;
            case 'E':
               return Direction.East;
            case 'S':
               return Direction.South;
            case 'W':
               return Direction.West;
            case 'L':
               return Direction.Left;
            case 'R':
               return Direction.Right;
            case 'F':
               return Direction.Forward;
            default:
               throw new FormatException($"Invalid Input ${s}");
         }
      }
      public static Direction Right(this Direction dir)
      {
         switch (dir)
         {
            case Direction.North:
               return Direction.East;
            case Direction.East:
               return Direction.South;
            case Direction.South:
               return Direction.West;
            case Direction.West:
               return Direction.North;
            default:
               throw new ArgumentException("Cannot go left of that direction");
         }
      }
      public static Direction Left(this Direction dir)
      {
         switch (dir)
         {
            case Direction.North:
               return Direction.West;
            case Direction.East:
               return Direction.North;
            case Direction.South:
               return Direction.East;
            case Direction.West:
               return Direction.South;
            default:
               throw new ArgumentException("Cannot go left of that direction");
         }
      }
   }
}
