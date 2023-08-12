using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TileGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button playTurnBtn;
        [SerializeField] private Button backwardsButton;
        [SerializeField] private Button imprisonButton;
        [SerializeField] private TextMeshProUGUI diceRollText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI statusNameText;
        [SerializeField] private TextMeshProUGUI powerNameText;

        [SerializeField] private GameManager gameManager;

        private TextMeshProUGUI playButtonText;

        public Action<string, string, string> TurnChangeUpdates;
        public Action<bool> DisplayDiceSwitch;
        public Action<bool> UpdatePlayButtonText;
        public Action<bool> SwitchOffBackwardsPowerCard;
        public Action<bool> SwitchOffImprisonPowerCard;

        private void OnEnable()
        {
            playTurnBtn.onClick.AddListener(PlayTurnFunction);
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
            playButtonText = playTurnBtn.GetComponentInChildren<TextMeshProUGUI>();
            //Debug.Log(playButtonText.text);
        }

        private void OnDisable()
        {
            playTurnBtn.onClick.RemoveAllListeners();
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
            SwitchPlayButton(false, false);
            StartCoroutine(PlayTurnCoroutine());
        }

        private IEnumerator PlayTurnCoroutine()
        {
            yield return StartCoroutine(gameManager.PlayTurn());
            gameManager.ChangeTurnUI();
            SwitchPlayButton(true, true);
        }

        private void SwitchPlayButton(bool isInteractable, bool isVisible)
        {
            playTurnBtn.interactable = isInteractable;
            playTurnBtn.gameObject.SetActive(isVisible);
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

        private void UpdateTurnChangeValues(string playerName, string statusName, string powerName)
        {
            string text = "Current Turn: ";
            text += playerName;
            nameText.SetText(text);

            text = "Status: ";
            text += statusName;
            statusNameText.SetText(text);

            text = "Active Power: ";
            text += powerName;
            powerNameText.SetText(text);
        }

        private void ChangePlayButtonText(bool canPlayTurn)
        {
            if (canPlayTurn)
            {
                playButtonText.SetText("Play Turn");
            }
            else
            {
                playButtonText.SetText("Skip Turn");
            }
        }

        private void SetActiveGameObject(GameObject gameObject, bool value)
        {
            if (value)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void SwitchButtonBackwards(bool value)
        {
            SetActiveGameObject(backwardsButton.gameObject, value);
        }

        private void SwitchImprisonButton(bool value)
        {
            SetActiveGameObject(imprisonButton.gameObject, value);
        }

        private void DisplayDiceSwitchFunction(bool value)
        {
            SetActiveGameObject(diceRollText.gameObject, value);
        }
    }
}
