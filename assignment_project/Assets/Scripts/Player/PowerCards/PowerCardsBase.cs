using UnityEngine;

namespace TileGame.PowerCards
{
    public class PowerCardsBase : ScriptableObject
    {
        public string cardName;
        public PowerCardType cardType;
        public bool isUsed;
        public int turnLifeDuration;
    }
}
