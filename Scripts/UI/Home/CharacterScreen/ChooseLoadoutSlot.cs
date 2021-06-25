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
using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using PV3.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Home.CharacterScreen
{
    public class ChooseLoadoutSlot : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private ListOfSpellsObject ListOfSpells;
        [SerializeField] private IntValue LoadoutIndexToPutNewSpell;
        [SerializeField] private IntValue SpellChosenIndex;

        [Header("UI")]
        [SerializeField] private GameObject SpellChosenSlotObject;

        private SpellObject spell;

        public void InitializeSpellChosen()
        {
            spell = ListOfSpells.FindSpellByID(SpellChosenIndex.Value, Player.Class);
            SpellChosenSlotObject.GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = spell.sprite;
            SpellChosenSlotObject.GetComponent<SpellTooltipTrigger>().Spell = spell;
            SpellChosenSlotObject.GetComponent<SpellTooltipTrigger>().Character = Player;
        }

        public void PutNewSpellInLoadoutSlot()
        {
            Player.SpellsListObject.SpellsList[LoadoutIndexToPutNewSpell.Value].spell = spell;
        }
    }
}