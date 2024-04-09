using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Runtime;
using Model.Runtime.ReadOnly;
using UnitBrains;
using UnityEngine;
using Utilities;

namespace Utilities
{
    public class UnitBuffs 
    {
        bool SpeedUp;
        bool SpeedDown;
        bool ShotSpeedUp;
        bool ShotSpeedDown; 
        RuntimeModel _runtimeModel = ServiceLocator.Get<RuntimeModel>();
        float UnitSpeed;
        float UnitShotSpeed;

        public float GetSpeed()
        {
            var Units = _runtimeModel.RoUnits.ToList();

            if (Units.Any())
                return Units.First().Config.MoveDelay;
            else return 1f;
        }
        public float GetShotSpeed()
        {
            var Units = _runtimeModel.RoUnits.ToList();

            if (Units.Any())
                return Units.First().Config.AttackDelay;
            else return 1f;
        }
        public void GetBuffed()
        {
            var Units = _runtimeModel.RoUnits.ToList();
            GetSpeed();
            GetShotSpeed();
            if (Units.First().Health <= Units.First().Health / 2)
            {
                ShotSpeedDown = true;
                ShotSpeedUp = false;
            }
                
            if (Units.First().Health <= Units.First().Health / 4)
            {
                ShotSpeedDown = true;
                ShotSpeedUp = false;
                SpeedDown = true;
                SpeedUp = false;
            }
                
            if (Units.First().Health >= Units.First().Health / 4)
            {
                ShotSpeedDown = false;
                ShotSpeedUp = true;
            }
            if(Units.First().Health >= Units.First().Health / 2)
            {
                ShotSpeedUp = true;
                SpeedUp = true;
                ShotSpeedDown = false;
                SpeedDown = false;
            }
            
        }
        public void BuffEffects()
        {
            var Units = _runtimeModel.RoUnits.ToList();
            GetSpeed();
            GetShotSpeed();
            GetBuffed();

            if(SpeedUp)
            {
                UnitSpeed *= 1.25f;
            }
            if(SpeedDown)
            {
                UnitSpeed *= 0.75f;
            }
            if(ShotSpeedDown)
            {
                UnitShotSpeed *= 0.75f;
            }
            if (ShotSpeedDown)
            {
                UnitShotSpeed *= 1.25f;
            }
        }
    }
}