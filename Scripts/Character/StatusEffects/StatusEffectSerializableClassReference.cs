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

using PV3.ScriptableObjects.Game;

namespace PV3.Character.StatusEffects
{
    [System.Serializable]
    public class StatusEffect
    {
        public StatusType type;
        public int bonusAmount;
        public int duration;
        public bool isDebuff;
        public bool isPercentage;
        public bool inUse;
        public bool isUnique;

        public StatusEffect(StatusType type = StatusType.Damage, int bonusAmount = 0, int duration = 0, bool isDebuff = false, bool isPercentage = false, bool isUnique = false, bool inUse = false)
        {
            this.type = type;
            this.bonusAmount = bonusAmount;
            this.duration = duration;
            this.isDebuff = isDebuff;
            this.isPercentage = isPercentage;
            this.inUse = inUse;
            this.isUnique = isUnique;
        }
    }
}