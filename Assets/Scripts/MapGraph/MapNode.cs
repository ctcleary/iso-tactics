using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode {
    TileKind kind;
    XY mapPos;
    float worldHeight;
    //Dictionary<Dir, EdgeData> edges = new Dictionary<Dir, EdgeData>();
    Dictionary<Dir, EdgeData> edges = new Dictionary<Dir, EdgeData>(4);

    public MapNode(TileKind kind, XY mapPos, float worldHeight) {
        this.kind = kind;
        this.mapPos = mapPos;
        this.worldHeight = worldHeight;
    }

    public bool HasEdgeData(Dir dir) {
        return edges[dir] != null;
    }

    public void SetEdgeData(Dir dir, EdgeData edgeData) {
        edges[dir] = edgeData;
    }

    public XY MapPos {
        private set { }
        get { return mapPos; }
    }

    public float WorldHeight {
        private set { }
        get { return worldHeight; }
    }
}