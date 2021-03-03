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
using PV3.Serialization;
using PV3.UI.Tooltip.Spell;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home
{
    public class DisplayCurrentSpellLoadout : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;

        [Header("")]
        [SerializeField] private GameObject[] SpellObjects;

        private void Awake()
        {
            for (var i = 0; i < SpellObjects.Length; i++)
            {
                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Character = PlayerObject;
            }
        }

        public void InitializeSpellObjects()
        {
            for (var i = 0; i < SpellObjects.Length; i++)
            {
                SpellObjects[i].GetComponent<SpellTooltipTrigger>().Spell = PlayerObject.ListOfSpells[i + 1].spell;
                SpellObjects[i].GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = PlayerObject.ListOfSpells[i + 1].spell.sprite;
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < PlayerObject.ListOfSpells.Count; i++)
            {
                var spellData = new SpellData(PlayerObject.ListOfSpells[i].spell.spellID);
                DataManager.UpdatePlayerSpellData(spellData, i);
            }

            DataManager.SaveDataToJson();
        }
    }
}