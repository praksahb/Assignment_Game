using TileGame.Player;
using UnityEngine;

namespace TileGame
{
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