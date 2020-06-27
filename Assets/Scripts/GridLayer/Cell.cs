using UnityEngine;

namespace GridLayer
{
    public class Cell
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Sprite sprite;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
