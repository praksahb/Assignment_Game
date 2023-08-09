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

        public int MovePlayer(int tileEndIndex)
        {
            int moveDir = (int)PlayerModel.MoveDirection;
            PlayerModel.TilePosition += moveDir;
            if (PlayerModel.TilePosition == 0 || PlayerModel.TilePosition == tileEndIndex)
            {
                moveDir *= -1;
                PlayerModel.MoveDirection = (PlayerMoveDirection)moveDir;
            }
            return PlayerModel.TilePosition;
        }

        public void ApplyPowerCard(PowerCardType cardType, int powerCardInt)
        {
            switch (cardType)
            {
                case PowerCardType.MoveBackward:
                    {
                        // move backwards 
                        break;
                    }
                case PowerCardType.Imprison:
                    {
                        // imprison the player for two turns
                        break;
                    }
            }


        }
    }
}
