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
using UnityEngine;

namespace PV3.UI.Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DisplayStageBossEncounter : MonobehaviourReference
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private GameEventObject OnInitializeNewEnemyEvent;
        [SerializeField] private GameEventObject OnPlayBossAudioEvent;

        public void DisplayBossEncounter()
        {
            canvasGroup.blocksRaycasts = true;

            var mySequence = DOTween.Sequence();
            mySequence.Append(canvasGroup.DOFade(1f, 1f));
            mySequence.AppendInterval(1.5f);
            mySequence.Append(canvasGroup.DOFade(0f, 1f).OnComplete(InitializeBoss));
        }

        private void InitializeBoss()
        {
            canvasGroup.blocksRaycasts = false;
            OnInitializeNewEnemyEvent.Raise();
            OnPlayBossAudioEvent.Raise();
        }
    }
}