using TileGame.Player;
using UnityEngine;

namespace TileGame.PowerCards
{
    [CreateAssetMenu(fileName = "Imprison Power Card", menuName = "Power Cards/Imprison")]
    public class ImprisonPowerCard : PowerCardsBase, IPowersInterface
    {
        public int imprisonTurns = 2;

        public void ApplyPowerEffects(PlayerController player)
        {
            player.PlayerModel.SetValuesStatusImprisoned(turnLifeDuration);
        }

        public void ActivatePowerCard(PlayerController player)
        {
            player.PlayerModel.SetValActivePowerImprison(cardType, turnLifeDuration);
        }
    }
}