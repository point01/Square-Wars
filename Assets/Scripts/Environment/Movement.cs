using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    private static System.Random RMove = new System.Random();

    // For Dijkstra's algorithm. Update it to a higher value if player movement bloats up too high
    private const int Infinity = 99999;
    public static TileTree CurrentMovementTree = new TileTree();
    //holds movement tiles in a tree and can return all the tiles, or queues from the middle tile to other tiles
    public class TileTree
    {
        public List<TileTree> Nodes;
        public TileTree Head;
        public Tile Value;

        public TileTree()
        {
            Nodes = new List<TileTree>();
            Head = null;
            Value = null;
        }
        public void Clear()
        {
            foreach (TileTree tt in Nodes)
            {
                tt.Clear();
            }
            Head = null;
            Value = null;
        }
        public bool Contains(Tile t)
        {
            if (t == Value)
                return true;
            bool retval = false;
            foreach (TileTree tt in Nodes)
            {
                retval |= tt.Contains(t);
                if (retval)
                    return retval;
            }
            return retval;
        }
        //contains, but returns the actual tile, or null if it is not contained
        public TileTree ContainsT(Tile t)
        {
            if (t == Value)
                return this;
            TileTree retval = null;
            foreach (TileTree tt in Nodes)
            {
                retval = tt.ContainsT(t);
                if (retval != null)
                    return retval;
            }
            return retval;
        }
        public List<Tile> ToList()
        {
            List<Tile> retval = new List<Tile>();
            retval.Add(Value);
            foreach (TileTree tt in Nodes)
            {
                foreach (Tile l in tt.ToList())
                {
                    retval.Add(l);
                }
            }
            return retval;
        }
        //queue of tiles from the middle tile to the specified one, or null if it is not contained within the tree
        public Queue<Tile> GetMoveQueue(Tile t)
        {
            TileTree owner = ContainsT(t);
            if (owner != null)
            {
                Stack<Tile> reverse = new Stack<Tile>();
                while (owner.Head != null)
                {
                    reverse.Push(owner.Value);
                    owner = owner.Head;
                }
                Queue<Tile> retval = new Queue<Tile>();
                while (reverse.Count > 0)
                    retval.Enqueue(reverse.Pop());
                return retval;
            }
            return null;
        }
    }

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
        //euclidean instead of absolute distance
        //        retval = (int)Mathf.Sqrt(Mathf.Pow(t1.gridPosition.x - t2.gridPosition.x, 2) + Mathf.Pow(t1.gridPosition.y - t2.gridPosition.y, 2));
        return retval;
    }

    public static Tile GetTileFromPlayer(UnitActions p)
    {
        return GameManager.map[(int)p.gridPosition.x][(int)p.gridPosition.y];
    }
    // CALL THIS EVERY TIME A UNIT GETS A NEW TURN
    public static void GenerateMovementTree(UnitActions mover)
    {
        System.Collections.Generic.List<Tile> all = new System.Collections.Generic.List<Tile>();
        //optimization
        int mx = (int)mover.gridPosition.x;
        int my = (int)mover.gridPosition.y;
        int mxmin = mx - mover.MovementTiles;
        int mxmax = mx + mover.MovementTiles;
        int mymin = my - mover.MovementTiles;
        int mymax = my + mover.MovementTiles;
        if (mxmin < 0)
            mxmin = 0;
        if (mxmax >= GameManager.MapWidth)
            mxmax = GameManager.MapWidth - 1;
        if (mymin < 0)
            mymin = 0;
        if (mymax >= GameManager.MapHeight)
            mymax = GameManager.MapHeight - 1;
        Tile temp = GetTileFromPlayer(mover);
        for (int i = mxmin; i <= mxmax; ++i)
        {
            for (int j = mymin; j <= mymax; ++j)
            {
                if (GetDistance(temp, GameManager.map[i][j]) < mover.MovementTiles + 1)
                    if (true) //TODO replace this with a check to see if the mover can walk on map[i][j]
                        all.Add(GameManager.map[i][j]);
            }
        }
        //TODO This will need to be tweaked if we allow movement through allied units
        //if allied movement is desired, this foreach can be removed to allow it, but you'll need to manually remove player location tiles
        // from the output of getmovement or they'll be able to walk into each other
        //uncomment this to make it so that allied units block player movement
        /*foreach (Player p in GameManager.currentTeam.myRoster)
        {
            temp = GetTileFromPlayer(p);
            if (all.Contains(temp))
                all.Remove(temp);
        }*/
        foreach (UnitActions p in GameManager.enemyTeam.myRoster)
        {
            temp = GetTileFromPlayer(p);
            if (all.Contains(temp))
                all.Remove(temp);
        }

        //Dijkstra's Algorithm
        Dictionary<Tile, int> weights = new Dictionary<Tile, int>();
        List<Tile> unvisited = new List<Tile>();
        Dictionary<Tile, Tile> nextParent = new Dictionary<Tile, Tile>();
        foreach (Tile t in all)
        {
            weights.Add(t, Infinity);
            unvisited.Add(t);
        }
        temp = GetTileFromPlayer(mover);
        weights[temp] = 0;
        unvisited.Remove(temp);
        CurrentMovementTree.Clear();
        CurrentMovementTree.Value = temp;
        while (unvisited.Count > 0)
        {
            List<Tile> uvn = GetUnvisitedNeighbors(unvisited, temp);
            int dist;
            foreach (Tile t in uvn)
            {
                float jdist = t.elevation - temp.elevation;
                if (jdist < 0)
                    jdist *= -1;
                if (t.isAccessible && jdist <= mover.MovementJump)
                    dist = t.MoveCost + weights[temp];
                else
                    dist = Infinity;
                if (dist < weights[t])
                {
                    weights[t] = dist;
                    if (nextParent.ContainsKey(t))
                        nextParent[t] = temp;
                    else
                        nextParent.Add(t, temp);
                }
            }
            if (unvisited.Count > 0)
            {
                temp = unvisited[0];
                foreach (Tile t in weights.Keys)
                {
                    if (unvisited.Contains(t) && weights[t] < weights[temp])
                        temp = t;
                }
            }
            unvisited.Remove(temp);
        }
        Queue<Tile> distancecheck = new Queue<Tile>();
        foreach (Tile t in nextParent.Keys)
        {
            distancecheck.Enqueue(t);
        }
        Queue<Tile> Remove = new Queue<Tile>();
        while (distancecheck.Count > 0)
        {
            if (weights[distancecheck.Peek()] > mover.MovementTiles)
                Remove.Enqueue(distancecheck.Dequeue());
            else
                distancecheck.Dequeue();
        }
        while (Remove.Count > 0)
            nextParent.Remove(Remove.Dequeue());
        RecursiveBuildTileTree(nextParent, CurrentMovementTree);
    }
    private static void RecursiveBuildTileTree(Dictionary<Tile, Tile> nextParent, TileTree tree)
    {
        if (nextParent.Count == 0)
            return;
        Queue<Tile> Remove = new Queue<Tile>();
        foreach (Tile t in nextParent.Keys)
        {
            if (nextParent[t] == tree.Value)
            {
                TileTree tt = new TileTree();
                tt.Value = t;
                tt.Head = tree;
                tree.Nodes.Add(tt);
                Remove.Enqueue(t);
            }
        }
        while (Remove.Count > 0)
            nextParent.Remove(Remove.Dequeue());
        foreach (TileTree tt in tree.Nodes)
        {
            RecursiveBuildTileTree(nextParent, tt);
        }
    }
    private static List<Tile> GetUnvisitedNeighbors(List<Tile> u, Tile t)
    {//can do fancy stuff with int array {{-1,0},{0,-1},{1,0},{0,1}}, but w/e
        List<Tile> retval = new List<Tile>();
        int x = (int)t.gridPosition.x;
        int y = (int)t.gridPosition.y;
        int nx = x - 1;
        int ny = y;
        if (nx < 0)
            nx = 0;
        if (u.Contains(GameManager.map[nx][ny]))
            retval.Add(GameManager.map[nx][ny]);
        nx = x;//can also move some of these around to reduce assignments, but w/e
        ny = y - 1;
        if (ny < 0)
            ny = 0;
        if (u.Contains(GameManager.map[nx][ny]))
            retval.Add(GameManager.map[nx][ny]);
        nx = x + 1;
        ny = y;
        if (nx >= GameManager.MapWidth)
            nx = GameManager.MapWidth - 1;
        if (u.Contains(GameManager.map[nx][ny]))
            retval.Add(GameManager.map[nx][ny]);
        nx = x;
        ny = y + 1;
        if (ny >= GameManager.MapHeight)
            ny = GameManager.MapHeight - 1;
        if (u.Contains(GameManager.map[nx][ny]))
            retval.Add(GameManager.map[nx][ny]);
        return retval;
    }


    public static System.Collections.Generic.List<Tile> GetMovement(UnitActions mover)
    {
        List<Tile> retval;
        retval = CurrentMovementTree.ToList();
        //retval.Remove(GetTileFromPlayer(mover));
        //stops the player from moving where units already exist
        Tile temp;
        foreach (UnitActions p in GameManager.currentTeam.myRoster)
        {
            temp = GetTileFromPlayer(p);
            if (retval.Contains(temp))
                retval.Remove(temp);
        }
        foreach (UnitActions p in GameManager.enemyTeam.myRoster)
        {
            temp = GetTileFromPlayer(p);
            if (retval.Contains(temp))
                retval.Remove(temp);
        }
        return retval;
    }

    public static System.Collections.Generic.List<Tile> GetAttack(UnitActions mover)
    {
        System.Collections.Generic.List<Tile> retval = new System.Collections.Generic.List<Tile>();
        for (int i = 0; i < GameManager.MapWidth; ++i)
        {
            for (int j = 0; j < GameManager.MapHeight; ++j)
            {
                if (GetDistance(GetTileFromPlayer(mover), GameManager.map[i][j]) < mover.AttackRange + 1)
                    retval.Add(GameManager.map[i][j]);
            }
        }
        return retval;
    }

    public static System.Collections.Generic.Queue<Tile> GetMovementPath(UnitActions mover, Tile destination)
    {
        return CurrentMovementTree.GetMoveQueue(destination);
    }

    public static void PaintTiles(System.Collections.Generic.List<Tile> movetiles, System.Collections.Generic.List<Tile> attacktiles)
    {
        if (movetiles != null)
            foreach (Tile t in movetiles)
                if (t != null)
                    t.transform.GetComponent<Renderer>().material.color = Color.blue;
        if (attacktiles != null)
            foreach (Tile t in attacktiles)
                if (movetiles == null || !movetiles.Contains(t))
                    t.transform.GetComponent<Renderer>().material.color = Color.red;
    }
    public static void UnPaintTiles(System.Collections.Generic.List<Tile> tiles)
    {
        for (int i = 0; i < GameManager.MapWidth; ++i)
        {//this should probably be moved to game manager and have things like tile effects incorporated into it
            for (int j = 0; j < GameManager.MapHeight; ++j)
            {
                Tile targetTile = GameManager.map[i][j];
                targetTile.transform.GetComponent<Renderer>().material.color = targetTile.tileColor;
            }
        }
    }
}
