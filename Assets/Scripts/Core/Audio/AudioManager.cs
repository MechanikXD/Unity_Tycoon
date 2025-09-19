using System;
using System.Collections;
using System.Collections.Generic;
using Core.Behaviour.Singleton;
using Core.DataSave;
using UnityEngine;

namespace Core.Audio
{
    public class AudioManager : SingletonBase<AudioManager>, ISaveAble
    {
        [SerializeField] private AudioSource _musicSource;

        [SerializeField] private int _localSourcePoolCount = 31;
        [SerializeField] private AudioSource _localSource;
        private Queue<AudioSource> _localSourcePool;
        private float _sfxVolume = 1f;
        private float _musicVolume = 1f;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume = value;
            _musicSource.volume = _musicVolume;
        }

        public void SetSfxVolume(float value)
        {
            _sfxVolume = value;
        }

        public void PlaySound(AudioClip clip, Vector3 position, float reach = 20, float pitch = 1f, float spatialBlend=1f)
        {
            if (_localSourcePool.Peek().isPlaying)
            {
                var newSource = Instantiate(_localSource, transform);
                newSource.clip = clip;
                newSource.transform.position = position;
                newSource.maxDistance = reach;
                newSource.pitch = pitch;
                newSource.spatialBlend = spatialBlend;
                newSource.volume = _sfxVolume;

                newSource.Play();
                StartCoroutine(DestroyDetachedSourceAfterFinish(newSource));
                return;
            }

            var source = _localSourcePool.Dequeue();

            source.clip = clip;
            source.transform.position = position;
            source.maxDistance = reach;
            source.pitch = pitch;
            source.spatialBlend = spatialBlend;
            source.volume = _sfxVolume;

            source.Play();
            _localSourcePool.Enqueue(source);
        }

        private IEnumerator DestroyDetachedSourceAfterFinish(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            Destroy(source.gameObject);
        }

        private void Initialize()
        {
            _localSourcePool = new Queue<AudioSource>();
            for (var i = 0; i < _localSourcePoolCount; i++)
            {
                _localSourcePool.Enqueue(Instantiate(_localSource, transform));
            }
        }

        public object SaveData()
        {
            return new AudioSaveData(_sfxVolume, _musicVolume);
        }

        public void LoadData(object data)
        {
            var saveData = (AudioSaveData)data;
            SetSfxVolume(saveData.SfxVolume);
            SetMusicVolume(saveData.MusicVolume);
        }
    }

    [Serializable]
    public class AudioSaveData
    {
        [SerializeField] private float _sfxVolume;
        [SerializeField] private float _musicVolume;

        public float SfxVolume => _sfxVolume;
        public float MusicVolume => _musicVolume;

        public AudioSaveData(float music, float sfx)
        {
            _sfxVolume = sfx;
            _musicVolume = music;
        }
    }
}