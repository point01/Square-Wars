using UnityEngine;
using System.Collections;

public class UnitActionsPlayer : UnitActions
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex] == this)
        {
            transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            if (GameManager.currentTeam.myRoster.Exists(x => x == this))
            {
                transform.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                transform.GetComponent<Renderer>().material.color = Color.white;
            }

        }

        if (unitHP <= 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private Coroutine PlayerSlide;
    private bool PlayerSlideRunning = false;
    private IEnumerator SlidePlayerAlongQueue()
    {
        Tile dest = moveQueue.Dequeue();
        while (true)
        {
            if (Vector3.Distance(dest.transform.position + 1.5f * Vector3.up, transform.position) <= 0.1f)
            {
                transform.position = dest.transform.position + 1.5f * Vector3.up;
                if (moveQueue.Count > 0)
                    dest = moveQueue.Dequeue();
            }
            transform.position += (dest.transform.position + 1.5f * Vector3.up - transform.position).normalized * moveSpeed * Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }
    }
    public override void TurnUpdate()
    {
        if (!PlayerSlideRunning && Vector3.Distance(moveDestination, transform.position) > 0.1f)
        {
            if (Movement.CurrentMovementTree.Value != null)
            {
                if (moveQueue.Count > 0)
                {
                    PlayerSlideRunning = true;
                    PlayerSlide = StartCoroutine(SlidePlayerAlongQueue());
                }
            }
        }
        if (PlayerSlideRunning && Vector3.Distance(moveDestination, transform.position) <= 0.1f)
        {
            PlayerSlideRunning = false;
            StopCoroutine(PlayerSlide);
            transform.position = moveDestination;
            CanMove = false;
        }
        base.TurnUpdate();
    }

    public override void TurnOnGUI()
    {
        //Add AITurn to get rid of GUI
        if (!GameManager.gameOver && !GameManager.freezeGame)
        {
            float buttonHeight = 50;
            float buttonWidth = 150;

            Rect buttonRect = new Rect(0, Screen.height - buttonHeight * 3, buttonWidth, buttonHeight);

            //move button
            if (GUI.Button(buttonRect, "Move") && CanMove)
            {
                
                StartMovement();

            }

            //attack button
            buttonRect = new Rect(0, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight);

            if (GUI.Button(buttonRect, "Attack") && CanAttack)
            {
                StartAttack();
             
            }

            //end turn button
            buttonRect = new Rect(0, Screen.height - buttonHeight * 1, buttonWidth, buttonHeight);

            if (GUI.Button(buttonRect, "End Turn"))
            {
                CanMove = false;
                CanAttack = false;
                Movement.UnPaintTiles();
                GameManager.instance.nextTurn();
            }

            base.TurnOnGUI();
        }
    }

    public static System.Collections.Generic.List<Tile> MoveList;
    public static System.Collections.Generic.List<Tile> AttackList;
    private void StartMovement()
    {
        Movement.UnPaintTiles();
        GameManager.CurrentTurnPlayer = GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex];
        Movement.GenerateMovementTree(GameManager.CurrentTurnPlayer);
        AttackList = null;
        MoveList = Movement.GetMovement(GameManager.CurrentTurnPlayer);
        Movement.PaintTiles(MoveList, AttackList);
    }
    private void StartAttack()
    {
        Movement.UnPaintTiles();
        //StopEverything();
        MoveList = null;
        GameManager.CurrentTurnPlayer = GameManager.currentTeam.myRoster[GameManager.currentPlayerIndex];
        AttackList = Movement.GetAttack(GameManager.CurrentTurnPlayer);
        Movement.PaintTiles(MoveList, AttackList);
    }
    //    public void StopEverything()
    //    {
    //     //   Movement.UnPaintTiles();
    ////        Movement.UnPaintTiles(MoveList);
    //  //      Movement.UnPaintTiles(AttackList);
    //    }
}
