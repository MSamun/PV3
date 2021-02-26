using PV3.Character;
using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game,
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New List of Spells", menuName = "Game/List of Spells*")]
    public class ListOfSpellsObject : ScriptableObject
    {
        public SpellObject[] WarriorSpells;
        [Header("")]
        public SpellObject[] WizardSpells;
        [Header("")]
        public SpellObject[] RangerSpells;

        public SpellObject FindSpellByID(int spellID, CombatClass combatClass)
        {
            if (combatClass == CombatClass.Warrior)
            {
                for (var i = 0; i < WarriorSpells.Length; i++)
                {
                    if (WarriorSpells[i].spellID != spellID) continue;

                    return WarriorSpells[i];
                }
            }
            else if (combatClass == CombatClass.Wizard)
            {
                for (var i = 0; i < WizardSpells.Length; i++)
                {
                    if (WizardSpells[i].spellID != spellID) continue;

                    return WizardSpells[i];
                }
            }
            else if (combatClass == CombatClass.Ranger)
            {
                for (var i = 0; i < RangerSpells.Length; i++)
                {
                    if (RangerSpells[i].spellID != spellID) continue;

                    return RangerSpells[i];
                }
            }

            Debug.LogError($"Error! Cannot find Spell #{spellID.ToString()} in the list of {combatClass.ToString()} Spells.");
            return null;
        }

        public SpellObject FindSpellAtIndex(int index, CombatClass combatClass)
        {
            if (combatClass == CombatClass.Warrior && index < WarriorSpells.Length) return WarriorSpells[index];

            if (combatClass == CombatClass.Wizard && index < WizardSpells.Length) return WizardSpells[index];

            if (combatClass == CombatClass.Ranger && index < RangerSpells.Length) return RangerSpells[index];

            Debug.LogError($"Error! Cannot find a {combatClass.ToString()} Spell at index #{index.ToString()}");
            return null;
        }
    }
}