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
    public class BuffsController : MonoBehaviour
    {
        private Dictionary<Unit, List<Buff>> _buffs = new Dictionary<Unit, List<Buff>>();
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
        }
    }
}
