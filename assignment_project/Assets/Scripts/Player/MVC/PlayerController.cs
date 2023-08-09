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

            if (PlayerModel.IsMovingBackwards)
            {
                if (PlayerModel.TilePosition == 0 || PlayerModel.TilePosition == tileEndIndex)
                {
                    return PlayerModel.TilePosition;
                }
            }

            PlayerModel.TilePosition += moveDir * PlayerModel.MoveBackwards;

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
                        PlayerModel.IsMovingBackwards = true;
                        PlayerModel.MoveBackwards = powerCardInt;
                        break;
                    }
                case PowerCardType.Imprison:
                    {
                        // imprison the player for two turns
                        PlayerModel.IsImprisoned = true;
                        PlayerModel.ImprisonedTurns = powerCardInt;
                        break;
                    }
            }


        }
    }
}
