using System.Threading;
using Microsoft.AspNetCore.Routing.Tree;
using Tetris.Models;
using Tetris. Services;

namespace Tetris
{
  class Program
  {
    static void Main(string[] args)
    {
      Board board = new Board (10, 20);
      GameManager gameManager = new GameManager(board);

      bool isRunning = true;

      while (isRunning)
      {
        if (Console.KeyAvailable)
        {
          var key = Console.ReadKey(true).Key;

          switch (key)
          {
            case ConsoleKey.LeftArrow:
              gameManager.MovePiece(-1, 0);
              break; 

            case ConsoleKey.RightArrow:
              gameManager.MovePiece(1, 0);
              break;

            case ConsoleKey.DownArrow:
              if (!gameManager.MovePiece(0, 1))
                  gameManager.LockPiece();
              break;

            case ConsoleKey.Q:
              isRunning = false;
              break;
          }
        }

        gameManager.Render();

        Thread.Sleep(200);
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