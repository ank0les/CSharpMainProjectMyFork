using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Runtime.ReadOnly;
using UnitBrains;
using UnityEngine;
using Utilities;

namespace UnitBrains.Player
{
    public class PlayerPosUtil : ROunitUtil, IDisposable
    {
        public Vector2Int RecommendedTarget { get; private set; }
        public Vector2Int RecommendedPoint { get; private set; }
        private IReadOnlyRuntimeModel _runtimeModel;
        private TimeUtil _timeUtil;
        private float _playerAttackRange;
        private float _unitAttackRange;
        private bool _enemiesOnPlayerHalf;
        private UnitSorter _unitSorter;
        public PlayerPosUtil() 
        {
            _runtimeModel = ServiceLocator.Get<IReadOnlyRuntimeModel>();
            _timeUtil = ServiceLocator.Get<TimeUtil>();
            _unitSorter = ServiceLocator.Get<UnitSorter>();
            _timeUtil.AddFixedUpdateAction(BetterUpdate);
        }
        private void BetterUpdate(float deltaTime)
        {
            var Targets = _runtimeModel.RoBotUnits.ToList();

            if (Targets.Any())
            {
                CheckMap();
                UpdateRecommendedTarget(Targets);
                UpdateRecommendedPoint(Targets);
                return;
            }

            var targetBase = _runtimeModel.RoMap.Bases[RuntimeModel.BotPlayerId];
            RecommendedTarget = targetBase;
            RecommendedPoint = targetBase;
        }

        private void CheckMap()
        {
            foreach (var unit in _runtimeModel.RoBotUnits)
            {
                if (MapIsCrossed(unit.Pos))
                {
                    _enemiesOnPlayerHalf = true;
                    return;
                }
            }

            _enemiesOnPlayerHalf = false;
        }

        private float GetUnitAttackRange()
        {
            var playerUnits = _runtimeModel.RoPlayerUnits.ToList();

            if (playerUnits.Any())
                return playerUnits.First().Config.AttackRange;
            else
                return 1f;
        }

        private void UpdateRecommendedTarget(List<IReadOnlyUnit> botUnits)
        {
            if (_enemiesOnPlayerHalf)
                _unitSorter.SortByDistanceToBase(botUnits, EBaseType.PlayerBase);
            else
                _unitSorter.SortByHealth(botUnits);

            RecommendedTarget = botUnits.First().Pos;
        }


        private void UpdateRecommendedPoint(List<IReadOnlyUnit> botUnits)
        {
            if (_enemiesOnPlayerHalf)
            {
                RecommendedPoint = _runtimeModel.RoMap.Bases[RuntimeModel.PlayerId] + Vector2Int.up;
            }
            else
            {
                _unitSorter.SortByDistanceToBase(botUnits, EBaseType.PlayerBase);
                _unitAttackRange = GetUnitAttackRange();
                int x = botUnits.First().Pos.x;
                int y = botUnits.First().Pos.y - Mathf.FloorToInt(_unitAttackRange);
                RecommendedPoint = new Vector2Int(x, y);
            }
        }

        private bool MapIsCrossed(Vector2Int botPos)
        {
            int playerBaseY = _runtimeModel.RoMap.Bases[RuntimeModel.PlayerId].y;
            int botBaseY = _runtimeModel.RoMap.Bases[RuntimeModel.BotPlayerId].y;
            int pointsToBorder = (botBaseY - playerBaseY) / 2;

            return botBaseY - botPos.y > pointsToBorder;
        }

        public void Dispose()
        {
            _timeUtil.RemoveFixedUpdateAction(BetterUpdate);
        }

    }
}