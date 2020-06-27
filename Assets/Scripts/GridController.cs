using System;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    private Grid _grid;
    private void Start()
    {
        _grid = new Grid(4, 2, 10f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetValue(GetMouseToWorldPosition(), 2);
        }
    }

    private Vector3 GetMouseToWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
