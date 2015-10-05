using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    //simple algo
    public static int GetDistance(Tile t1, Tile t2)
    {
        int retval = 0;
        int t = (int)t1.gridPosition.x - (int)t2.gridPosition.x;
        if (t < 0)
            t = -t;
        retval += t;
        t = (int)t1.gridPosition.y - (int)t2.gridPosition.y;
        if (t < 0)
            t = -t;
        retval += t;
        return retval;
    }
    public static Tile GetTileFromPlayer(Player p)
    {
        return GameManager.map[(int)p.gridPosition.x][(int)p.gridPosition.y];
    }
    public static System.Collections.Generic.List<Tile> GetMovement(Player mover)
    {
        System.Collections.Generic.List<Tile> retval = new System.Collections.Generic.List<Tile>();
        for (int i = 0; i < GameManager.mapSize; ++i)
        {
            for (int j = 0; j < GameManager.mapSize; ++j)
            {
                if(GetDistance(GetTileFromPlayer(mover), GameManager.map[i][j]) < mover.MovementTiles + 1)
                    retval.Add(GameManager.map[i][j]);
            }
        }
        foreach (Player p in GameManager.players)
        {
            retval.Remove(GameManager.map[(int)p.gridPosition.x][(int)p.gridPosition.y]);
        }
        return retval;
    }
    public static System.Collections.Generic.List<Tile> GetAttack(Player mover)
    {
        System.Collections.Generic.List<Tile> retval = new System.Collections.Generic.List<Tile>();
        for (int i = 0; i < GameManager.mapSize; ++i)
        {
            for (int j = 0; j < GameManager.mapSize; ++j)
            {
                if (GetDistance(GetTileFromPlayer(mover), GameManager.map[i][j]) < mover.AttackRange + 1)
                    retval.Add(GameManager.map[i][j]);
            }
        }
        return retval;
    }
    public static System.Collections.Generic.Queue<Tile> GetMovementPath(Player mover, Tile destination)
    { return null; }
    public static void PaintTiles(System.Collections.Generic.List<Tile> movetiles, System.Collections.Generic.List<Tile> attacktiles)
    {
        if (movetiles != null)
        {
            foreach (Tile t in movetiles) 
                t.transform.GetComponent<Renderer>().material.color = Color.blue;
            if (attacktiles != null)
                foreach (Tile t in attacktiles)
                    if (!movetiles.Contains(t))
                        t.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            if (attacktiles != null)
                foreach (Tile t in attacktiles)
                    t.transform.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    public static void UnPaintTiles(System.Collections.Generic.List<Tile> tiles)
    {
        if (tiles != null)
            foreach (Tile t in tiles)
                t.transform.GetComponent<Renderer>().material.color = Color.white;
    }
    /*
    // example code for when a unit is right clicked/w/e to show movement/attack tiles
    public static void Example(Player p)
    {
        System.Collections.Generic.List<Tile> Movement = GetMovement(p);
        System.Collections.Generic.List<Tile> Attack = GetAttack(p);
        PaintTiles(Movement, Attack);
        MovableTiles = Movement;
    }
    private static System.Collections.Generic.List<Tile> MovableTiles;
    // example for when the player clicks on a valid (blue) tile to move to
    public static void Example2(Player p, Tile movetarget)
    {
        System.Collections.Generic.Queue<Tile> MoveQueue;
        if (MovableTiles.Contains(movetarget))
            MoveQueue = GetMovementPath(p, movetarget);
         while (MoveQueue.Count > 0) // may need to do stuff to wait for coroutines to finish etc.
           MovePlayerToTile(p, MoveQueue.Dequeue());
    }*/
}
