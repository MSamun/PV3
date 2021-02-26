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
        [SerializeField] protected Slider slider = null;
        [SerializeField] protected TextMeshProUGUI sliderText = null;
        [SerializeField] private ParticleSystem sliderEdgeParticleSystem = null;

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