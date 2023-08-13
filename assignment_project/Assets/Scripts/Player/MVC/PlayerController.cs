using UnityEngine;

namespace TileGame.Player

{
    public class PlayerController
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }
        public UIManager UiManager { get; }

        public PlayerController(PlayerModel playerModel, PlayerView playerView, float tilePositionX, UIManager uiManager)
        {
            PlayerModel = playerModel;

            PlayerView = Object.Instantiate(playerView);
            PlayerView.PlayerController = this;
            PlayerView.MoveCurrentPosition(tilePositionX);

            UiManager = uiManager;
        }

        public int MovePlayer(int tileEndIndex)
        {
            int moveDir = (int)PlayerModel.MoveDirection * PlayerModel.MoveBackwards;

            PlayerModel.TilePosition += moveDir;

            if (PlayerModel.IsMovingBackwards)
            {
                return PlayerModel.TilePosition;
            }

            if (PlayerModel.TilePosition == 0 || PlayerModel.TilePosition == tileEndIndex)
            {
                moveDir *= -1;
                PlayerModel.MoveDirection = (PlayerMoveDirection)moveDir;
            }
            return PlayerModel.TilePosition;
        }
    }
}
