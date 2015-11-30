using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AIControl
{

    private static bool hasAttacked = false;
    private static bool firstAttempt = true;

    public static void controlAI()
    {
        // Attack
        attackAI();
        // Move
        move();

        //Check if the unit attacked first
        //this won't actually work since it runs before the unit finishes movement
        if (GameManager.CurrentTurnPlayer.CanAttack)
        {
            attackAI();
        }
        endTurnAI();




    }

    private static void endTurnAI()
    {
        hasAttacked = false;
        GameManager.CurrentTurnPlayer.CanAttack = false;

    }

    private static void attackAI()
    {
        //Tiles that unit can attack
        List<Tile> attackTiles = Movement.GetAttack(GameManager.CurrentTurnPlayer);
        UnitActionsPlayer.AttackList = attackTiles;
        List<UnitActions> possibleTargets = new List<UnitActions>();

        //Get all the possible enemies to attack
        //Go through all the possible attack tiles
        for (int a = 0; GameManager.CurrentTurnPlayer.CanAttack && a < attackTiles.Count; ++a)
        {
            //Find any and all enemies in the possible attack tiles and add them to a list of possible targets
            foreach (UnitActions p in GameManager.enemyTeam.myRoster)
            {
                if (p.gridPosition == attackTiles[a].gridPosition && p.isAlive)
                {
                    possibleTargets.Add(p);
                }
            }
        }

        //Pick a target and attack
        //Check if there are possible targets
        if (possibleTargets.Count > 0)
        {
            UnitActions target = possibleTargets[0];
            foreach (UnitActions p in possibleTargets)
            {
                if (p.unitHP < target.unitHP)
                {
                    target = p;
                }
            }
            //Attack target
            GameManager.attackWithCurrentPlayer(Movement.GetTileFromPlayer(target));
            hasAttacked = true;
        }


    }


    private static void move()
    {

        //Generate the movement tree of the currentplayer
        Movement.GenerateMovementTree(GameManager.CurrentTurnPlayer);
        //
        UnitActions target = null;
        int currentTargetDistance = 9999;

        // Choose which tile to go to
        // TODO : Check if movementTiles + attackRange can reach an opponent
        foreach (UnitActions p in GameManager.enemyTeam.myRoster)
        {
            if (p.isAlive)
            {
                int tempDist = Movement.GetDistance(Movement.GetTileFromPlayer(p), Movement.GetTileFromPlayer(GameManager.CurrentTurnPlayer));
                // Check if the currentTargetDistance is closer than the previous
                if (tempDist < currentTargetDistance)
                {
                    currentTargetDistance = tempDist;
                    target = p;
                }
            }
        }
        if (target != null)
            moveTowardsUnit(target);
        else
            GameManager.CurrentTurnPlayer.CanMove = false;
        // Check distance between movementTiles and enemy. Move to closest tile.
        // Tell unit to move to tileChoice
        //GameManager.CurrentTurnPlayer.CanMove = false;
    }


    public static void moveTowardsUnit(UnitActions target)
    {
        int currentDist = 9999;
        List<Tile> MoveTiles = Movement.GetMovement(GameManager.CurrentTurnPlayer);
        Tile ldestTile = null;

        // Check distance between movementTiles and enemy to get the closest tile.
        /*foreach (UnitActions p in GameManager.enemyTeam.myRoster)
        {
            foreach (Tile t in MoveTiles)
            {
                int tempDist = Movement.GetDistance(Movement.GetTileFromPlayer(p), t);
                //TODO add condition to check if tile is full or not
                if (tempDist < currentDist)
                {
                    currentDist = tempDist;
                    ldestTile = t;
                }

            }
        }*/
        //previous code doesn't actually use the target?
        foreach (Tile t in MoveTiles)
        {
            int tempDist = Movement.GetDistance(Movement.GetTileFromPlayer(target), t);
            if (tempDist < currentDist)
            {
                currentDist = tempDist;
                ldestTile = t;
            }
        }
        GameManager.moveCurrentPlayer(ldestTile);

    }
}
