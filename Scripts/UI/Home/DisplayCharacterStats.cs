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

using System;
using PV3.Character;
using PV3.Miscellaneous;
using PV3.Serialization;
using TMPro;
using UnityEngine;

namespace PV3.UI.Home
{
    public class DisplayCharacterStats : MonobehaviourReference
    {
        [SerializeField] private PlayerObject PlayerObject;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI combatClassText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI dexterityText;
        [SerializeField] private TextMeshProUGUI constitutionText;
        [SerializeField] private TextMeshProUGUI intelligenceText;
        [SerializeField] private TextMeshProUGUI armorText;

        [Header("")]
        [SerializeField] private TextMeshProUGUI blockChanceText;
        [SerializeField] private TextMeshProUGUI dodgeChanceText;
        [SerializeField] private TextMeshProUGUI criticalChanceText;
        [SerializeField] private TextMeshProUGUI damageReductionText;

        public void InitializeUIComponents()
        {
            InitializeCombatClassAndAttributesText();
            InitializeBonusesText();
        }

        private void InitializeCombatClassAndAttributesText()
        {
            combatClassText.text = PlayerObject.Class.ToString();
            strengthText.text = PlayerObject.Attributes.Strength.ToString();
            dexterityText.text = PlayerObject.Attributes.Dexterity.ToString();
            constitutionText.text = PlayerObject.Attributes.Constitution.ToString();
            intelligenceText.text = PlayerObject.Attributes.Intelligence.ToString();
            armorText.text = PlayerObject.Attributes.Armor.ToString();
        }

        private void InitializeBonusesText()
        {
            blockChanceText.text = $"{PlayerObject.BlockChance.ToString()}%";
            dodgeChanceText.text = $"{PlayerObject.DodgeChance.ToString()}%";
            criticalChanceText.text = $"{PlayerObject.CriticalChance.ToString()}%";
            damageReductionText.text = $"{PlayerObject.DamageReduction.ToString()}%";
        }
    }
}