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

namespace PV3.UI.Scenes.Home.Character
{
    [RequireComponent(typeof(SpellTooltipTrigger))]
    public class CombatClassSlotBehaviour : MonobehaviourReference
    {
        [SerializeField] private IntValue SpellChosenID;

        [Header("UI")] [SerializeField] private Button Button;

        [SerializeField] private Image FocusBorder;
        [SerializeField] private Image EquippedIcon;
        public SpellObject Spell { get; private set; }

        public void Initialize(SpellObject spell, bool isEquipped)
        {
            if (!spell)
            {
                gameObject.SetActive(false);
                return;
            }

            Spell = spell;
            Button.GetComponent<Image>().sprite = spell.sprite;
            GetComponent<SpellTooltipTrigger>().Spell = spell;

            ToggleFocusBorder();
            ToggleEquippedIcon(isEquipped);
        }

        public void ToggleFocusBorder()
        {
            bool shouldEnable = !FocusBorder.gameObject.activeInHierarchy && SpellChosenID.Value == Spell.spellID;
            FocusBorder.gameObject.SetActive(shouldEnable);
        }

        public void ToggleEquippedIcon(bool isEquipped)
        {
            if (EquippedIcon)
            {
                EquippedIcon.gameObject.SetActive(isEquipped);
            }
        }

        public void SetSpellSlotAsSelected()
        {
            SpellChosenID.Value = Spell.spellID;
            ToggleFocusBorder();
        }
    }
}