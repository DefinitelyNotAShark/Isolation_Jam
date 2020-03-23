using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateLookedAtObjects : MonoBehaviour 
{
    [SerializeField]
    private float maxActivateDistance = 6.0f;

    [SerializeField]
    private Text lookedAtObjectText;

    private IActivatable objectLookedAt;
	
	void FixedUpdate ()
    {
        Debug.DrawRay(transform.position, transform.forward * maxActivateDistance);

        UpdateObjectLookedAt();
        UpdateLookedAtObjectText();
        ActivateLookedAtObject();
    }

    private void ActivateLookedAtObject()
    {
        if (objectLookedAt != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                objectLookedAt.DoActivate();
            }
        }
    }

    private void UpdateLookedAtObjectText()
    {
        if (objectLookedAt != null)
            lookedAtObjectText.text = objectLookedAt.NameText;
        else
            lookedAtObjectText.text = "";
    }

    private void UpdateObjectLookedAt()
    {
        RaycastHit hit;
        objectLookedAt = null;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxActivateDistance))
        {
            Debug.Log("Hit: " + hit.transform.name);

            objectLookedAt = hit.transform.GetComponent<IActivatable>();
        }
    }
}
