using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class HexGrid : MonoBehaviour
{
    HexTile[,] grid;
    public HexTile[,] Grid { get => grid; set => grid = value; }


    [SerializeField][HideInInspector]
    private float size;   
    
    [Header("Values")]
    [ReadOnly][SerializeField]
    private float width;
    [ReadOnly][SerializeField]
    private float height;

    [SerializeField]
    int defaultMax = 20;

    private enum HexType
    {
        Flat,
        Pointy
    }

    [SerializeField] [HideInInspector]
    private HexType type;

    [SerializeField] [HideInInspector]
    private bool inSnap;

    




    // Start is called before the first frame update
    void Start()
    {
        grid = new HexTile[defaultMax, defaultMax];    
    }

 
    public void ChangeSize()
    {
        if (type == HexType.Pointy)
        {
            width = Mathf.Sqrt(3) * size;
            height = 2 * size;
        }
        else if (type == HexType.Flat)
        {
            width = 2 * size;
            height = Mathf.Sqrt(3) * size;           
        }
    }

    private void OnDrawGizmos()
    {
        if (!inSnap) return;

        Vector3 topLeft = 
            transform.position - new Vector3(size * defaultMax, 0, size * defaultMax);
        
        grid = new HexTile[defaultMax, defaultMax];



        if (type == HexType.Pointy)
        {
            Vector3 currPos = topLeft + new Vector3(size, 0, 0);
            for (int i = 0; i < defaultMax; i++)
            {

                for (int j = 0; j < defaultMax; j++)
                {
                    DrawHex(currPos);
                    grid[i, j] = new HexTile(currPos, null);
                    currPos += new Vector3(width, 0, 0);
                }

                bool indent = i % 2 == 0;
                float indentPos = topLeft.x + size;
                float xPos = !indent ? indentPos : indentPos - width / 2;

                currPos = new Vector3(xPos, currPos.y, currPos.z + (size / 2 * 3));

            }
        }
        else
        {
            Vector3 currPos = topLeft + new Vector3(0, 0, size);
            for (int i = 0; i < defaultMax; i++)
            {

                for (int j = 0; j < defaultMax; j++)
                {
                    grid[i, j] = new HexTile(currPos, null);
                    DrawHex(currPos);
                    currPos += new Vector3(0, 0, height);
                }

                bool indent = i % 2 == 0;
                float indentPos = topLeft.z + size;
                float zPos = !indent ? indentPos : indentPos + height/2;

                currPos = new Vector3(currPos.x + (width /4 * 3), currPos.y, zPos);

            }
        }

        foreach (Transform t in transform)
        {
            HexTile hex = ClosestTileAt(t.position);
            Vector3 tilePos = hex.Pos;
            hex.Tile = t.gameObject;
            t.gameObject.transform.position = tilePos;
        }
    
    }

    private void DrawHex(Vector3 center)
    {
        

        Vector3 point_0 = GetHexCorner(center, 0);
        Vector3 point_1 = GetHexCorner(center, 1);
        Vector3 point_2 = GetHexCorner(center, 2);
        Vector3 point_3 = GetHexCorner(center, 3);
        Vector3 point_4 = GetHexCorner(center, 4);
        Vector3 point_5 = GetHexCorner(center, 5);

        Gizmos.DrawLine(point_0, point_1);
        Gizmos.DrawLine(point_1, point_2);
        Gizmos.DrawLine(point_2, point_3);
        Gizmos.DrawLine(point_3, point_4);
        Gizmos.DrawLine(point_4, point_5);
        Gizmos.DrawLine(point_5, point_0);
    }

    private Vector3 GetHexCorner(Vector3 center, int i)
    {
        int angle = type == HexType.Flat ? 0 : 30;
        var angle_deg = 60 * i - angle;
        var angle_rad = Mathf.PI / 180 * angle_deg;
        return new Vector3(center.x + size * Mathf.Cos(angle_rad),
                                center.y, 
                                center.z + size * Mathf.Sin(angle_rad));
    }

    public HexTile ClosestTileAt(Vector3 pos)
    {
        return grid.Cast<HexTile>().Where(x => x.Tile == null)
            .OrderBy(x => Vector3.Distance(pos, x.Pos)).First();
    }


    #if UNITY_EDITOR
    public HexTile ClosestTilePointOnScreen(Vector3 pos, SceneView scene)
    {
        return grid.Cast<HexTile>()
            .Where(x => x.Tile == null)
            .OrderBy(x => Vector3.Distance(pos, scene.camera.WorldToScreenPoint(x.Pos))).First();
    }
    #endif
}
