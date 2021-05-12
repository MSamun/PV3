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

namespace PV3.Character
{
    // The asterik (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game,
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Attribute List", menuName = "Character/Attribute List*")]
    public class AttributesObject : ScriptableObject
    {
        [Header("---------- ATTRIBUTES ----------")]
        [SerializeField] [Range(1, 200)] private int strength;
        public int Strength
        {
            get => strength;
            set => strength = value;
        }

        [SerializeField] [Range(1, 200)] private int dexterity;
        public int Dexterity
        {
            get => dexterity;
            set => dexterity = value;
        }

        [SerializeField] [Range(1, 200)] private int constitution;
        public int Constitution
        {
            get => constitution;
            set => constitution = value;
        }

        [SerializeField] [Range(1, 200)] private int intelligence;
        public int Intelligence
        {
            get => intelligence;
            set => intelligence = value;
        }

        [SerializeField] [Range(1, 200)] private int armor;
        public int Armor
        {
            get => armor;
            set => armor = value;
        }
    }
}