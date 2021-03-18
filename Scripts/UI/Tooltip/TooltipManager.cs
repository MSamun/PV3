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
using PV3.UI.Tooltip.Spell;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public class TooltipManager : MonobehaviourReference
    {
        private static TooltipManager Instance;

        [SerializeField] private Tooltip tooltip;
        [SerializeField] private SpellTooltip spellTooltip;

        private void Awake()
        {
            Instance = this;
        }

        // Used for non-Spell Tooltips (Attributes, Buttons in Home Screen, or any UI Component that needs more information.)
        public static void SetTooltipText(string content, string header)
        {
            Instance.tooltip.gameObject.SetActive(true);
            Instance.tooltip.SetText(content, header);
        }

        // Used for Spell Tooltips.
        public static void SetTooltipText(string content, string cooldown, string header)
        {
            Instance.spellTooltip.gameObject.SetActive(true);
            Instance.spellTooltip.SetSpellText(content, cooldown, header);
        }

        public static void SetPivotPoint(PivotHorizontal pivotHorizontal, PivotVertical pivotVertical, bool isSpellTooltip)
        {
            if (isSpellTooltip)
            {
                Instance.spellTooltip.SetPivotPoint(pivotHorizontal, pivotVertical);
            }
            else
            {
                Instance.tooltip.SetPivotPoint(pivotHorizontal, pivotVertical);
            }
        }

        public static void DisplayTooltip(Vector3 position, bool isSpellTooltip)
        {
            if (isSpellTooltip)
            {
                Instance.spellTooltip.SetPosition(position);
            }
            else
            {
                Instance.tooltip.SetPosition(position);
            }
        }
    }
}