using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public Transform from;
    public Transform to;
    float speed = 0.01f;
    float timeCount = 0.0f;
    void Start()
    {
        //Application.(60); //set frameRate to 60
    }
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, timeCount * speed);
        timeCount += Time.deltaTime;
    }
}
