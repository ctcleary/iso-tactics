using UnityEngine;
using System.Collections;

public class EdgeData {
    public bool mapBound;
    public bool passable;
    //public int? moveCost;
    public float heightChange;

    public EdgeData(float heightChange = 0, bool passable = true, bool mapBound = false) {
        this.heightChange = heightChange;
        this.passable = passable;
        this.mapBound = mapBound;
    }

    public void SetIsMapBoundary() {
        mapBound = true;
        passable = false;
    }
}