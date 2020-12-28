using System.Drawing;
using System.Linq;

namespace Day12
{
   public class Ship
   {
      public bool Part1 { get; init; } = true;
      public Point Position { get; private set; }
      public Direction Direction { get; private set; }
      public Point WayPoint { get; private set; }
      public Ship()
      {
         Position = new Point(0, 0);
         Direction = Direction.East;
         WayPoint = new Point(x: 10, y: 1);
      }
      public void Move(Direction direction, int value)
      {
         if (Part1)
         {
            MovePart1(direction, value);
         }
         else
         {
            MovePart2(direction, value);
         }
      }

      private void MovePart2(Direction direction, int value)
      {
         var wp = WayPoint;
         switch (direction)
         {
            case Direction.North:
               wp.Y += value;
               break;
            case Direction.East:
               wp.X += value;
               break;
            case Direction.South:
               wp.Y -= value;
               break;
            case Direction.West:
               wp.X -= value;
               break;
            // For left/right keep turning until we have done all the degrees
            case Direction.Left:
               for (; value > 0; value -= 90)
               {
                  Direction = Direction.Left();
                  var rwp = new Point();
                  rwp.X = wp.X - Position.X;
                  rwp.Y = wp.Y - Position.Y;
                  // To rotate left, negate Y then swap X and Y 
                  rwp.Y *= -1;
                  rwp.X = rwp.X + rwp.Y;
                  rwp.Y = rwp.X - rwp.Y;
                  rwp.X = rwp.X - rwp.Y;
                  wp.X = Position.X + rwp.X;
                  wp.Y = Position.Y + rwp.Y;
               }
               break;
            case Direction.Right:
               for (; value > 0; value -= 90)
               {
                  Direction = Direction.Right();
                  var rwp = new Point();
                  rwp.X = wp.X - Position.X;
                  rwp.Y = wp.Y - Position.Y;
                  // To rotate right, swap X and Y then negate Y
                  rwp.X = rwp.X + rwp.Y;
                  rwp.Y = rwp.X - rwp.Y;
                  rwp.X = rwp.X - rwp.Y;
                  rwp.Y *= -1;
                  wp.X = Position.X + rwp.X;
                  wp.Y = Position.Y + rwp.Y;
               }
               break;
            // Calculate relative position between wp and boat and move both by that ammount
            case Direction.Forward:
               var pos = Position;
               wp.X += (WayPoint.X - Position.X) * value;
               wp.Y += (WayPoint.Y - Position.Y) * value;
               pos.X += (WayPoint.X - Position.X) * value;
               pos.Y += (WayPoint.Y - Position.Y) * value;
               Position = pos;
               break;
         }
         WayPoint = wp;
      }

      /// <summary>
      /// Does the movement of a single command for part one. Just the ship
      /// </summary>
      /// <param name="direction">Direction command to excecute</param>
      /// <param name="value">Value of the command</param>
      private void MovePart1(Direction direction, int value)
      {
         var pos = Position;
         switch (direction)
         {
            case Direction.North:
               pos.Y += value;
               break;
            case Direction.East:
               pos.X += value;
               break;
            case Direction.South:
               pos.Y -= value;
               break;
            case Direction.West:
               pos.X -= value;
               break;
            // For left/right keep turning until we have done all the degrees
            case Direction.Left:
               for (; value > 0; value -= 90)
               {
                  Direction = Direction.Left();
               }
               break;
            case Direction.Right:
               for (; value > 0; value -= 90)
               {
                  Direction = Direction.Right();
               }
               break;
            // If we are going forward, call this function again with our current direction
            case Direction.Forward:
               Move(Direction, value);
               // Return since the move command above will update our position
               return;
            default:
               break;
         }
         Position = pos;
      }
   }
}
