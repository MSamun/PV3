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
using PV3.Miscellaneous;
using PV3.ScriptableObjects.GameEvents;
using PV3.ScriptableObjects.Variables;
using PV3.Serialization;
using UnityEngine;

namespace PV3.Managers
{
    public class GameManager : MonobehaviourReference
    {
        [SerializeField] private IntValue StageListIndex;
        [SerializeField] private GameEventObject OnLoadedStageIndexFromJSONEvent;

        [Header("UI Panels")]
        [SerializeField] private GameObject VictoryScreen;
        [SerializeField] private GameObject DefeatScreen;

        public void InitializeStageListIndexValueFromJson()
        {
            StageListIndex.Value = DataManager.LoadProgressionDataFromJson().StageData.ChosenStage - 1;
            OnLoadedStageIndexFromJSONEvent.Raise();
        }

        public void DisplayVictoryScreen()
        {
            VictoryScreen.gameObject.SetActive(true);
        }

        public void DisplayDefeatScreen()
        {
            PauseGame();
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