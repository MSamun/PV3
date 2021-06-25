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
using System.Collections.Generic;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.ScriptableObjects.Character
{
    [CreateAssetMenu(fileName = "New Character Spells Object", menuName = "Character/Spells List*")]
    public class CharacterSpellsObject : ScriptableObject
    {
        public List<Spells> SpellsList;

        public void SetSpellOnCooldown(int index)
        {
            SpellsList[index].isOnCooldown = true;
            SpellsList[index].cooldownTimer = SpellsList[index].spell.totalCooldown;
        }

        public void DecrementSpellCooldownTimer(int index)
        {
            if (!IsSpellOnCooldown(index)) return;
            SpellsList[index].cooldownTimer--;

            if (SpellsList[index].cooldownTimer != 0) return;
            SpellsList[index].isOnCooldown = false;
        }

        public bool IsSpellOnCooldown(int index)
        {
            return SpellsList[index].isOnCooldown;
        }

        public void ResetSpellCooldowns()
        {
            for (var i = 0; i < SpellsList.Count; i++)
            {
                SpellsList[i].isOnCooldown = false;
                SpellsList[i].cooldownTimer = 0;
            }
        }

        [Serializable] public class Spells
        {
            public SpellObject spell;
            public bool isOnCooldown;
            public int cooldownTimer;
        }
    }
}