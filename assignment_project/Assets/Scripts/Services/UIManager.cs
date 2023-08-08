using TileGame.MainGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TileGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button playTurn;
        [SerializeField] private TextMeshProUGUI diceRollText;


        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            playTurn.onClick.AddListener(PlayTurnFunction);
            gameManager.RolledDice += UpdateDiceRollValue;
        }

        private void OnDisable()
        {
            playTurn.onClick.RemoveAllListeners();
            gameManager.RolledDice -= UpdateDiceRollValue;

        }

        private void PlayTurnFunction()
        {
            gameManager.PlayTurn();
        }

        private void UpdateDiceRollValue(int value)
        {
            diceRollText.SetText("Dice Roll: {0}", value);
        }
    }
}
