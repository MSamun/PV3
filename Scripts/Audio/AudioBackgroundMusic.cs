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
using PV3.Serialization;
using UnityEngine;

namespace PV3.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioBackgroundMusic : MonobehaviourReference
    {
        private AudioSource source;
        private AudioClip backgroundMusicClip;
        private AudioClip victoryCueClip;
        private AudioClip defeatCueClip;

        [SerializeField] private BackgroundMusic[] BackgroundMusicClips = new BackgroundMusic[0];

        private void Awake()
        {
            source = GetComponent<AudioSource>();

            if (!source) return;
            source.volume = DataManager.LoadDataFromJson().AudioData.BackgroundMusicVolume;

            StartCoroutine(LoopThroughMainBackgroundMusic());
        }

        private System.Collections.IEnumerator LoopThroughMainBackgroundMusic()
        {
            var backgroundMusic = FindClip(BackgroundMusicType.Main);

            while (source)
            {
                if (backgroundMusic.introClip && !backgroundMusic.playedIntroClip)
                {
                    PlayClip(backgroundMusic.introClip);
                    yield return new WaitForSeconds(backgroundMusic.introClip.length - 0.5f);
                    backgroundMusic.playedIntroClip = true;
                }

                PlayClip(backgroundMusic.mainClip);
                yield return new WaitForSeconds(backgroundMusic.mainClip.length);
            }
        }

        private BackgroundMusic FindClip(BackgroundMusicType type)
        {
            for (var i = 0; i < BackgroundMusicClips.Length; i++)
            {
                if (BackgroundMusicClips[i].type != type) continue;
                return BackgroundMusicClips[i];
            }

            return null;
        }

        private void PlayClip(AudioClip clip)
        {
            if (!clip) return;

            source.clip = clip;
            source.Play();
        }

        public void PlayDefeatClip()
        {
            StopMainBackgroundMusic();
            var clip = FindClip(BackgroundMusicType.Defeat);
            PlayClip(clip.mainClip);
        }

        public void PlayVictoryClip()
        {
            StopMainBackgroundMusic();
            var clip = FindClip(BackgroundMusicType.Victory);
            PlayClip(clip.mainClip);
        }

        private void StopMainBackgroundMusic()
        {
            StopCoroutine(LoopThroughMainBackgroundMusic());
            source.Stop();
        }

        // Referenced by GameObject: Audio -> BGM -> Event Listener. Gets executed when the OnChangedAudioBGM Event gets raised.
        // (Note: The OnChangedAudioBGM Event gets raised whenever the user changes the value of the BGM Slider in the Settings Scene.)
        public void AdjustSourceVolume()
        {
            source.volume = AudioManager.BackgroundVolume;
        }
    }
}