using NUnit.Framework;
using SDK.Models;
using System.Collections;

namespace UnitTests.SDK.Models
{
  /// <summary>
  /// Tests for <see cref="Rover"/>.
  /// </summary>
  public class RoverTests
  {
    /// <summary>
    /// Gets test cases for <see cref="Test_Rover_Move"/>.
    /// </summary>
    private static IEnumerable Test_Rover_Move_TestCases
    {
      get
      {
        yield return new TestCaseData(CompassPoint.N, 0u, 0u, "0 1 N").SetName("Move from: N 0 0");
        yield return new TestCaseData(CompassPoint.E, 0u, 0u, "1 0 E").SetName("Move from: E 0 0");
        yield return new TestCaseData(CompassPoint.S, 0u, 0u, "0 0 S").SetName("Move from: S 0 0");
        yield return new TestCaseData(CompassPoint.W, 0u, 0u, "0 0 W").SetName("Move from: W 0 0");
        yield return new TestCaseData(CompassPoint.N, 0u, uint.MaxValue, $"0 {uint.MaxValue} N").SetName($"Move from: N 0 {uint.MaxValue}");
        yield return new TestCaseData(CompassPoint.E, uint.MaxValue, 0u, $"{uint.MaxValue} 0 E").SetName($"Move from: E {uint.MaxValue} 0");
        yield return new TestCaseData(CompassPoint.S, 0u, uint.MaxValue, $"0 {uint.MaxValue - 1} S").SetName($"Move from: S 0 {uint.MaxValue}");
        yield return new TestCaseData(CompassPoint.W, uint.MaxValue, 0u, $"{uint.MaxValue - 1} 0 W").SetName($"Move from: W {uint.MaxValue} 0");
      }
    }

    /// <summary>
    /// Gets test cases for <see cref="Test_Rover_Turn"/>.
    /// </summary>
    private static IEnumerable Test_Rover_Turn_TestCases
    {
      get
      {
        yield return new TestCaseData(CompassPoint.N, Instruction.L, "0 0 W").SetName("Turn L from N");
        yield return new TestCaseData(CompassPoint.N, Instruction.R, "0 0 E").SetName("Turn R from N");
        yield return new TestCaseData(CompassPoint.E, Instruction.L, "0 0 N").SetName("Turn L from E");
        yield return new TestCaseData(CompassPoint.E, Instruction.R, "0 0 S").SetName("Turn R from E");
        yield return new TestCaseData(CompassPoint.S, Instruction.L, "0 0 E").SetName("Turn L from S");
        yield return new TestCaseData(CompassPoint.S, Instruction.R, "0 0 W").SetName("Turn R from S");
        yield return new TestCaseData(CompassPoint.W, Instruction.L, "0 0 S").SetName("Turn L from W");
        yield return new TestCaseData(CompassPoint.W, Instruction.R, "0 0 N").SetName("Turn R from W");
      }
    }

    /// <summary>
    /// Tests <see cref="Rover.TryMove"/>.
    /// </summary>
    /// <param name="initHeading">The initial heading to test.</param>
    /// <param name="initX">The initial X point to test.</param>
    /// <param name="initY">The inital Y point to test.</param>
    /// <param name="expected">The expected result.</param>
    [TestCaseSource(nameof(Test_Rover_Move_TestCases))]
    public void Test_Rover_Move(CompassPoint initHeading, uint initX, uint initY, string expected)
    {
      // Arrange
      Rover instance = new Rover(initHeading, initX, initY);

      // Act
      instance.TryMove();

      // Assert
      Assert.That(instance.ToString(), Is.EqualTo(expected));
    }


    /// <summary>
    /// Tests <see cref="Rover.Turn"/>.
    /// </summary>
    /// <param name="initHeading">The initial heading to test.</param>
    /// <param name="direction">The direction to test.</param>
    /// <param name="expected">The expected result.</param>
    [TestCaseSource(nameof(Test_Rover_Turn_TestCases))]
    public void Test_Rover_Turn(CompassPoint initHeading, Instruction direction, string expected)
    {
      // Arrange
      Rover instance = new Rover(initHeading);

      // Act
      instance.Turn(direction);

      // Assert
      Assert.That(instance.ToString(), Is.EqualTo(expected));
    }
  }
}