using NUnit.Framework;
using SDK.Models;
using SDK.Services;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.SDK.Services
{
  /// <summary>
  /// Tests for <see cref="CommandCenterService"/>.
  /// </summary>
  public class CommandCenterServiceTests
  {
    /// <summary>
    /// Gets test cases for Test_CommandCenterService_RunCommands
    /// </summary>
    private static IEnumerable Test_CommandCenterService_RunCommands_TestCases
    {
      get
      {
        yield return new TestCaseData(
          new Grid2D(5, 5),
          new Dictionary<Rover, List<Instruction>>
          {
            [new Rover(CompassPoint.N, 1, 2)] = new List<Instruction>
              {
                Instruction.L,
                Instruction.M,
                Instruction.L,
                Instruction.M,
                Instruction.L,
                Instruction.M,
                Instruction.L,
                Instruction.M,
                Instruction.M,
              },
            [new Rover(CompassPoint.E, 3, 3)] = new List<Instruction>
              {
                Instruction.M,
                Instruction.M,
                Instruction.R,
                Instruction.M,
                Instruction.M,
                Instruction.R,
                Instruction.M,
                Instruction.R,
                Instruction.R,
                Instruction.M,
              }
          },
          @"1 3 N
5 1 E
");
      }
    }

    /// <summary>
    /// Tests <see cref="CommandCenterService.RunCommands"/>.
    /// </summary>
    /// <param name="grid">The grid to test.</param>
    /// <param name="rovers">The rovers to test.</param>
    /// <param name="expectedResult">The expected output.</param>
    [TestCaseSource(nameof(Test_CommandCenterService_RunCommands_TestCases))]
    public void Test_CommandCenterService_RunCommands(Grid2D grid, Dictionary<Rover, List<Instruction>> rovers, string expectedResult)
    {
      // Arrange
      CommandCenterService instance = new CommandCenterService(grid, rovers);

      // Act
      string actualResult = instance.RunCommands();

      // Assert
      Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
  }
}