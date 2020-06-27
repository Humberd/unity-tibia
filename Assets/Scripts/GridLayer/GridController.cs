using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GridLayer
{
    public class GridController : MonoBehaviour
    {
        public GameObject prefab;
        public Grid Grid = new Grid(5, 2, 1f, Vector3.zero);

        private void Start()
        {
            instantiateObject();
        }

        public void instantiateObject()
        {
            foreach (var cell in Grid.GridArray)
            {
                var instance = Instantiate(prefab, new Vector3(cell.x + Grid.CellSize/2, cell.y + Grid.CellSize/2), Quaternion.identity);
                DontDestroyOnLoad(instance);
            }
        }

        private void Update()
        {
            foreach (var cell in Grid.GridArray)
            {

            }

            if (Input.GetMouseButtonDown(0))
            {
                // _grid.SetValue(GetMouseToWorldPosition(), 2);
            }
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

}
