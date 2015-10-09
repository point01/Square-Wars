using UnityEngine;
using System.Collections;

public class GrassTile : Tile
{
    public GrassTile() {
        envType = EnvironmentType.Grass;
        tileColor = new Color(144f/255f, 219f/255f, 31f/255f, 1.0f);
        isAccessible = true;
    }
}
