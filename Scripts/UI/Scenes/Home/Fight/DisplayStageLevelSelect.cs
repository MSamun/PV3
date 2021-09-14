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
using PV3.Serialization;
using UnityEngine;

namespace PV3.UI.Scenes.Home.Fight
{
    public class DisplayStageLevelSelect : MonobehaviourReference
    {
        [SerializeField] private StageListScriptableObject StageList;

        private StageLevelUI[] _amountOfStages;

        private void Awake()
        {
            _amountOfStages = GetComponentsInChildren<StageLevelUI>();
            int currentStageIndex = DataManager.LoadProgressionDataFromJson().StageData.HighestStageAvailable;

            if (_amountOfStages.Length <= 0)
            {
                Debug.LogError("<color=red>ERROR:</color> _amountOfStages is NULL in DisplayStageLevelSelect.cs. Ignoring request to populate the individual stages in Stage Select...");
                return;
            }

            for (var i = 0; i < _amountOfStages.Length; i++)
            {
                if (i < StageList.ListOfStages.Count)
                {
                    _amountOfStages[i].Initialize(StageList.ListOfStages[i].Stage, currentStageIndex);
                }
                else
                {
                    _amountOfStages[i].gameObject.SetActive(false);
                }

            }
        }
    }
}