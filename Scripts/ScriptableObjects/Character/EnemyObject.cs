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
    public enum Stance { Aggressive, Defensive, Balanced }

    [CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy")]
    public class EnemyObject : CharacterObject
    {
        [Header("")]
        public Stance Stance;

        [Header("")]
        // Determines the order in which the Enemy would like to attack the Player. It'll prioritize the use of Spell-types in this array, and in the order the Spell-types are in.
        public SpellType[] orderOfSpellTypes = new SpellType[0];
    }
}