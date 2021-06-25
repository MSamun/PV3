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
using UnityEngine.UI;

namespace PV3.UI.Game
{
    public class Bar : MonobehaviourReference
    {
        [SerializeField] private IntValue MaxBarValue;
        [SerializeField] private IntValue CurrentBarValue;
        [SerializeField] private bool showTextInPercent;
        [Range(0f, 1f)] [SerializeField] private float sliderFillRate;
        [Range(0f, 1f)] [SerializeField] private float sliderBackFillRate;

        [Header("UI Components")]
        [SerializeField] protected Slider slider;

        [SerializeField] protected TextMeshProUGUI sliderText;
        [SerializeField] private ParticleSystem sliderEdgeParticleSystem;
        [SerializeField] private Image sliderBackFill;
        [SerializeField] private Color sliderBackFillColor;
        private bool hasCreatedSequence;

        private void FixedUpdate()
        {
            if (Mathf.Approximately(slider.value, CurrentBarValue.Value) || hasCreatedSequence) return;
            UpdateBar();
        }

        public void Initialize()
        {
            slider.minValue = 0;
            slider.maxValue = MaxBarValue.Value;
            slider.value = MaxBarValue.Value;

            sliderBackFill.fillAmount = 1f;
            sliderBackFill.color = sliderBackFillColor;

            hasCreatedSequence = false;

            UpdateBarText();
        }

        private void UpdateBar()
        {
            hasCreatedSequence = true;
            var mySequence = DOTween.Sequence();

            if (slider.value > CurrentBarValue.Value)
            {
                mySequence.Append(slider.DOValue(CurrentBarValue.Value, sliderFillRate).OnUpdate(UpdateBarText).OnComplete(sliderEdgeParticleSystem.Stop));

                var percentage = (float) CurrentBarValue.Value / MaxBarValue.Value;
                mySequence.Append(sliderBackFill.DOFillAmount(percentage, sliderBackFillRate));

                mySequence.OnKill(() => hasCreatedSequence = false);
            }
            else
            {
                var percentage = (float) CurrentBarValue.Value / MaxBarValue.Value;
                mySequence.Append(sliderBackFill.DOFillAmount(percentage, sliderBackFillRate));

                mySequence.Append(slider.DOValue(CurrentBarValue.Value, sliderBackFillRate).OnUpdate(UpdateBarText).OnComplete(sliderEdgeParticleSystem.Stop));
                mySequence.OnKill(() => hasCreatedSequence = false);
            }
        }

        private void UpdateBarText()
        {
            if (!sliderEdgeParticleSystem.isPlaying && CurrentBarValue.Value < MaxBarValue.Value)
                sliderEdgeParticleSystem.Play();

            if (sliderText.color != Color.white) sliderText.color = Color.white;
            sliderText.text = showTextInPercent ? $"{((int) slider.value).ToString()}%" : $"{((int) slider.value).ToString()} | {MaxBarValue.Value.ToString()}";

            if (slider.value > 0) return;

            sliderText.text = showTextInPercent ? "EXHAUSTED" : "DEAD";
            sliderText.color = showTextInPercent ? new Color(1f, 0.83f, 0f, 1f) : new Color(0.89f, 0.2f, 0.2f, 1.0f);
        }
    }
}