using UnityEngine;
using System.Collections;

public class TallGrassTile : Tile
{
    public TallGrassTile()
    {
        envType = EnvironmentType.TallGrass;
        tileColor = new Color(27f/255f, 183/255f, 21/255f, 1f);
        isAccessible = true;
    }
}