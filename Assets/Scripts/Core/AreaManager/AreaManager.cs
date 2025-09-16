using System.Collections.Generic;
using Core.Behaviour.Singleton;
using Player;
using UnityEngine;

namespace Core.AreaManager
{
    public class AreaManager : SingletonBase<AreaManager>
    {
        [SerializeField] private float _topBoundsCorrection = -15f;
        [SerializeField] private Vector2 _chunkSize = new Vector2(30f, 30f);
        private Dictionary<Vector2Int, BuildingArea> _areas;
        private Vector4 _chunkBounds;

        protected override void Awake()
        {
            base.Awake();
            UpdateCameraBoundForPlayer();
            _areas = new Dictionary<Vector2Int, BuildingArea>();
        }
        
        public bool HasAreaAt(Vector2Int coordinates)
        {
            return coordinates == Vector2Int.zero || _areas.ContainsKey(coordinates);
        }
        
        public void AddArea(BuildingArea area)
        {
            _areas.TryAdd(area.ChunkCoordinate, area);

            if (area.ChunkCoordinate.x > _chunkBounds.z)
            {
                _chunkBounds.z = area.ChunkCoordinate.x;
            }
            else if (area.ChunkCoordinate.x < _chunkBounds.x)
            {
                _chunkBounds.x = area.ChunkCoordinate.x;
            }

            if (area.ChunkCoordinate.y > _chunkBounds.y)
            {
                _chunkBounds.y = area.ChunkCoordinate.y;
            }
            else if (area.ChunkCoordinate.y < _chunkBounds.w)
            {
                _chunkBounds.w = area.ChunkCoordinate.y;
            }
            
            UpdateCameraBoundForPlayer();
        }

        private void UpdateCameraBoundForPlayer()
        {
            var newBounds = new Vector4(
                _chunkBounds.x * _chunkSize.x - _chunkSize.x * 0.5f,
                _chunkBounds.y * _chunkSize.y + _chunkSize.x * 0.5f + _topBoundsCorrection, 
                _chunkBounds.z * _chunkSize.x + _chunkSize.x * 0.5f,
                _chunkBounds.w * _chunkSize.y - _chunkSize.x * 0.5f);
            PlayerController.MoveBounds = newBounds;
        }
    }
}