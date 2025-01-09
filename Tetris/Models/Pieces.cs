namespace Tetris.Models
{
  public class Piece
  {
    public int [,] Shape { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Piece(int[,] shape)
    {
      Shape = shape;
      X = 0;
      Y = 0;
    }

    public void Move(int dx, int dy)
    {
      X += dx;
      Y += dy;
    }

    public void Rotate()
    {
      int rows = Shape.GetLength(0);
      int cols = Shape.GetLength(1);
      int [,] rotated = new int[cols, rows];

      for (int row = 0; row < rows; row++)
      {
        for (int col = 0; col < cols; col++)
        {
          rotated[col, rows - 1 - row] = Shape[row, col];
        }
      }

      Shape = rotated;
    }
  }

  public static class Tetrominoes
  {
    public static int [][,] Shapes = new int[][,]
    {
      // O shape
      new int[,]
      {
        {1, 1},
        {1, 1}
      },

      // I shape
      new int[,]
      {
        {1, 1, 1, 1}
      },

      // T shape
      new int[,]
      {
        {0, 1, 0},
        {1, 1, 1}
      },

      // S shape
      new int[,]
      {
        {0, 1, 1},
        {1, 1, 0}
      },

      // Z shape
      new int[,]
      {
        {1, 1, 0},
        {0, 1, 1}
      },

      // L shape
      new int[,]
      {
        {1, 0},
        {1, 0},
        {1, 1}
      },

      // J shape
      new int[,]
      {
        {0, 1},
        {0, 1},
        {1, 1}
      }
    };
  }
}