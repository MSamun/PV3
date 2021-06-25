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

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using PV3.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home.CharacterScreen
{
    public class ChooseSpellForLoadout : MonobehaviourReference
    {
        public SpellObject Spell;
        [SerializeField] private IntValue SpellChosenIndex;

        [Header("UI Components")]
        [SerializeField] private Button Button;

        [SerializeField] private Image SpellFocus;
        [SerializeField] private Image EquippedSpellIcon;
        private int index;

        private void OnEnable()
        {
            ResetSpellFocus();
        }

        public void Initialize(SpellObject spell)
        {
            if (!spell)
            {
                gameObject.SetActive(false);
                return;
            }

            Spell = spell;
            index = Spell.spellID;

            Button.GetComponent<Image>().sprite = Spell.sprite;
            GetComponent<SpellTooltipTrigger>().Spell = Spell;

            ResetSpellFocus();
        }

        public void ResetSpellFocus()
        {
            if (SpellFocus.gameObject.activeInHierarchy && SpellChosenIndex.Value != index) SpellFocus.gameObject.SetActive(false);

            if (!SpellFocus.gameObject.activeInHierarchy && SpellChosenIndex.Value == index) SpellFocus.gameObject.SetActive(true);
        }

        public void DisplayEquippedIcon(bool shouldDisplay)
        {
            EquippedSpellIcon.gameObject.SetActive(shouldDisplay);
        }

        public void SetSpellToEquip()
        {
            SpellChosenIndex.Value = index;
            SpellFocus.gameObject.SetActive(true);
        }
    }
}