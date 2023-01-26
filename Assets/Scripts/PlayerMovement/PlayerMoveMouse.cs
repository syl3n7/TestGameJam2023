using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMouse : MonoBehaviour
{
    private Transform shipTransform;

    #region Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public static Vector3 GetDirToMouse(Vector3 fromPosition)
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        return (mouseWorldPosition - fromPosition).normalized;
    }
    #endregion

    private void Start()
    {
        shipTransform = this.transform;
    }

    private void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        if (Input.GetMouseButton(1))
        {
            GetComponent<MovePositionD>().SetMovePosition(mousePosition);

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            shipTransform.eulerAngles = new Vector3(0, 180, -angle - 90);
        }
    }
}
