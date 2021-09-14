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

namespace PV3.UI.Scenes.Home.Fight
{
    public class StageLevelUI : MonobehaviourReference
    {
        public StageInfoObject Stage;
        [SerializeField] private IntValue StageIndex;

        [Header("Locked UI Components")]
        [SerializeField] private GameObject LockedPanelObject;
        [SerializeField] private GameObject LockedPanelBossIcon;

        [Header("Current UI Components")]
        [SerializeField] private GameObject CurrentPanelObject;
        [SerializeField] private GameObject CurrentPanelBossIcon;
        [SerializeField] private TextMeshProUGUI CurrentPanelStageLevelText;

        [Header("Completed UI Components")]
        [SerializeField] private GameObject CompletedPanelObject;
        [SerializeField] private GameObject CompletedPanelBossIcon;
        [SerializeField] private TextMeshProUGUI CompletedPanelStageLevelText;

        private int _index;

        public void Initialize(StageInfoObject stage, int currentStage)
        {
            if (!stage)
            {
                gameObject.SetActive(false);
                return;
            }

            Stage = stage;
            _index = stage.StageID;

            DetermineWhichPanelTypeToDisplay(currentStage);
            SetStageLevelText();
            CheckIfPanelNeedsBossIcon();
        }

        private void CheckIfPanelNeedsBossIcon()
        {
            LockedPanelBossIcon.gameObject.SetActive(Stage.HasBoss);
            CurrentPanelBossIcon.gameObject.SetActive(Stage.HasBoss);
            CompletedPanelBossIcon.gameObject.SetActive(Stage.HasBoss);
        }

        private void SetStageLevelText()
        {
            CurrentPanelStageLevelText.text = Stage.StageID.ToString();
            CompletedPanelStageLevelText.text = Stage.StageID.ToString();
        }

        private void DetermineWhichPanelTypeToDisplay(int currentStage)
        {
            LockedPanelObject.gameObject.SetActive(_index > currentStage);
            CurrentPanelObject.gameObject.SetActive(_index == currentStage);
            CompletedPanelObject.gameObject.SetActive(_index < currentStage);
        }

        public void SetStageIndex()
        {
            StageIndex.Value = _index - 1;
        }
    }
}