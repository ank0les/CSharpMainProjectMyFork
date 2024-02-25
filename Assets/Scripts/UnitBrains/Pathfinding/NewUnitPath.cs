using JetBrains.Annotations;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnitBrains;
using UnitBrains.Pathfinding;
using UnityEngine;
using System.Net;
using System.Security.Cryptography;

namespace Assets.Scripts.UnitBrains.Pathfinding
{
        
    public abstract class NewUnitPath : BaseUnitPath
    {

        public List<Vector2Int> FindPath()
        {
            List<Vector2Int> openList = new List<Vector2Int>();
            List<Vector2Int> closedList = new List<Vector2Int>();
                    
            while (openList.Count > 0)
            {
                Vector2Int currentPoint = openList[0];

                foreach (var Vector2Int in openList)                
                { 
                    
                }
            }
        }    
    }
}
