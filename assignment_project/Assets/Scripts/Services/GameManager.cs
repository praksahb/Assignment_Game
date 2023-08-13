using System.Collections;
using TileGame.Player;
using TileGame.PowerCards;
using TileGame.Tiles;
using UnityEngine;

namespace TileGame
{
    // trying it without generic singleton for modularity
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerView playerRedPrefab;
        [SerializeField] private PlayerView playerBluePrefab;
        [SerializeField] private int numOfPlayers;
        [SerializeField] private TileView tilePrefab;
        [SerializeField] private float distanceBetweenTiles = 1.1f;
        [SerializeField] private int totalTiles = 10;

        [SerializeField] private UIManager uiManager;
        [SerializeField] private PowerCardsList cardList;

        // game obj private references
        private PlayerController playerRed;
        private PlayerController playerBlue;
        private TileListController tileListController;
        private PlayerController currentPlayer;

        // pvt variables to be reused
        private Vector3 currentTilePosition;
        private int playerRedPosition = 0;
        private int playerBluePosition;
        private int turnIndex = 0;
        private PlayerController[] playersList;

        //Actions 
        public System.Action<int> RolledDice;
        public System.Action<string, string> TurnChangeUpdates;
        public System.Action<PowerCardType> ActivatePower;

        private void OnEnable()
        {
            ActivatePower += ActivatePowerCard;
        }

        private void OnDisable()
        {
            ActivatePower -= ActivatePowerCard;
        }

        private void Start()
        {
            playerBluePosition = totalTiles - 1;
            playersList = new PlayerController[numOfPlayers];
            InitializeGame();
        }

        private void InitializeGame()
        {
            // instantiate tiles list manager
            tileListController = new TileListController(totalTiles, tilePrefab, distanceBetweenTiles);

            // instantiate player objs
            InitializePlayers();
        }

        private void InitializePlayers()
        {
            // red is on left start
            PlayerModel playerRedModel = new PlayerModel(PlayerType.Red, playerRedPosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerRedModel.TilePosition);
            playerRed = new PlayerController(playerRedModel, playerRedPrefab, currentTilePosition.x, uiManager);
            playersList[turnIndex++] = playerRed;
            // blue is on right end
            PlayerModel playerBlueModel = new PlayerModel(PlayerType.Blue, playerBluePosition);
            currentTilePosition.x = tileListController.GetTilePositionX(playerBlueModel.TilePosition);
            playerBlue = new PlayerController(playerBlueModel, playerBluePrefab, currentTilePosition.x, uiManager);
            playersList[turnIndex++] = playerBlue;

            // reset turnindex
            turnIndex = 0;

            ChangeTurnUI();
        }

        private IPowersInterface GetPowerCard(PowerCardType cardType)
        {
            for (int i = 0; i < cardList.powerCardsList.Length; i++)
            {
                if (cardType == cardList.powerCardsList[i].cardType)
                {
                    return (IPowersInterface)cardList.powerCardsList[i];
                }
            }
            return null;
        }

        private void ActivatePowerCard(PowerCardType powerCardType)
        {
            IPowersInterface powerCardStrategy = GetPowerCard(powerCardType);
            if (powerCardStrategy == null)
            {
                Debug.LogError("PowerCard type invalid");
                return;
            }

            PowersClient powerCardClient = new PowersClient(playersList, (turnIndex % numOfPlayers), powerCardStrategy);

            powerCardClient.Execute();
        }

        private IEnumerator PerformMoveCoroutine(int diceValue)
        {
            ICommand moveCommand = new MoveCommand(currentPlayer, totalTiles - 1);

            for (int i = 0; i < diceValue; i++)
            {
                if (currentPlayer.PlayerModel.IsMovingBackwards && (currentPlayer.PlayerModel.TilePosition == 0 || currentPlayer.PlayerModel.TilePosition == totalTiles - 1))
                {
                    break;
                }
                int tilePos = moveCommand.Execute();
                currentTilePosition.x = tileListController.GetTilePositionX(tilePos);
                currentPlayer.PlayerView.MoveCurrentPosition(currentTilePosition.x);

                yield return new WaitForSeconds(0.5f);
            }
        }

        public IEnumerator PlayTurn()
        {
            int diceValue = Random.Range(1, 7);
            RolledDice?.Invoke(diceValue);

            if (currentPlayer.PlayerModel.ActivePower != PowerCardType.None)
            {
                currentPlayer.PlayerModel.PowerDurationTurns--;
            }

            if (currentPlayer.PlayerModel.CurrentStatus != Status.Imprisoned)
            {
                // MOVE function
                yield return StartCoroutine(PerformMoveCoroutine(diceValue));
            }

            turnIndex++;

            if (currentPlayer.PlayerModel.CurrentStatus != Status.None)
            {
                currentPlayer.PlayerModel.TurnsEffected--;
            }
        }

        public void ChangeTurnUI()
        {
            currentPlayer = playersList[turnIndex % numOfPlayers];

            currentPlayer.PlayerView.TurnChangeUpdateUI();
        }
    }
}
