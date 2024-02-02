using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mousecontroller : MonoBehaviour
{
    [SerializeField] private LayerMask m_raycastlayer;

    private interactivecontroller m_hoveredInteractable;
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, m_raycastlayer))
        {
            interactivecontroller newInteractable = hit.transform.GetComponent<interactivecontroller>();
            if (newInteractable)
            {
                if (CanHoverInteractable(newInteractable))
                {
                    m_hoveredInteractable = newInteractable;
                    m_hoveredInteractable.OnHoverStart();
                }
                newInteractable.OnHoverStart();
            }
        }

        else
        {
            if (m_hoveredInteractable)
            {
                m_hoveredInteractable.OnHoverEnd();
                m_hoveredInteractable = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_hoveredInteractable != null)
            {
                Debug.Log("Click");
            }
        }
    }
    private bool CanHoverInteractable(interactivecontroller interactable)
    {
        if (m_hoveredInteractable == interactable) return false;
        return true;
    }
}
