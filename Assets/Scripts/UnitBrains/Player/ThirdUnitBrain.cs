using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Model;
using Model.Runtime.Projectiles;
using Unity.VisualScripting;
using UnityEngine;
using UnitBrains.Player;

namespace UnitBrains.Player
{
    public class ThirdUnitBrain : DefaultPlayerUnitBrain
    {
        public string TargetUnitBrain => "Ironclad Behemoth";

        public override Vector2Int GetNextStep()
        {
            Update(1, 1);
            return unit.Pos;
        }

        protected override List<Vector2Int> SelectTargets()
        {
            List<Vector2Int> targets = new List<Vector2Int>();

            foreach (Vector2Int i in GetAllTargets())
            {
                targets.Add(i);

                if (IsPlayerUnitBrain) targets.Add(runtimeModel.RoMap.Bases[RuntimeModel.BotPlayerId]);
                
            }


            Update(1, 1);
            return targets;
            
        }


    }

}