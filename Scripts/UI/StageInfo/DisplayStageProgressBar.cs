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

using PV3.Character;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.StageInfo
{
    [RequireComponent(typeof(DetermineCurrentEnemyInStage))]
    public class DisplayStageProgressBar : MonobehaviourReference
    {
        private DetermineCurrentEnemyInStage stageInfo;

        [Header("Progress Bars")]
        [SerializeField] private GameObject nonBossProgressBar;
        [SerializeField] private GameObject bossProgressBar;

        public void Initialize()
        {
            stageInfo = GetComponent<DetermineCurrentEnemyInStage>();

            if (stageInfo.ListOfStagesObject.listOfStages[stageInfo.StageListIndex.Value].Stage.hasBoss)
            {
                nonBossProgressBar.gameObject.SetActive(false);
                bossProgressBar.gameObject.SetActive(true);

            }
            else
            {
                bossProgressBar.gameObject.SetActive(false);
                nonBossProgressBar.gameObject.SetActive(true);
            }
        }
    }
}