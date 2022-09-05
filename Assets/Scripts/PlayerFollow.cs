using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraTrackSpeed = .5f;
    public float offset = 4;

    private float yPosition;
    private float xPosition;

    private void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    private void LateUpdate()
    {
        if (playerTransform.position.y > transform.position.y - offset)
        {
            yPosition = playerTransform.position.y + offset;
        }
        Vector3 targetPos = new Vector3(xPosition, yPosition, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, cameraTrackSpeed * Time.deltaTime);
    }
}
