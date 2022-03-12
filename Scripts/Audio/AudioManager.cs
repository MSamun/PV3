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

using System;
using System.Collections;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.UI;
using PV3.Serialization;
using UnityEngine;

namespace PV3.Audio
{
    public enum MusicType
    {
        Main,
        IncomingBoss,
        Boss,
        Dungeon,
        Victory,
        Defeat
    }

    [Serializable]
    public class Music
    {
        public MusicType Type;
        public AudioClip IntroClip;
        public AudioClip MainClip;
        public bool PlayedIntroClip;
    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonobehaviourReference
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource BackgroundMusicSource;

        [SerializeField] private AudioSource SfxSource;

        [Header("")]
        [SerializeField] private IntValue BackgroundMusicVolume;

        [SerializeField] private IntValue SfxVolume;
        [SerializeField] private SpellEffectsScriptableObject SpellEffectsObject;

        [Header("")]
        [SerializeField] private Music[] MusicClips = Array.Empty<Music>();

        private Coroutine _coroutine;

        private void Awake()
        {
            if (!BackgroundMusicSource)
            {
                Debug.LogWarning("<color=yellow>WARNING:</color> BackgroundMusicSource is NULL in AudioManager.cs. Ignoring requests to play BGM...", this);
            }
            else
            {
                BackgroundMusicSource.volume = DataManager.LoadSettingsDataFromJson().AudioData.BackgroundMusicVolume / 100f;
                _coroutine = StartCoroutine(LoopThroughMusicClips());
            }

            if (!SfxSource)
                Debug.LogWarning("<color=yellow>WARNING:</color> SfxSource is NULL in AudioManager.cs. Ignoring requests to play SFX...", this);
            else
                SfxSource.volume = DataManager.LoadSettingsDataFromJson().AudioData.SfxVolume / 100f;
        }

        private void PlayClipWithSource(AudioClip clip = null, AudioSource source = null)
        {
            if (!clip || !source) return;

            source.clip = clip;
            source.Play();
        }

        #region Background Music

        private IEnumerator LoopThroughMusicClips(MusicType type = MusicType.Main)
        {
            Music music = FindClip(type);

            while (BackgroundMusicSource && music != null)
            {
                if (music.IntroClip && !music.PlayedIntroClip)
                {
                    PlayClipWithSource(music.IntroClip, BackgroundMusicSource);
                    yield return new WaitForSeconds(music.IntroClip.length - 0.5f);
                    music.PlayedIntroClip = true;
                }

                if (!music.MainClip) break;

                PlayClipWithSource(music.MainClip, BackgroundMusicSource);
                yield return new WaitForSeconds(music.MainClip.length);
            }

            yield return null;
        }

        private Music FindClip(MusicType type = MusicType.Main)
        {
            if (MusicClips.Length > 0)
                for (var i = 0; i < MusicClips.Length; i++)
                {
                    if (MusicClips[i].Type != type) continue;

                    MusicClips[i].PlayedIntroClip = false;
                    return MusicClips[i];
                }

            Debug.LogWarning("<color=yellow>WARNING:</color> There are no MusicClips assigned in AudioManager.cs. Ignoring requests to find Music Clips...", this);
            return null;
        }

        private void StopBackgroundMusic()
        {
            StopCoroutine(_coroutine);

            if (BackgroundMusicSource)
                BackgroundMusicSource.Stop();
        }

        public void AdjustBackgroundMusicSourceVolume()
        {
            if (BackgroundMusicSource)
                BackgroundMusicSource.volume = BackgroundMusicVolume.Value / 100f;
        }

        #endregion Background Music

        #region SFX

        public void PlayButtonSfx()
        {
            PlayClipWithSource(SpellEffectsObject.GetButtonSfx(), SfxSource);
        }

        public void PlaySpellSfx()
        {
            PlayClipWithSource(SpellEffectsObject.SpellSfx, SfxSource);
        }

        public void AdjustSfxSourceVolume()
        {
            if (SfxSource)
                SfxSource.volume = SfxVolume.Value / 100f;
        }

        #endregion SFX

        #region Stage Events Audio

        public void PlayDefeatAudio()
        {
            StopBackgroundMusic();
            _coroutine = StartCoroutine(LoopThroughMusicClips(MusicType.Defeat));
        }

        public void PlayVictoryAudio()
        {
            StopBackgroundMusic();
            _coroutine = StartCoroutine(LoopThroughMusicClips(MusicType.Victory));
        }

        public void PlayIncomingBossAudio()
        {
            StopBackgroundMusic();
            _coroutine = StartCoroutine(LoopThroughMusicClips(MusicType.IncomingBoss));
        }

        public void PlayFinalBossAudio()
        {
            StopBackgroundMusic();
            _coroutine = StartCoroutine(LoopThroughMusicClips(MusicType.Boss));
        }

        public void PlayDungeonAudio()
        {
            StopBackgroundMusic();
            _coroutine = StartCoroutine(LoopThroughMusicClips(MusicType.Dungeon));
        }

        #endregion Stage Events Audio
    }
}