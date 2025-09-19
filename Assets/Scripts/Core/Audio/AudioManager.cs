using System.Collections;
using System.Collections.Generic;
using Core.Behaviour.Singleton;
using UnityEngine;

namespace Core.Audio
{
    public class AudioManager : SingletonBase<AudioManager>
    {
        [SerializeField] private AudioSource _musicSource;

        [SerializeField] private int _localSourcePoolCount = 31;
        [SerializeField] private AudioSource _localSource;
        private Queue<AudioSource> _localSourcePool;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void PlaySound(AudioClip clip, Vector3 position, float reach = 20, float pitch = 1f)
        {
            if (_localSourcePool.Peek().isPlaying)
            {
                var newSource = Instantiate(_localSource, transform);
                newSource.clip = clip;
                newSource.transform.position = position;
                newSource.maxDistance = reach;
                newSource.pitch = pitch;

                newSource.Play();
                StartCoroutine(DestroyDetachedSourceAfterFinish(newSource));
                return;
            }

            var source = _localSourcePool.Dequeue();

            source.clip = clip;
            source.transform.position = position;
            source.maxDistance = reach;
            source.pitch = pitch;

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
    }
}