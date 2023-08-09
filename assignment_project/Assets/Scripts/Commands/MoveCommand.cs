using TileGame.Player;

namespace TileGame
{
    public class MoveCommand : ICommand
    {
        private PlayerController player;
        private int tileEndIndex;

        public MoveCommand(PlayerController _player, int tileEndIndex)
        {
            player = _player;
            this.tileEndIndex = tileEndIndex;
        }

        public int Execute()
        {
            return player.MovePlayer(tileEndIndex);
        }
    }
}
