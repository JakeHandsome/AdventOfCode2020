using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
   class Day8
   {
      static bool Part1Finished = false;
      static List<Instruction> program = new List<Instruction>();
      static void Main(string[] args)
      {
         var input = File.ReadAllLines("Input.txt");
         foreach (var line in input)
         {
            ParseInstruction(line);
         }
         // Execute a copy as to not change the original
         var copy = program.Clone().ToList();
         ExecuteProgram(copy);

         for (int i = 0; i < program.Count; i++)
         {
            // Brute force. If the instruction is a jump or nop, swap it and run it again
            if (program[i].InstructionType == InstructionType.Jump)
            {
               copy = program.Clone().ToList();
               copy[i].InstructionType = InstructionType.Nop;
               ExecuteProgram(copy);
            }
            else if (program[i].InstructionType == InstructionType.Nop)
            {
               copy = program.Clone().ToList();
               copy[i].InstructionType = InstructionType.Jump;
               ExecuteProgram(copy);
            }
         }
      }

      private static void ExecuteProgram(List<Instruction> program)
      {
         int accumulator = 0;
         int instructionPointer = 0;
         for (; ; )
         {
            if (instructionPointer >= program.Count)
            {
               Console.WriteLine($"Part2:{accumulator}");
               break;
            }
            var currentInstruction = program[instructionPointer];
            if (currentInstruction.Executed)
            {
               if (!Part1Finished)
               {
                  Console.WriteLine($"Part1:{accumulator}");
                  Part1Finished = true;
               }
               break;
            }
            currentInstruction.Executed = true;
            switch (currentInstruction.InstructionType)
            {
               case InstructionType.Accumulate:
                  accumulator += currentInstruction.Value;
                  break;
               case InstructionType.Jump:
                  instructionPointer += currentInstruction.Value;
                  continue;
               case InstructionType.Nop:
                  break;
            }
            instructionPointer++;
         }
      }
      private static void ParseInstruction(string line)
      {
         var instruction = new Instruction();
         var split = line.Split(' ');
         switch (split[0])
         {
            case "acc":
               instruction.InstructionType = InstructionType.Accumulate;
               break;
            case "jmp":
               instruction.InstructionType = InstructionType.Jump;
               break;
            case "nop":
               instruction.InstructionType = InstructionType.Nop;
               break;
            default:
               throw new NotImplementedException();
         }
         instruction.Value = Convert.ToInt32(split[1]);
         program.Add(instruction);
      }

      class Instruction : ICloneable
      {
         public InstructionType InstructionType;
         public int Value;
         public bool Executed = false;

         public object Clone()
         {
            var clone = new Instruction();
            clone.InstructionType = InstructionType;
            clone.Value = Value;
            clone.Executed = Executed;
            return clone;
         }
      }

      enum InstructionType
      {
         Accumulate,
         Jump,
         Nop
      }
   }

   static class Extenisions
   {
      public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable
      {
         return collection.Select(item => (T)item.Clone());
      }
   }

}