using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimator : MonoBehaviour
{
    //using a getcomponent method since we have attached the script PlayerAnimator do the VisualPlayer Object

    //creating a field for the animator
    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    //adding a field for referencing the player
        [SerializeField] private Player player;


    private void Awake()
    {
        // assgining the animator component to the field in order to manipulate the animator settings - it is a reference to the animator component
        animator = GetComponent<Animator>();
        // now we can manipulate the animator settings via code cause of the reference above ingested in the animator type field.
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsInvoking());
    }
}
