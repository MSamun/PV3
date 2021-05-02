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
using PV3.ScriptableObjects.Stages;
using PV3.ScriptableObjects.Variables;
using PV3.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.LevelSelect
{
    public class DisplayConfirmStageUI : MonobehaviourReference
    {
        [System.Serializable]
        public class EnemyPortraitUIComponents
        {
            public GameObject container;
            public Image icon;
            public TextMeshProUGUI level;
        }

        private StageInfoObject localStage;

        [Header("Stage Component Objects")]
        [SerializeField] private StageListObject ListOfStagesObject;
        [SerializeField] private IntValue StageIndex;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI stageTitleText;
        [SerializeField] private TextMeshProUGUI waveCountText;

        [Header("")]
        [SerializeField] private EnemyPortraitUIComponents[] enemyPortraits;

        private void OnEnable()
        {
            localStage = ListOfStagesObject.listOfStages[StageIndex.Value].Stage;

            SetStageTitle();
            SetWaveCount();
            SetEnemyPortraits();
            // [INSERT MISSION DECLARATIONS HERE]
        }

        private void SetStageTitle()
        {
            stageTitleText.text = $"Stage {(StageIndex.Value < 9 ? "0" : string.Empty)}{(StageIndex.Value + 1).ToString()}";;
        }

        private void SetWaveCount()
        {
            // Since Non-Boss Stages have 3 enemies and Boss Stages have 4 enemies, all we need to do is check if the Stage has a boss.
            // If this changes further into development, then I can simply replace this with a check to see how many enemies are in a Stage.
            waveCountText.text = $"{(localStage.hasBoss ? "Four" : "Three")} Waves";
        }

        private void SetEnemyPortraits()
        {
            if (!localStage.hasBoss) enemyPortraits[localStage.listOfEnemies.Count].container.gameObject.SetActive(false);

            for (var i = 0; i < localStage.listOfEnemies.Count; i++)
            {
                enemyPortraits[i].container.gameObject.SetActive(true);
                enemyPortraits[i].icon.sprite = localStage.listOfEnemies[i].enemy.portraitSprite;
                enemyPortraits[i].level.text = localStage.listOfEnemies[i].level.ToString();
            }
        }

        public void SaveStageIndexToJson()
        {
            var highestStageCompleted = DataManager.LoadProgressionDataFromJson().StageData.HighestStageAvailable;
            var stageData = new StageData(StageIndex.Value + 1, highestStageCompleted);

            DataManager.UpdateProgressionStageData(stageData);
        }
    }
}