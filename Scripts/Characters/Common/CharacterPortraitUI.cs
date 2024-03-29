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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Characters.Common
{
    public abstract class CharacterPortraitUI : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;

        [Header("UI Components")]
        [SerializeField] protected Image Icon;

        [SerializeField] protected TextMeshProUGUI LevelText;
        [SerializeField] protected TextMeshProUGUI NameText;

        public void InitializePortrait()
        {
            PopulateUIComponents();
            Character.Initialize();
        }

        public virtual void PopulateUIComponents()
        {
            Icon.sprite = Character.PortraitSprite;
            NameText.text = Character.Name;
            LevelText.text = Character.Level.Value.ToString();
        }

        public void RecoverStamina()
        {
            Character.AddStamina(50);
        }
    }
}