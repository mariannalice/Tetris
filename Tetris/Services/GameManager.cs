using Tetris.Models;

namespace Tetris.Services
{
  public class GameManager
  {
    private Board _board;
    private Piece _currentPiece;

    public GameManager(Board board)
    {
      _board = board;
      SpawnNewPiece();
    }

    public void SpawnNewPiece()
    {
      int[,] tShape = new int[,]
      {
        { 0, 1, 0 },
        { 1, 1, 1 }
      };

      _currentPiece = new Piece(tShape)
      {
        X = _board.Width / 2 - 1,
        Y = 0
      };
    }

    public bool MovePiece(int dx, int dy)
    {
      int newX = _currentPiece.X + dx;
      int newY = _currentPiece.Y + dy;

      if(IsCollision(newX, newY, _currentPiece.Shape))
        return false;

      _currentPiece.X = newX;
      _currentPiece.Y = newY;
      return true; 
    }

    public void LockPiece()
    {
      for (int row = 0; row < _currentPiece.Shape.GetLength(0); row++)
      {
        for (int col = 0; col < _currentPiece.Shape.GetLength(1); col++)
        {
          if(_currentPiece.Shape[row, col] == 1)
          {
            _board.Grid[_currentPiece.Y + row, _currentPiece.X + col] = 1;
          }
        }
      }

      SpawnNewPiece();
    }

    private bool IsCollision(int x, int y, int [,] shape)
    {
      for (int row = 0; row < shape.GetLength(0); row++)
      {
        for (int col = 0; col < shape.GetLength(1); col++)
        {
          if (shape[row, col] == 1)
          {
            int boardX = x + col;
            int boardY = y + row;

            // Checking if the piece goes out of bounds
            if (boardX < 0 || boardX >= _board.Width || boardY < 0 || boardY >= _board.Height)
            return true;

            // Checking if piece overlocks with existing blocks
            if (_board.Grid[boardY, boardX] == 1)
            return true;
          }
        }
      }
      return false;
    }

    public void Render()
    {
      Console.Clear();

      var tempGrid = (int[,])_board.Grid.Clone();

      for (int row = 0; row < _currentPiece.Shape.GetLength(0); row++)
      {
        for (int col = 0; col < _currentPiece.Shape.GetLength(1); col++)
        {
          if (_currentPiece.Shape[row, col] == 1)
          {
            tempGrid[_currentPiece.Y + row, _currentPiece.X + col] = 1;
          }
        }
      }

      for (int row = 0; row < _board.Height; row++)
      {
        for (int col = 0; col < _board.Width; col++)
        {
          Console.Write(tempGrid[row, col] == 1 ? "[]" : " .");
        }
        Console.WriteLine();
      }
    }
  }
}