using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSignal
{
    public class MergedObjectCollision
    {
        public int MergedObjectID;
        public Vector3 HitPosition;
        public MergedObjectCollision(int mergedObjectID, Vector3 hitPosition)
        {
            MergedObjectID = mergedObjectID;
            HitPosition = hitPosition;
        }
    }

    
}
