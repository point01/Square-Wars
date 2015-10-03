using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public static System.Collections.Generic.List<Tile> GetMovement(Player mover)
    { return null; }
    public static System.Collections.Generic.List<Tile> GetAttack(Player mover)
    { return null; }
    public static System.Collections.Generic.Queue<Tile> GetMovementPath(Player mover, Tile destination)
    { return null; }
    public static void PaintTiles(System.Collections.Generic.List<Tile> movetiles, System.Collections.Generic.List<Tile> attacktiles)
    {
        foreach (Tile t in attacktiles)
            if (!movetiles.Contains(t))
                t.GetComponent<Material>().color = Color.red;
        foreach (Tile t in movetiles)
            t.GetComponent<Material>().color = Color.blue;
    }

    // example code for when a unit is right clicked/w/e to show movement/attack tiles
   /* public static void Example(Player p)
    {
        System.Collections.Generic.List<Tile> Movement = GetMovement(p);
        System.Collections.Generic.List<Tile> Attack = GetAttack(p);
        PaintTiles(Movement, Attack);
        MovableTiles = Movement;
    }
    private static System.Collections.Generic.List<Tile> MovableTiles;
    // example for when the player clicks on a valid (blue) tile to move to
    public static void Example2(Player p, Tile movetarget){
        System.Collections.Generic.Queue<Tile> MoveQueue;
        if (MovableTiles.Contains(movetarget))
            MoveQueue = GetMovementPath(p, movetarget);
        while (MoveQueue.Count > 0) // may need to do stuff to wait for coroutines to finish etc.
            MovePlayerToTile(p, MoveQueue.Dequeue());
    } */
}
