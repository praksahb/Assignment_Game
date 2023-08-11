using TileGame.Player;

namespace TileGame
{
    public class ApplyPowerCommand : ICommand
    {
        private PlayerController[] effectedPlayers;
        private PlayerController effectingPlayer;
        private PowerCardsBase currentPowerCard;

        public ApplyPowerCommand(PlayerController[] playersList, int currentPlayerIdx, PowerCardsBase currentPowerCard)
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

            this.currentPowerCard = currentPowerCard;
        }

        public int Execute()
        {
            effectingPlayer.ActivatePowerCard(currentPowerCard.cardType, currentPowerCard.turnLife);

            for (int i = 0; i < effectedPlayers.Length; i++)
            {
                currentPowerCard.ApplyEffect(effectedPlayers[i]);
            }

            return 0;
        }
    }
}