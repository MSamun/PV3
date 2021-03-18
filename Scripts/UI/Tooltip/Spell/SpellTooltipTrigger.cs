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

using System;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.UI.SpellDescription;
using UnityEngine;

namespace PV3.UI.Tooltip.Spell
{
    public class SpellTooltipTrigger : MonobehaviourReference
    {
        public SpellObject Spell;
        public Character.CharacterObject Character;

        [Header("Pivot Points")]
        [SerializeField] protected PivotVertical PivotVertical;
        [SerializeField] protected PivotHorizontal PivotHorizontal;

        public void DisplayTooltip()
        {
            if (!Spell || !Character)
            {
                Debug.LogError($"SpellObject or CharacterObject is NULL. SPELL: {Spell}, CHARACTER: {Character}");
                return;
            }

            var spellDesc = SpellDescriptionManager.SetDescription(Spell, Character.Attributes);
            var cooldownDesc = $"{Spell.totalCooldown.ToString()} turn{(Spell.totalCooldown > 1 ? "s" : string.Empty)}";

            TooltipManager.SetTooltipText(spellDesc, cooldownDesc, Spell.name);
            TooltipManager.SetPivotPoint(PivotHorizontal, PivotVertical, true);
            TooltipManager.DisplayTooltip(transform.position, true);
        }
    }
}