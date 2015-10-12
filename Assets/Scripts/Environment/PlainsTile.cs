using UnityEngine;
using System.Collections;

public class PlainsTile : Tile
{
    public PlainsTile()
    {
        envType = EnvironmentType.Plains;
        tileColor = new Color(210f/255f, 170f/255f, 40f/255f, 1f);
        isAccessible = true;
    }
}