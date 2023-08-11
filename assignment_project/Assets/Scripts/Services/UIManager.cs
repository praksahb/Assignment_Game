using System;
using TileGame.MainGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TileGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button playTurn;
        [SerializeField] private Button backwardsButton;
        [SerializeField] private Button imprisonButton;
        [SerializeField] private TextMeshProUGUI diceRollText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI powerNameText;

        [SerializeField] private GameManager gameManager;

        private TextMeshProUGUI playButtonText;

        public Action<string, string> TurnChangeUpdates;
        public Action<bool> DisplayDiceSwitch;
        public Action<bool> UpdatePlayButtonText;
        public Action<bool> SwitchOffBackwardsPowerCard;
        public Action<bool> SwitchOffImprisonPowerCard;

        private void OnEnable()
        {
            playTurn.onClick.AddListener(PlayTurnFunction);
            backwardsButton.onClick.AddListener(ActivateBackwardsPower);
            imprisonButton.onClick.AddListener(ActivateImprisonPower);
            gameManager.RolledDice += UpdateDiceRollValue;
            TurnChangeUpdates += UpdateTurnChangeValues;
            UpdatePlayButtonText += ChangePlayButtonText;
            SwitchOffBackwardsPowerCard += SwitchButtonBackwards;
            SwitchOffImprisonPowerCard += SwitchImprisonButton;
            DisplayDiceSwitch += DisplayDiceSwitchFunction;
        }

        private void Start()
        {
            playButtonText = playTurn.GetComponentInChildren<TextMeshProUGUI>();
            //Debug.Log(playButtonText.text);
        }

        private void OnDisable()
        {
            playTurn.onClick.RemoveAllListeners();
            backwardsButton.onClick.RemoveAllListeners();
            imprisonButton.onClick.RemoveAllListeners();
            gameManager.RolledDice -= UpdateDiceRollValue;
            TurnChangeUpdates -= UpdateTurnChangeValues;
            UpdatePlayButtonText -= ChangePlayButtonText;
            SwitchOffBackwardsPowerCard -= SwitchButtonBackwards;
            SwitchOffImprisonPowerCard -= SwitchImprisonButton;
            DisplayDiceSwitch -= DisplayDiceSwitchFunction;
        }

        private void PlayTurnFunction()
        {
            gameManager.PlayTurn();
            gameManager.ChangeTurnUI();
        }

        private void ActivateBackwardsPower()
        {
            gameManager.ActivatePower?.Invoke(PowerCardType.MoveBackward);
        }

        private void ActivateImprisonPower()
        {
            gameManager.ActivatePower?.Invoke(PowerCardType.Imprison);
        }

        private void UpdateDiceRollValue(int value)
        {
            diceRollText.SetText("Dice Roll: {0}", value);
        }

        private void UpdateTurnChangeValues(string playerName, string statusName)
        {
            string text = "Current Turn: ";
            text += playerName;
            nameText.SetText(text);

            text = "Status: ";
            text += statusName;
            powerNameText.SetText(text);
        }

        private void ChangePlayButtonText(bool isImprisoned)
        {
            if (isImprisoned)
            {
                playButtonText.SetText("Skip Turn");
            }
            else
            {
                playButtonText.SetText("Play Turn");
            }
        }

        private void SetInactiveGameObject(GameObject gameObject, bool value)
        {
            if (value)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        private void SwitchButtonBackwards(bool isImprisoned)
        {
            SetInactiveGameObject(backwardsButton.gameObject, isImprisoned);
        }

        private void SwitchImprisonButton(bool isImprisoned)
        {
            SetInactiveGameObject(imprisonButton.gameObject, isImprisoned);
        }

        private void DisplayDiceSwitchFunction(bool value)
        {
            if (value)
            {
                diceRollText.gameObject.SetActive(true);
            }
            else
            {
                diceRollText.gameObject.SetActive(false);
            }
        }
    }
}
