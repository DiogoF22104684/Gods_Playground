using UnityEngine;

public class HexTile
{
    Vector3 pos;
    GameObject tile;

    public HexTile(Vector3 pos, GameObject tile)
    {
        this.pos = pos;
        this.tile = tile;
    }

    public Vector3 Pos { get => pos; set => pos = value; }
    public GameObject Tile { get => tile; set => tile = value; }
}
