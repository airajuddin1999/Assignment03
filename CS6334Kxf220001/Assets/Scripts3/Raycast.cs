using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;

[RequireComponent(typeof(LineRenderer))]
public class Raycast : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float laserDistance = 1f;

    public ObjectInteraction currentActive;
    public GameObject currentCopyItem;

    public string groundTag = "Ground";

    LineRenderer laserLine;
    float fireTimer;

    public GameObject MenuObject;
    public GameObject EventSystem;

    public TMP_Text raycastLenText;
    public TMP_Text speedText;

    public int raycastLen;
    public int speed = 5;

    public CharacterMovement movementScript;

    public EventSystem eventSystem;

    public GameObject ResumeButtonss;

    public bool menuOpen = false;


    /// <summary>
    /// For Debugging the code
    /// </summary>
    /// 
    public TMP_Text rayhitText, currentCopyText, buttonPressedText;
    

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        raycastLenText.text = "Raycast Length 50M";
        speedText.text = "Speed: Medium";
        
    }

    void Update()
    {



        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, raycastLen))
        {
            laserLine.SetPosition(1, hit.point);

            if(hit.transform.GetComponent<ObjectInteraction>())
            {
                
                hit.transform.GetComponent<ObjectInteraction>().focused = true;
            }

            if (hit.transform.GetComponent<VRButton>())
            {

                hit.transform.GetComponent<VRButton>().focused = true;
            }

            Debug.Log(hit.transform.name);
            
            //If Player is focusing on Ground
            if(hit.transform.tag == groundTag)
            {
                Debug.Log(hit.transform.name+ "Test2");
                //If there is some item copied
                if (currentCopyItem)
                {
                    Debug.Log(hit.transform.name + "Test3");
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("jsA"))
                    {
                        Debug.Log(hit.transform.name + "Test4");
                       GameObject newObject =  Instantiate(currentCopyItem,hit.point,Quaternion.identity);
                        newObject.SetActive(true);

                    }
                }

                //Teleporting User if it is on the ground
                if(Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("jsY"))
                {
                    this.gameObject.SetActive(false);
                    this.gameObject.transform.position = hit.point;
                    this.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
        }
        laserLine.enabled = true;


        if(Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Submit") || Input.GetButtonDown("js4"))
        {
            if (menuOpen == false)
            {
                SetMenu(true);
            }
        }


        if(currentCopyItem)
        currentCopyText.text = "Current Copied item is " + currentCopyItem.name;
        else
            currentCopyText.text = "Current Copied item is null";

        if(hit.collider != null)
        rayhitText.text = "Ray is hitting to "+ hit.collider.name;

        if (Input.GetButtonDown("jsA"))
            buttonPressedText.text = "JSA_0";
        if (Input.GetButtonDown("jsB"))
            buttonPressedText.text = "JSB_1";

        if (Input.GetButtonDown("jsX"))
            buttonPressedText.text = "JSX_2";
        if (Input.GetButtonDown("jsY"))
            buttonPressedText.text = "JSY_3";

        if (Input.GetButtonDown("Submit"))
            buttonPressedText.text = "Sumbit Key";


    }

    public void SetMenu(bool value)
    {
       
        this.gameObject.GetComponent<CharacterMovement>().enabled = !value;
        if (currentActive)
            currentActive.deselectThis();
        currentActive = null;

        this.transform.GetChild(0).GetComponent<XRCardboardController>().enabled = !value;

        if (!value)
        {
            EventSystem.GetComponent<StandaloneInputModule>().enabled = value;
            EventSystem.GetComponent<XRCardboardInputModule>().enabled = !value;
        }

        if(value)
        {
            EventSystem.GetComponent<XRCardboardInputModule>().enabled = !value;
            EventSystem.GetComponent<StandaloneInputModule>().enabled = value;
        }


        eventSystem.SetSelectedGameObject(ResumeButtonss);
        MenuObject.SetActive(value);
        Debug.Log("Called2");

        Invoke("ChangeMenuState",1);

    }

    void ChangeMenuState()
    {
        menuOpen = !menuOpen;
    }

    

public void SetCurrentTo(ObjectInteraction obj)
    {
        if(currentActive)
        currentActive.deselectThis();
        currentActive = obj;

    }

    public void ResumeButton()
    {
        Debug.Log("Called1");
        SetMenu(false);

    }

    public void RayCastLengthButton( )
    {
        Debug.Log("Length button called");
        if(raycastLen == 1)
        {
            Debug.Log("Length button Conditioon Match");
            raycastLen = 10;
            raycastLenText.text = "Raycast Length 50M";
            return;
        }

        if (raycastLen == 10)
        {
            raycastLen = 50;
            raycastLenText.text = "Raycast Length 1M";
            return;
        }

        if (raycastLen == 50)
        {
            raycastLen = 1;
            raycastLenText.text = "Raycast Length 10M";
            return;
        }



    }

    public void SpeedButton()
    {
        if (speed == 5)
        {

            speed = 10;
            movementScript.speed = 10;
            speedText.text = "Speed:- HIGH";
            return;
        }

        if (speed == 10)
        {
            speed = 20;
            movementScript.speed = 20;
            speedText.text = "Speed:- LOW";
            return;
        }

        if (speed == 20)
        {
            speed = 5;
            movementScript.speed = 5;
            speedText.text = "Speed:- Medium";
            return;
        }
    }

    public void QuitButton()
    {
        Debug.Log("Application is Quit");
        Application.Quit();
    }
}