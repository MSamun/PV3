using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game,
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New List of Spells", menuName = "Game/List of Spells*")]
    public class ListOfSpellsObject : ScriptableObject
    {
        public SpellObject[] ListOfSpellsInGame;

        public SpellObject FindSpellByID(int spellID)
        {
            for (var i = 0; i < ListOfSpellsInGame.Length; i++)
            {
                if (ListOfSpellsInGame[i].spellID != spellID) continue;

                return ListOfSpellsInGame[i];
            }

            return null;
        }
    }
}