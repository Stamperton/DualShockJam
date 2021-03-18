using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Camera cam;
    float timer;

    I_Interactable currentInteractable = null;


    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.2f)
        {
            timer = 0;
            InteractionRaycast();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteract();
            }
        }
    }

    void InteractionRaycast()
    {
        RaycastHit _hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 5f);

        I_Interactable _interactable = _hit.collider.GetComponent<I_Interactable>();
        if (_interactable != null)
        {
            currentInteractable = _interactable;
            //CURSOR ON
        }
        else
        {
            _interactable = null;
            //CURSOR OFF
        }
    }
}
