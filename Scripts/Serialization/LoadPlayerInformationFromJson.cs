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
using PV3.ScriptableObjects.Home;
using PV3.ScriptableObjects.Spells;
using PV3.ScriptableObjects.UI;
using UnityEngine;

namespace PV3.Serialization
{
    public class LoadPlayerInformationFromJson : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private ListOfSpellsObject ListOfSpells;
        [SerializeField] private PortraitIconSpritesObject PortraitSprites;

        [Header("Game Event")]
        [SerializeField] private GameEventObject OnLoadPlayerSpellsFromJsonEvent;

        public void InitializePlayerBaseInformation()
        {
            BaseData data = DataManager.LoadPlayerDataFromJson().BaseData;

            Player.Name = data.Name;
            Player.Class = (CombatClass) data.CombatClassID;
            Player.PortraitSprite = PortraitSprites.Icons[data.PortraitID];
            Player.Level.Value = data.Level;
        }

        public void InitializePlayerSpells()
        {
            PlayerSaveData playerData = DataManager.LoadPlayerDataFromJson();

            for (var i = 0; i < Player.SpellsListObject.SpellsList.Count; i++)
            {
                SpellObject spell = ListOfSpells.FindSpellByID(playerData.SpellData[i].SpellID, Player.Class);
                if (spell != null) Player.SpellsListObject.SpellsList[i].Spell = spell;
            }

            OnLoadPlayerSpellsFromJsonEvent.Raise();
        }
    }
}