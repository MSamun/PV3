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

namespace PV3.Game
{
    public static class GameStateManager
    {
        public enum GameState
        {
            Start,
            PlayerTurn,
            EnemyTurn,
            NextEnemy,
            BossEncounter,
            Victory,
            Defeat
        }

        public static GameState CurrentGameState { get; private set; }
        public static EnemyObject CurrentEnemy { get; private set; }
        public static int CurrentEnemyLevel { get; private set; }

        public static void SetCurrentEnemy(EnemyObject enemy)
        {
            CurrentEnemy = enemy;
        }

        public static void SetCurrentEnemyLevel(int level)
        {
            CurrentEnemyLevel = level;
        }

        public static void StartMatch()
        {
            CurrentGameState = GameState.Start;
        }

        public static void SwitchToPlayerTurn()
        {
            CurrentGameState = GameState.PlayerTurn;
        }

        public static void SwitchToEnemyTurn()
        {
            CurrentGameState = GameState.EnemyTurn;
        }

        public static void SwitchToNextEnemy()
        {
            CurrentGameState = GameState.NextEnemy;
        }

        public static void SwitchToBossEncounter()
        {
            CurrentGameState = GameState.BossEncounter;
        }

        public static void SwitchToVictory()
        {
            CurrentGameState = GameState.Victory;
        }

        public static void SwitchToDefeat()
        {
            CurrentGameState = GameState.Defeat;
        }
    }
}