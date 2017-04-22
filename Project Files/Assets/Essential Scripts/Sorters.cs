﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortRayCastHitsByDist : IComparer<RaycastHit>
{
    public int Compare(RaycastHit x, RaycastHit y)
    {
        return x.distance.CompareTo(y.distance);
    }
}