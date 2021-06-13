namespace SDK.Models
{
  /// <summary>
  /// A class for creating a two-dimensional grid.
  /// </summary>
  public class Grid2D
  {
    /// <summary>
    /// The X (East) boundary of the grid.
    /// </summary>
    public readonly uint BoundaryX;

    /// <summary>
    /// The Y (North) boundary of the grid.
    /// </summary>
    public readonly uint BoundaryY;

    /// <summary>
    /// Constructs the grid.
    /// </summary>
    /// <param name="boundaryX">The X boundary.</param>
    /// <param name="boundaryY">The Y boundary.</param>
    public Grid2D(uint boundaryX, uint boundaryY)
    {
      BoundaryX = boundaryX;
      BoundaryY = boundaryY;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }

      return obj is Grid2D other &&
        other.BoundaryX == BoundaryX &&
        other.BoundaryY == BoundaryY;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return (int)(BoundaryX ^ BoundaryY);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return $"{BoundaryX}, {BoundaryY}";
    }
  }
}
