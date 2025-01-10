using System.Data;
using Tetris.Models;

namespace Tetris.Services
{
  public class GameManager
  {
    public int Score { get; private set; } = 0;
    public int Level {get; private set; } = 1;
    private Board _board;
    private Piece _currentPiece;

    public GameManager(Board board)
    {
      _board = board;
      SpawnNewPiece();
    }
    
    private void UpdateLevel()
    {
      Level = Score / 400 + 1;
      Console.WriteLine($"Level: {Level}");
    }
    public void SpawnNewPiece()
    {
      Random random = new Random();

      int shapeIndex = random.Next(0, Tetrominoes.Shapes.Length);
      int[,] shape = Tetrominoes.Shapes[shapeIndex];

      _currentPiece = new Piece(shape)
      {
        X = _board.Width / 2 - shape.GetLength(1) / 2,
        Y = 0
      };
    }

    public void RotatePiece()
    {
      Console.WriteLine("Attempting to rotate...");
      Console.WriteLine("Before rotation:");
      PrintShape(_currentPiece.Shape);

      var originalShape = (int[,])_currentPiece.Shape.Clone();
      _currentPiece.Rotate();

      Console.WriteLine("After rotation:");
      PrintShape(_currentPiece.Shape);

      if (IsCollision(_currentPiece.X, _currentPiece.Y, _currentPiece.Shape))
      {
        Console.WriteLine("Collision detected! Reverting rotation.");
        _currentPiece.Shape = originalShape;
      }
    }

    private void PrintShape(int[,] shape)
{
    for (int row = 0; row < shape.GetLength(0); row++)
    {
        for (int col = 0; col < shape.GetLength(1); col++)
        {
            Console.Write(shape[row, col] + " ");
        }
        Console.WriteLine();
    }
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

      ClearFullRows();
      SpawnNewPiece();

      if (IsCollision(_currentPiece.X, _currentPiece.Y, _currentPiece.Shape))
      {
        Console.WriteLine("Gameover!");
        Environment.Exit(0);
      }
    }

    public void ClearFullRows()
    {
      int rowesCleared = 0; 

      for (int row = 0; row < _board.Height; row++)
      {
        bool isFullRow = true;

        for (int col = 0; col < _board.Width; col++)
        {
          if (_board.Grid[row, col] == 0)
          {
            isFullRow = false;
            break;
          }
        }

        if (isFullRow)
        {
          ClearRow(row);
          ShiftRowsDown(row);
          row--;
          rowesCleared++;
        }
      }

      if (rowesCleared > 0)
      {
        Console.WriteLine($"{rowesCleared} row(s) cleared! Totalt score: {Score}!");
      }
    }

    private void ClearRow(int row)
    {
      for (int col = 0; col < _board.Width; col++)
      {
        _board.Grid[row, col] = 0;
      }

      Score += 100;
      UpdateLevel();
      Console.WriteLine($" Row cleard! Score: {Score}");
    }

    private void ShiftRowsDown(int clearedRow)
    {
      for (int row = clearedRow; row > 0; row--)
      {
        for (int col = 0; col < _board.Width; col++)
        {
          _board.Grid[row, col] = _board.Grid[row - 1, col];
        }
      }

      for (int col = 0; col < _board.Width; col++)
      {
        _board.Grid[0, col] = 0;
      }
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

      Console.WriteLine();
      Console.WriteLine($"Score: {Score}");
      Console.WriteLine($"Level: {Level}");
    }
  }
}