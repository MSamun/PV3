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

namespace PV3.ScriptableObjects.Spells
{
    public enum AttributeType
    {
        None,
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Armor
    }

    public abstract class SpellComponentObject : ScriptableObject
    {
        [Header("Base Information")]
        [Range(0, 100)] public int minimumValue = 3;

        [Range(0, 100)] public int maximumValue = 6;
        public bool usePercentage;

        [Header("Attribute Percentage")]
        public AttributeType attributeType;

        [Range(0, 1)] public float attributePercentage;

        public int GetRandomValueBetweenRange()
        {
            if (maximumValue < minimumValue) maximumValue = minimumValue++;

            // maximumValue is exclusive, so you add one.
            return Random.Range(minimumValue, maximumValue + 1);
        }

        public int GetPercentageOfValue(int value)
        {
            return value <= 0 || !usePercentage ? 0 : Mathf.RoundToInt(value * (GetRandomValueBetweenRange() / 100f));
        }
    }
}