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

using PV3.ScriptableObjects.Characters;

namespace PV3.Game
{
    public enum GameState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        NextEnemy,
        Victory,
        Defeat
    }

    public static class GameStateManager
    {
        public static GameState CurrentGameState { get; private set; }
        public static EnemyObject CurrentEnemy { get; private set; }
        public static int CurrentEnemyLevel { get; private set; }

        public static void SetEnemy(EnemyObject enemy, int level)
        {
            CurrentEnemy = enemy;
            CurrentEnemyLevel = level;
        }

        public static void SetGameState(GameState state)
        {
            CurrentGameState = state;
        }
    }
}