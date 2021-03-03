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
            var playerSpellJsonData = DataManager.LoadDataFromJson().PlayerData.SpellData;
            var playerCombatClass = DataManager.LoadDataFromJson().PlayerData.BaseData.CombatClassID;

            for (var i = 0; i < PlayerObject.ListOfSpells.Count; i++)
            {
                PlayerObject.ListOfSpells[i].spell = ListOfSpellsObject.FindSpellByID(playerSpellJsonData[i].SpellID, (CombatClass)playerCombatClass);
            }

            OnLoadPlayerSpellsFromJsonEvent.Raise();
        }
    }
}