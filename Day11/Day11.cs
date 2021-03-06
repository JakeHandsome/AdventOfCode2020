﻿using System;
using System.IO;
using System.Linq;

/// <summary>
/// Part 1 wasn't so bad
/// Part 2 took a while since I had to rewrite a lot and keep the classes common
/// </summary>
namespace Day11
{
   public enum SearchType
   {
      Adjacent,
      LineOfSight,
   }

   public struct Slope
   {
      public int X;
      public int Y;
      public Slope(int x, int y)
      {
         X = x;
         Y = y;
      }
   }
   /// <summary>
   /// This represents one tile on the board
   /// </summary>
   public class Tile
   {
      public const char FLOOR = '.';
      public const char EMPTY = 'L';
      public const char OCCUPIED = '#';
      private int X { get; }
      private int Y { get; }
      public char state { get; private set; }
      private char newState { get; set; }
      /// <summary>
      /// There are all the directions for the line of sight check
      /// </summary>
      private readonly Slope[] Directions = new Slope[] {
            new Slope( 1,  0),
            new Slope( 1,  1),
            new Slope( 1, -1),
            new Slope( 0,  1),
            new Slope( 0, -1),
            new Slope(-1,  0),
            new Slope(-1,  1),
            new Slope(-1,- 1),
         };
      public Tile(int x, int y, char val)
      {
         X = x;
         Y = y;
         this.state = val;
      }
      public bool IsOccupied
      {
         get
         {
            return state == Tile.OCCUPIED;
         }
      }
      /// <summary>
      /// Returns the number of occupied adjecent seat
      /// </summary>
      /// <param name="current">The current model this tile belongs to</param>
      /// <returns>The number of seats that are occupied</returns>
      private int GetAdjacentOccupied(Model current)
      {
         var numOccupied = 0;
         for (int x = X - 1; x <= X + 1; x++)
         {
            for (int y = Y - 1; y <= Y + 1; y++)
            {
               if (x == X && y == Y || x < 0 || y < 0 || x >= current.Rows || y >= current.Cols)
                  continue;
               if (current.arr[x, y].IsOccupied)
                  numOccupied++;
            }
         }
         return numOccupied;
      }
      /// <summary>
      /// Does a line of sight check in each directory to see if the next seat is occupied
      /// </summary>
      /// <param name="current">The current model this tile belongs to</param>
      /// <returns></returns>
      private int GetLineOfSightOccupied(Model current)
      {
         var numOccupied = 0;
         // Go through each direction and keep steping that way until we find a seat
         foreach (var dir in Directions)
         {
            var x = X;
            var y = Y;
            while (true)
            {
               x += dir.X;
               y += dir.Y;
               // If we get out of bounds of the model, exit loop
               if (x < 0 || y < 0 || x >= current.Rows || y >= current.Cols)
                  break;
               // If the seat we find is occupied, increment and exit loop
               if (current.arr[x, y].IsOccupied)
               {
                  numOccupied++;
                  break;
               }
               // If we find an empty seat exit because we cannot see past that seat
               else if (current.arr[x, y].state == Tile.EMPTY)
               {
                  break;
               }
            }
         }
         return numOccupied;
      }
      /// <summary>
      /// Cacluates the next state of this tile based on the model parameters
      /// </summary>
      /// <param name="current">The current model we are simulating</param>
      public void CalculateNext(Model current)
      {
         int numOccupied = 0;
         if (current.SearchType == SearchType.Adjacent)
         {
            numOccupied = GetAdjacentOccupied(current);
         }
         else
         {
            numOccupied = GetLineOfSightOccupied(current);
         }
         if (numOccupied >= current.OccupiedThreshold && state == Tile.OCCUPIED)
         {
            newState = Tile.EMPTY;
         }
         else if (numOccupied == 0 && state == Tile.EMPTY)
         {
            newState = Tile.OCCUPIED;
         }
         else
         {
            newState = state;
         }
      }
      /// <summary>
      /// Advances the tile to the next state. This needs to happen at the same
      /// time for all the tiles for accuracte calculations
      /// </summary>
      public void Next()
      {
         state = newState;
      }
   }
   /// <summary>
   /// Model class is for setting up how the simulation for seats with various settings and initial state
   /// </summary>
   public class Model
   {
      public Tile[,] arr;
      public readonly int Rows;
      public readonly int Cols;
      public readonly int OccupiedThreshold;
      public readonly SearchType SearchType;
      public Model(string[] inputs, int occupiedThreshold = 4, SearchType st = SearchType.Adjacent)
      {
         SearchType = st;
         OccupiedThreshold = occupiedThreshold;
         Rows = inputs.Length;
         Cols = inputs.Max(x => x.Length);
         arr = new Tile[Rows, Cols];
         for (var x = 0; x < Rows; x++)
         {
            for (var y = 0; y < Cols; y++)
            {
               arr[x, y] = new Tile(x, y, inputs[x][y]);
            }
         }
      }
      public override string ToString()
      {
         string str = "";
         for (int x = 0; x < arr.GetLength(0); x++)
         {
            for (int y = 0; y < arr.GetLength(1); y++)
            {
               str += arr[x, y].state;
            }
            str += Environment.NewLine;
         }
         return str;
      }
      public void Step()
      {
         foreach (Tile tile in arr)
         {
            tile.CalculateNext(this);
         }
         foreach (Tile tile in arr)
         {
            tile.Next();
         }
      }
   }
   class Day11
   {
      static void Main(string[] args)
      {
         var inputs = File.ReadAllLines("Input.txt");
         var modelpt1 = new Model(inputs);
         string before = "";
         while (before != modelpt1.ToString())
         {
            before = modelpt1.ToString();
            Console.WriteLine(before);
            modelpt1.Step();
         }
         var part1Seats = from Tile tile in modelpt1.arr
                          where tile.IsOccupied
                          select tile;
         Console.WriteLine($"Part 1:{part1Seats.Count()}");
         var modelpt2 = new Model(inputs, 5, SearchType.LineOfSight);
         before = "";
         while (before != modelpt2.ToString())
         {
            before = modelpt2.ToString();
            //  Console.WriteLine(before);
            modelpt2.Step();
         }
         var part2Seats = from Tile tile in modelpt2.arr
                          where tile.IsOccupied
                          select tile;
         Console.WriteLine($"Part 2:{part2Seats.Count()}");
      }

   }
}