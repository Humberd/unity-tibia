using System;
using UnityEngine;

namespace GridLayer
{
    public class GridController : MonoBehaviour
    {
        public Grid<Item> Grid = new Grid<Item>(5, 2, 1f, Vector3.zero);

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // _grid.SetValue(GetMouseToWorldPosition(), 2);
            }

            // Grid.DrawEdges();
        }

        private Vector3 GetMouseToWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnDrawGizmos()
        {
            Grid.DrawEdges();
        }
    }


    public class Item
    {

    }

}
