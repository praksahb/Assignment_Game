using TileGame.Player;

namespace TileGame.PowerCards
{
    public class PowersClient
    {
        private IPowersInterface powersInterface;
        private PlayerController effectingPlayer;
        private PlayerController[] effectedPlayers;

        public PowersClient(PlayerController[] playersList, int currentPlayerIdx, IPowersInterface currentPowerStrategy)
        {
            effectedPlayers = new PlayerController[playersList.Length - 1];

            int j = 0;
            for (int i = 0; i < playersList.Length; i++)
            {
                if (i != currentPlayerIdx)
                {
                    effectedPlayers[j++] = playersList[i];
                }
                else
                {
                    effectingPlayer = playersList[i];
                }
            }

            powersInterface = currentPowerStrategy;
        }

        public void Execute()
        {
            powersInterface.ActivatePowerCard(effectingPlayer);

            for (int i = 0; i < effectedPlayers.Length; i++)
            {
                powersInterface.ApplyPowerEffects(effectedPlayers[i]);
            }
        }
    }
}