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
    float delay = 2;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Lerp(cameraTransform.position.x, playerTransform.position.x, delay * Time.deltaTime);
        cameraTransform.position = new Vector3(newX, cameraTransform.position.y, cameraTransform.position.z);
    }
}
