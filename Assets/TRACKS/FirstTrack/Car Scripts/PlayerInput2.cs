//This script handles reading inputs from the player and passing it on to the vehicle. We 
//separate the input code from the behaviour code so that we can easily swap controls 
//schemes or even implement and AI "controller". Works together with the VehicleMovement script

using UnityEngine;

public class PlayerInput2 : MonoBehaviour
{
    //The keys for the thruster, rudder, and brake inputs
    public KeyCode thrusterKey = KeyCode.UpArrow;
    public KeyCode reverseKey = KeyCode.DownArrow;
    public KeyCode rudderLeftKey = KeyCode.LeftArrow;
    public KeyCode rudderRightKey = KeyCode.RightArrow;
    public KeyCode brakingKey = KeyCode.Space;

    //We hide these in the inspector because we want 
    //them public but we don't want people trying to change them
    [HideInInspector] public float thruster;         //The current thruster value
    [HideInInspector] public float rudder;           //The current rudder value
    [HideInInspector] public bool isBraking;        //The current brake value

    void Update()
    {
        //If the player presses the Escape key and this is a build (not the editor), exit the game
        if (Input.GetKeyDown(KeyCode.Escape) && !Application.isEditor)
            Application.Quit();

        //If a GameManager exists and the game is not active...
        if (GameManager.instance != null && !GameManager.instance.IsActiveGame())
        {
            //...set all inputs to neutral values and exit this method
            thruster = rudder = 0f;
            isBraking = false;
            return;
        }

        //Get the values of the thruster, rudder, and brake from the keyboard input
        thruster = (Input.GetKey(thrusterKey) ? 1f : 0f) - (Input.GetKey(reverseKey) ? 1f : 0f);
        rudder = (Input.GetKey(rudderRightKey) ? 1f : 0f) - (Input.GetKey(rudderLeftKey) ? 1f : 0f);
        isBraking = Input.GetKey(brakingKey);
    }
}
