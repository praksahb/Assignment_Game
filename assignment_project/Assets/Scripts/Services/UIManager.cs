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

        public Action OnTurnChange;
        public Action<bool> UpdatePlayButtonText;

        public Action<bool> SwitchOffBackwardsPowerCard;
        public Action<bool> SwitchOffImprisonPowerCard;

        private void OnEnable()
        {
            playTurn.onClick.AddListener(PlayTurnFunction);
            backwardsButton.onClick.AddListener(ActivateBackwardsPower);
            imprisonButton.onClick.AddListener(ActivateImprisonPower);
            gameManager.RolledDice += UpdateDiceRollValue;
            gameManager.TurnChangeUpdates += UpdateTurnChangeValues;
            UpdatePlayButtonText += ChangePlayButtonText;
            SwitchOffBackwardsPowerCard += switchButtonBackwards;
            SwitchOffImprisonPowerCard += switchImprisonButton;
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
            gameManager.TurnChangeUpdates -= UpdateTurnChangeValues;
            UpdatePlayButtonText -= ChangePlayButtonText;
            SwitchOffBackwardsPowerCard -= switchButtonBackwards;
            SwitchOffImprisonPowerCard -= switchImprisonButton;
        }

        private void PlayTurnFunction()
        {
            gameManager.PlayTurn();
            OnTurnChange?.Invoke();
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

        private void UpdateTurnChangeValues(string playerName, string powerName)
        {
            string text = "Current Turn: ";
            text += playerName;
            nameText.SetText(text);

            text = "Active Power: ";
            text += powerName;
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

        private void switchButtonBackwards(bool isImprisoned)
        {
            SetInactiveGameObject(backwardsButton.gameObject, isImprisoned);
        }

        private void switchImprisonButton(bool isImprisoned)
        {
            SetInactiveGameObject(imprisonButton.gameObject, isImprisoned);
        }
    }
}
