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
using PV3.ScriptableObjects.GameEvents;
using UnityEngine;

namespace PV3.Serialization
{
    public class LoadPlayerSpellsFromJSON : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;
        [SerializeField] private ListOfSpellsObject ListOfSpellsObject;

        [Header("Game Event")]
        [SerializeField] private GameEventObject OnLoadPlayerSpellsFromJsonEvent;

        private void OnEnable()
        {
            InitializePlayerSpells();
        }

        public void InitializePlayerSpells()
        {
            var playerData = DataManager.LoadPlayerDataFromJson();

            PlayerObject.ListOfSpells[0].spell = ListOfSpellsObject.FindPotionByID(playerData.SpellData[0].SpellID);

            // Start at index #1 cause index 0 is reserved for Potions.
            for (var i = 1; i < PlayerObject.ListOfSpells.Count; i++)
            {
                PlayerObject.ListOfSpells[i].spell = ListOfSpellsObject.FindSpellByID(playerData.SpellData[i].SpellID, (CombatClass)playerData.BaseData.CombatClassID);
            }

            OnLoadPlayerSpellsFromJsonEvent.Raise();
        }
    }
}