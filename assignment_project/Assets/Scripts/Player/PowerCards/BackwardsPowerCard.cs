using TileGame.Player;
using UnityEngine;

namespace TileGame.PowerCards
{
    [CreateAssetMenu(fileName = "Backward Power Card", menuName = "Power Cards/Backwards")]
    public class BackwardsPowerCard : PowerCardsBase, IPowersInterface
    {
        public int backwardDirection = -1;

        public void ApplyPowerEffects(PlayerController player)
        {
            player.PlayerModel.SetValuesStatusBackwards(turnLifeDuration);
        }

        public void ActivatePowerCard(PlayerController player)
        {
            player.PlayerModel.SetValActivePowerBackwards(cardType, turnLifeDuration);
        }
    }
}