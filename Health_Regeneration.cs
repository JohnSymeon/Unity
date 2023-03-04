//Many video games use a player health system that goes as follows; 
//-if the player gets damaged, their health goes temporarily down and if it reaches 0, player dies.
//-if the player does not get damaged for a set time window, their health starts regenerating until it gets to
//maximum again.
//- if at any moment after being damaged, the player gets damaged again, the regeneration time window resets 
//cancelling any regeneration that might or might not be happening.
//
//This small and effective code implements this system for a 2D Unity game that damages the player after a collision
//but its logic can be used anywhere a similar effect might be desired.
//
//-JohnSymeon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
public float HP;
public float HPmax = 100;
private int dmg = 10;

private bool DAMAGED = false; //init DAMAGED and counter variables 
private float counter = 0f;
private int regen_coeff = 5; //adjust regen_coeff to your prefered regeneration speed 
private float delay = 5f; //set to preferred uninterrupted time delay to start the regeneration process

void Start()
{
	HP = HPmax; //init health as maxhealth
}

void FixedUpdate()
{
	if (DAMAGED) //if the player has been damaged at any point
	{
		counter += Time.fixedDeltaTime; //start counting up until you reach the set delay 
		if (counter >= delay)
		{
			DAMAGED = false;  // set DAMAGED to false and allow to regenerate
			counter = 0f;
		}
	}
	
	if (HP < HPmax && !DAMAGED) //if player has lost health and is allowed to regenerate, start regenerating HP
	{							
		HP += Time.fixedDeltaTime*regen_coeff; 
	}
}

void OnCollisionEnter2D(Collision2D collision) 
{
	if (collision.gameObject.tag == "Bullet") // if a collision happens, damage the player and set the 
	{						//DAMAGED bool to true and counter to 0, interrupting any 
							//regeneration that may be happening in FIxedUpdate
		HP -= dmg;    
		DAMAGED = true;
		counter = 0f;
	}
}



}
