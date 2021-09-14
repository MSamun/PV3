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
using PV3.ScriptableObjects.Game;
using PV3.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.UI.Settings
{
    public class SettingsManager : MonobehaviourReference
    {
        [Header("Audio Values")]
        [SerializeField] private IntValue BackgroundMusicVolume;
        [SerializeField] private IntValue SfxVolume;

        [Header("Sliders")]
        [SerializeField] private Slider BackgroundMusicSlider;
        [SerializeField] private Slider SfxSlider;

        private TextMeshProUGUI _backgroundMusicSliderText;
        private TextMeshProUGUI _sfxSliderText;

        private void Awake()
        {
            InitializeSliders();
            var audioData = DataManager.LoadSettingsDataFromJson().AudioData;

            BackgroundMusicSlider.value = audioData.BackgroundMusicVolume;
            SfxSlider.value = audioData.SfxVolume;

            UpdateBackgroundMusicSliderText();
            UpdateButtonSfxSliderText();
        }

        private void InitializeSliders()
        {
            _backgroundMusicSliderText = BackgroundMusicSlider.GetComponentInChildren<TextMeshProUGUI>(true);
            _sfxSliderText = SfxSlider.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        // Referenced by the BGM Slider in the Settings Scene. Gets executed whenever the user changes the value of the BGM Slider.
        public void AdjustBackgroundMusicVolume()
        {
            BackgroundMusicVolume.Value = Mathf.FloorToInt(BackgroundMusicSlider.value);
            UpdateBackgroundMusicSliderText();
        }

        // Referenced by the SFX Slider in the Settings Scene. Gets executed whenever the user changes the value of the SFX Slider.
        public void AdjustButtonSfxVolume()
        {
            SfxVolume.Value = Mathf.FloorToInt(SfxSlider.value);
            UpdateButtonSfxSliderText();
        }

        private void UpdateBackgroundMusicSliderText()
        {
            _backgroundMusicSliderText.text = $"{BackgroundMusicVolume.Value.ToString()}%";
        }

        private void UpdateButtonSfxSliderText()
        {
            _sfxSliderText.text = $"{SfxVolume.Value.ToString()}%";
        }

        private void OnDestroy()
        {
            var data = new AudioData(BackgroundMusicVolume.Value, SfxVolume.Value);
            DataManager.UpdateSettingsAudioData(data);
        }
    }
}