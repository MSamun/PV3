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

using System.Collections;
using DG.Tweening;
using PV3.Miscellaneous;
using UnityEngine;

namespace PV3.UI.Scenes
{
    public class AnimateTitleUIComponents : MonobehaviourReference
    {
        [SerializeField] private Transform BackgroundImage;
        [SerializeField] private CanvasGroup StartButton;

        private void Awake()
        {
            if (BackgroundImage)
            {
                BackgroundImage.DOScale(1.10f, 0);
                return;
            }

            Debug.LogWarning("<color=yellow>WARNING:</color> No reference found for BackgroundImage in AnimateTitleUIComponents.cs. Ignoring request to animate Background Image...");
        }

        private IEnumerator Start()
        {
            if (!BackgroundImage || !StartButton) yield return null;

            yield return new WaitForSeconds(1.5f);

            BackgroundImage.transform.DOKill();
            BackgroundImage.transform.DOScale(1f, 1f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(0.5f);

            StartButton.gameObject.SetActive(true);
            StartButton.DOFade(1f, 0.75f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            yield return null;
        }
    }
}