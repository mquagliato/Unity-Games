using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //making a public event to fire
    public event EventHandler OnSeletectCounterChanged;



    [SerializeField] private float rotateSpeed = 8.0f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayer;
    
    //necessary field to store the function "IsWalking"
    private bool isWalking;
    private Vector3 moveLastDir;

    //making a field for keeping track of the selected counter
    private ClearCounter selectedCounter;

    //listening to the event in the GameInput Script
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    // Handle all the interactions when Pressing the "E" Keyboard button
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
    //calling the movement Function
    HandMovement();
    HandleInteraction();
    }

    //creating a function to be accessed by the Animation script in order to animate the PlayerObject
    public bool IsWalking()
    {
         return isWalking;
    }

    //identifying and interacting
    private void HandleInteraction()
    {
        //access the player controller input system
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        //safe the last position from moveDir to a new vector in order to be used to identify there is something to be interacted with
        if (moveDir != Vector3.zero)
        {
            moveLastDir = moveDir;
        }

        //doing a raycast in order to identify if there is construct in front of the player
        float InteractDistance = 2f;
        
        //interacting via layers
        if (Physics.Raycast(transform.position, moveLastDir, out RaycastHit raycastHit, InteractDistance,counterLayer))
        {
            //identifying which object we are interaction with
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
                // has clearCounter
                if (clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;
                }
                
            }
            else
            {
                selectedCounter = null;
            }
            
        }
        else
        {
            selectedCounter = null;
        }
        Debug.Log(selectedCounter);
    }
    private void HandMovement()
    {
        //accessing the transform where the script is attateched to in order for the objet move around
        //adding the inputVector to the transform.position

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        isWalking = moveDir != Vector3.zero;

        //checking if there is any object in front with raycast
        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHighet = 2f;

        // logic = the code looks for no colision on the way
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHighet, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHighet, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move only in x
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHighet, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move only in x
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }


        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        //Method for rotation of the object.Player according the movement done by the player
        //trasnform.right would be very helpfull for 2D games.
        //unsing "Slerp for sferical interpolation" in order to interpolate  between 2 values ans smooth transition between movements
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
