using TileGame.Player;

namespace TileGame
{
    public interface IPowersInterface
    {
        void ActivatePowerCard(PlayerController player);
        void ApplyPowerEffects(PlayerController player);
    }
}
