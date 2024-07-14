using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

namespace RDCT
{
    enum AudioType
    {
        BGM,
        SFX,
        VCL
    }

    [System.Serializable]
    class AudioObject
    {
        public string AudioName;
        public AudioClip AudioClip;
        [Range(-3f, 3f)] public float AudioPitch = 1f;
        [Range(0f, 1f)] public float AudioVolume = 1f;
        [HideInInspector] public AudioSource Source;
        public bool Looping;
        public bool PlayOnAwake;
    }

    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerGroup BGMGroup;
        [SerializeField] private AudioMixerGroup SFXGroup;
        [SerializeField] private AudioMixerGroup VCLGroup;
        [SerializeField] private AudioObject[] audioBGM;
        [SerializeField] private AudioObject[] audioSFX;
        [SerializeField] private AudioObject[] audioVCL;

        private AudioSource currentBGMPlaying;

        private static AudioController _instance;
        public static AudioController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Audio Controller is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            DontDestroyOnLoad(gameObject);

            InitializeAudios();
        }

        private void InitializeAudios()
        {
            foreach (var a in audioBGM)
            {
                AudioSource aSource = gameObject.AddComponent<AudioSource>();

                aSource.clip = a.AudioClip;
                aSource.loop = a.Looping;
                aSource.pitch = a.AudioPitch;
                aSource.volume = a.AudioVolume;
                aSource.playOnAwake = a.PlayOnAwake;
                aSource.outputAudioMixerGroup = BGMGroup;

                a.Source = aSource;

                if (aSource.playOnAwake)
                    aSource.Play();
                else
                    aSource.Stop();
            }

            foreach (var a in audioSFX)
            {
                AudioSource aSource = gameObject.AddComponent<AudioSource>();

                aSource.clip = a.AudioClip;
                aSource.loop = a.Looping;
                aSource.pitch = a.AudioPitch;
                aSource.volume = a.AudioVolume;
                aSource.playOnAwake = a.PlayOnAwake;
                aSource.outputAudioMixerGroup = SFXGroup;

                a.Source = aSource;
            }

            foreach (var a in audioVCL)
            {
                AudioSource aSource = gameObject.AddComponent<AudioSource>();

                aSource.clip = a.AudioClip;
                aSource.loop = a.Looping;
                aSource.pitch = a.AudioPitch;
                aSource.volume = a.AudioVolume;
                aSource.playOnAwake = a.PlayOnAwake;
                aSource.outputAudioMixerGroup = VCLGroup;

                a.Source = aSource;
            }
        }

        private AudioObject SearchAudio(string audioName, AudioType type)
        {
            AudioObject audio = null;

            switch (type)
            {
                case AudioType.BGM:
                    audio = audioBGM.FirstOrDefault(s => s.AudioName == audioName);
                    break;

                case AudioType.SFX:
                    audio = audioSFX.FirstOrDefault(s => s.AudioName == audioName);
                    break;

                case AudioType.VCL:
                    audio = audioVCL.FirstOrDefault(s => s.AudioName == audioName);
                    break;
            }

            if (audio == null)
            {
                Debug.LogError("Audio is not available or you're searching in the wrong type.");
                return null;
            }

            return audio;
        }

        // - Description: Plays a background music
        // - Params:
        //      - string audioName: The audio that will be played
        // - Returns: none
        public void PlayBGM(string audioName)
        {
            var audio = SearchAudio(audioName, AudioType.BGM);
            if (audio == null)
                return;

            foreach (var a in audioBGM)
            {
                a.Source.Stop();
            }

            currentBGMPlaying = audio.Source;
            audio.Source.Play();
        }

        // - Description: Plays a sound effect
        // - Params:
        //      - string audioName: The audio that will be played
        // - Returns: none
        public void PlaySFX(string audioName)
        {
            var audio = SearchAudio(audioName, AudioType.SFX);
            if (audio == null)
                return;

            foreach (var a in audioBGM)
            {
                a.Source.Stop();
            }

            currentBGMPlaying = audio.Source;

            if (audio.Looping) 
                audio.Source.Play();
            else
                audio.Source.PlayOneShot(audio.AudioClip);
        }
    }
}
