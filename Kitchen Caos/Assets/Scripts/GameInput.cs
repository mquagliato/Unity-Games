//addes "System" because this is the lib when "EventHandler" exists
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //making an event for the player to interact with thing in the game. PLayer script will access it, so it need to be public
    //when working with events you need first to test if there is any trigger, otherwise you will end up with a non event error
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //Adding the events of the interaction "+=" means to listesn to the event. The function signature has to match the private void created "UnityEngine.InputSystem.InputAction.CallbackContext obj"
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /*(this "if" would test if the event exists or not.
            if (OnInteractAction != null){
            OnInteractAction(this, EventArgs.Empty);
        }*/

        //Firing the event "browse more about this." Testin if the there is interaction by using the following instead using if, used a lot in events.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
            
        
    }

    public Vector2 GetMovementVectorNormalized() {
        // assing the 
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        //normalize the speed when moving diagonaly
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
