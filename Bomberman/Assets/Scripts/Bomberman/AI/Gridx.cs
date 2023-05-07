using System;
using UnityEngine;


public class Gridx {

    // public static event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

    public delegate void OnGridValueChanged(float x, float y, int value);
    public static OnGridValueChanged onGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

    private int width;
    private int length;
    private float cellSize = 1f;
    private Vector3 originPosition = Vector3.zero;
    private int[,] gridArray;

    public Gridx(int[,] grid)
    {
        onGridValueChanged += SetValue;
        gridArray = grid;
        length = gridArray.GetLength(0);
        width = gridArray.GetLength(1);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetLength()
    {
        return length;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetValue(float xf, float yf, int value)
    {
        var x = Mathf.FloorToInt(xf);
        var y = Mathf.FloorToInt(yf);
        Debug.Log(x + " : " + y + " - " + value);
        if (x >= 0 && y >= 0 && x < length && y < width) {
            gridArray[x, y] = value;
            // if (OnGridValueChanged != null) 
            //     OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, int value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < length) {
            return gridArray[x, y];
        } else {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public int[,] GetGrid()
    {
        return gridArray;
    }
}
