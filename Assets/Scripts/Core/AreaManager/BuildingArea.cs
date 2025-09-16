using UnityEngine;

namespace Core.AreaManager
{
    public class BuildingArea : MonoBehaviour
    {
        [SerializeField] private BuildingArea _prefab;
        [SerializeField] private MistObject _mist;
        
        [SerializeField] private Vector2 _areaSize;
        [SerializeField] private bool _isStartingArea;
        [SerializeField] private int _originalCost;
        [SerializeField] private float _distanceMultiplayer;

        private int _cost;
        private bool _isAvailable;

        public int Cost => _cost;

        public Vector2Int ChunkCoordinate { get; private set; }

        public void Start()
        {
            AreaManager.Instance.AddArea(this);
            if (_isStartingArea || _isAvailable)
            {
                _isAvailable = true;
                UnlockArea();
            }
        }
        
        private void SetValues(Vector2Int newCoordinates, bool isAvailable=false)
        {
            _isStartingArea = false;
            ChunkCoordinate = newCoordinates;
            var tempCost = (float)_originalCost;
            for (var i = 0; i < Mathf.Abs(newCoordinates.x) + Mathf.Abs(newCoordinates.y) - 1; i++)
            {
                tempCost *= _distanceMultiplayer;
            }

            _cost = (int)tempCost;
            
            if (isAvailable)
            {
                _isAvailable = true;
                UnlockArea();
            }
        }

        public void UnlockArea()
        {
            if (_mist == null) return;
            
            Destroy(_mist.gameObject);
            _mist = null;
            CreateAdjacentAreas();
        }

        private void CreateAdjacentAreas()
        {
            var leftArea = ChunkCoordinate;
            leftArea.x -= 1;
            if (!AreaManager.Instance.HasAreaAt(leftArea))
            {
                var position = transform.position;
                position.x -= _areaSize.x;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {leftArea}";
                instance.SetValues(leftArea);
            }
            
            var rightArea = ChunkCoordinate;
            rightArea.x += 1;
            if (!AreaManager.Instance.HasAreaAt(rightArea))
            {
                var position = transform.position;
                position.x += _areaSize.x;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {rightArea}";
                instance.SetValues(rightArea);
            }
            
            var upArea = ChunkCoordinate;
            upArea.y += 1;
            if (!AreaManager.Instance.HasAreaAt(upArea))
            {
                var position = transform.position;
                position.z += _areaSize.y;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {upArea}";
                instance.SetValues(upArea);
            }
            
            var downArea = ChunkCoordinate;
            downArea.y -= 1;
            if (!AreaManager.Instance.HasAreaAt(downArea))
            {
                var position = transform.position;
                position.z -= _areaSize.y;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {downArea}";
                instance.SetValues(downArea);
            }
        }
    }
}