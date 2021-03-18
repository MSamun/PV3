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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Tooltip
{
    public class Tooltip : MonobehaviourReference
    {
        private RectTransform rectTransform;

        [SerializeField] protected bool isSpellTooltip;
        [SerializeField] protected int characterWrapLimit;

        [Header("UI Components")]
        [SerializeField] protected LayoutElement layoutElement;
        [SerializeField] protected TextMeshProUGUI headerText;
        [SerializeField] protected TextMeshProUGUI contentText;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;

            if (!isSpellTooltip) gameObject.SetActive(false);
            else
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended )
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void SetText(string content, string header = "")
        {
            if (!headerText || !contentText) return;

            if (string.IsNullOrEmpty(header))
            {
                headerText.gameObject.SetActive(false);
            }
            else
            {
                headerText.gameObject.SetActive(true);
                headerText.text = header;
            }

            contentText.text = content;
            CheckIfNeedLayoutElement();
        }

        protected virtual void CheckIfNeedLayoutElement()
        {
            if (headerText.text == null) return;

            layoutElement.enabled = headerText.text.Length > characterWrapLimit || contentText.text.Length > characterWrapLimit;
        }

        public void SetPivotPoint(PivotHorizontal pivotHorizontal, PivotVertical pivotVertical)
        {
            rectTransform.pivot = new Vector2((int)pivotHorizontal / 2f, (int)pivotVertical / 2f);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}