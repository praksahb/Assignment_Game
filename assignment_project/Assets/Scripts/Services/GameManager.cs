using TileGame.Player;
using TileGame.Tiles;
using UnityEngine;

namespace TileGame.MainGame
{
    // trying it without generic singleton for modularity

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerView playerRedPrefab;
        [SerializeField]
        private PlayerView playerBluePrefab;
        [SerializeField]
        private TileView tilePrefab;
        [SerializeField]
        private float distanceBetweenTiles = 1.1f;
        [SerializeField]
        private int totalTiles = 10;

        // game obj private references
        private PlayerController playerRed;
        private PlayerController playerBlue;
        private TileListController tileListController;


        // pvt variables to be reused
        private Vector3 currentTilePosition;
        private int playerRedPosition = 0;
        private int playerBluePosition;

        private void Awake()
        {
            playerBluePosition = totalTiles - 1;
            InitializeGame();
        }

        private void InitializeGame()
        {
            // instantiate tiles
            tileListController = new TileListController(totalTiles, tilePrefab, distanceBetweenTiles);

            // instantiate player objs

            // red is on left start
            PlayerModel playerRedModel = new PlayerModel(PlayerType.Red, playerRedPosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerRedModel.TilePosition);
            playerRed = new PlayerController(playerRedModel, playerRedPrefab, currentTilePosition.x);

            // blue is on right end
            PlayerModel playerBlueModel = new PlayerModel(PlayerType.Blue, playerBluePosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerBlueModel.TilePosition);
            playerBlue = new PlayerController(playerBlueModel, playerBluePrefab, currentTilePosition.x);
        }
    }
}
