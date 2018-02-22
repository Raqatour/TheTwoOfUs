using UnityEngine;

namespace TwoOfUs.Player.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : TwoOfUsBehaviour, IPlayerController
    {
        [SerializeField]
        protected AudioClip exhale, inhale, whoosh;

        public bool IsPlaying
        {
            get { return audio.isPlaying; }
        }
        
        private new AudioSource audio;
        private float initialVolume;
        
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public Creator Creator { get; private set; }

        public void Init(Creator creator)
        {
            Creator = creator;
        }

        public void Stop()
        {
            audio.Stop();
        }

        public void Exhale(float volumeScale = 1f)
        {
            Play(exhale, volumeScale);
        }

        public void Inhale(float volumeScale = 1f)
        {
            Play(inhale, volumeScale);
        }

        public void Whoosh(float volumeScale = 1f)
        {
            Play(whoosh, volumeScale);
        }

        protected virtual void Awake()
        {
            audio = GetComponent<AudioSource>();
            initialVolume = audio.volume;
            audio.loop = false;
        }

        private void Play(AudioClip clip, float volumeScale = 1f)
        {
            if (IsPlaying)
            {
                return;
            }

            audio.volume = volumeScale * initialVolume;
            audio.clip = clip;
            audio.Play();
        }
    }
}