using TileGame.Player;
using UnityEngine;

namespace TileGame
{
    public abstract class PowerCardsBase : ScriptableObject
    {
        public string cardName;
        public PowerCardType cardType;
        public abstract void ApplyEffect(PlayerController player);
    }

    [CreateAssetMenu(fileName = "Backward Power Card", menuName = "Power Cards/Backwards")]
    public class BackwardsPowerCard : PowerCardsBase
    {
        public int backwardDirection = -1;

        public override void ApplyEffect(PlayerController player)
        {
            player.ApplyPowerCard(cardType, backwardDirection);
        }
    }

    [CreateAssetMenu(fileName = "Imprison Power Card", menuName = "Power Cards/Imprison")]
    public class ImprisonPowerCard : PowerCardsBase
    {
        public int imprisonTurns = 2;

        public override void ApplyEffect(PlayerController player)
        {
            player.ApplyPowerCard(cardType, imprisonTurns);
        }
    }
}
