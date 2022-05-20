using UnityEngine;


[ExecuteInEditMode]
public class TilePainterTools : MonoBehaviour
{
    [SerializeField]
    public GameObject tile;

    [SerializeField][HideInInspector]
    private bool paint;

    [SerializeField]
    private HexGrid grid;

    public HexGrid GetGrid()
    {
        return grid;
    }



}
