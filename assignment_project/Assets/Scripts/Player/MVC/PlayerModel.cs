namespace TileGame.Player
{
    public class PlayerModel
    {
        // public read-only properties set once in constructor
        public PlayerType PlayerType { get; }
        public string PlayerName { get; }

        private PlayerMoveDirection moveDirection;
        private int tilePosition;
        private bool isMovingBackwards;
        private int moveBackwards;
        private bool isImprisoned;
        private Status currentStatus;
        private int turnsEffected;
        private int powerDuration;
        private PowerCardType activePower;

        public PlayerMoveDirection MoveDirection
        {
            get { return moveDirection; }
            set
            {
                moveDirection = value;
            }
        }
        public int TilePosition
        {
            get { return tilePosition; }
            set
            {
                tilePosition = value;
            }
        }

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
        public int MoveBackwards
        {
            get { return moveBackwards; }
            set
            {
                moveBackwards = value;
            }
        }
        public bool IsImprisoned
        {
            get { return isImprisoned; }
            set
            {
                isImprisoned = value;
            }
        }
        public Status CurrentStatus
        {
            get { return currentStatus; }
            set
            {
                currentStatus = value;
            }
        }
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
        // Public read - Private write Properties
        public bool DisplayDiceRoll { get; private set; }
        public bool ChangePlayTurnBtnText { get; set; }
        public bool DisplayBackwardsPowerCardBtn { get; set; }
        public bool DisplayImprisonPowerCardBtn { get; set; }
        public string StatusName { get; set; }
        public string ActivePowerName { get; set; }

        // ACTION
        // change active power immediately for current player
        public System.Action ChangeActivePowerName;

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
            ActivePowerName = "Imprison";
            ChangeActivePowerName?.Invoke();
            DisplayBackwardsPowerCardBtn = false;
            DisplayImprisonPowerCardBtn = false;
        }
        public void SetValActivePowerBackwards(PowerCardType powerCardType, int turnsImpacted)
        {
            ActivePower = powerCardType;
            PowerDurationTurns = turnsImpacted;

            // UI 
            ActivePowerName = "Backwards";
            ChangeActivePowerName?.Invoke();
        }

        // constructor
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
