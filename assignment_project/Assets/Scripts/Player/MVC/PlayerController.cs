using UnityEngine;

namespace TileGame.Player

{
    public class PlayerController
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            PlayerModel = playerModel;
            PlayerView = Object.Instantiate(playerView);

            // create link btw playerController and playerView
            PlayerView.PlayerController = this;
        }
    }
}
