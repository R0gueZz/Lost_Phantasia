using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Transform playerTransform;
    Transform cameraTransform;

    [SerializeField]
    Vector2 delay = new Vector2(2, 4);
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newSurface = new Vector2();
        
        newSurface.x = Mathf.Lerp(cameraTransform.position.x, playerTransform.position.x, delay.x * Time.deltaTime);
        newSurface.y = Mathf.Lerp(cameraTransform.position.y, playerTransform.position.y, delay.y * Time.deltaTime);

        cameraTransform.position = new Vector3(newSurface.x, newSurface.y, cameraTransform.position.z);
    }
}
