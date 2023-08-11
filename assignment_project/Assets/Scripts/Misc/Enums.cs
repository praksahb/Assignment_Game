namespace TileGame
{
    public enum PlayerType
    {
        None = 0,
        Red,
        Blue
    }

    public enum PlayerMoveDirection
    {
        None = 0,
        MoveRight = 1,
        MoveLeft = -1,
    }

    public enum PowerCardType
    {
        None,
        MoveBackward,
        Imprison
    }

    public enum Status
    {
        None,
        Backwards,
        Imprisoned,
    }
}
