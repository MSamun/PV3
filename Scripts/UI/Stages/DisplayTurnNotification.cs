// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021-2022 Matthew Samun.
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
using PV3.ScriptableObjects.UI;
using TMPro;
using UnityEngine;

namespace PV3.UI.Stages
{
    public class DisplayTurnNotification : MonobehaviourReference
    {
        [SerializeField] private TurnNotificationObject TurnNotifObject;

        private float _interval;
        private TextMeshProUGUI _notifText;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _interval = 10f;
            _notifText = GetComponentInChildren<TextMeshProUGUI>(true);
            _canvasGroup = GetComponentInChildren<CanvasGroup>(true);
        }

        public void Toggle()
        {
            _notifText.text = TurnNotifObject.Description;

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(_canvasGroup.DOFade(1, 0.75f));
            mySequence.AppendInterval(_interval);
            mySequence.Append(_canvasGroup.DOFade(0, 0.75f));
        }
    }
}