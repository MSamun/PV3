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
using TMPro;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    [ExecuteInEditMode]
    public class TooltipUI : MonobehaviourReference
    {
        [Header("UI Components")]
        [SerializeField] private GameObject cooldownTextHolder;

        [SerializeField] private GameObject staminaCostTextHolder;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Text Components")]
        [SerializeField] private TextMeshProUGUI headerText;

        [SerializeField] private TextMeshProUGUI descriptionText;

        private RectTransform rectTransform;
        private TextMeshProUGUI cooldownText;
        private TextMeshProUGUI staminaCostText;
        private bool canHide;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            staminaCostText = staminaCostTextHolder.GetComponentInChildren<TextMeshProUGUI>(true);
            cooldownText = cooldownTextHolder.GetComponentInChildren<TextMeshProUGUI>(true);
            canHide = false;
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;

            if (staminaCostTextHolder.gameObject.activeInHierarchy)
            {
                if (canHide && Input.GetTouch(0).phase == TouchPhase.Ended)
                    Hide();
            }
            else
            {
                if (canHide && Input.GetTouch(0).phase != TouchPhase.Ended)
                    Hide();
            }

        }

        // Tooltips are going to be split up into three different categories: Basic, Spell, and Status Effect.
        //      - Basic Tooltips are utilized for UI components in any scene that may need additional context, such as Attributes.
        //      - They contain a header and a description, which are both set manually in the hierarchy. They are NOT dynamically generated at runtime.
        //
        //      - Spell Tooltips are utilized for Spells being used in a Stage and Spells in the Character Loadout screen.
        //      - They contain a header, description, cooldown, and stamina cost, which are ALL dynamically generated at runtime.
        //
        //      - Status Effect Tooltips are utilized for Skills in the Character Loadout screen and Status Effects applied to a Player/Enemy during a Stage.
        //      - They contain a header, description, and duration, which are ALL dynamically generated at runtime.
        public void PopulateTooltipComponents(string header, string desc, string cooldown = "", string staminaCost = "")
        {
            SetHeaderAndDescription(header, desc);
            SetCooldown(cooldown);
            SetStaminaCost(staminaCost);
        }

        private void SetHeaderAndDescription(string header, string desc)
        {
            if (!headerText || !descriptionText) return;

            headerText.text = header;
            descriptionText.text = desc;
        }

        private void SetCooldown(string cooldown = "")
        {
            if (!cooldownText) return;
            var isStringEmpty = string.IsNullOrEmpty(cooldown);

            cooldownText.text = isStringEmpty ? string.Empty : cooldown;
            cooldownTextHolder.gameObject.SetActive(!isStringEmpty);
        }

        private void SetStaminaCost(string cost = "")
        {
            if (!staminaCostText) return;
            var isStringEmpty = string.IsNullOrEmpty(cost);

            staminaCostText.text = isStringEmpty ? string.Empty : cost;
            staminaCostTextHolder.gameObject.SetActive(!isStringEmpty);
        }

        public void SetPivotAndPosition(PivotHorizontal pivotX, PivotVertical pivotY, Vector3 position)
        {
            // I originally used the PivotHorizontal/PivotVertical's enum index values to calculate its pivot points, but it was confusing to read.
            // Manually setting the values will give a clear idea as to what I am accomplishing.
            var xPos = 0f;
            var yPos = 0f;

            xPos = pivotX switch
            {
                PivotHorizontal.Right => xPos, PivotHorizontal.Center => 0.5f, PivotHorizontal.Left => 1f, _ => xPos
            };

            yPos = pivotY switch
            {
                PivotVertical.Top => yPos, PivotVertical.Center => 0.5f, PivotVertical.Bottom => 1f, _ => yPos
            };

            rectTransform.pivot = new Vector2(xPos, yPos);
            transform.position = position;
        }

        public void Show()
        {
            canvasGroup.DOFade(1f, 0.25f).OnComplete(() => canHide = true);
        }

        private void Hide()
        {
            canvasGroup.DOFade(0f, 0.25f).OnComplete(() => canHide = false);
        }
    }
}