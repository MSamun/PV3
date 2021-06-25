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
using PV3.ScriptableObjects.Stages;
using TMPro;
using UnityEngine;

namespace PV3.UI.Home.LevelSelect
{
    public class StageLevelUI : MonobehaviourReference
    {
        public StageInfoObject Stage;
        [SerializeField] private IntValue StageIndex;

        [Header("Locked UI Components")]
        [SerializeField] private GameObject lockedPanelObject;

        [SerializeField] private GameObject lockedPanelBossIcon;

        [Header("Current UI Components")]
        [SerializeField] private GameObject currentPanelObject;

        [SerializeField] private GameObject currentPanelBossIcon;
        [SerializeField] private TextMeshProUGUI currentPanelStageLevelText;

        [Header("Completed UI Components")]
        [SerializeField] private GameObject completedPanelObject;

        [SerializeField] private GameObject completedPanelBossIcon;
        [SerializeField] private TextMeshProUGUI completedPanelStageLevelText;
        private int index;

        public void Initialize(StageInfoObject stage, int currentStage)
        {
            if (!stage)
            {
                gameObject.SetActive(false);
                return;
            }

            Stage = stage;
            index = stage.stageID;

            CheckIfPanelNeedsBossIcon();
            SetStageLevelText();
            DetermineWhichPanelToDisplay(currentStage);
        }

        private void CheckIfPanelNeedsBossIcon()
        {
            lockedPanelBossIcon.gameObject.SetActive(Stage.hasBoss);
            currentPanelBossIcon.gameObject.SetActive(Stage.hasBoss);
            completedPanelBossIcon.gameObject.SetActive(Stage.hasBoss);
        }

        private void SetStageLevelText()
        {
            currentPanelStageLevelText.text = Stage.stageID.ToString();
            completedPanelStageLevelText.text = Stage.stageID.ToString();
        }

        private void DetermineWhichPanelToDisplay(int currentStage)
        {
            lockedPanelObject.gameObject.SetActive(index > currentStage);
            currentPanelObject.gameObject.SetActive(index == currentStage);
            completedPanelObject.gameObject.SetActive(index < currentStage);
        }

        public void SetStageIndex()
        {
            StageIndex.Value = index - 1;
        }
    }
}