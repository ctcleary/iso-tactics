using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Might be a totally unneccesary struct now.
// Keep it TEMP in case the separation becomes useful
// soon, otherwise, scrap it.
public struct TileData {
    public Vector3 worldPos;
    public TileKind kind;

    public TileData(Vector3 worldPos, TileKind kind) {
        this.worldPos = worldPos;
        this.kind = kind;
    }
}


public class MapBuilder : MonoBehaviour {
    // TODO TEMP
    List<TileData> testMap = new List<TileData>();

    GameObject mapGraphPrefab;
    GameObject tileRendererPrefab;
    GameObject alphaWalkPrefab;

    MapGraph mapGraph;

    private static int ROW_COUNT = 12;
    private static int COL_COUNT = 10;

    // Load Prefab references.
    //void Awake() {
    //    tileRendererPrefab = (GameObject)Resources.Load("/Prefabs/TileRenderer");
    //}

    // Use this for initialization
    void Start () {
        mapGraphPrefab = (GameObject)Resources.Load("Prefabs/MapGraph");
        tileRendererPrefab = (GameObject)Resources.Load("Prefabs/TileRenderer");
        alphaWalkPrefab = (GameObject)Resources.Load("Prefabs/AlphaWalk");

        for (int col = 0; col < COL_COUNT; col++) {
            List<TileData> rowData = new List<TileData>();
            for (int row = 0; row < ROW_COUNT; row++) {
                //TileKind k = (rand(0, 7) > 1) ? TileKind.GROUND : TileKind.HALF_GROUND;
                TileKind k = TileKind.GROUND;

                if (row % 2 != 0) {
                    k = TileKind.HALF_GROUND;
                }

                Vector3 worldPos = new Vector3(col, 0, row);
                TileData td = new TileData(worldPos, k);
                
                rowData.Add(td);
            }
            testMap.AddRange(rowData);
        }

        GameObject tileContainer = new GameObject();
        tileContainer.name = "TilesContainer";

        mapGraph = InitMapGraph();
        
        // probably an unnecessary loop
        foreach (TileData td in testMap) {
            Tile tile = new Tile(td.kind, td.worldPos);
            TileRenderer tileRenderer = Instantiate(tileRendererPrefab)
                .GetComponent<TileRenderer>();
            
            tileRenderer.SetTile(tile);
            tileRenderer.transform.SetParent(tileContainer.transform);

            mapGraph.AddNode(tile.WorldPosition, tile.Kind);
        }
        mapGraph.FinalizeMap();
        FinalizeMapGraph();
    }

    // Update is called once per frame
    //void Update () {

    //}

    private MapGraph InitMapGraph() {
        MapGraph mapGraph = Instantiate(mapGraphPrefab)
            .GetComponent<MapGraph>();

        mapGraph.SetDimensions(ROW_COUNT, COL_COUNT);
        return mapGraph;
    }

    private void FinalizeMapGraph() {
        //GameObject alphaWalk = Instantiate(alphaWalkPrefab);
        //if (tile.Kind == TileKind.GROUND) {
        //    alphaWalk.transform.position = tile.MapPosition + new Vector3(0, 1, 0);
        //} else if (tile.Kind == TileKind.HALF_GROUND) {
        //    alphaWalk.transform.position = tile.MapPosition + new Vector3(0, 0.5f, 0);
        //}
    }


    private int rand(float min, float max) {
        float f = UnityEngine.Random.Range(min, max+1);
        int i = Mathf.FloorToInt(f);
        return i;
    }
}


