using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TileRenderer : MonoBehaviour {

    Tile tile;
    
    GameObject mesh;
    Dictionary<TileKind, GameObject> MeshMap;

    public TileRenderer (Tile tile) {
        this.tile = tile;
    }

    // Load Prefab references.
    void Awake() {
        MeshMap = new Dictionary<TileKind, GameObject> {
            { TileKind.GROUND, (GameObject)Resources.Load("Prefabs/FullBlock") },
            { TileKind.HALF_GROUND, (GameObject)Resources.Load("Prefabs/HalfBlock") }
        };
    }

	// Use this for initialization
	void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTile(Tile tile) {
        this.tile = tile;

        DetermineMesh(tile.Kind);
        this.transform.position = tile.WorldPosition;

        if (tile.Kind == TileKind.NULL) {
            Debug.Log("No information for Tile.");
        }
    }

    private void DetermineMesh(TileKind kind) {
        GameObject meshPrefab;
        if (MeshMap.TryGetValue(kind, out meshPrefab)) {
            mesh = Instantiate(meshPrefab);
            mesh.transform.SetParent(this.transform);
        }
    }
}
