using UnityEngine;

namespace TileGame.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController PlayerController { get; set; }

        private void Start()
        {
            PlayerController.PlayerModel.ChangeActivePowerName += PerformTxtValueChange;
        }

        private void OnDisable()
        {
            PlayerController.PlayerModel.ChangeActivePowerName -= PerformTxtValueChange;
        }

        private void PerformTxtValueChange()
        {
            PlayerController.UiManager.ChangeActivePowerName?.Invoke(PlayerController.PlayerModel.ActivePowerName);
        }

        public void MoveCurrentPosition(float xPos)
        {
            Vector3 position = transform.position;
            position.x = xPos;
            transform.position = position;
        }

        public void TurnChangeUpdateUI()
        {
            // middle row play turn btn and dice roll value
            PlayerController.UiManager.DisplayDiceSwitch?.Invoke(PlayerController.PlayerModel.DisplayDiceRoll);
            PlayerController.UiManager.UpdatePlayButtonText?.Invoke(PlayerController.PlayerModel.ChangePlayTurnBtnText);

            // bottom row - power cards
            PlayerController.UiManager.SwitchOffBackwardsPowerCard?.Invoke(PlayerController.PlayerModel.DisplayBackwardsPowerCardBtn);

            PlayerController.UiManager.SwitchOffImprisonPowerCard?.Invoke(PlayerController.PlayerModel.DisplayImprisonPowerCardBtn);

            // invoke top row changes
            PlayerController.UiManager.TurnChangeUpdates?.Invoke(PlayerController.PlayerModel.PlayerName, PlayerController.PlayerModel.StatusName, PlayerController.PlayerModel.ActivePowerName);
        }
    }
}
