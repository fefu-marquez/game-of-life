using System.Collections.Generic;
using System;
using UnityEngine;

public class Automaton
{
    //private List<Rule> rules;
    private JohnConwaysGameOfLife rules;
    private Board board;
    private Board newBoard;

    public Automaton()
    {
        this.rules = new JohnConwaysGameOfLife();
        this.rules.InitializeRules();
        this.rules.Sort();

        //var i = -1;
        //foreach (Rule r in rules)
        //{
        //    i++;
        //    if (i % 10 != 0) continue;
        //    r.PrintRule();
        //}

        this.board = new Board(new byte[,]
            {
                { 0,0,1,0,0,0,0,0,0,0,0,0,0 },
                { 1,0,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,1,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0 },
            });


        //Rule rule = new Rule(this.board.GetCellWithNeighbours(1, 1), true);
        //rule.PrintRule();
    }

    public void Step()
    {
        this.newBoard = new Board(new byte[board.board.GetLength(0), board.board.GetLength(1)]);

        for (int i = 0; i < board.board.GetLength(0); i++)
        {
            for (int j = 0; j < board.board.GetLength(1); j++)
            {
                UpdateCell(i, j);
            }
        }

        this.board = this.newBoard;
    }

    public void UpdateCell(int row, int column)
    {
        // Get cell state
        Rule rule = new Rule(this.board.GetCellWithNeighbours(row, column), true);

        // Check if state is in rules
        var i = rules.Find(rule);
        if (i == -1) throw new Exception("Did not found rule!");

        // Update cell
        this.newBoard.UpdateCell(row, column, rules[i].CellNextState);
    }

    public void PrintBoard()
    {
        this.board.PrintBoard();
    }
}