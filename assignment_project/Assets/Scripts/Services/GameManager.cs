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

        [SerializeField] private UIManager uiManager;
        [SerializeField] private BackwardsPowerCard backwardsPowerCard;
        [SerializeField] private ImprisonPowerCard imprisonedPowerCard;

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
        private PowerCardsBase[] activePowerCard;

        //Actions 
        public System.Action<int> RolledDice;
        public System.Action<string, string> TurnChangeUpdates;
        public System.Action<PowerCardType> ActivatePower;


        private void OnEnable()
        {
            uiManager.OnTurnChange += TurnChangeEvents;
            ActivatePower += activatePowerCard;
        }

        private void OnDisable()
        {
            uiManager.OnTurnChange -= TurnChangeEvents;
            ActivatePower -= activatePowerCard;
        }

        private void Start()
        {
            playerBluePosition = totalTiles - 1;
            playersList = new PlayerController[numOfPlayers];
            activePowerCard = new PowerCardsBase[numOfPlayers];
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
            string playerName = currentPlayerTurn.PlayerModel.PlayerName;
            string powerName = "None";
            TurnChangeUpdates?.Invoke(playerName, powerName);
        }

        public void PlayTurn()
        {
            // get random dice roll value
            int diceValue = Random.Range(1, 7);
            // Invoke Action - received by UIManager
            RolledDice?.Invoke(diceValue);

            // get current player
            // is already updated in SendNameString function
            //currentPlayerTurn = playersList[turnIndex % numOfPlayers];

            if (activePowerCard[turnIndex % numOfPlayers] != null)
            {
                activePowerCard[turnIndex % numOfPlayers].ApplyEffect(currentPlayerTurn);
                activePowerCard[turnIndex % numOfPlayers] = null;
            }

            if (!currentPlayerTurn.PlayerModel.IsImprisoned)
            {
                // MOVE function
                ICommand moveCommand = new MoveCommand(currentPlayerTurn, totalTiles - 1);

                bool isBackwardsActive = currentPlayerTurn.PlayerModel.IsMovingBackwards;

                for (int i = 0; i < diceValue; i++)
                {
                    int tilePos = moveCommand.Execute();
                    currentTilePosition.x = tileListController.GetTilePositionX(tilePos);
                    currentPlayerTurn.MoveCurrentPosition(currentTilePosition.x);
                    if (isBackwardsActive && (tilePos == 0 || tilePos == totalTiles - 1))
                    {
                        break;
                    }
                }

                // reset backwards 
                if (isBackwardsActive)
                {
                    currentPlayerTurn.PlayerModel.IsMovingBackwards = false;
                }
            }

            turnIndex++;
            // reduce value for imprisoned turns
            if (currentPlayerTurn.PlayerModel.IsImprisoned)
            {
                currentPlayerTurn.PlayerModel.ImprisonedTurns--;
                if (currentPlayerTurn.PlayerModel.IsImprisoned)
                {
                    // invoke events for setting off buttons for play cards
                    uiManager.SwitchBackwardsPowerCard?.Invoke(true);
                    uiManager.SwitchImprisonPowerCard?.Invoke(true);
                }
            }
            else
            {
                uiManager.SwitchBackwardsPowerCard?.Invoke(false);
                uiManager.SwitchImprisonPowerCard?.Invoke(false);
            }
        }

        private void TurnChangeEvents()
        {
            currentPlayerTurn = playersList[turnIndex % numOfPlayers];
            string playerName = currentPlayerTurn.PlayerModel.PlayerName;
            string powerName;
            if (activePowerCard[turnIndex % numOfPlayers] == null)
            {
                powerName = "None";
                if (currentPlayerTurn.PlayerModel.IsImprisoned == false)
                {
                    uiManager.UpdatePlayButtonText?.Invoke(false);
                }
                else
                {
                    uiManager.UpdatePlayButtonText?.Invoke(true);
                    uiManager.SwitchBackwardsPowerCard?.Invoke(true);
                    uiManager.SwitchImprisonPowerCard?.Invoke(true);
                }
            }
            else
            {
                powerName = activePowerCard[turnIndex % numOfPlayers].cardName;
                if (activePowerCard[turnIndex % numOfPlayers].cardType == PowerCardType.Imprison)
                {
                    uiManager.UpdatePlayButtonText?.Invoke(true);
                }
            }
            TurnChangeUpdates?.Invoke(playerName, powerName);
        }

        private void activatePowerCard(PowerCardType powerCardType)
        {
            //Debug.Log("curr player: " + currentPlayerTurn.PlayerModel.PlayerName);

            if (powerCardType == PowerCardType.MoveBackward)
            {
                activePowerCard[(turnIndex + 1) % numOfPlayers] = backwardsPowerCard;
            }
            if (powerCardType == PowerCardType.Imprison)
            {
                activePowerCard[(turnIndex + 1) % numOfPlayers] = imprisonedPowerCard;
            }
        }
    }
}
