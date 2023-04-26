using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private bool isAlive;
    private bool willBeAlive;
    private Button button;
    private int x;
    private int y;
    private int numberOfNeighbours;

    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int NumberOfNeighbours { get => numberOfNeighbours; set => numberOfNeighbours = value; }

    public bool WillDie
    {
        get => IsAlive && (NumberOfNeighbours != 2 && NumberOfNeighbours != 3);
    }
    public bool WillBeBorn
    {
        get => !IsAlive && (NumberOfNeighbours == 3);
    }
    public Button Button { get => button; set => button = value; }
    public bool WillBeAlive { get => willBeAlive; set => willBeAlive = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsAlive = false;
        WillBeAlive = false;
        Button = GetComponent<Button>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update() { }

    public void OnClick()
    {
        ToggleIsEnabled();
        UpdateColor();
    }

    private void ToggleIsEnabled()
    {
        IsAlive = !IsAlive;
        WillBeAlive = IsAlive;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void UpdateColor()
    {
        var colors = Button.colors;

        if (IsAlive)
        {
            colors.normalColor = Color.white;
            colors.disabledColor = Color.white;
        }
        else
        {
            colors.normalColor = Color.black;
            colors.disabledColor = Color.black;
        }

        Button.colors = colors;
    }
}
