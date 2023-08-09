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

        public Action OnTurnChange;

        private void OnEnable()
        {
            playTurn.onClick.AddListener(PlayTurnFunction);
            gameManager.RolledDice += UpdateDiceRollValue;
            gameManager.TurnChangePlayerName += UpdateTurnName;
        }

        private void OnDisable()
        {
            playTurn.onClick.RemoveAllListeners();
            gameManager.RolledDice -= UpdateDiceRollValue;
            gameManager.TurnChangePlayerName -= UpdateTurnName;

        }

        private void PlayTurnFunction()
        {
            gameManager.PlayTurn();
            OnTurnChange?.Invoke();
        }

        private void UpdateDiceRollValue(int value)
        {
            diceRollText.SetText("Dice Roll: {0}", value);
        }

        private void UpdateTurnName(string name)
        {
            string text = "Current Turn: ";
            text += name;
            nameText.SetText(text);
        }
    }
}
