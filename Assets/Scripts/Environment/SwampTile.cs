using UnityEngine;
using System.Collections;

public class SwampTile : Tile
{
    public SwampTile()
    {
        envType = EnvironmentType.Swamp;
        tileColor = new Color(52f/255f, 125f/255f, 13f/255f, 1f);
        isAccessible = true;
    }
}