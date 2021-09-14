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
using UnityEngine.Serialization;

namespace PV3.Game
{
    public class DetermineCurrentEnemyInStage : MonobehaviourReference
    {
        [SerializeField] private StageListScriptableObject StageList;
        [SerializeField] private IntValue StageIndex;
        [SerializeField] private IntValue CurrentEnemyIndex;

        public void SetCurrentEnemy()
        {
            if (!StageList || !StageIndex || !CurrentEnemyIndex)
            {
                Debug.LogError("<color=red>ERROR:</color> StageList, StageIndex, and/or CurrentEnemyIndex is NULL in DetermineCurrentEnemyInStage.cs." +
                               " Ignoring request to populate the current Enemy and its level...");
                return;
            }

            GameStateManager.SetEnemy(StageList.ListOfStages[StageIndex.Value].Stage.ListOfEnemies[CurrentEnemyIndex.Value].Enemy,
                StageList.ListOfStages[StageIndex.Value].Stage.ListOfEnemies[CurrentEnemyIndex.Value].Level);
        }
    }
}