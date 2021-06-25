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

using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.ScriptableObjects.UI
{
    // The asterisk (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Turn Notification", menuName = "Game/UI/Turn Notification*")]
    public class TurnNotificationObject : ScriptableObject
    {
        private const string PLAYER_COLOR_TEXT = "#96FF00";
        private const string ENEMY_COLOR_TEXT = "#E13232";
        private const string SPELL_COLOR_TEXT = "#FFA000";

        [SerializeField] private GameEventObject OnDisplayTurnNotificationEvent;

        [Header("")]
        [TextArea(3, 6)] public string Description;

        public void UpdateDescription(string caster = "", string target = "", string spell = "", int value = 0, bool isCasterPlayer = false, bool isHealSpell = false)
        {
            if (string.IsNullOrEmpty(caster) || string.IsNullOrEmpty(target) || string.IsNullOrEmpty(spell)) return;

            // Value of 0 means that it is strictly a Status Effect Spell.
            if (value == 0)
            {
                Description = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{caster}</color> used <color={SPELL_COLOR_TEXT}>{spell}</color>.";
            }
            else
            {
                if (!isHealSpell)
                    Description = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{caster}</color> used <color={SPELL_COLOR_TEXT}>{spell}</color> on " +
                                  $"<color={(isCasterPlayer ? ENEMY_COLOR_TEXT : PLAYER_COLOR_TEXT)}>{target}</color>, " +
                                  $"dealing <color={ENEMY_COLOR_TEXT}>{value.ToString()} Damage</color>.";
                else
                    Description = $"<color={(isCasterPlayer ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{caster}</color> used <color={SPELL_COLOR_TEXT}>{spell}</color>, " +
                                  $"healing for <color={PLAYER_COLOR_TEXT}>{value.ToString()} Health</color>.";
            }

            OnDisplayTurnNotificationEvent.Raise();
        }


        public void UpdateDescriptionToStunned(CharacterObject character)
        {
            Description = $"<color={(character is PlayerObject ? PLAYER_COLOR_TEXT : ENEMY_COLOR_TEXT)}>{character.name}</color> is <color=#AF00FF>stunned</color>! Skipping turn...";
            OnDisplayTurnNotificationEvent.Raise();
        }

        public void ResetDescription()
        {
            Description = string.Empty;
        }
    }
}