using UnityEngine;

namespace Core.AreaManager
{
    public class BuildingArea : MonoBehaviour
    {
        [SerializeField] private BuildingArea _prefab;
        [SerializeField] private MistObject _mist;
        
        [SerializeField] private Vector2 _areaSize;
        [SerializeField] private int _originalCost;
        [SerializeField] private float _distanceMultiplayer;

        public int Cost { get; private set; }

        public Vector2Int ChunkCoordinate { get; private set; }
        
        public void Initialize(Vector2Int newCoordinates)
        {
            ChunkCoordinate = newCoordinates;
            var tempCost = (float)_originalCost;
            for (var i = 0; i < Mathf.Abs(newCoordinates.x) + Mathf.Abs(newCoordinates.y) - 1; i++)
            {
                tempCost *= _distanceMultiplayer;
            }

            Cost = (int)tempCost;
            AreaManager.Instance.AddArea(this);
        }

        public void UnlockArea()
        {
            Destroy(_mist.gameObject);
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
                instance.Initialize(leftArea);
            }
            
            var rightArea = ChunkCoordinate;
            rightArea.x += 1;
            if (!AreaManager.Instance.HasAreaAt(rightArea))
            {
                var position = transform.position;
                position.x += _areaSize.x;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {rightArea}";
                instance.Initialize(rightArea);
            }
            
            var upArea = ChunkCoordinate;
            upArea.y += 1;
            if (!AreaManager.Instance.HasAreaAt(upArea))
            {
                var position = transform.position;
                position.z += _areaSize.y;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {upArea}";
                instance.Initialize(upArea);
            }
            
            var downArea = ChunkCoordinate;
            downArea.y -= 1;
            if (!AreaManager.Instance.HasAreaAt(downArea))
            {
                var position = transform.position;
                position.z -= _areaSize.y;
                var instance = Instantiate(_prefab, position, Quaternion.identity);
                instance.name = $"Building Area {downArea}";
                instance.Initialize(downArea);
            }
        }
    }
}