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

using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.ScriptableObjects.Character
{
    // The asterisk (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Character Bonus", menuName = "Character/Bonus*")]
    public class CharacterGameBonuses : ScriptableObject
    {
        public int BlockBonus { get; private set; }
        public int DodgeBonus { get; private set; }
        public int DamageBonus { get; private set; }
        public int CriticalBonus { get; private set; }
        public int DamageReductionBonus { get; private set; }
        public int LifestealBonus { get; private set; }

        public void InfluenceBonus(bool isEffectDebuff = false, StatusType type = StatusType.Damage, int amount = 0)
        {
            if (amount == 0) return;

            if (type == StatusType.Damage)
                DamageBonus = isEffectDebuff ? DamageBonus -= amount : DamageBonus += amount;
            else if (type == StatusType.Block)
                BlockBonus = isEffectDebuff ? BlockBonus -= amount : BlockBonus += amount;
            else if (type == StatusType.Dodge)
                DodgeBonus = isEffectDebuff ? DodgeBonus -= amount : DodgeBonus += amount;
            else if (type == StatusType.DamageReduction)
                DamageReductionBonus = isEffectDebuff ? DamageReductionBonus -= amount : DamageReductionBonus += amount;
            else if (type == StatusType.Critical)
                CriticalBonus = isEffectDebuff ? CriticalBonus -= amount : CriticalBonus += amount;
            else if (type == StatusType.Lifesteal)
                LifestealBonus = isEffectDebuff ? LifestealBonus -= amount : LifestealBonus += amount;
        }

        public void ResetAllBonusesToZero()
        {
            DamageBonus = 0;
            DodgeBonus = 0;
            BlockBonus = 0;
            DamageReductionBonus = 0;
            CriticalBonus = 0;
            LifestealBonus = 0;
        }

        public void ResetBonusToZero(StatusType type)
        {
            if (type == StatusType.Damage)
                DamageBonus = 0;
            else if (type == StatusType.Block)
                BlockBonus = 0;
            else if (type == StatusType.Dodge)
                DodgeBonus = 0;
            else if (type == StatusType.DamageReduction)
                DamageReductionBonus = 0;
            else if (type == StatusType.Critical)
                CriticalBonus = 0;
            else if (type == StatusType.Lifesteal)
                LifestealBonus = 0;
        }
    }
}