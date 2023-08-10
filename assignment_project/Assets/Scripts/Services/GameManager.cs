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
        [SerializeField] private PowerCardsList cardList;

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
            PlayerModel playerRedModel = new PlayerModel(PlayerType.Red, playerRedPosition, cardList.powerCardsList);
            currentTilePosition.x = tileListController.GetTilePositionX(playerRedModel.TilePosition);
            playerRed = new PlayerController(playerRedModel, playerRedPrefab, currentTilePosition.x);
            playersList[turnIndex++] = playerRed;
            // blue is on right end
            PlayerModel playerBlueModel = new PlayerModel(PlayerType.Blue, playerBluePosition, cardList.powerCardsList);
            currentTilePosition.x = tileListController.GetTilePositionX(playerBlueModel.TilePosition);
            playerBlue = new PlayerController(playerBlueModel, playerBluePrefab, currentTilePosition.x);
            playersList[turnIndex++] = playerBlue;

            // reset turnindex
            turnIndex = 0;
            currentPlayerTurn = playersList[turnIndex % numOfPlayers];
            string playerName = currentPlayerTurn.PlayerModel.PlayerName;
            string statusName = "None";
            TurnChangeUpdates?.Invoke(playerName, statusName);
        }

        public void PlayTurn()
        {
            // get random dice roll value
            int diceValue = Random.Range(1, 7);
            // Invoke Action - received by UIManager
            RolledDice?.Invoke(diceValue);

            //if (activePowerCard[turnIndex % numOfPlayers] != null)
            //{
            //    activePowerCard[turnIndex % numOfPlayers].ApplyEffect(currentPlayerTurn);
            //    activePowerCard[turnIndex % numOfPlayers] = null;
            //}

            Debug.LogFormat("Curr: {0}", currentPlayerTurn.PlayerModel.IsImprisoned);
            if (currentPlayerTurn.PlayerModel.CurrentStatus != Status.Imprisoned)
            {
                // MOVE function
                ICommand moveCommand = new MoveCommand(currentPlayerTurn, totalTiles - 1);

                //bool isBackwardsActive = currentPlayerTurn.PlayerModel.IsMovingBackwards;

                for (int i = 0; i < diceValue; i++)
                {
                    if (currentPlayerTurn.PlayerModel.IsMovingBackwards && (currentPlayerTurn.PlayerModel.TilePosition == 0 || currentPlayerTurn.PlayerModel.TilePosition == totalTiles - 1))
                    {
                        break;
                    }
                    int tilePos = moveCommand.Execute();
                    currentTilePosition.x = tileListController.GetTilePositionX(tilePos);
                    currentPlayerTurn.MoveCurrentPosition(currentTilePosition.x);
                }

                //// reset backwards 
                //if (isBackwardsActive)
                //{
                //    currentPlayerTurn.PlayerModel.IsMovingBackwards = false;
                //}
            }

            turnIndex++;
            if (currentPlayerTurn.PlayerModel.CurrentStatus != Status.None)
            {
                currentPlayerTurn.PlayerModel.TurnsEffected--;
            }


            //// reduce value for imprisoned turns
            //if (currentPlayerTurn.PlayerModel.IsImprisoned)
            //{
            //    currentPlayerTurn.PlayerModel.ImprisonedTurns--;
            //    if (currentPlayerTurn.PlayerModel.IsImprisoned)
            //    {
            //        // invoke events for setting off buttons for play cards
            //        uiManager.SwitchOffBackwardsPowerCard?.Invoke(true);
            //        uiManager.SwitchOffImprisonPowerCard?.Invoke(true);
            //    }
            //}
            //else
            //{
            //    uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
            //    uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
            //}
        }

        private void ActivatePowerCard(PowerCardType powerCardType)
        {
            if (powerCardType == PowerCardType.MoveBackward)
            {
                activePowerCard[(turnIndex + 1) % numOfPlayers] = backwardsPowerCard;
                //apply effect directly?
                ApplyPowerCommand powerMove = new ApplyPowerCommand(playersList, (turnIndex % numOfPlayers), backwardsPowerCard);
                powerMove.Execute();
            }
            if (powerCardType == PowerCardType.Imprison)
            {
                activePowerCard[(turnIndex + 1) % numOfPlayers] = imprisonedPowerCard;
                ApplyPowerCommand powerMove = new ApplyPowerCommand(playersList, (turnIndex % numOfPlayers), imprisonedPowerCard);
                powerMove.Execute();
            }
        }


        public void OnTurnChange()
        {
            // check current player's values to modify ui elements
            currentPlayerTurn = playersList[turnIndex % numOfPlayers];

            string playerName = currentPlayerTurn.PlayerModel.PlayerName;
            string statusName;
            // status name
            if (currentPlayerTurn.PlayerModel.CurrentStatus == Status.None)
            {
                statusName = "None";
                uiManager.DisplayDiceSwitch?.Invoke(true);
                uiManager.UpdatePlayButtonText?.Invoke(false);
                uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
                uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
            }
            else if (currentPlayerTurn.PlayerModel.CurrentStatus == Status.Backwards)
            {
                statusName = "Backwards";
                uiManager.DisplayDiceSwitch?.Invoke(true);
                uiManager.UpdatePlayButtonText?.Invoke(false);
                uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
                uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
            }
            // imprisoned
            else
            {
                statusName = "Imprisoned";
                // dont show dice roll either
                uiManager.DisplayDiceSwitch?.Invoke(false);
                uiManager.UpdatePlayButtonText?.Invoke(true);
                uiManager.SwitchOffBackwardsPowerCard?.Invoke(true);
                uiManager.SwitchOffImprisonPowerCard?.Invoke(true);
            }
            // invoke top row changes
            TurnChangeUpdates?.Invoke(playerName, statusName);


            //if (activePowerCard[turnIndex % numOfPlayers] == null)
            //{
            //    powerName = "None";
            //}
            //else
            //{
            //    powerName = activePowerCard[turnIndex % numOfPlayers].cardName;
            //}
            //if (currentPlayerTurn.PlayerModel.IsImprisoned)
            //{
            //    powerName = "Imprison";
            //}
            //TurnChangeUpdates?.Invoke(playerName, powerName);

            //if (activePowerCard[turnIndex % numOfPlayers] != null && activePowerCard[turnIndex % numOfPlayers].cardType == PowerCardType.Imprison)
            //{
            //    uiManager.UpdatePlayButtonText?.Invoke(true);
            //    uiManager.SwitchOffBackwardsPowerCard?.Invoke(true);
            //    uiManager.SwitchOffImprisonPowerCard?.Invoke(true);
            //}

            //if (currentPlayerTurn.PlayerModel.IsImprisoned == true || playersList[(turnIndex + 1) % numOfPlayers].PlayerModel.IsImprisoned == true)
            //{
            //    uiManager.UpdatePlayButtonText?.Invoke(true);
            //    uiManager.SwitchOffBackwardsPowerCard?.Invoke(true);
            //    uiManager.SwitchOffImprisonPowerCard?.Invoke(true);
            //}
            //else
            //{
            //    uiManager.UpdatePlayButtonText?.Invoke(false);
            //    uiManager.SwitchOffBackwardsPowerCard?.Invoke(false);
            //    uiManager.SwitchOffImprisonPowerCard?.Invoke(false);
            //}
        }
    }
}
