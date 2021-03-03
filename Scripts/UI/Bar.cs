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
using PV3.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI
{
    public class Bar : MonobehaviourReference
    {
        [SerializeField] private IntValue MaxBarValue;
        [SerializeField] private IntValue CurrentBarValue;

        [Range(0.35f, 0.65f)] [SerializeField] private float barLerpSpeed;

        [Header("UI Components")]
        [SerializeField] protected Slider slider;
        [SerializeField] protected TextMeshProUGUI sliderText;
        [SerializeField] private ParticleSystem sliderEdgeParticleSystem;

        // Referenced by Event Listener Holder -> [Player/Enemy] Events -> On[Player/Enemy]AttributesCalculated's Game Event Listener.
        public void Initialize()
        {
            slider.minValue = 0;
            slider.maxValue = MaxBarValue.Value;
            slider.value = MaxBarValue.Value;

            UpdateBarText();
        }

        private void FixedUpdate()
        {
            if (!Mathf.Approximately(slider.value, CurrentBarValue.Value))
            {
                UpdateBar();
            }
            else
            {
                if (sliderEdgeParticleSystem && sliderEdgeParticleSystem.isPlaying)
                {
                    sliderEdgeParticleSystem.Stop();
                }
            }
        }

        private void UpdateBar()
        {
            if (slider.value > CurrentBarValue.Value)
            {
                slider.value -= barLerpSpeed;
                slider.value = Mathf.Clamp(slider.value, CurrentBarValue.Value, MaxBarValue.Value);
            }
            else
            {
                slider.value += barLerpSpeed;
            }

            if (sliderEdgeParticleSystem && !sliderEdgeParticleSystem.isPlaying && CurrentBarValue.Value <= MaxBarValue.Value)
            {
                sliderEdgeParticleSystem.Play();
            }

            UpdateBarText();
        }

        private void UpdateBarText()
        {
            if (slider.value > 0)
            {
                sliderText.text = $"{((int)slider.value).ToString()} | {MaxBarValue.Value.ToString()}";
                sliderText.color = Color.white;
            }
            else
            {
                sliderText.text = "DEAD";
                sliderText.color = new Color(0.89f, 0.2f, 0.2f, 1.0f);
            }
        }
    }
}