using TileGame.Player;
using UnityEngine;

namespace TileGame
{
    public abstract class PowerCardsBase : ScriptableObject
    {
        public string cardName;
        public PowerCardType cardType;
        public bool isUsed;
        public abstract void ApplyEffect(PlayerController player);
    }
}
