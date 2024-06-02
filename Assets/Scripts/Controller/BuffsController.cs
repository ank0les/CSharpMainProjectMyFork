using Model;
using Model.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Assets.Scripts.Controller
{
    public class BuffsController 
    {
        private Dictionary<Unit, List<Buff>> _buffs = new Dictionary<Unit, List<Buff>>();
        public RuntimeModel _runtimeModel = new RuntimeModel();
        Buff moveDeBuff = new Buff(2, -1, 0, 0, 0);
        Buff attackDeBuff = new Buff(2, 0, -1, 0, 0);
        List<Unit> units = new List<Unit>();
        public void GiveBuff(Unit unit, Buff buff)
        {
            if (!_buffs.ContainsKey(unit))
            {
                _buffs[unit] = new List<Buff>();
            }
            _buffs[unit].Add(buff);
        }
        public bool IsBuffed(Unit unit)
        {
            if (_buffs.TryGetValue(unit, out List<Buff> buffs))
            {
                return buffs.Count > 0;
            }
            return false;
        }
        public void Update()
        {
            _runtimeModel = ServiceLocator.Get<RuntimeModel>();
            
            foreach (var pair in _buffs)
            {
                for (int i = pair.Value.Count - 1; i >= 0; i--)
                {
                    pair.Value[i].MinusDuration(Time.deltaTime);
                    if (pair.Value[i].Duration <= 0)
                    {
                        pair.Value.RemoveAt(i);
                    }
                }
            }
            foreach (Unit Unit in _runtimeModel.AllUnits)
            {
                if(Unit.Health <= Unit.Health / 2)
                {
                    GiveBuff(Unit, moveDeBuff);
                }
            }
            foreach (Unit Unit in _runtimeModel.AllUnits)
            {
                if (Unit.Health <= Unit.Health / 4)
                {
                    GiveBuff(Unit, attackDeBuff);
                }
            }
        }
    }
}
