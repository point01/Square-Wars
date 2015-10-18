using UnityEngine;
using System.Collections;

public class StoneTile : Tile
{
    public StoneTile()
    {
        envType = EnvironmentType.Plains;
        tileColor = new Color(195f / 255f, 5f / 255f, 55f / 255f, 1f);
        isAccessible = true;
    }
}