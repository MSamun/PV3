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
using PV3.Serialization;
using TMPro;
using UnityEngine;

namespace PV3.UI.Scenes.Home.Character
{
    public class CharacterScreenManager : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;
        [SerializeField] private TextMeshProUGUI PlayerHealthBarText;

        [Header("Game Events")] [SerializeField]
        private GameEventObject OnCharacterScreenEnabledEvent;

        private void Start()
        {
            OnCharacterScreenEnabledEvent.Raise();
        }

        private void OnEnable()
        {
            OnCharacterScreenEnabledEvent.Raise();
        }

        private void OnDisable()
        {
            for (var i = 0; i < Player.SpellsListObject.SpellsList.Count; i++)
            {
                if (Player.SpellsListObject.SpellsList[i].Spell)
                {
                    DataManager.UpdatePlayerSpellData(Player.SpellsListObject.SpellsList[i].Spell.spellID, i);
                }
            }
        }

        public void SetPlayerHealthBarText()
        {
            PlayerHealthBarText.text = $"{Player.MaxHealth.Value.ToString()}";
        }
    }
}