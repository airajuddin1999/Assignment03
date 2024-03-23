using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public bool focused = false;
    public Canvas canvas;
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckRay() && focused)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            focused = false;
        }

        if(focused && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("jsX")))
        {
            player.GetComponent<Raycast>().SetCurrentTo(this);
            canvas.transform.gameObject.SetActive(true);
            canvas.transform.LookAt(player.transform);
            canvas.transform.eulerAngles = new Vector3(0, canvas.transform.eulerAngles.y, 0);

            player.GetComponent<CharacterMovement>().lockMovement = true;
        }
    }

    public void CopyButton()
    {
        player.GetComponent<Raycast>().currentCopyItem = this.gameObject;
    }

    public void CutButton()
    {
        player.GetComponent<Raycast>().currentCopyItem = this.gameObject;
        this.gameObject.SetActive(false);
    }

    public void deselectThis()
    {
        canvas.gameObject.SetActive(false);
    }

    bool CheckRay()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);

        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);

        // Create a RaycastHit variable to store information about what the ray hits
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == this.transform)
            {
                this.GetComponent<Outline>().enabled = true;
              
                return true;
            }
            else
            {
                this.GetComponent<Outline>().enabled = false;
                return false;
            }

        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
            return false;

        }

    }
}
