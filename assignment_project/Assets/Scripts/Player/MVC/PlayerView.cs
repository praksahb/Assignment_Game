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
            string playerName = PlayerController.PlayerModel.PlayerName;
            string statusName;
            bool displayDiceRoll, updatePlayBtnText, switchBackwardsPowerBtn, switchImprisonPowerBtn;

            if (PlayerController.PlayerModel.CurrentStatus == Status.None)
            {
                statusName = "None";
                displayDiceRoll = true;
                updatePlayBtnText = false;
                switchBackwardsPowerBtn = false;
                switchImprisonPowerBtn = false;
            }
            else if (PlayerController.PlayerModel.CurrentStatus == Status.Backwards)
            {
                statusName = "Backwards";
                displayDiceRoll = true;
                updatePlayBtnText = false;
                switchBackwardsPowerBtn = false;
                switchImprisonPowerBtn = false;
            }
            // imprisoned
            else
            {
                statusName = "Imprisoned";
                displayDiceRoll = false;
                updatePlayBtnText = true;
                switchBackwardsPowerBtn = true;
                switchImprisonPowerBtn = true;
            }

            if (PlayerController.PlayerModel.ActivePower == PowerCardType.Imprison)
            {
                switchBackwardsPowerBtn = true;
                switchImprisonPowerBtn = true;
            }

            PlayerController.UiManager.DisplayDiceSwitch?.Invoke(displayDiceRoll);
            PlayerController.UiManager.UpdatePlayButtonText?.Invoke(updatePlayBtnText);
            PlayerController.UiManager.SwitchOffBackwardsPowerCard?.Invoke(switchBackwardsPowerBtn);
            PlayerController.UiManager.SwitchOffImprisonPowerCard?.Invoke(switchImprisonPowerBtn);

            // invoke top row changes
            PlayerController.UiManager.TurnChangeUpdates?.Invoke(playerName, statusName);
        }
    }
}
