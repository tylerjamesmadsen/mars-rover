using MarsRover.Services;
using SDK.Models;
using SDK.Services;
using System;
using System.Collections.Generic;

namespace MarsRover
{
  /// <summary>
  /// The MarsRover console program.
  /// </summary>
  class Program
  {
    /// <summary>
    /// The main entrypoint for this program.
    /// </summary>
    /// <param name="args">The arguments.</param>
    static void Main(string[] args)
    {
      Console.WriteLine(@"Mars Rover

Input:
");
      Grid2D grid = PromptForGrid();
      Dictionary<Rover, List<Instruction>> rovers = PromptForRovers();

      CommandCenterService commandCenter = new CommandCenterService(grid, rovers);
      string output = commandCenter.RunCommands();

      Console.WriteLine($@"Output:

{output}");
    }

    /// <summary>
    /// Prompt the user for a grid.
    /// </summary>
    /// <returns></returns>
    private static Grid2D PromptForGrid()
    {
      while (true) // Prompt until the user enters a valid grid
      {
        string gridInput = Console.ReadLine();
        if (InputService.TryParseGrid(gridInput, out Grid2D grid))
        {
          return grid;
        }
        else
        {
          Console.WriteLine("Invalid grid boundaries. Please try again.");
        }
      }
    }

    /// <summary>
    /// Prompts for rover(s) with instruction(s).
    /// </summary>
    /// <returns>A dictionary of rovers with their corresponding instructions.</returns>
    private static Dictionary<Rover, List<Instruction>> PromptForRovers()
    {
      Dictionary<Rover, List<Instruction>> roverInstructions = new Dictionary<Rover, List<Instruction>>();
      while (true) // Prompt until the user is finished (enters a blank line)
      {
        Rover rover = PromptForRover();

        if (rover == null)
        {
          return roverInstructions;
        }

        List<Instruction> instructions = PromptForInstructions();

        roverInstructions[rover] = instructions;
      }
    }

    /// <summary>
    /// Prompts for rover position and heading.
    /// </summary>
    /// <returns>A rover.</returns>
    private static Rover PromptForRover()
    {
      while (true) // Prompt until the user enters a valid position and heading
      {
        Rover rover = null;
        string roverInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(roverInput) || InputService.TryParseRover(roverInput, out rover))
        {
          return rover;
        }
        else
        {
          Console.WriteLine("Invalid position/heading. Please try again.");
        }
      }
    }

    /// <summary>
    /// Prompts for rover turn/move instructions.
    /// </summary>
    /// <returns>A list of instructions.</returns>
    private static List<Instruction> PromptForInstructions()
    {
      while (true) // Prompt until the user enters valid instructions
      {
        string instructionsInput = Console.ReadLine();
        
        if (InputService.TryParseMovementInstructions(instructionsInput, out List<Instruction> instructions))
        {
          return instructions;
        }
        else
        {
          Console.WriteLine("Invalid instructions. Please try again.");
        }
      }
    }
  }
}
