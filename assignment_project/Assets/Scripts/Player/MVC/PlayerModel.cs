namespace TileGame.Player
{
    public class PlayerModel
    {
        public PlayerType PlayerType { get; }


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

        private bool isImprisoned;
        public bool IsImprisoned
        {
            get { return isImprisoned; }
            set
            {
                isImprisoned = value;
            }
        }

        //constructor
        public PlayerModel(PlayerType playerType, int position)
        {
            PlayerType = playerType;
            MoveDirection = PlayerType == PlayerType.Red ? PlayerMoveDirection.MoveRight : PlayerMoveDirection.MoveLeft;

            TilePosition = position;
            IsImprisoned = false;
        }
    }
}
