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
using UnityEngine.Serialization;

namespace PV3.UI.Scenes.Home.Character
{
    public class CharacterStatsUI : MonobehaviourReference
    {
        [SerializeField] private PlayerObject Player;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI CombatClassText;

        [SerializeField] private TextMeshProUGUI StrengthText;
        [SerializeField] private TextMeshProUGUI DexterityText;
        [SerializeField] private TextMeshProUGUI ConstitutionText;
        [SerializeField] private TextMeshProUGUI IntelligenceText;
        [SerializeField] private TextMeshProUGUI ArmorText;

        [Header("")]
        [SerializeField] private TextMeshProUGUI BlockChanceText;
        [SerializeField] private TextMeshProUGUI DodgeChanceText;
        [SerializeField] private TextMeshProUGUI CriticalChanceText;
        [SerializeField] private TextMeshProUGUI DamageReductionText;

        public void Initialize()
        {
            PopulateCombatClassAndAttributesText();
            PopulateBonusesText();
        }

        private void PopulateCombatClassAndAttributesText()
        {
            CombatClassText.text = Player.Class.ToString();
            StrengthText.text = Player.Attributes.Strength.ToString();
            DexterityText.text = Player.Attributes.Dexterity.ToString();
            ConstitutionText.text = Player.Attributes.Constitution.ToString();
            IntelligenceText.text = Player.Attributes.Intelligence.ToString();
            ArmorText.text = Player.Attributes.Armor.ToString();
        }

        private void PopulateBonusesText()
        {
            BlockChanceText.text = $"{Player.BlockChance.ToString()}%";
            DodgeChanceText.text = $"{Player.DodgeChance.ToString()}%";
            CriticalChanceText.text = $"{Player.CriticalChance.ToString()}%";
            DamageReductionText.text = $"{Player.DamageReduction.ToString()}%";
        }
    }
}