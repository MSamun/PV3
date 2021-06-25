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

using PV3.Game;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.Character.Spells
{
    public class EnemySpells : MonobehaviourReference
    {
        [SerializeField] private IntValue SpellIndex;
        [SerializeField] private GameEventObject OnSpellUseEvent;

        private EnemyObject _enemy;

        public void SetCurrentEnemy()
        {
            _enemy = GameStateManager.CurrentEnemy;
        }

        public void DecrementCooldownTimersOfAllSpells()
        {
            if (GameStateManager.CurrentGameState != GameStateManager.GameState.EnemyTurn) return;

            for (var i = 0; i < _enemy.SpellsListObject.SpellsList.Count; i++)
            {
                _enemy.SpellsListObject.DecrementSpellCooldownTimer(i);
            }
        }

        public void UseSpell()
        {
            _enemy.SpellsListObject.SetSpellOnCooldown(SpellIndex.Value);
            OnSpellUseEvent.Raise();
        }
    }
}