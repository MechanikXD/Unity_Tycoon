using System;
using System.Collections.Generic;
using System.Linq;
using Core.Behaviour.Singleton;
using Core.DataSave;
using Player;
using UnityEngine;

namespace Core.AreaManager
{
    public class AreaManager : SingletonBase<AreaManager>, ISaveAble
    {
        [SerializeField] private float _topBoundsCorrection = -15f;
        [SerializeField] private Vector2 _chunkSize = new Vector2(30f, 30f);
        private Dictionary<Vector2Int, BuildingArea> _areas;
        private Vector4 _chunkBounds;

        protected override void Awake()
        {
            base.Awake();
            SaveManager.Register("Area Manager", this);
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

        public object SaveData()
        {
            var areas = new List<Vector2Int>();

            foreach (var area in _areas)
            {
                if (area.Value.IsUnlocked) areas.Add(area.Key);
            }
            
            return new ChunkCoordinateData(areas
                .OrderBy(v => Mathf.Abs(v.x) + Mathf.Abs(v.y))
                .ToArray());
        }

        public void LoadData(object data)
        {
            var coordinateData = (ChunkCoordinateData)data;
            foreach (var coordinates in coordinateData.Coordinate)
            {
                _areas[coordinates].UnlockArea(out _);
            }
        }
    }

    [Serializable]
    public class ChunkCoordinateData
    {
        [SerializeField] private Vector2Int[] _coordinate;

        public Vector2Int[] Coordinate => _coordinate;
            
        public ChunkCoordinateData(Vector2Int[] coordinate)
        {
            _coordinate = coordinate;
        }
    }
}