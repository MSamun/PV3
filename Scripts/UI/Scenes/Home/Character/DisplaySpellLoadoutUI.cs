// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021-2022 Matthew Samun.
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
using PV3.ScriptableObjects.Characters;
using PV3.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home.Character
{
    public class DisplaySpellLoadoutUI : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;

        [Header("")] [SerializeField] private GameObject[] SpellLoadoutObjects;

        public void Initialize()
        {
            if (SpellLoadoutObjects.Length <= 0) return;

            for (var i = 0; i < SpellLoadoutObjects.Length; i++)
            {
                if (!PlayerObject.SpellsListObject.SpellsList[i].Spell) continue;

                var tooltipTrigger = SpellLoadoutObjects[i].GetComponent<SpellTooltipTrigger>();
                tooltipTrigger.Spell = PlayerObject.SpellsListObject.SpellsList[i].Spell;

                SpellLoadoutObjects[i].GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = PlayerObject.SpellsListObject.SpellsList[i].Spell.sprite;
            }
        }
    }
}