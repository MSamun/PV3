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
using UnityEngine.UI;

namespace PV3.UI.StageInfo
{
    public class StageProgressBar : MonobehaviourReference
    {
        [System.Serializable]
        private class ProgressBarPointers
        {
            public GameObject pointerFocus;
            public GameObject pointerUnfocus;
        }

        private DetermineCurrentEnemyInStage stageInfoScript;

        [SerializeField] private Slider slider;
        [Range(0.05f, 0.10f)] [SerializeField] private float barLerpSpeed = 0.075f;

        [Header("")]
        [SerializeField] private ProgressBarPointers[] Pointers = new ProgressBarPointers[0];
        private void Start()
        {
            stageInfoScript = GetComponentInParent<DetermineCurrentEnemyInStage>();

            slider.minValue = 0;
            slider.maxValue = stageInfoScript.ListOfStagesObject.listOfStages[stageInfoScript.StageListIndex.Value].Stage.listOfEnemies.Count - 1;
            slider.value = stageInfoScript.CurrentEnemyIndex.Value;

            UpdateBarPointer();
        }

        private void FixedUpdate()
        {
            if (!Mathf.Approximately(slider.value, stageInfoScript.CurrentEnemyIndex.Value))
            {
                UpdateBar();
            }
        }

        private void UpdateBar()
        {
            // Stage Bar has filled the progress bar more than it should, so you subtract to match them.
            // This method is in FixedUpdate to smoothly increase the bar, rather than an instant change in value.
            if (slider.value > stageInfoScript.CurrentEnemyIndex.Value)
            {
                slider.value -= barLerpSpeed;
                slider.value = Mathf.Clamp(slider.value, stageInfoScript.CurrentEnemyIndex.Value, slider.maxValue);
            }
            else
            {
                slider.value += barLerpSpeed;
            }

            UpdateBarPointer();
        }

        private void UpdateBarPointer()
        {
            Pointers[stageInfoScript.CurrentEnemyIndex.Value].pointerFocus.gameObject.SetActive(true);
            Pointers[stageInfoScript.CurrentEnemyIndex.Value].pointerUnfocus.gameObject.SetActive(false);
        }
    }
}