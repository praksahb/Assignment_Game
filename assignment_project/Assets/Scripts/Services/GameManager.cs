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

        //
        [SerializeField] private UIManager uiManager;

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
        public System.Action<string> TurnChangePlayerName;


        private void OnEnable()
        {
            uiManager.OnTurnChange += SendNameString;
        }

        private void OnDisable()
        {
            uiManager.OnTurnChange -= SendNameString;
        }

        private void Start()
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
            currentPlayerTurn = playersList[turnIndex % numOfPlayers];

            TurnChangePlayerName?.Invoke(currentPlayerTurn.PlayerModel.PlayerName);
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
            ICommand moveCommand = new MoveCommand(currentPlayerTurn, totalTiles - 1);

            for (int i = 0; i < diceValue; i++)
            {
                int tilePos = moveCommand.Execute();

                // these functions can be removed if invoking action from tile position 
                // value change in PlayerModel... would require hard ref in PController
                // or by making GameManager global access via singleton
                currentTilePosition.x = tileListController.GetTilePositionX(tilePos);
                currentPlayerTurn.MoveCurrentPosition(currentTilePosition.x);
            }

            turnIndex++;
        }

        private void SendNameString()
        {
            TurnChangePlayerName?.Invoke(playersList[turnIndex % numOfPlayers].PlayerModel.PlayerName);
        }
    }
}
