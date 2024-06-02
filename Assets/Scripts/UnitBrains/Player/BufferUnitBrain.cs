using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Model;
using Model.Runtime.Projectiles;
using Unity.VisualScripting;
using UnityEngine;
using Model.Runtime.ReadOnly;
using UnityEngine.UIElements;
using UnitBrains.Player;
using Utilities;
using View;
using Assets.Scripts.Controller;
using Model.Runtime;

namespace Assets.Scripts.UnitBrains.Player
{
    public class BufferUnitBrain : DefaultPlayerUnitBrain
    {
        public override string TargetUnitName => "Buffer"; 
        private float buffTimer = 0;
        private float buffInterval = 6.5f;
        UnitBuffs UnitBuffs = new UnitBuffs();
        BuffsController BuffsController = new BuffsController();
        Buff moveBuff = new Buff(2, 1, 0, 0, 0);
        Buff attackBuff = new Buff(2, 0, 1, 0, 0);
        Buff doubleShotBuff = new Buff(2, 0, 0, 1, 0);
        Buff attackRangeBuff = new Buff(2, 0, 0, 0, 1);
        private IEnumerable<IReadOnlyUnit> RoPlayerUnits;
        private VFXView _vfxView;

        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
        {
        }

        public override void Update(float deltaTime, float time)
        {
            base.Update(deltaTime, time);

            buffTimer += deltaTime;
            if (buffTimer >= buffInterval)
            {
                AddBuffs(RoPlayerUnits, moveBuff);
                buffTimer = 0;
            }
        }
        public void AddBuffs(IEnumerable<IReadOnlyUnit> RoPlayerUnits, Buff buffToApply)
        {
            foreach (Unit myUnit in RoPlayerUnits)
            {
                var _type = myUnit.Config.UnitType;
                if (IsTargetInRange(myUnit.Pos))
                {  
                    switch(_type)
                    {
                        case "SecondUnit":                                                       
                            BuffsController.GiveBuff(myUnit, doubleShotBuff);
                            _vfxView.PlayVFX(myUnit.Pos, VFXView.VFXType.BuffApplied);
                            break;
                        case "ThirdUnit":
                            BuffsController.GiveBuff(myUnit, attackRangeBuff);
                            _vfxView.PlayVFX(myUnit.Pos, VFXView.VFXType.BuffApplied);
                            break;
                    }
                    if (!BuffsController.IsBuffed(myUnit))
                    {
                        BuffsController.GiveBuff(myUnit, buffToApply);
                        _vfxView.PlayVFX(myUnit.Pos, VFXView.VFXType.BuffApplied);
                    }
                    
                }
                
               
            }
        }
    }
}
