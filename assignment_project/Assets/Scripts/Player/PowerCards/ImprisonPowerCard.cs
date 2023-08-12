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
            player.PlayerModel.CurrentStatus = Status.Imprisoned;
            player.PlayerModel.IsImprisoned = true;
            player.PlayerModel.TurnsEffected = turnLifeDuration;
        }

        public void ActivatePowerCard(PlayerController player)
        {
            player.PlayerModel.ActivePower = cardType;
            player.PlayerModel.PowerDurationTurns = turnLifeDuration;
        }
    }
}