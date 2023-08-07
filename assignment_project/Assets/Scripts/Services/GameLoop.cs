using TileGame.Player;
using UnityEngine;

namespace TileGame.MainGame
{
    public class GameLoop : MonoBehaviour
    {
        private PlayerController playerRed;
        private PlayerController playerBlue;

        [SerializeField]
        private PlayerView playerRedPrefab;
        [SerializeField]
        private PlayerView playerBluePrefab;

        private void Awake()
        {
            PlayerModel playerModel = new PlayerModel(PlayerType.Blue, 0);
            playerBlue = new PlayerController(playerModel, playerBluePrefab);
        }
    }
}
