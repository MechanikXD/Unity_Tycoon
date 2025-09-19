using UnityEngine;

namespace Other
{
    public class TextureTilingMove : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Vector2 _moveSpeed;
        private Material _renderMaterial;
        private Vector2 _currentOffset;

        private void Awake()
        {
            _renderMaterial = _renderer.material;
            _currentOffset = Vector2.zero;
        }

        private void Update()
        {
            _currentOffset += _moveSpeed * Time.deltaTime;
            _renderMaterial.mainTextureOffset = _currentOffset;
        }
    }
}