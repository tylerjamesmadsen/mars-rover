using SDK.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDK.Services
{
  /// <summary>
  /// A class for managing rovers.
  /// </summary>
  public class CommandCenterService
  {
    /// <summary>
    /// The Mars plateau to investigate.
    /// </summary>
    private readonly Grid2D plateau_;

    /// <summary>
    /// The list of rovers.
    /// </summary>
    private readonly Dictionary<Rover, List<Instruction>> rovers_;

    /// <summary>
    /// Constructs a command center.
    /// </summary>
    /// <param name="plateau">The plateau grid to investigate.</param>
    /// <param name="rovers">The rovers and instructions.</param>
    public CommandCenterService(Grid2D plateau, Dictionary<Rover, List<Instruction>> rovers)
    {
      plateau_ = plateau;
      rovers_ = rovers ?? new Dictionary<Rover, List<Instruction>>();
    }

    /// <summary>
    /// Runs the commands for each rover in sequence.
    /// </summary>
    public string RunCommands()
    {
      StringBuilder commandOutput = new StringBuilder();
      foreach ((Rover rover, List<Instruction> instructions) in rovers_)
      {
        rover.SetBoundaries(plateau_.BoundaryX, plateau_.BoundaryY);
        if (rover.TryExecuteInstructions(instructions))
        {
          commandOutput.AppendLine(rover.ToString());
        }
        else
        {
          commandOutput.Append(rover).AppendLine(" -- Error: Unable to execute instructions.");
        }
      }

      return commandOutput.ToString();
    }
  }
}
