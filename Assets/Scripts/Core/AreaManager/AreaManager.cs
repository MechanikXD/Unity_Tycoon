using System.Collections.Generic;
using Core.Behaviour.SingletonBehaviour;
using UnityEngine;

namespace Core.AreaManager
{
    public class AreaManager : SingletonBase<AreaManager>
    {
        private Dictionary<Vector2Int, BuildingArea> _areas;

        protected override void Awake()
        {
            base.Awake();
            _areas = new Dictionary<Vector2Int, BuildingArea>();
        }
        
        public bool HasAreaAt(Vector2Int coordinates)
        {
            return _areas.ContainsKey(coordinates);
        }
        
        public void AddArea(BuildingArea area)
        {
            _areas.TryAdd(area.ChunkCoordinate, area);
        }
    }
}