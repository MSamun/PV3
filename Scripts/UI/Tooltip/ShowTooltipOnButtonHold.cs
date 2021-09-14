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
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace PV3.UI.Tooltip
{
    public class ShowTooltipOnButtonHold : MonobehaviourReference, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float RequiredTime;

        public UnityEvent OnLongClick;
        private bool _isHoldingDownSpell;
        private float _touchDuration;

        private void Update()
        {
            if (!_isHoldingDownSpell) return;
            _touchDuration += Time.deltaTime;

            if (_touchDuration <= RequiredTime) return;

            OnLongClick?.Invoke();
            Reset();
        }

        private void Reset()
        {
            _isHoldingDownSpell = false;
            _touchDuration = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHoldingDownSpell = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
        }
    }
}