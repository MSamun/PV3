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
using PV3.Miscellaneous;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PV3.UI.Tooltip.Spell
{
    public class ShowTooltipOnButtonHold : MonobehaviourReference, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float requiredTime;
        private float touchDuration;
        private bool isHoldingDownSpell;

        public UnityEvent OnLongClick;

        private void Update()
        {
            if (isHoldingDownSpell)
            {
                touchDuration += Time.deltaTime;
                if (touchDuration >= requiredTime)
                {
                    OnLongClick?.Invoke();
                    Reset();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isHoldingDownSpell = true;
            print(isHoldingDownSpell.ToString());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
            print(isHoldingDownSpell.ToString());
        }

        private void Reset()
        {
            isHoldingDownSpell = false;
            touchDuration = 0;
        }
    }
}