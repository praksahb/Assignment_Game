using TileGame.Player;

namespace TileGame.PowerCards
{
    public interface IPowersInterface
    {
        void ActivatePowerCard(PlayerController player);
        void ApplyPowerEffects(PlayerController player);
    }
}