using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public bool focused;
   
    public bool copyButton;
    public bool cutButton;
    public bool exitButton;

    public Raycast Player;
    public ObjectInteraction thisInteraction;

    public GameObject mainObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(focused)
        {
            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("jsB"))
            {
                if (copyButton)
                {
                    Player.currentCopyItem = mainObject;
                    thisInteraction.deselectThis();
                    Player.GetComponent<CharacterMovement>().lockMovement = false;
                }

                if(cutButton)
                {
                    Player.currentCopyItem = mainObject;
                    thisInteraction.deselectThis();
                    mainObject.gameObject.SetActive(false);
                    Player.GetComponent<CharacterMovement>().lockMovement = false;


                }

                if(exitButton)
                {
                    thisInteraction.deselectThis();
                    Player.GetComponent<CharacterMovement>().lockMovement = false;
                }
            }
        }
        
       
    }

    public void SetFocused(bool value)
    {
        focused = value;
    }

    
}
