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

using PV3.Character;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCombatClassSpells : MonobehaviourReference
    {
        private ChooseSpellForLoadout[] amountOfSpells;

        [SerializeField] private PlayerObject Player;
        [SerializeField] private IntValue SpellChosenIndex;
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;
        [SerializeField] private CombatClass spellType;

        [Header("UI Panel")]
        [SerializeField] private GameObject contentPanel;
        [SerializeField] private Button equipButton;

        private void Awake()
        {
            amountOfSpells = contentPanel.GetComponentsInChildren<ChooseSpellForLoadout>();

            for (var i = 0; i < amountOfSpells.Length; i++)
            {
                amountOfSpells[i].Initialize(ListOfSpellsObject.FindSpellAtIndex(i, spellType));
                amountOfSpells[i].DisplayEquippedIcon(CheckIfSpellIsEquipped(amountOfSpells[i].Spell));
            }
        }

        public void UpdateSpellFocusAndEquippedIcons()
        {
            equipButton.interactable = false;

            for (var i = 0; i < amountOfSpells.Length; i++)
            {
                SpellChosenIndex.Value = -1;
                amountOfSpells[i].ResetSpellFocus();
                amountOfSpells[i].DisplayEquippedIcon(CheckIfSpellIsEquipped(amountOfSpells[i].Spell));
            }
        }

        private bool CheckIfSpellIsEquipped(SpellObject spell)
        {
            var foundSpell = false;

            for (var i = 0; i < Player.ListOfSpells.Count; i++)
            {
                if (spell != Player.ListOfSpells[i].spell) continue;
                foundSpell = true;
            }

            return foundSpell;
        }

        public void SetSpellToEquip(int index)
        {
            var isAlreadyEquipped = CheckIfSpellIsEquipped(amountOfSpells[index].Spell);
            var isSameCombatClass = Player.Class == amountOfSpells[index].Spell.combatClass;

            equipButton.interactable = !isAlreadyEquipped && isSameCombatClass;
            amountOfSpells[index].SetSpellToEquip();
        }

        private void OnEnable()
        {
            UpdateSpellFocusAndEquippedIcons();
        }
    }
}