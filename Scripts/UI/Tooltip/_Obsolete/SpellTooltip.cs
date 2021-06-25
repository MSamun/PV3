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

using TMPro;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    [ExecuteInEditMode]
    public class SpellTooltip : Tooltip
    {
        [SerializeField] private TextMeshProUGUI spellCooldownText;

        public void SetSpellText(string content, string cooldownAmount, string header = "")
        {
            spellCooldownText.text = cooldownAmount;
            SetText(content, header);
        }

        protected override void CheckIfNeedLayoutElement()
        {
            if (headerText.text == null || spellCooldownText.text == null) return;

            layoutElement.enabled = headerText.text.Length + spellCooldownText.text.Length > characterWrapLimit || contentText.text.Length > characterWrapLimit;
        }
    }
}