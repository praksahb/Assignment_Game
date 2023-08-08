using System;
using UnityEngine;

namespace TileGame.Tiles
{
    public class TileListController
    {
        private TileController[] tileList;
        private TileView tilePrefab;


        public TileListController(int length, TileView tilePrefab, float distanceBetweenTiles)
        {
            tileList = new TileController[length];
            this.tilePrefab = tilePrefab;

            CreateTilesHorizontal(length, distanceBetweenTiles);
        }

        public float GetTilePositionX(int index)
        {
            if (index < 0 || index > tileList.Length)
            {
                throw new Exception();
            }

            return tileList[index].TileView.transform.position.x;
        }



        private void CreateTilesHorizontal(int length, float distanceBetweenTiles)
        {
            for (int i = 0; i < length; i++)
            {
                TileModel tileModel = new TileModel(i);
                Vector3 tilePosition = new Vector3(tileModel.TileNumber * distanceBetweenTiles, 0, 0);
                TileController tileController = new TileController(tileModel, tilePrefab, tilePosition);
                tileList[i] = tileController;
            }
        }
    }
}