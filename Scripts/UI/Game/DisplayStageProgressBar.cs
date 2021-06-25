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
using UnityEngine;

namespace PV3.UI.Game
{
    public class DisplayStageProgressBar : MonobehaviourReference
    {
        [Header("Stage Info Objects")]
        [SerializeField] private StageListObject listOfStagesObject;
        [SerializeField] private IntValue stageListIndex;

        [Header("Progress Bars")]
        [SerializeField] private GameObject nonBossProgressBar;

        [SerializeField] private GameObject bossProgressBar;

        public void Initialize()
        {
            nonBossProgressBar.gameObject.SetActive(!listOfStagesObject.listOfStages[stageListIndex.Value].Stage.hasBoss);
            bossProgressBar.gameObject.SetActive(listOfStagesObject.listOfStages[stageListIndex.Value].Stage.hasBoss);
        }
    }
}