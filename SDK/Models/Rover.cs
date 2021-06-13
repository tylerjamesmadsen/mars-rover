using System;
using System.Collections.Generic;

namespace SDK.Models
{
  public class Rover
  {
    /// <summary>
    /// The current heading.
    /// </summary>
    private CompassPoint heading_;

    /// <summary>
    /// The X boundary.
    /// </summary>
    private uint boundaryX_ = uint.MaxValue;

    /// <summary>
    /// The Y boundary.
    /// </summary>
    private uint boundaryY_ = uint.MaxValue;

    /// <summary>
    /// The current X (East) position.
    /// </summary>
    public uint PositionX { get; private set; }

    /// <summary>
    /// The current Y (North) position.
    /// </summary>
    public uint PositionY { get; private set; }

    /// <summary>
    /// Constructs a <see cref="Rover"/>.
    /// </summary>
    /// <param name="id">The ID.</param>
    /// <param name="initialHeading">The initial heading.</param>
    /// <param name="initialPosition">The initial X position.</param>
    public Rover(CompassPoint initialHeading, uint initialPositionX = 0, uint initialPositionY = 0)
    {
      heading_ = initialHeading;
      PositionX = initialPositionX;
      PositionY = initialPositionY;
    }

    /// <summary>
    /// Sets the movement boundaries for this rover.
    /// </summary>
    /// <param name="boundaryX">The X boundary.</param>
    /// <param name="boundaryY">The Y boundary.</param>
    public void SetBoundaries(uint boundaryX, uint boundaryY)
    {
      boundaryX_ = boundaryX;
      boundaryY_ = boundaryY;
    }

    /// <summary>
    /// Tries to execute a list of instructions.
    /// </summary>
    /// <param name="instructions">The instructions to execute.</param>
    /// <returns>True if instructions were executed successfully, otherwise false.</returns>
    public bool TryExecuteInstructions(IEnumerable<Instruction> instructions)
    {
      foreach (Instruction instruction in instructions)
      {
        switch (instruction)
        {
          case Instruction.M:
            if (!TryMove())
            {
              return false;
            }
            break;
          case Instruction.L:
          case Instruction.R:
            Turn(instruction);
            break;
        }
      }

      return true;
    }

    /// <summary>
    /// Tries to move forward.
    /// </summary>
    /// <returns>True if movement was successful, otherwise false.</returns>
    public bool TryMove()
    {
      switch (heading_)
      {
        case CompassPoint.N:
          if (PositionY < boundaryY_)
          {
            PositionY++;
          }
          else
          {
            return false;
          }
          break;
        case CompassPoint.S:
          if (PositionY > 0)
          {
            PositionY--;
          }
          else
          {
            return false;
          }
          break;
        case CompassPoint.E:
          if (PositionX < boundaryX_)
          {
            PositionX++;
          }
          else
          {
            return false;
          }
          break;
        case CompassPoint.W:
          if (PositionX > 0)
          {
            PositionX--;
          }
          else
          {
            return false;
          }
          break;
      }

      return true;
    }

    /// <summary>
    /// Performs a 90-degree turn in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to turn.</param>
    public void Turn(Instruction direction)
    {
      int headingNumeric = (int)heading_ + (int)direction;

      if (headingNumeric < 0)
      {
        headingNumeric = 3;
      }
      else if (headingNumeric > 3)
      {
        headingNumeric = 0;
      }

      heading_ = (CompassPoint)headingNumeric;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }

      return obj is Rover other
        && other.heading_ == heading_
        && other.PositionX == PositionX
        && other.PositionY == PositionY;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return (int)heading_ ^ (int)(PositionX ^ PositionY);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
      return $"{PositionX} {PositionY} {heading_}";
    }
  }
}
