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

using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    public enum StatusType { Random, Damage, Block, Dodge, DamageReduction, Critical, Lifesteal, Linger, Regenerate, Stun}

    [CreateAssetMenu(fileName = "New Status Component", menuName = "Spell/Components/Status")]
    public class StatusComponent : SpellComponentObject
    {
        [Header("Duration of Status Effect")]
        [Range(1, 6)] public int duration = 2;

        [Header("Type of Status Effect")]
        public StatusType StatusType;
        public bool isDebuff;
        public bool isUnique;

        public StatusType ChooseRandomStatusType()
        {
            // minInclusive - maxExclusive
            return (StatusType)Random.Range(1, 7);
        }
    }
}