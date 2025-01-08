using Tetris.Models;

namespace Tetris
{
  class Program
  {
    static void Main(string[] args)
    {
      Board board = new Board (10, 20);

      int[,] tShape = new int[,]
      {
        { 0, 1, 0 },
        { 1, 1, 1 }
      };

      Piece tPiece = new Piece(tShape)
      {
        X = 3,
        Y = 0
      };

      for (int row = 0; row < tPiece.Shape.GetLength(0); row++)
      {
        for (int col = 0; col < tPiece.Shape.GetLength(1); col++)
        {
          if (tPiece.Shape[row,col] == 1)
          {
            board.Grid[tPiece.Y + row, tPiece.X +col] = 1;
          }
        }
      }


      // Displaying the board
      for (int row = 0; row < board.Height; row++)
      {
        for (int col = 0; col < board.Width; col++)
        {
          Console.Write(board.Grid[row, col] == 1 ? "[]" : " .");
        }
        Console.WriteLine();
      }
    }
  }
}