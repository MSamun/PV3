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

using PV3.Audio;
using PV3.Miscellaneous;
using PV3.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Settings
{
    public class SettingsManager : MonobehaviourReference
    {
        [Header("Sliders")]
        [SerializeField] private Slider backgroundMusicSlider;

        [SerializeField] private Slider buttonSfxSlider;
        private TextMeshProUGUI backgroundMusicSliderText;
        private TextMeshProUGUI buttonSfxSliderText;

        private void Awake()
        {
            InitializeSliders();
            var audioData = DataManager.LoadSettingsDataFromJson().AudioData;

            backgroundMusicSlider.value = audioData.BackgroundMusicVolume;
            buttonSfxSlider.value = audioData.ButtonSfxVolume;

            UpdateBackgroundMusicSliderText();
            UpdateButtonSfxSliderText();
        }

        private void OnDestroy()
        {
            var data = new AudioData(backgroundMusicSlider.value, buttonSfxSlider.value);
            DataManager.UpdateSettingsAudioData(data);
        }

        private void InitializeSliders()
        {
            backgroundMusicSliderText = backgroundMusicSlider.GetComponentInChildren<TextMeshProUGUI>(true);
            buttonSfxSliderText = buttonSfxSlider.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        // Referenced by the BGM Slider in the Settings Scene. Gets executed whenever the user changes the value of the BGM Slider.
        public void AdjustBackgroundMusicVolume()
        {
            UpdateBackgroundMusicSliderText();
            AudioManager.BackgroundVolume = backgroundMusicSlider.value;
        }

        // Referenced by the SFX Slider in the Settings Scene. Gets executed whenever the user changes the value of the SFX Slider.
        public void AdjustButtonSfxVolume()
        {
            UpdateButtonSfxSliderText();
            AudioManager.SoundEffectVolume = buttonSfxSlider.value;
        }

        private void UpdateBackgroundMusicSliderText()
        {
            backgroundMusicSliderText.text = $"{Mathf.RoundToInt(backgroundMusicSlider.value * 100f).ToString()}%";
        }

        private void UpdateButtonSfxSliderText()
        {
            buttonSfxSliderText.text = $"{Mathf.RoundToInt(buttonSfxSlider.value * 100f).ToString()}%";
        }
    }
}