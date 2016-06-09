using UnityEngine;
using System.Collections;


public enum TileKind
{
    NULL,
    GROUND,
    HALF_GROUND,
    WALK,
    AIR
};

public class Tile {
    TileKind kind;
    Vector3 worldPos;
    int[] mapPos;
    // more to come...

    public Tile(TileKind kind, Vector3 worldPos) {
        this.kind = kind;
        this.worldPos = worldPos;
        this.mapPos = new int[2] {
            Mathf.FloorToInt(worldPos.x),
            Mathf.FloorToInt(worldPos.z)
        };
    }

    public TileKind Kind {
        private set { }
        get { return kind; }
    }

    public Vector3 WorldPosition {
        private set { }
        get { return worldPos; }
    }
}
