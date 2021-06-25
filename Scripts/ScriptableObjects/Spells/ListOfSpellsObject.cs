// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

using PV3.ScriptableObjects.Character;
using UnityEngine;

namespace PV3.ScriptableObjects.Spells
{
    // The asterisk (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game,
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New List of Spells", menuName = "Game/List of Spells*")]
    public class ListOfSpellsObject : ScriptableObject
    {
        // Temporary implementation. Will remove after adding Inventory and Equipment functionality.
        public SpellObject[] Potions;

        [Header("")]
        public SpellObject[] WarriorSpells;

        [Header("")]
        public SpellObject[] WizardSpells;

        [Header("")]
        public SpellObject[] RangerSpells;

        public SpellObject FindSpellByID(int spellID, CombatClass combatClass)
        {
            if (combatClass == CombatClass.Warrior)
                for (var i = 0; i < WarriorSpells.Length; i++)
                {
                    if (WarriorSpells[i].spellID != spellID) continue;

                    return WarriorSpells[i];
                }
            else if (combatClass == CombatClass.Wizard)
                for (var i = 0; i < WizardSpells.Length; i++)
                {
                    if (WizardSpells[i].spellID != spellID) continue;

                    return WizardSpells[i];
                }
            else if (combatClass == CombatClass.Ranger)
                for (var i = 0; i < RangerSpells.Length; i++)
                {
                    if (RangerSpells[i].spellID != spellID) continue;

                    return RangerSpells[i];
                }

            Debug.LogError($"Error! Cannot find Spell with ID #{spellID.ToString()} in the list of {combatClass.ToString()} Spells. Maybe you are searching in the wrong List of Spells?");
            return null;
        }

        public SpellObject FindSpellAtIndex(int index, CombatClass combatClass)
        {
            if (combatClass == CombatClass.Warrior && index < WarriorSpells.Length) return WarriorSpells[index];

            if (combatClass == CombatClass.Wizard && index < WizardSpells.Length) return WizardSpells[index];

            if (combatClass == CombatClass.Ranger && index < RangerSpells.Length) return RangerSpells[index];

            Debug.LogError($"Error! Cannot find a {combatClass.ToString()} Spell at Index #{index.ToString()}. Hiding Button in List of {combatClass.ToString()} Spells.");
            return null;
        }

        public SpellObject FindPotionByID(int potionID)
        {
            for (var i = 0; i < Potions.Length; i++)
            {
                if (Potions[i].spellID != potionID) continue;
                return Potions[i];
            }

            Debug.LogError($"Error! Cannot find Potion with ID #{potionID.ToString()} in the list of Potions.");
            return null;
        }
    }
}