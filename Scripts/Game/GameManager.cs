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

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace PV3.Game
{
    public class GameManager : MonobehaviourReference
    {
        [SerializeField] private IntValue StageIndex;

        [Header("UI Panels")]
        [SerializeField] private GameObject VictoryScreen;
        [SerializeField] private GameObject DefeatScreen;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnStageVictoryEvent;
        [SerializeField] private GameEventObject OnStageDefeatEvent;

        [Header("Character Game Events")]
        [SerializeField] private GameEventObject OnPlayerStartTurnEvent;
        [SerializeField] private GameEventObject OnEnemyStartTurnEvent;
        [SerializeField] private GameEventObject OnMoveToNextEnemyEvent;

        private void Start()
        {
            if (!StageIndex)
            {
                Debug.LogError("<color=red>ERROR:</color> StageListIndex is NULL in GameManager.cs. Ignoring request to set the index of the Stage...");
                return;
            }

            GameStateManager.SetGameState(GameState.Start);
            StageIndex.Value = DataManager.LoadProgressionDataFromJson().StageData.ChosenStage - 1;
        }

        public void StartPlayerTurn()
        {
            if (!OnPlayerStartTurnEvent)
            {
                Debug.LogError("<color=red>ERROR:</color> OnPlayerStartTurnEvent is NULL in GameManager.cs. Ignoring request to start the Player's turn...");
                return;
            }

            GameStateManager.SetGameState(GameState.PlayerTurn);
            OnPlayerStartTurnEvent.Raise();
        }

        public void StartEnemyTurn()
        {
            if (!OnEnemyStartTurnEvent)
            {
                Debug.LogError("<color=red>ERROR:</color> OnEnemyStartTurnEvent is NULL in GameManager.cs. Ignoring request to start the Enemy's turn...");
                return;
            }

            GameStateManager.SetGameState(GameState.EnemyTurn);
            OnEnemyStartTurnEvent.Raise();
        }

        public void MoveToNextEnemy()
        {
            if (!OnMoveToNextEnemyEvent)
            {
                Debug.LogError("<color=red>ERROR:</color> OnMoveToNextEnemyEvent is NULL in GameManager.cs. Ignoring request to go to the next Enemy in Stage...");
                return;
            }

            GameStateManager.SetGameState(GameState.NextEnemy);
            OnMoveToNextEnemyEvent.Raise();
        }

        public void StartVictoryState()
        {
            if (!OnStageVictoryEvent)
            {
                Debug.LogError("<color=red>ERROR:</color> OnStartStageVictoryEvent is NULL in GameManager.cs. Ignoring request to initiate the Player's victory...");
                return;
            }

            GameStateManager.SetGameState(GameState.Victory);
            OnStageVictoryEvent.Raise();
        }

        public void StartDefeatState()
        {
            if (!OnStageDefeatEvent)
            {
                Debug.LogError("<color=red>ERROR:</color> OnStartStageDefeatEvent is NULL in GameManager.cs. Ignoring request to initiate the Player's defeat...");
                return;
            }

            GameStateManager.SetGameState(GameState.Defeat);
            OnStageDefeatEvent.Raise();
        }

        public void DisplayVictoryScreen()
        {
            var newHighestStage = DataManager.LoadProgressionDataFromJson().StageData.ChosenStage;
            var data = new StageData(newHighestStage, newHighestStage + 1);
            DataManager.UpdateProgressionStageData(data);

            if (!VictoryScreen)
            {
                Debug.LogError("<color=red>ERROR:</color> VictoryScreen is NULL in GameManager.cs. Ignoring request to display the Victory Panel...");
                return;
            }

            VictoryScreen.gameObject.SetActive(true);
        }

        public void DisplayDefeatScreen()
        {
            if (!DefeatScreen)
            {
                Debug.LogError("<color=red>ERROR:</color> DefeatScreen is NULL in GameManager.cs. Ignoring request to display the Defeat Panel...");
                return;
            }

            DefeatScreen.gameObject.SetActive(true);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}