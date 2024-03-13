using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Model;
using Model.Runtime.Projectiles;
using Unity.VisualScripting;
using UnityEngine;
using UnitBrains.Player;
using Model.Runtime;
using TowerDefense.UI.HUD;
using Utilities;
using System.Net;


namespace Assets.Scripts.Utilities
{
    public class PosUtil
    {
        private bool _onPlayerSide;
        private IReadOnlyRuntimeModel _runtimeModel = ServiceLocator.Get<IReadOnlyRuntimeModel>();
        private Vector2Int _currentTarget;
        private Vector2Int _bestTargetForAttack;
        private Assets.Scripts.UnitBrains.Pathfinding.Point _recomendedPoint;
        private Assets.Scripts.UnitBrains.Pathfinding.Point _currentPoint;


        public Vector2Int UnitUtil(List<Vector2Int> units, IReadOnlyList<Vector2Int> Bases)
        {
            _currentTarget = units[0];

            if(OnPlayerSide(_currentTarget, Bases))
            {
                _bestTargetForAttack = _currentTarget;
            }

            return _bestTargetForAttack;
        }

        public Assets.Scripts.UnitBrains.Pathfinding.Point RecomendedPointUtil(List<Assets.Scripts.UnitBrains.Pathfinding.Point> points)
        {
            _currentPoint = points[0];

            if(_onPlayerSide)
            {
                _recomendedPoint = _currentPoint;
            }
            
            return _recomendedPoint;
        }

        public bool IsBigger(Vector2Int a, Vector2Int b)
        {
            if (a - b != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool OnPlayerSide(Vector2Int target, IReadOnlyList<Vector2Int> Bases)
        {
            if (IsBigger(target - Bases[0], target - Bases[0]))
            {
                _onPlayerSide = true;
                return true;
            }
            else
            {
                _onPlayerSide  = false;
                return false;
            }
        }
    }
}
