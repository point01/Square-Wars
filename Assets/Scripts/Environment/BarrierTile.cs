using UnityEngine;
using System.Collections;

public class BarrierTile : Tile
{
    public BarrierTile()
    {
        envType = EnvironmentType.Barrier;
        tileColor = new Color(42f / 255f, 42f / 255f, 42f / 255f, 1f);
        isAccessible = false;
    }
}