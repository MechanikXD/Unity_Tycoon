using UnityEngine;

namespace Core.AreaManager
{
    public class StartingArea : MonoBehaviour
    {
        [SerializeField] private Vector2 _areaSize;
        [SerializeField] private BuildingArea _areaPrefab;

        private void OnEnable()
        {
            CreateAdjacentAreas();
        }

        private void CreateAdjacentAreas()
        {
            var leftArea = new Vector2Int(-1, 0);
            if (!AreaManager.Instance.HasAreaAt(leftArea))
            {
                var position = transform.position;
                position.x -= _areaSize.x;
                var instance = Instantiate(_areaPrefab, position, Quaternion.identity);
                instance.name = $"Building Area {leftArea}";
                instance.Initialize(leftArea);
            }
            
            var rightArea = new Vector2Int(1, 0);
            if (!AreaManager.Instance.HasAreaAt(rightArea))
            {
                var position = transform.position;
                position.x += _areaSize.x;
                var instance = Instantiate(_areaPrefab, position, Quaternion.identity);
                instance.name = $"Building Area {rightArea}";
                instance.Initialize(rightArea);
            }
            
            var upArea = new Vector2Int(0, 1);
            if (!AreaManager.Instance.HasAreaAt(upArea))
            {
                var position = transform.position;
                position.z += _areaSize.y;
                var instance = Instantiate(_areaPrefab, position, Quaternion.identity);
                instance.name = $"Building Area {upArea}";
                instance.Initialize(upArea);
            }
            
            var downArea = new Vector2Int(0, -1);
            if (!AreaManager.Instance.HasAreaAt(downArea))
            {
                var position = transform.position;
                position.z -= _areaSize.y;
                var instance = Instantiate(_areaPrefab, position, Quaternion.identity);
                instance.name = $"Building Area {downArea}";
                instance.Initialize(downArea);
            }
        }
    }
}