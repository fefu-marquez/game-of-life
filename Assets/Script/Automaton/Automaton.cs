using System.Collections.Generic;
using System;
using UnityEngine;

public class Automaton
{
    //private List<Rule> rules;
    private Dictionary<string, Rule> rules;
    private Board board;
    private Board newBoard;
    private StepTimer timer;

    public Automaton()
    {
        this.rules = JohnConwaysGameOfLife.InitializeRules();

        //this.rules.InitializeRules();
        //this.rules.Sort();

        //var i = -1;
        //foreach (Rule r in rules)
        //{
        //    i++;
        //    if (i % 10 != 0) continue;
        //    r.PrintRule();
        //}

        this.board = new Board(new byte[32, 48]);
        this.board.board[0, 2] = 1;
        this.board.board[1, 0] = 1;
        this.board.board[1, 2] = 1;
        this.board.board[2, 1] = 1;
        this.board.board[2, 2] = 1;
        this.newBoard = new Board(new byte[board.board.GetLength(0), board.board.GetLength(1)]);

        timer = new StepTimer(100);

        var timer2 = new StepTimer(1000);
        for (int i = 0; i < 1000; i++)
        {
            timer2.StartStep();
            var r = new Rule(this.board.GetCellWithNeighbours(3, 4), true);
            rules.ContainsKey(r.Key);
            timer2.EndStep();
        }
        //Rule rule = new Rule(this.board.GetCellWithNeighbours(1, 1), true);
        //rule.PrintRule();
    }

    /// <summary>
    /// Old function vs New function time comparison:
    /// ----------------------------------------------------
    /// Parameters:
    /// - 32x48 grid
    /// - Game of Life
    /// - Number of steps: 100
    /// - Initial setup: glider on top corner:
    ///     0 0 1 ...
    ///     1 0 1 ...
    ///     0 1 1 ...
    ///     ...
    /// ----------------------------------------------------
    /// 
    /// v1.0 Original Game of Life:
    ///   Time: 0,09 ms
    /// 
    /// v2.0 Generalization for Langton's Loop:
    ///   Time: 2,14 ms
    /// 
    /// v2.1 Remove redundant rules (rules that don't
    /// change the board):
    ///   Time: 1,83 ms
    ///   
    /// v2.2 Use Dictionaries for rules:
    ///   Time: 1,12 ms
    ///   
    ///   There isn't much room for improvement. The two functions
    ///   that are "slow" are:
    ///   - The Rule constructor and
    ///   - The GetCell method.
    ///   The only way I can think of making it faster is by linking
    ///   every Cell directly to its neighbours, before hand. So instead
    ///   of doing every check to know if this is and edge of the board, 
    ///   we could access the neighbours directly from the cell.
    ///   
    ///   For now I'm focusing on readability.
    /// </summary>
    public void Step()
    {
        for (int i = 0; i < board.board.GetLength(0); i++)
        {
            for (int j = 0; j < board.board.GetLength(1); j++)
            {
                UpdateCell(i, j);
            }
        }
        var aux = this.board;
        this.board = this.newBoard;
        this.newBoard = aux;
    }

    public void DebugStep()
    {
        timer.StartStep();
        Step();
        timer.EndStep();
    }

    public void UpdateCell(int row, int column)
    {
        // Get cell state
        Rule rule = new Rule(this.board.GetCellWithNeighbours(row, column), true);

        // Check if state is in rules
        //var i = rules.ContainsKey(rule);
        if (rules.ContainsKey(rule.Key)) 
        {
            // Update cell
            this.newBoard.UpdateCell(row, column, rules[rule.Key].CellNextState);
            return;
        }

        // Set value to previous one
        this.newBoard.UpdateCell(row, column, this.board.board[row,column]);
    }

    public void PrintBoard()
    {
        this.board.PrintBoard();
    }
}