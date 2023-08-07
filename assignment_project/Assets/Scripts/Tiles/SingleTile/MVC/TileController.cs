using UnityEngine;

namespace TileGame.Tiles
{
    public class TileController
    {
        public TileModel TileModel { get; }
        public TileView TileView { get; }

        public TileController(TileModel tileModel, TileView tileView)
        {
            TileModel = tileModel;
            TileView = Object.Instantiate(tileView, new Vector3(TileModel.TileNumber * 1.25f, 0, 0), Quaternion.identity);

            TileView.TileController = this;
        }
    }
}
