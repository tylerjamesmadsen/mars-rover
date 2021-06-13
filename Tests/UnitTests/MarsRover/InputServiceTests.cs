using MarsRover.Services;
using NUnit.Framework;
using SDK.Models;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.MarsRover.Services
{
  /// <summary>
  /// Tests the <see cref="InputService"/>.
  /// </summary>
  public class InputServiceTests
  {
    /// <summary>
    /// Gets test cases for <see cref="Test_InputService_TryParseGrid"/>.
    /// </summary>
    private static IEnumerable Test_InputService_TryParseGrid_TestCases
    {
      get
      {
        yield return new TestCaseData("5 5", new Grid2D(5, 5), true);
        yield return new TestCaseData("0 0", new Grid2D(0, 0), true);
        yield return new TestCaseData("55", null, false);
        yield return new TestCaseData("0, -1", null, false);
        yield return new TestCaseData("-1, 0", null, false);
        yield return new TestCaseData("-1, -1", null, false);
        yield return new TestCaseData("-5, -5", null, false);
        yield return new TestCaseData(null, null, false);
      }
    }

    /// <summary>
    /// Gets test cases for <see cref="Test_InputService_TryParseRover"/>
    /// </summary>
    private static IEnumerable Test_InputService_TryParseRover_TestCases
    {
      get
      {
        yield return new TestCaseData("1 2 N", new Rover(CompassPoint.N, 1, 2), true);
        yield return new TestCaseData("1 2 n", new Rover(CompassPoint.N, 1, 2), true);
        yield return new TestCaseData("3 3 E", new Rover(CompassPoint.E, 3, 3), true);
        yield return new TestCaseData("1 2 S", new Rover(CompassPoint.S, 1, 2), true);
        yield return new TestCaseData("1 2 W", new Rover(CompassPoint.W, 1, 2), true);
        yield return new TestCaseData("0 0 N", new Rover(CompassPoint.N, 0, 0), true);
        yield return new TestCaseData("0 -1 N", null, false);
        yield return new TestCaseData("-1 0 N", null, false);
        yield return new TestCaseData("0 0 A", null, false);
        yield return new TestCaseData("12N", null, false);
        yield return new TestCaseData(null, null, false);
        yield return new TestCaseData(string.Empty, null, false);
      }
    }

    /// <summary>
    /// Gets test cases for <see cref="Test_InputService_TryParseMovementInstructions"/>
    /// </summary>
    private static IEnumerable Test_InputService_TryParseMovementInstructions_TestCases
    {
      get
      {
        yield return new TestCaseData(
          "LMLMLMLMM",
          new List<Instruction>
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
          true);
        yield return new TestCaseData(
          "MMRMMRMRRM",
          new List<Instruction>
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
          },
          true);
        yield return new TestCaseData("AMRMMRMRRM", new List<Instruction>(), false);
        yield return new TestCaseData(null, new List<Instruction>(), false);
        yield return new TestCaseData(string.Empty, new List<Instruction>(), false);
      }
    }

    /// <summary>
    /// Tests <see cref="InputService.TryParseGrid"/>.
    /// </summary>
    /// <param name="input">The test input.</param>
    /// <param name="expectedGrid">The expected grid output.</param>
    /// <param name="expectedResult">The expected parse result.</param>
    [TestCaseSource(nameof(Test_InputService_TryParseGrid_TestCases))]
    public void Test_InputService_TryParseGrid(string input, Grid2D expectedGrid, bool expectedResult)
    {
      // Act
      bool actualResult = InputService.TryParseGrid(input, out Grid2D actualGrid);

      // Assert
      Assert.That(actualResult, Is.EqualTo(expectedResult));
      Assert.That(actualGrid, Is.EqualTo(expectedGrid));
    }

    /// <summary>
    /// Tests <see cref="InputService.TryParseRover"/>.
    /// </summary>
    /// <param name="input">The test input</param>
    /// <param name="expectedRover">The expected rover output.</param>
    /// <param name="expectedResult">The expected parse result.</param>
    [TestCaseSource(nameof(Test_InputService_TryParseRover_TestCases))]
    public void Test_InputService_TryParseRover(string input, Rover expectedRover, bool expectedResult)
    {
      // Act
      bool actualResult = InputService.TryParseRover(input, out Rover actualRover);

      // Assert
      Assert.That(actualResult, Is.EqualTo(expectedResult));
      Assert.That(actualRover, Is.EqualTo(expectedRover));
    }

    /// <summary>
    /// Tests <see cref="InputService.TryParseMovementInstructions"/>.
    /// </summary>
    /// <param name="input">The test input.</param>
    /// <param name="expectedInstructions">The expected instructions output.</param>
    /// <param name="expectedResult">The expected parse result.</param>
    [TestCaseSource(nameof(Test_InputService_TryParseMovementInstructions_TestCases))]
    public void Test_InputService_TryParseMovementInstructions(string input, List<Instruction> expectedInstructions, bool expectedResult)
    {
      // Act
      bool actualResult = InputService.TryParseMovementInstructions(input, out List<Instruction> actualInstructions);

      // Assert
      Assert.That(actualResult, Is.EqualTo(expectedResult));
      Assert.That(actualInstructions, Is.EquivalentTo(expectedInstructions));
    }
  }
}