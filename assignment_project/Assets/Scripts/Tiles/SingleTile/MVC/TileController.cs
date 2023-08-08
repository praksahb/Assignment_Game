using UnityEngine;

namespace TileGame.Tiles
{
    public class TileController
    {
        public TileModel TileModel { get; }
        public TileView TileView { get; }

        public TileController(TileModel tileModel, TileView tileView, Vector3 tilePosition)
        {
            TileModel = tileModel;
            TileView = Object.Instantiate(tileView, tilePosition, Quaternion.identity);

            TileView.TileController = this;
        }
    }
}
