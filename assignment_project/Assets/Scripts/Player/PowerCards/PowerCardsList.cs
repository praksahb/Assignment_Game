using UnityEngine;

namespace TileGame
{
    [CreateAssetMenu(fileName = "ListOfPowerCards", menuName = "PowerCardsList")]
    public class PowerCardsList : ScriptableObject
    {
        public PowerCardsBase[] powerCardsList;
    }
}