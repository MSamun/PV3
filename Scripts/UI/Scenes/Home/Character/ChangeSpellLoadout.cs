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
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using PV3.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home.Character
{
    public class ChangeSpellLoadout : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private GameObject SpellChosenObject;

        [Header("Spell Components")] [SerializeField]
        private ListOfSpellsObject ListOfSpellInGame;

        // This is the index number for the Spell that you want to replace in the Player's Spell Loadout.
        [SerializeField] private IntValue SpellChangeIndex;
        [SerializeField] private IntValue SpellChosenID;

        private SpellObject _spell;

        public void PopulateSpellChosenUI()
        {
            _spell = ListOfSpellInGame.FindSpellByID(SpellChosenID.Value, Player.Class);
            SpellChosenObject.GetComponentInChildren<Button>(true).GetComponent<Image>().sprite = _spell.sprite;
            SpellChosenObject.GetComponentInChildren<SpellTooltipTrigger>().Spell = _spell;
        }

        public void ReplaceSpellInLoadout()
        {
            Player.SpellsListObject.SpellsList[SpellChangeIndex.Value].Spell = _spell;
        }
    }
}