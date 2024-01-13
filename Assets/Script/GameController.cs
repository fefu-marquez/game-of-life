using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gridCellPrefab;
    public GameObject gridGameObject;
    public float cellSize;
    public float gridRows;
    public float gridColumns;
    public Vector3 offset;
    private List<List<Cell>> board;
    private bool isPlaying;
    private bool inTurn;
    Automaton automaton;
    StepTimer timer;
    public List<List<Cell>> Board { get => board; set => board = value; }

    // Start is called before the first frame update
    void Start()
    {
        automaton = new Automaton();
        //automaton.PrintBoard();
        
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();
        //automaton.Step();
        //automaton.PrintBoard();

        //isPlaying = false;
        //inTurn = false;
        //CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        automaton.DebugStep();
        //automaton.PrintBoard();
        //if (isPlaying && !inTurn)
        //{
        //    StartCoroutine(HandleTurns());
        //}
    }

    private IEnumerator HandleTurns()
    {
        inTurn = true;

        PlayOneTurn();
        UpdateAllButtons();
        yield return new WaitForSeconds(0.1f);

        inTurn = false;
    }

    private void CreateGrid()
    {
        Board = new List<List<Cell>>();

        for (int i = 0; i < gridRows; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < gridColumns; j ++)
            {
                GameObject instance = Instantiate(gridCellPrefab, gridGameObject.transform);
                Cell instanceScript = instance.GetComponent<Cell>();
                instanceScript.Y = i;
                instanceScript.X = j;
                row.Add(instanceScript);
                instance.transform.localPosition = new Vector3(j * cellSize, i * cellSize, 0) + offset;
            }
            Board.Add(row);
        }
    }

    public void OnStartButtonPressed()
    {
        DisableAllButtons();
        isPlaying = true;
    }

    private void DisableAllButtons()
    {
        Button[] buttons = GameObject.FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    private void UpdateAllButtons()
    {
        foreach (List<Cell> row in Board)
        {
            foreach (Cell cell in row)
            {
                cell.IsAlive = cell.WillBeAlive;
                cell.UpdateColor();
            }
        }
    }

    private void PlayOneTurn()
    {

        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridColumns; j++)
            {
                CalculateNumberOfAliveNeighbours(Board[i][j]);
                if (Board[i][j].WillDie)
                {
                    Board[i][j].WillBeAlive = false;
                }

                if (Board[i][j].WillBeBorn)
                {
                    Board[i][j].WillBeAlive = true;
                }
            }
        }
    }

    public Cell GetCellInPosition(int x, int y)
    {
        return Board[y][x];
    }

    public void CalculateNumberOfAliveNeighbours(Cell cell)
    {
        int aliveNeighbours = 0;

        if (cell.Y > 0 && Board[cell.Y - 1][cell.X].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.Y < gridRows-1 && Board[cell.Y + 1][cell.X].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.X > 0 && Board[cell.Y][cell.X - 1].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.X < gridColumns-1 && Board[cell.Y][cell.X + 1].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.Y > 0 && cell.X > 0 && Board[cell.Y - 1][cell.X - 1].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.Y > 0 && cell.X < gridColumns-1 && Board[cell.Y - 1][cell.X + 1].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.Y < gridRows-1 && cell.X > 0 && Board[cell.Y + 1][cell.X - 1].IsAlive)
        {
            aliveNeighbours++;
        }

        if (cell.Y < gridRows-1 && cell.X < gridColumns-1 && Board[cell.Y + 1][cell.X + 1].IsAlive)
        {
            aliveNeighbours++;
        }

        cell.NumberOfNeighbours = aliveNeighbours;
    }
}
