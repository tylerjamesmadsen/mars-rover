using SDK.Models;
using System;
using System.Collections.Generic;

namespace MarsRover.Services
{
  /// <summary>
  /// A class for parsing user input.
  /// </summary>
  public static class InputService
  {
    /// <summary>
    /// Tires to parses a string into boundaries.
    /// </summary>
    /// <param name="boundaries">The user-entered boundaries.</param>
    /// <returns>True if input was parsed successfully, otherwise false.</returns>
    public static bool TryParseGrid(string input, out Grid2D grid)
    {
      if (!string.IsNullOrWhiteSpace(input) &&
        input.Split(" ") is string[] inputArr &&
        inputArr.Length == 2 &&
        uint.TryParse(inputArr[0], out uint boundX) &&
        uint.TryParse(inputArr[1], out uint boundY))
      {
        grid = new Grid2D(boundX, boundY);
        return true;
      }
      else
      {
        grid = null;
        return false;
      }
    }

    /// <summary>
    /// Tries to parse a string into a X/Y position and heading values.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <param name="heading"></param>
    /// <returns>True if input was parsed successfully, otherwise false.</returns>
    public static bool TryParseRover(string input, out Rover rover)
    {
      if (!string.IsNullOrWhiteSpace(input) &&
        input.ToUpper().Split(" ") is string[] inputArr &&
        inputArr.Length == 3 &&
        uint.TryParse(inputArr[0], out uint posX) &&
        uint.TryParse(inputArr[1], out uint posY) &&
        Enum.TryParse(inputArr[2], out CompassPoint heading))
      {
        rover = new Rover(heading, posX, posY);
        return true;
      }
      else
      {
        rover = null;
        return false;
      }
    }

    /// <summary>
    /// Tries to parse a string into movement instructions.
    /// </summary>
    /// <returns>True if input was parsed successfully, otherwise false.</returns>
    public static bool TryParseMovementInstructions(string input, out List<Instruction> instructions)
    {
      instructions = new List<Instruction>();

      if (string.IsNullOrWhiteSpace(input))
      {
        return false;
      }

      foreach (char inputChar in input.ToUpper())
      {
        if (Enum.TryParse(inputChar.ToString(), out Instruction instruction))
        {
          instructions.Add(instruction);
        }
        else
        {
          return false;
        }
      }

      return true;
    }
  }
}
