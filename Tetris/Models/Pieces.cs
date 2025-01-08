namespace Tetris.Models
{
  public class Piece
  {
    public int [,] Shape { get; private set; }
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
  }
}