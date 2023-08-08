using TileGame.MainGame;
using UnityEngine;
using UnityEngine.UI;

namespace TileGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Button playTurn;
        [SerializeField]
        private GameManager gameManager;

        private void OnEnable()
        {
            playTurn.onClick.AddListener(PlayTurnFunction);

        }

        private void PlayTurnFunction()
        {
            gameManager.PlayTurn();
        }
    }
}
