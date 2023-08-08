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
        private int numOfPlayers;
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
        private PlayerController currentPlayerTurn;

        // pvt variables to be reused
        private Vector3 currentTilePosition;
        private int playerRedPosition = 0;
        private int playerBluePosition;
        private int turnIndex = 0;
        private PlayerController[] playersList;

        //Actions 
        public System.Action<int> RolledDice;


        private void Awake()
        {
            playerBluePosition = totalTiles - 1;
            playersList = new PlayerController[numOfPlayers];
            InitializeGame();
        }

        private void InitializeGame()
        {
            // instantiate tiles
            tileListController = new TileListController(totalTiles, tilePrefab, distanceBetweenTiles);

            // instantiate player objs
            InitializePlayers();
        }

        private void InitializePlayers()
        {
            // red is on left start
            PlayerModel playerRedModel = new PlayerModel(PlayerType.Red, playerRedPosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerRedModel.TilePosition);
            playerRed = new PlayerController(playerRedModel, playerRedPrefab, currentTilePosition.x);
            playersList[turnIndex++] = playerRed;
            // blue is on right end
            PlayerModel playerBlueModel = new PlayerModel(PlayerType.Blue, playerBluePosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerBlueModel.TilePosition);
            playerBlue = new PlayerController(playerBlueModel, playerBluePrefab, currentTilePosition.x);
            playersList[turnIndex++] = playerBlue;

            // reset turnindex
            turnIndex = 0;
        }

        public void PlayTurn()
        {
            // get random dice roll value
            int diceValue = Random.Range(1, 7);
            // Invoke Action - received by UIManager
            RolledDice?.Invoke(diceValue);

            // get current player
            currentPlayerTurn = playersList[turnIndex % numOfPlayers];

            // MOVE function
            // change tilePos value in PM
            int currentTilePos = currentPlayerTurn.PlayerModel.TilePosition;
            int moveDirection = (int)currentPlayerTurn.PlayerModel.MoveDirection;
            int newTilePos = currentTilePos + (moveDirection * diceValue);
            if (newTilePos > totalTiles - 1)
            {
                moveDirection *= -1;
                currentPlayerTurn.PlayerModel.MoveDirection = (PlayerMoveDirection)moveDirection;
                int offset = newTilePos - (totalTiles - 1);
                newTilePos = totalTiles - 1 - offset;
            }
            if (newTilePos < 0)
            {
                moveDirection *= -1;
                currentPlayerTurn.PlayerModel.MoveDirection = (PlayerMoveDirection)moveDirection;
                int offset = newTilePos;
                newTilePos = 0 + offset * -1;
            }

            currentPlayerTurn.PlayerModel.TilePosition = newTilePos;
            // get value of tile from tile list
            currentTilePosition.x = tileListController.GetTilePositionX(currentPlayerTurn.PlayerModel.TilePosition);
            // move transform value
            currentPlayerTurn.MoveCurrentPosition(currentTilePosition.x);

            turnIndex++;

        }
    }
}
