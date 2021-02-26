using TMPro;
using PV3.Audio;
using PV3.Miscellaneous;
using PV3.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace PV3.Settings
{
    public class SettingsManager : MonobehaviourReference
    {
        private TextMeshProUGUI backgroundMusicSliderText;
        private TextMeshProUGUI buttonSfxSliderText;

        [Header("Sliders")]
        [SerializeField] private Slider backgroundMusicSlider;
        [SerializeField] private Slider buttonSfxSlider;

        private void Awake()
        {
            InitializeSliders();

            var audioData = DataManager.LoadDataFromJson().AudioData;
            backgroundMusicSlider.value = audioData.BackgroundMusicVolume;
            buttonSfxSlider.value = audioData.ButtonSfxVolume;
        }

        private void InitializeSliders()
        {
            backgroundMusicSliderText = backgroundMusicSlider.GetComponentInChildren<TextMeshProUGUI>(true);
            buttonSfxSliderText = buttonSfxSlider.GetComponentInChildren<TextMeshProUGUI>(true);
        }

        // Referenced by the BGM Slider in the Settings Scene. Gets executed whenever the user changes the value of the BGM Slider.
        public void AdjustBackgroundMusicVolume()
        {
            backgroundMusicSliderText.text = $"{Mathf.RoundToInt(backgroundMusicSlider.value * 100f).ToString()}%";
            AudioManager.BackgroundVolume = backgroundMusicSlider.value;
        }

        // Referenced by the SFX Slider in the Settings Scene. Gets executed whenever the user changes the value of the SFX Slider.
        public void AdjustButtonSfxVolume()
        {
            buttonSfxSliderText.text = $"{Mathf.RoundToInt(buttonSfxSlider.value * 100f).ToString()}%";
            AudioManager.SoundEffectVolume = buttonSfxSlider.value;
        }

        private void OnDestroy()
        {
            var data = new AudioData(backgroundMusicSlider.value, buttonSfxSlider.value);
            DataManager.UpdateAudioData(data);
            DataManager.SaveDataToJson();
        }
    }
}