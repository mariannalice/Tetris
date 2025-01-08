namespace Tetris.Models
{
  public class Board
  {
    public int [,] Grid { get; private set; }
    public int Width { get; }
    public int Height { get; }

    public Board (int width, int height)
    {
        Width = width; 
        Height = height;
        Grid = new int[height, width];
    }

    public void ClearRow(int row)
    {
        for (int col = 0; col < Width; col++)
        {
          Grid [row, col] = 0;
        }
    }

    public bool IsRowFull(int row)
    {
      for (int col = 0; col < Width; col++)
      {
        if (Grid[row, col] == 0)
        {
          return false;
        }
      }
      return true;
    }

    public void ShiftRowsDown(int fromRow)
    {
      for (int row = fromRow; row > 0; row--)
      {
        for (int col = 0; col < Width; col++)
        {
          Grid[row, col] = Grid[row - 1, col];
        }
      }
      for (int col = 0; col < Width; col++)
      {
        Grid[0, col] = 0;
      }
    }
  }
}