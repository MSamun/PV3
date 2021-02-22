using UnityEngine;

namespace PV3.Audio
{
    public enum BackgroundMusicType
    {
        Main,
        Boss,
        Victory,
        Defeat
    }

    [System.Serializable]
    public class BackgroundMusic
    {
        public BackgroundMusicType type;
        public AudioClip mainClip;
        public AudioClip introClip;
        public bool playedIntroClip;
    }
}