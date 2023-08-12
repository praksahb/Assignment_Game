using UnityEngine;

namespace TileGame.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }

        public void MoveCurrentPosition(float xPos)
        {
            Vector3 position = transform.position;
            position.x = xPos;
            transform.position = position;
        }

        public void TurnChangeUpdateUI()
        {
            PlayerController.UiManager.DisplayDiceSwitch?.Invoke(PlayerController.PlayerModel.DisplayDiceRoll);
            PlayerController.UiManager.UpdatePlayButtonText?.Invoke(PlayerController.PlayerModel.ChangePlayTurnBtnText);

            PlayerController.UiManager.SwitchOffBackwardsPowerCard?.Invoke(PlayerController.PlayerModel.DisplayBackwardsPowerCardBtn);

            PlayerController.UiManager.SwitchOffImprisonPowerCard?.Invoke(PlayerController.PlayerModel.DisplayImprisonPowerCardBtn);

            // invoke top row changes
            PlayerController.UiManager.TurnChangeUpdates?.Invoke(PlayerController.PlayerModel.PlayerName, PlayerController.PlayerModel.StatusName, PlayerController.PlayerModel.ActivePowerName);
        }
    }
}
