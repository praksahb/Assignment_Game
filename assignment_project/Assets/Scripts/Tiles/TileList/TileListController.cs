namespace TileGame.Tiles
{
    public class TileListController
    {
        private TileController[] tileList;

        public TileListController(int length, TileView tilePrefab)
        {
            tileList = new TileController[length];

            for (int i = 0; i < length; i++)
            {
                TileModel tileModel = new TileModel(i);
                TileController tileController = new TileController(tileModel, tilePrefab);
                tileList[i] = tileController;
            }
        }
    }
}