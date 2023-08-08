using UnityEngine;

namespace TileGame.Player

{
    public class PlayerController
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }

        public PlayerController(PlayerModel playerModel, PlayerView playerView, float tilePositionX)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerView);

            // create link btw playerController and playerView
            PlayerView.PlayerController = this;

            // Move at tile position
            MoveCurrentPosition(tilePositionX);
        }

        public void MoveCurrentPosition(float xPos)
        {
            Vector3 position = PlayerView.transform.position;
            position.x = xPos;
            PlayerView.transform.position = position;
        }
    }
}
