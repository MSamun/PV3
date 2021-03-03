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
using PV3.ScriptableObjects.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Character.Portraits
{
    public abstract class CharacterPortraitUI : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;

        [Header("UI Components")]
        [SerializeField] protected Image Icon;
        [SerializeField] protected TextMeshProUGUI LevelText;
        [SerializeField] protected TextMeshProUGUI NameText;

        [Header("Game Events")]
        [SerializeField] protected GameEventObject OnAttributesCalculatedEvent;

        protected virtual void Start()
        {
            PopulateUIComponents();
            InitializeCharacterValues();
        }

        public virtual void PopulateUIComponents() { }

        protected void InitializeCharacterValues()
        {
            ResetSpellCooldowns();

            Character.StatusEffectObject.ResetStatusEffectList();
            Character.StatusEffectObject.BonusObject.ResetBonus();

            Character.InitializeHealthValues();
            Character.InitializeSubAttributes();

            // Health Bar gets initialized after Attributes of Character gets set.
            OnAttributesCalculatedEvent.Raise();
        }

        private void ResetSpellCooldowns()
        {
            for (var i = 0; i < Character.ListOfSpells.Count; i++)
            {
                Character.ListOfSpells[i].isOnCooldown = false;
                Character.ListOfSpells[i].cooldownTimer = 0;
            }
        }
    }
}