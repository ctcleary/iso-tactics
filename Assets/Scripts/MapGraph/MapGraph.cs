using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public enum Dir {
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3
};

public struct XY {
    public int x, y;

    public XY(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

/**
 * noob notes until this is memorized.
 *
 * in iso, the far left point is 0,0
 * the lowest point is [cols], 0
 * this is the first [row]
 *
 * x + 1 = increments current "cols" position by 1,
 * by moving down a row.
 *
 *
 */
public class MapGraph : MonoBehaviour {

    MapNode[,] mapNodes;
    int rows = 0;
    int cols = 0;
    
    public void SetDimensions(int rows, int cols) {
        this.rows = rows;
        this.cols = cols;
        mapNodes = new MapNode[cols, rows];
    }

    public void AddNode(Vector3 worldPosition, TileKind k) {
        XY xy = XYPos(worldPosition);
        mapNodes[xy.x, xy.y] = new MapNode(k, xy, worldPosition.y);
    }

    public void FinalizeMap() {
        Debug.Log("FinalizeMap " + mapNodes.Length);

        MapNode current;
        MapNode adjacent;

        int justInCase = 0;
        
        int row = 0;
        int col = 0;

        while (row < this.rows && col < this.cols) {
            justInCase++;

            // x: col, y: row
            current = mapNodes[col, row];
            
            // Set boundary on map ends.
            if (row == 0) SetBoundaryEdge(Dir.SOUTH, current);
            if (col == 0) SetBoundaryEdge(Dir.WEST, current);

            // Run through the row by iterating column index;
            bool isLastCol = (col == this.cols - 1);
            bool isLastRow = (row == this.rows - 1);

            if (!isLastCol) {
                adjacent = GetNeighbor(current, Dir.EAST);
                EdgeData edgeDataEast = GetEdgeData(current, adjacent);
                current.SetEdgeData(Dir.EAST, edgeDataEast);
                adjacent = null;
            }
            if (!isLastRow) {
                adjacent = GetNeighbor(current, Dir.NORTH);
                EdgeData edgeDataNorth = GetEdgeData(current, adjacent);
                current.SetEdgeData(Dir.NORTH, edgeDataNorth);
                adjacent = null;
            }
            
            if (isLastCol && !isLastRow) {
                // Last col.
                SetBoundaryEdge(Dir.EAST, current);
                col = 0; // Reset column index.
                row++; // Increment row index;
                continue;
            }

            col++;

            if (justInCase > 400) {
                Debug.Log("just in case loop break");
                break;
            }
        }
    }

    private XY XYPos(Vector3 worldPos) {
        return new XY(
            Mathf.FloorToInt(worldPos.x),
            Mathf.FloorToInt(worldPos.z)
        );
    }

    private void SetBoundaryEdge(Dir direction, MapNode mapNode) {
        EdgeData boundaryData = new EdgeData();
        boundaryData.SetIsMapBoundary();
        mapNode.SetEdgeData(direction, boundaryData);
    }

    private EdgeData GetEdgeData(MapNode current, MapNode adjacent) {
        EdgeData result = new EdgeData();
        result.heightChange = adjacent.WorldHeight - current.WorldHeight;
        return result;
    }

    private MapNode GetNeighbor(MapNode curr, Dir d) {
        XY currXY = curr.MapPos;
        XY nextXY = GoDir(currXY, d);
        if (nextXY.x < this.cols && nextXY.y < this.rows) {
            MapNode neighbor = mapNodes[nextXY.x, nextXY.y];
            return neighbor;
        } else {
            return null;
        }
    }

    private XY GoDir(XY currXY, Dir d) {
        XY nextXY = currXY;
        switch (d) {
            case Dir.NORTH:
                nextXY.y += 1;
                break;
            case Dir.EAST:
                nextXY.x += 1;
                break;
            case Dir.SOUTH:
                nextXY.y -= 1;
                break;
            case Dir.WEST:
                nextXY.x += 1;
                break;
        }
        return nextXY;
    }
}

