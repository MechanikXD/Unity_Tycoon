using Core.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class ButtonPlaySoundOnClick : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private AudioClip _clip;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PLaySound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PLaySound);
        }

        private void PLaySound()
        {
            AudioManager.Instance.PlaySound(_clip, transform.position, spatialBlend:0f);
        }
    }
}