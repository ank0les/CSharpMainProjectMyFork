using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class Buff
    {
        public float Duration { get; private set; }
        public float MoveSpeedModifier { get; private set; }
        public float AttackSpeedModifier { get; private set; }

        public Buff(float duration, float moveSpeedModifier, float attackSpeedModifier)
        {
            Duration = duration;
            MoveSpeedModifier = moveSpeedModifier;
            AttackSpeedModifier = attackSpeedModifier;
        }

        
        public void MinusDuration(float time)
        {
            Duration -= time;
            if (Duration <= 0)
            {
                Expired();
            }
        }

        protected virtual void Expired()
        {
            Debug.Log("Buff Expired!");
        }
    }
}
