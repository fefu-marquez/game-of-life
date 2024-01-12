using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Board
{
    private bool loopBoard = false;
    public byte[,] board;

    public Board(int height, int width)
    {
        this.board = new byte[height, width];
    }

    public Board(byte[,] boardState)
    {
        this.board = boardState;
    }

    /// <summary>
    /// Gets a cell and its neighbours.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="useEightNeighbours"></param>
    /// <returns></returns>
    public byte[] GetCellWithNeighbours(int row, int column, bool useEightNeighbours = true)
    {
        if (!useEightNeighbours)
        {
            return new byte[] {
                // Fill unused bytes with 0s
                0, 
                GetCell(row, column - 1),
                GetCell(row + 1, column),
                GetCell(row, column + 1),
                GetCell(row - 1, column),
                GetCell(row, column),
                0, 0,
                // Fill second long
                0,0,0,0,0,0,0,0,
            };
        }

        return new byte[] {
            // Byte for next state
            0,
            GetCell(row - 1, column - 1),
            GetCell(row, column - 1),
            GetCell(row + 1, column - 1),
            GetCell(row + 1, column),
            GetCell(row + 1, column + 1),
            GetCell(row, column + 1),
            GetCell(row - 1, column + 1),
            GetCell(row - 1, column),
            GetCell(row, column),

            // Fill unused bytes with 0s
            0, 0, 0, 0, 0, 0, 
        };
    }

    internal void UpdateCell(int row, int column, byte cellNextState)
    {
        board[row, column] = cellNextState;
    }

    private byte GetCell(int row, int column)
    {
        if (loopBoard)
        {
            var newRow = row % board.GetLength(0);
            var newColumn = column % board.GetLength(1);
            
            return board[newRow, newColumn];
        }
        else
        {
            if (row < 0 || row >= board.GetLength(0) || column < 0 || column >= board.GetLength(1))
            {
                return 0;
            } else
            {
                return board[row, column];
            }
        }
    }

    public void PrintBoard()
    {
        string output = "";
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j < board.GetLength(1); j++)
            {
                output += board[i, j] + " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }
}
