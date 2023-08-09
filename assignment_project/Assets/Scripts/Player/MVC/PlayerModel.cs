namespace TileGame.Player
{
    public class PlayerModel
    {
        public PlayerType PlayerType { get; }

        private string playerName;
        public string PlayerName { get { return playerName; } }

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

        private int imprisonedTurns;
        public int ImprisonedTurns
        {
            get { return imprisonedTurns; }
            set
            {
                imprisonedTurns = value;
                if (imprisonedTurns == 0)
                {
                    IsImprisoned = false;
                }
            }
        }

        //constructor
        public PlayerModel(PlayerType playerType, int position)
        {
            PlayerType = playerType;
            // only works for 2 players
            playerName = PlayerType == PlayerType.Red ? "Red" : "Blue";

            MoveDirection = PlayerType == PlayerType.Red ? PlayerMoveDirection.MoveRight : PlayerMoveDirection.MoveLeft;

            TilePosition = position;

            MoveBackwards = 1;
            IsImprisoned = false;
            imprisonedTurns = 0;
        }
    }
}
