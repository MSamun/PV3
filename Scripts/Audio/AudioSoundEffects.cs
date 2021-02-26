using PV3.Miscellaneous;
using PV3.Serialization;
using UnityEngine;

namespace PV3.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSoundEffects : MonobehaviourReference
    {
        private AudioSource source;
        [SerializeField] private AudioClip buttonSfx;

        private void Awake()
        {
            source = GetComponent<AudioSource>();

            if (!source) return;
            source.volume = DataManager.LoadDataFromJson().AudioData.ButtonSfxVolume;
        }

        public void PlayButtonSFX()
        {
            if (!source || !buttonSfx) return;

            source.clip = buttonSfx;
            source.Play();
        }

        public void PlaySpellSFX()
        {
            if (!source || !AudioManager.SpellSFX) return;

            source.clip = AudioManager.SpellSFX;
            source.Play();
        }

        public void AdjustSourceVolume()
        {
            source.volume = AudioManager.SoundEffectVolume;
        }
    }
}