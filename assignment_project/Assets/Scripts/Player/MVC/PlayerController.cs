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

            // create link btw playerController and playerView
            PlayerView.PlayerController = this;

            // Move at tile position
            PlayerView.MoveCurrentPosition(tilePositionX);
            UiManager = uiManager;
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

        public void ActivatePowerCard(PowerCardType powerCardType, int turnLifeDuration)
        {
            switch (powerCardType)
            {
                case PowerCardType.MoveBackward:
                    {
                        PlayerModel.ActivePower = powerCardType;
                        PlayerModel.PowerDurationTurns = turnLifeDuration;
                        break;
                    }
                case PowerCardType.Imprison:
                    {
                        PlayerModel.ActivePower = powerCardType;
                        PlayerModel.PowerDurationTurns = turnLifeDuration;
                        break;
                    }
            }
        }

        public void ApplyPowerEffects(PowerCardType cardType, int turnsEffected)
        {
            switch (cardType)
            {
                case PowerCardType.MoveBackward:
                    {
                        // move backwards
                        PlayerModel.IsMovingBackwards = true;
                        PlayerModel.CurrentStatus = Status.Backwards;
                        PlayerModel.TurnsEffected = turnsEffected;
                        break;
                    }
                case PowerCardType.Imprison:
                    {
                        // imprison the player for two turns
                        PlayerModel.CurrentStatus = Status.Imprisoned;
                        PlayerModel.IsImprisoned = true;
                        PlayerModel.TurnsEffected = turnsEffected;
                        break;
                    }
            }
        }
    }
}
