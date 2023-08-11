using UnityEngine;

namespace TileGame
{
    public class PowerCardsBase : ScriptableObject
    {
        public string cardName;
        public PowerCardType cardType;
        public bool isUsed;
        public int turnLifeDuration;
    }
}
