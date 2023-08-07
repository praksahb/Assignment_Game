using TileGame.Player;
using TileGame.Tiles;
using UnityEngine;

namespace TileGame.MainGame
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PlayerView playerRedPrefab;
        [SerializeField]
        private PlayerView playerBluePrefab;
        [SerializeField]
        private TileView tilePrefab;

        private PlayerController playerRed;
        private PlayerController playerBlue;

        private TileListController tileList;

        private void Awake()
        {
            PlayerModel playerModel = new PlayerModel(PlayerType.Blue, 0);
            playerBlue = new PlayerController(playerModel, playerBluePrefab);

            tileList = new TileListController(10, tilePrefab);
        }
    }
}
