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

namespace PV3.Game
{
    public class GameManager : MonobehaviourReference
    {
        [SerializeField] private IntValue StageListIndex;

        [Header("UI Panels")]
        [SerializeField] private GameObject VictoryScreen;

        [SerializeField] private GameObject DefeatScreen;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnStartStageVictoryEvent;

        [SerializeField] private GameEventObject OnStartStageDefeatEvent;
        [SerializeField] private GameEventObject OnStartStageInitializationEvent;

        [Header("Character Game Events")]
        [SerializeField] private GameEventObject OnPlayerStartTurnEvent;

        [SerializeField] private GameEventObject OnEnemyStartTurnEvent;
        [SerializeField] private GameEventObject OnMoveToNextEnemyEvent;
        [SerializeField] private GameEventObject OnStartBossEncounterInitializationEvent;

        private void Start()
        {
            GameStateManager.StartMatch();
            StageListIndex.Value = DataManager.LoadProgressionDataFromJson().StageData.ChosenStage - 1;
            OnStartStageInitializationEvent.Raise();
        }

        public void StartPlayerTurn()
        {
            GameStateManager.SwitchToPlayerTurn();
            OnPlayerStartTurnEvent.Raise();
        }

        public void StartEnemyTurn()
        {
            GameStateManager.SwitchToEnemyTurn();
            OnEnemyStartTurnEvent.Raise();
        }

        public void MoveToNextEnemy()
        {
            GameStateManager.SwitchToNextEnemy();
            OnMoveToNextEnemyEvent.Raise();
        }

        public void StartBossEncounterInitialization()
        {
            GameStateManager.SwitchToBossEncounter();
        }

        public void StartVictoryState()
        {
            GameStateManager.SwitchToVictory();
            OnStartStageVictoryEvent.Raise();
        }

        public void StartDefeatState()
        {
            GameStateManager.SwitchToDefeat();
            OnStartStageDefeatEvent.Raise();
        }

        public void DisplayVictoryScreen()
        {
            VictoryScreen.gameObject.SetActive(true);

            var newHighestStage = DataManager.LoadProgressionDataFromJson().StageData.ChosenStage;
            var data = new StageData(newHighestStage, newHighestStage + 1);
            DataManager.UpdateProgressionStageData(data);
        }

        public void DisplayDefeatScreen()
        {
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