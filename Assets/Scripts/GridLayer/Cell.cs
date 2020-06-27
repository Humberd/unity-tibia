namespace GridLayer
{
    public class Cell<TInnerType>
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
