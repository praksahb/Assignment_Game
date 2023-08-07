namespace TileGame.Tiles
{
    public class TileModel
    {
        private int tileNumber;
        public int TileNumber { get { return tileNumber; } set { tileNumber = value; } }

        public TileModel(int index)
        {
            TileNumber = index;
        }
    }
}
