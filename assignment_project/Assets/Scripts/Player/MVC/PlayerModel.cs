namespace TileGame.Player
{
    public class PlayerModel
    {
        public PlayerType PlayerType { get; }

        public string PlayerName { get; }

        private PlayerMoveDirection moveDirection;
        public PlayerMoveDirection MoveDirection
        {
            get { return moveDirection; }
            set
            {
                moveDirection = value;
            }
        }

        private int tilePosition;
        public int TilePosition
        {
            get { return tilePosition; }
            set
            {
                tilePosition = value;
            }
        }

        private bool isMovingBackwards;
        public bool IsMovingBackwards
        {
            get { return isMovingBackwards; }
            set
            {
                isMovingBackwards = value;
                if (isMovingBackwards == false)
                {
                    MoveBackwards = 1;
                }
                else
                {
                    MoveBackwards = -1;
                }
            }
        }
        private int moveBackwards;
        public int MoveBackwards
        {
            get { return moveBackwards; }
            set
            {
                moveBackwards = value;
            }
        }

        private bool isImprisoned;
        public bool IsImprisoned
        {
            get { return isImprisoned; }
            set
            {
                isImprisoned = value;
            }
        }

        private Status currentStatus;
        public Status CurrentStatus
        {
            get { return currentStatus; }
            set
            {
                currentStatus = value;
            }
        }

        private int turnsEffected;
        public int TurnsEffected
        {
            get { return turnsEffected; }
            set
            {
                turnsEffected = value;
                if (turnsEffected == 0)
                {
                    SetValuesStatusNone();
                }
            }
        }

        private int powerDuration;
        public int PowerDurationTurns
        {
            get { return powerDuration; }
            set
            {
                powerDuration = value;
                if (powerDuration == 0)
                {
                    ActivePower = PowerCardType.None;
                }
            }
        }

        private PowerCardType activePower;
        public PowerCardType ActivePower
        {
            get { return activePower; }
            set
            {
                activePower = value;
                if (ActivePower == PowerCardType.None)
                {
                    SetValActivePowerNone();
                }
            }
        }

        // bool values used for UI changes in PlayerView
        private bool displayDiceRollValue;
        // changes from either Play Turn or Skip Turn
        private bool changePlayTurnBtnText;
        private bool displayBackwardsPowerCardBtn;
        private bool displayImprisonPowerCardBtn;

        // string values for status and power-active
        // used for UI changes in PlayerView
        private string statusName;
        private string activePowerName;

        // properties for the above private variables
        public bool DisplayDiceRoll
        {
            get { return displayDiceRollValue; }
            set
            {
                displayDiceRollValue = value;
            }
        }
        public bool ChangePlayTurnBtnText
        {
            get { return changePlayTurnBtnText; }
            set
            {
                changePlayTurnBtnText = value;
            }
        }

        public bool DisplayBackwardsPowerCardBtn
        {
            get { return displayBackwardsPowerCardBtn; }
            set
            {
                displayBackwardsPowerCardBtn = value;
            }
        }
        public bool DisplayImprisonPowerCardBtn
        {
            get { return displayImprisonPowerCardBtn; }
            set
            {
                displayImprisonPowerCardBtn = value;
            }
        }
        public string StatusName
        {
            get { return statusName; }
            set
            {
                statusName = value;
            }
        }
        public string ActivePowerName
        {
            get { return activePowerName; }
            set
            {
                activePowerName = value;
            }
        }

        private void SetValuesStatusNone()
        {
            CurrentStatus = Status.None;
            IsMovingBackwards = false;
            IsImprisoned = false;

            // UI
            StatusName = "None";
            DisplayDiceRoll = true;
            ChangePlayTurnBtnText = true;
            DisplayBackwardsPowerCardBtn = true;
            DisplayImprisonPowerCardBtn = true;
        }

        private void SetValActivePowerNone()
        {
            // UI
            ActivePowerName = "None";
            DisplayBackwardsPowerCardBtn = true;
            DisplayImprisonPowerCardBtn = true;
        }

        // Setters for Power effecting player
        public void SetValuesStatusImprisoned(int turnsEffected)
        {
            CurrentStatus = Status.Imprisoned;
            IsImprisoned = true;
            TurnsEffected = turnsEffected;

            // changes for UI effects
            StatusName = "Imprisoned";
            DisplayDiceRoll = false;
            ChangePlayTurnBtnText = false;
            DisplayBackwardsPowerCardBtn = false;
            DisplayImprisonPowerCardBtn = false;
        }

        public void SetValuesStatusBackwards(int turnsEffected)
        {
            IsMovingBackwards = true;
            CurrentStatus = Status.Backwards;
            TurnsEffected = turnsEffected;

            // UI
            StatusName = "Moving Backwards";
            DisplayDiceRoll = true;
            ChangePlayTurnBtnText = true;
            DisplayBackwardsPowerCardBtn = true;
            DisplayImprisonPowerCardBtn = true;
        }

        // Setters for Player activating Power
        public void SetValActivePowerImprison(PowerCardType powerCardType, int turnsImpacted)
        {
            ActivePower = powerCardType;
            PowerDurationTurns = turnsImpacted;

            // UI
            activePowerName = "Imprison";
            DisplayBackwardsPowerCardBtn = false;
            DisplayImprisonPowerCardBtn = false;
        }

        public void SetValActivePowerBackwards(PowerCardType powerCardType, int turnsImpacted)
        {
            ActivePower = powerCardType;
            PowerDurationTurns = turnsImpacted;

            // UI 
            activePowerName = "Backwards";
        }

        //constructor
        public PlayerModel(PlayerType playerType, int position)
        {
            PlayerType = playerType;
            // only works for 2 players
            PlayerName = PlayerType == PlayerType.Red ? "Red" : "Blue";
            // 2 directions
            MoveDirection = PlayerType == PlayerType.Red ? PlayerMoveDirection.MoveRight : PlayerMoveDirection.MoveLeft;

            TilePosition = position;

            IsMovingBackwards = false;

            TurnsEffected = 0;
            PowerDurationTurns = 0;
            SetValuesStatusNone();
        }
    }
}
