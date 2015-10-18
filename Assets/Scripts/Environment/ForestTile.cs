using UnityEngine;
using System.Collections;

public class ForestTile : Tile
{
    public ForestTile()
    {
        envType = EnvironmentType.Forest;
        tileColor = new Color(122f / 255f, 86f / 255f, 54f / 255f, 1f);
        isAccessible = true;
    }
}