using TileGame.Player;
using TileGame.Tiles;
using UnityEngine;

namespace TileGame.MainGame
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
        [SerializeField] private BackwardsPowerCard backwardsPowerCard;
        [SerializeField] private ImprisonPowerCard imprisonedPowerCard;
        //[SerializeField] private PowerCardsList cardList;

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

        //private PowerCardsBase[] activePowerCard;

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

        public void PlayTurn()
        {
            // get random dice roll value
            int diceValue = Random.Range(1, 7);
            // Invoke Action - received by UIManager
            RolledDice?.Invoke(diceValue);

            if (currentPlayer.PlayerModel.ActivePower != PowerCardType.None)
            {
                currentPlayer.PlayerModel.PowerDurationTurns--;
            }

            if (currentPlayer.PlayerModel.CurrentStatus != Status.Imprisoned)
            {
                // MOVE function
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
                }
            }

            turnIndex++;
            if (currentPlayer.PlayerModel.CurrentStatus != Status.None)
            {
                currentPlayer.PlayerModel.TurnsEffected--;
            }
        }

        private void ActivatePowerCard(PowerCardType powerCardType)
        {
            if (powerCardType == PowerCardType.MoveBackward)
            {
                //apply effect directly?
                ApplyPowerCommand powerMove = new ApplyPowerCommand(playersList, (turnIndex % numOfPlayers), backwardsPowerCard);
                powerMove.Execute();
            }
            if (powerCardType == PowerCardType.Imprison)
            {
                ApplyPowerCommand powerMove = new ApplyPowerCommand(playersList, (turnIndex % numOfPlayers), imprisonedPowerCard);
                powerMove.Execute();
            }
        }

        // this does not need to be in game manager 
        //public void OnTurnChange()
        //{
        //    // check current player's values to modify ui elements
        //    currentPlayer = playersList[turnIndex % numOfPlayers];

        //    string playerName = currentPlayer.PlayerModel.PlayerName;
        //    string statusName;
        //    // status name
        //    if (currentPlayer.PlayerModel.CurrentStatus == Status.None)
        //    {
        //        statusName = "None";
        //        uiManager.DisplayDiceSwitch?.Invoke(true);
        //        uiManager.UpdatePlayButtonText?.Invoke(false);
        //        uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
        //        uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
        //    }
        //    else if (currentPlayer.PlayerModel.CurrentStatus == Status.Backwards)
        //    {
        //        statusName = "Backwards";
        //        uiManager.DisplayDiceSwitch?.Invoke(true);
        //        uiManager.UpdatePlayButtonText?.Invoke(false);
        //        uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
        //        uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
        //    }
        //    // imprisoned
        //    else
        //    {
        //        statusName = "Imprisoned";
        //        // dont show dice roll either
        //        uiManager.DisplayDiceSwitch?.Invoke(false);
        //        uiManager.UpdatePlayButtonText?.Invoke(true);
        //        uiManager.SwitchOffBackwardsPowerCard?.Invoke(true);
        //        uiManager.SwitchOffImprisonPowerCard?.Invoke(true);
        //    }
        //    // invoke top row changes
        //    TurnChangeUpdates?.Invoke(playerName, statusName);
        //}

        public void ChangeTurnUI()
        {
            currentPlayer = playersList[turnIndex % numOfPlayers];

            currentPlayer.PlayerView.TurnChangeUpdateUI();
        }
    }
}
