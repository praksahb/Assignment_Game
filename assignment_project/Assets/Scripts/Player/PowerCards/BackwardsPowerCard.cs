using TileGame.Player;
using UnityEngine;

namespace TileGame
{
    [CreateAssetMenu(fileName = "Backward Power Card", menuName = "Power Cards/Backwards")]
    public class BackwardsPowerCard : PowerCardsBase
    {
        public int backwardDirection = -1;

        public override void ApplyEffect(PlayerController player)
        {
            player.ApplyPowerEffects(cardType, backwardDirection);
        }
    }
}