using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] Transform pointALeftLowest;
    [SerializeField] Transform pointBRightHighest;
    [SerializeField] float moveSpeed;
 
    Rigidbody2D myRigidbody;

    Vector2 pointAVector; //Point A is the Left most position in case of horizontal or lowest in case of vertical movement
    Vector2 pointBVector;
    enum State
    {
        FromAtoB,
        FromBtoA,
    }

    enum Direction
    {
        Vertical,
        Horizontal,
    }

    bool canMove;
    [SerializeField] State boxState;
    [SerializeField] Direction moveDirection;
    [SerializeField] int waitingTime;
    int timerInteger = 0;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        pointAVector = pointALeftLowest.position;
        pointBVector = pointBRightHighest.position;
    }

    void Update()
    {

        //StateMachine();
        Move();
        //MoveWithPhysics();
    }

    private void FixedUpdate()
    {
        if (moveDirection == Direction.Vertical)
        {
            if (transform.position.y <= pointAVector.y)
            {
                boxState = State.FromAtoB;
                canMove = false;
                timerInteger++;
                if (timerInteger >= waitingTime)
                {
                    canMove = true;
                    timerInteger = 0;
                }
            }
            if (transform.position.y >= pointBVector.y)
            {
                boxState = State.FromBtoA;
                canMove = false;
                timerInteger++;
                if (timerInteger >= waitingTime)
                {
                    canMove = true;
                    timerInteger = 0;
                }
            }
        }
        else
        {
            if (transform.position.x <= pointAVector.x)
            {
                boxState = State.FromAtoB;
                canMove = false;
                timerInteger++;
                if (timerInteger >= waitingTime)
                {
                    canMove = true;
                    timerInteger = 0;
                }
            }
            if (transform.position.x >= pointBVector.x)
            {
                boxState = State.FromBtoA;
                canMove = false;
                timerInteger++;
                if (timerInteger >= waitingTime)
                {
                    canMove = true;
                    timerInteger = 0;
                }
            }
        }
    }

    private void Move() //control movement based on the current move direction and wether the box waiting time is over (canMove)
    {

        if (boxState == State.FromAtoB && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointBVector, moveSpeed * Time.deltaTime);
        }
        else if (boxState == State.FromBtoA && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointAVector, moveSpeed * Time.deltaTime);
        }
        /*else
        {
            transform.position = transform.position;
        }*/
    }
    private void MoveWithPhysics() //experimental stuff for using rigidbody to drag the player if they stand on the box
    {
        if (moveDirection == Direction.Vertical)
        {
            if (boxState == State.FromAtoB)
            {
                myRigidbody.velocity = new Vector2(0f, moveSpeed); 
            }
            else if (boxState == State.FromBtoA)
            {
                myRigidbody.velocity = new Vector2(0f, -moveSpeed);
            }
        }
        else
        {
            if (boxState == State.FromAtoB)
            {
                myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            }
            else if (boxState == State.FromBtoA)
            {
                myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
    }
   /* private void StateMachine() //control the current movement direction 
    {
        if (moveDirection == Direction.Vertical)
        {
            if (transform.position.y <= pointAVector.y)
            {
                boxState = State.FromAtoB;
            }
            else if (transform.position.y >= pointBVector.y)
            {
                boxState = State.FromBtoA;
            }
        }
        else
        {
            if (transform.position.x <= pointAVector.x)
            {
                boxState = State.FromAtoB;
            }
            else if (transform.position.x >= pointBVector.x)
            {
                boxState = State.FromBtoA;
            }
        }
    }*/

}
