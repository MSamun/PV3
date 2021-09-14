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
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Stages
{
    public class ToggleEndTurnButton : MonobehaviourReference
    {
        private Button _button;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Hide()
        {
            _button.interactable = false;
            _canvasGroup.DOFade(0f, 0.35f);
        }

        public void Show()
        {
            _canvasGroup.DOFade(1f, 0.35f).OnComplete(() => _button.interactable = true);
        }
    }
}