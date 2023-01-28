using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "DropArea";

    private void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider2D>().enabled = false;
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection);
        if (hit2D)
        {
            if (hit2D.transform.tag == destinationTag)
            {
                transform.position = hit2D.transform.position;
                Debug.Log(hit2D);
            }
        }
        transform.GetComponent<Collider2D>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
