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
            powersInterface = currentPowerStrategy;

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
        }

        public void Execute()
        {
            // player that is activating the power
            powersInterface.ActivatePowerCard(effectingPlayer);

            // power that is effecting rest of players
            for (int i = 0; i < effectedPlayers.Length; i++)
            {
                powersInterface.ApplyPowerEffects(effectedPlayers[i]);
            }
        }
    }
}