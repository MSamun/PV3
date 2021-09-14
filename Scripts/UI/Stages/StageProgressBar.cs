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
using DG.Tweening;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Stages
{
    public class StageProgressBar : MonobehaviourReference
    {
        [SerializeField] private IntValue CurrentEnemyIndex;
        [SerializeField] private Slider slider;

        [Header("")]
        [SerializeField] private ProgressBarPointers[] Pointers = new ProgressBarPointers[0];

        private bool hasCreatedTween;

        private void Start()
        {
            slider.minValue = 0;
            slider.maxValue = Pointers.Length - 1;
            slider.value = slider.minValue;

            hasCreatedTween = false;
            UpdateBarPointers();
        }

        private void FixedUpdate()
        {
            if ((int) slider.value == CurrentEnemyIndex.Value || hasCreatedTween) return;
            MatchStageBarWithProgression();
        }

        private void MatchStageBarWithProgression()
        {
            hasCreatedTween = true;
            slider.DOValue(CurrentEnemyIndex.Value, 0.5f).OnComplete(UpdateBarPointers).OnKill(() => hasCreatedTween = false);
        }

        private void UpdateBarPointers()
        {
            Pointers[CurrentEnemyIndex.Value].pointerFocus.gameObject.SetActive(true);
            Pointers[CurrentEnemyIndex.Value].pointerUnfocus.gameObject.SetActive(false);
        }

        [Serializable]
        private class ProgressBarPointers
        {
            public GameObject pointerFocus;
            public GameObject pointerUnfocus;
        }
    }
}