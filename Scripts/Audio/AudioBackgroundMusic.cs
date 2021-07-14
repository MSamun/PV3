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

using System.Collections;
using PV3.Miscellaneous;
using PV3.Serialization;
using UnityEngine;

namespace PV3.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioBackgroundMusic : MonobehaviourReference
    {
        [SerializeField] private BackgroundMusic[] BackgroundMusicClips = new BackgroundMusic[0];
        private AudioSource backgroundMusicSource;
        private Coroutine co;

        private void Awake()
        {
            backgroundMusicSource = GetComponent<AudioSource>();

            if (!backgroundMusicSource) return;
            backgroundMusicSource.volume = DataManager.LoadSettingsDataFromJson().AudioData.BackgroundMusicVolume;

            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.Main));
        }

        private IEnumerator LoopThroughBackgroundMusic(BackgroundMusicType type)
        {
            var backgroundMusic = FindClip(type);

            while (backgroundMusicSource)
            {
                if (backgroundMusic.introClip && !backgroundMusic.playedIntroClip)
                {
                    PlayClip(backgroundMusic.introClip);
                    yield return new WaitForSeconds(backgroundMusic.introClip.length - 0.5f);
                    backgroundMusic.playedIntroClip = true;
                }

                if (!backgroundMusic.mainClip) break;

                PlayClip(backgroundMusic.mainClip);
                yield return new WaitForSeconds(backgroundMusic.mainClip.length);
            }

            yield return null;
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
            if (!clip || !backgroundMusicSource) return;

            backgroundMusicSource.clip = clip;
            backgroundMusicSource.Play();
        }

        public void PlayDefeatAudio()
        {
            StopBackgroundMusic();
            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.Defeat));
        }

        public void PlayVictoryAudio()
        {
            StopBackgroundMusic();
            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.Victory));
        }

        public void PlayIncomingBossAudio()
        {
            StopBackgroundMusic();
            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.IncomingBoss));
        }

        public void PlayFinalBossAudio()
        {
            StopBackgroundMusic();
            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.Boss));
        }

        public void PlayDungeonAudio()
        {
            StopBackgroundMusic();
            co = StartCoroutine(LoopThroughBackgroundMusic(BackgroundMusicType.Dungeon));
        }

        private void StopBackgroundMusic()
        {
            StopCoroutine(co);

            if (backgroundMusicSource)
                backgroundMusicSource.Stop();
        }

        // Referenced by GameObject: Audio -> BGM -> Event Listener. Gets executed when the OnChangedAudioBGM Event gets raised.
        // (Note: The OnChangedAudioBGM Event gets raised whenever the user changes the value of the BGM Slider in the Settings Scene.)
        public void AdjustSourceVolume()
        {
            if (backgroundMusicSource)
                backgroundMusicSource.volume = AudioManager.BackgroundVolume;
        }
    }
}