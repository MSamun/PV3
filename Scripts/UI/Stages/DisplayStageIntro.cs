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

using DG.Tweening;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using TMPro;
using UnityEngine;

namespace PV3.UI.Stages
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DisplayStageIntro : MonobehaviourReference
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private IntValue StageListIndex;

        [Header("Game Event")]
        [SerializeField] private GameEventObject OnStartStageInitializationEvent;

        private void Start()
        {
            text.text = $"Stage {(StageListIndex.Value < 9 ? "0" : string.Empty)}{(StageListIndex.Value + 1).ToString()}";

            var mySequence = DOTween.Sequence();
            mySequence.AppendInterval(0.5f);
            mySequence.Append(canvasGroup.DOFade(1f, 0.75f));
            mySequence.AppendInterval(0.5f);
            mySequence.Append(canvasGroup.DOFade(0f, 0.75f).OnComplete(InitializeStage));
        }

        // Initializes everything that is necessary before combat begins. This includes Player/Enemy Portrait Icons, Spells, Names, Health/Stamina Bars, and Levels.
        // This also include basic Stage Information such as Stage Number, Stage Background, and Stage Progression Bar.
        // Referenced by the following GameObject in the Unity hierarchy: Game Events Listener Container -> Game Events -> OnStartStageInitialization.
        private void InitializeStage()
        {
            OnStartStageInitializationEvent.Raise();
        }
    }
}