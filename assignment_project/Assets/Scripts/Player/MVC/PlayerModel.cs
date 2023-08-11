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
                    CurrentStatus = Status.None;
                    IsMovingBackwards = false;
                    IsImprisoned = false;
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

                // sent action for imprisoned and none to change button displays
                if (activePower == PowerCardType.Imprison)
                {
                    // perform ui actions to modify buttons
                    // check once to see if actions does not effect for all player types
                    // else pass normally by refs
                }
                else // works for both backwards and forward
                {
                    // display the buttons and dice roll value
                }

            }
        }

        //constructor
        public PlayerModel(PlayerType playerType, int position)
        {
            PlayerType = playerType;
            // only works for 2 players
            PlayerName = PlayerType == PlayerType.Red ? "Red" : "Blue";

            MoveDirection = PlayerType == PlayerType.Red ? PlayerMoveDirection.MoveRight : PlayerMoveDirection.MoveLeft;

            TilePosition = position;

            IsMovingBackwards = false;

            TurnsEffected = 0;
            PowerDurationTurns = 0;
        }
    }
}
