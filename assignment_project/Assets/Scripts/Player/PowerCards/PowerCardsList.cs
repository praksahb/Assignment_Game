using UnityEngine;

namespace TileGame.PowerCards
{
    [CreateAssetMenu(fileName = "ListOfPowerCards", menuName = "PowerCardsList")]
    public class PowerCardsList : ScriptableObject
    {
        public PowerCardsBase[] powerCardsList;
    }
}