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
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Stages;
using PV3.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Scenes.Home
{
    public class DisplayConfirmStageUI : MonobehaviourReference
    {
        [Header("Stage Component Objects")]
        [SerializeField] private StageListScriptableObject StageList;

        [SerializeField] private IntValue StageIndex;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI StageTitleText;

        [SerializeField] private TextMeshProUGUI WaveCountText;
        [SerializeField] private Button StartStageButton;

        [Header("")]
        [SerializeField] private EnemyPortraitUIComponents[] EnemyPortraits;

        private StageInfoObject _localStage;

        private void OnEnable()
        {
            if (!StageList || !StageIndex) return;

            _localStage = StageList.ListOfStages[StageIndex.Value].Stage;
            InitializePanel();

            // [INSERT MISSION DECLARATIONS HERE]
        }

        private void InitializePanel()
        {
            SetStageTitle();

            if (_localStage.ListOfEnemies.Count <= 0)
            {
                WaveCountText.text = $"<color=red>No Waves Found</color>";

                for (var i = 0; i < EnemyPortraits.Length; i++)
                    EnemyPortraits[i].Container.gameObject.SetActive(false);

                StartStageButton.interactable = false;

                Debug.LogWarning($"<color=yellow>WARNING:</color> Stage #{(StageIndex.Value + 1).ToString()} has no Enemies assigned to it. Ignoring request to populate Portraits...", this);
                return;
            }

            StartStageButton.interactable = true;
            SetWaveCount();
            SetEnemyPortraits();
        }

        private void SetStageTitle()
        {
            StageTitleText.text = $"Stage {(StageIndex.Value < 9 ? "0" : string.Empty)}{(StageIndex.Value + 1).ToString()}";
        }

        private void SetWaveCount()
        {
            // Since Non-Boss Stages have 3 enemies and Boss Stages have 4 enemies, all we need to do is check if the Stage has a boss.
            // If, for whatever reason, this no longer becomes the case, we can simply check how many Enemies are in a Stage.
            WaveCountText.text = $"{(_localStage.HasBoss ? "Four" : "Three")} Waves";
        }

        private void SetEnemyPortraits()
        {
            if (!_localStage.HasBoss)
                EnemyPortraits[_localStage.ListOfEnemies.Count].Container.gameObject.SetActive(false);

            for (var i = 0; i < _localStage.ListOfEnemies.Count; i++)
            {
                EnemyPortraits[i].Container.gameObject.SetActive(true);
                EnemyPortraits[i].Icon.sprite = _localStage.ListOfEnemies[i].Enemy.PortraitSprite;
                EnemyPortraits[i].Level.text = _localStage.ListOfEnemies[i].Level.ToString();
            }
        }

        public void SaveStageIndexToJson()
        {
            var highestStageCompleted = DataManager.LoadProgressionDataFromJson().StageData.HighestStageAvailable;
            var stageData = new StageData(StageIndex.Value + 1, highestStageCompleted);

            DataManager.UpdateProgressionStageData(stageData);
        }

        [Serializable]
        public class EnemyPortraitUIComponents
        {
            public GameObject Container;
            public Image Icon;
            public TextMeshProUGUI Level;
        }
    }
}