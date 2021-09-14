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
    [ExecuteInEditMode] public class TooltipUI : MonobehaviourReference
    {
        private const float HEADER_TEXT_WIDTH_DEFAULT = 225f;
        private const float HEADER_TEXT_WIDTH_EXTENDED = 330f;

        [Header("UI Components")] [SerializeField]
        private GameObject CooldownTextHolder;

        [SerializeField] private GameObject StaminaCostTextHolder;
        [SerializeField] private CanvasGroup CanvasGroup;

        [Header("Text Components")] [SerializeField]
        private TextMeshProUGUI HeaderText;

        [SerializeField] private TextMeshProUGUI DescriptionText;
        private bool _canHide;

        private TextMeshProUGUI _cooldownText;

        private RectTransform _rectTransform;
        private TextMeshProUGUI _staminaCostText;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _staminaCostText = StaminaCostTextHolder.GetComponentInChildren<TextMeshProUGUI>(true);
            _cooldownText = CooldownTextHolder.GetComponentInChildren<TextMeshProUGUI>(true);
            _canHide = false;
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;

            // There's a weird issue that happens where tapping on a Button that utilizes the Tooltip, then tap on another Button that utilizes the Tooltip
            // will result in the Tooltip to quickly show the latter Button's Tooltip, then immediately hide.
            // Expected functionality: Tap on Button, show Tooltip, tap on another Button, hide Tooltip from first Button then show Tooltip for second Button.
            if (StaminaCostTextHolder.gameObject.activeInHierarchy)
            {
                if (_canHide && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Hide();
                }
            }
            else
            {
                if (_canHide && Input.GetTouch(0).phase != TouchPhase.Ended)
                {
                    Hide();
                }
            }
        }

        // Tooltips are going to be split up into three different categories: Basic, Spell, and Status Effect.
        //      - Basic Tooltips are utilized for UI components in any scene that may need additional context, such as Attributes and Currency.
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
            if (!HeaderText || !DescriptionText) return;

            HeaderText.text = header;
            DescriptionText.text = desc;
        }

        private void SetCooldown(string cooldown = "")
        {
            if (!_cooldownText) return;

            bool doesTooltipNeedToShowCooldown = !string.IsNullOrEmpty(cooldown);
            _cooldownText.text = doesTooltipNeedToShowCooldown ? cooldown : string.Empty;
            CooldownTextHolder.gameObject.SetActive(doesTooltipNeedToShowCooldown);

            // If the Tooltip's cooldown does not need to be shown, we can expand the width of the Tooltip Header to give more room for the text.
            HeaderText.GetComponent<RectTransform>().sizeDelta = new Vector2(doesTooltipNeedToShowCooldown ? HEADER_TEXT_WIDTH_DEFAULT : HEADER_TEXT_WIDTH_EXTENDED, HeaderText.GetComponent<RectTransform>().sizeDelta.y);
        }

        private void SetStaminaCost(string cost = "")
        {
            if (!_staminaCostText) return;

            bool doesTooltipNeedToShowStamina = !string.IsNullOrEmpty(cost);
            _staminaCostText.text = doesTooltipNeedToShowStamina ? cost : string.Empty;
            StaminaCostTextHolder.gameObject.SetActive(doesTooltipNeedToShowStamina);
        }

        public void SetPivotAndPosition(Vector3 position, PivotHorizontal pivotX = PivotHorizontal.Right, PivotVertical pivotY = PivotVertical.Top)
        {
            // I originally used the PivotHorizontal/PivotVertical's enum index values to calculate its pivot points, but it was confusing to read.
            // Manually setting the values will give a clear idea as to what the code is doing.

            // Since the parameters have default values set, we only need to check for the other two possible enums.
            var pivotPositionX = 0f;
            var pivotPositionY = 0f;

            switch (pivotX)
            {
                case PivotHorizontal.Center:
                    pivotPositionX = 0.5f;
                    break;
                case PivotHorizontal.Left:
                    pivotPositionX = 1f;
                    break;
            }

            switch (pivotY)
            {
                case PivotVertical.Center:
                    pivotPositionY = 0.5f;
                    break;
                case PivotVertical.Bottom:
                    pivotPositionY = 1f;
                    break;
            }

            _rectTransform.pivot = new Vector2(pivotPositionX, pivotPositionY);
            transform.position = position;
        }

        public void Show()
        {
            CanvasGroup.DOFade(1f, 0.25f).OnComplete(() => _canHide = true);
        }

        private void Hide()
        {
            CanvasGroup.DOFade(0f, 0.25f).OnComplete(() => _canHide = false);
        }
    }
}