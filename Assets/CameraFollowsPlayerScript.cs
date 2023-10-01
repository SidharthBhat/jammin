using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayerScript : MonoBehaviour
{
    private GameObject player;
    public float MaxDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 differenceBetweenPlayerAndCamera = player.transform.position - transform.position;
        differenceBetweenPlayerAndCamera.z = 0;

        float x = differenceBetweenPlayerAndCamera.x;
        float y = differenceBetweenPlayerAndCamera.y;

        float xDiff = ((x > 0)? 1 : -1) * Mathf.Pow(x / MaxDistance, 2);
        float yDiff = ((y > 0)? 1 : -1) * Mathf.Pow(y / MaxDistance, 2);
        transform.Translate(xDiff, yDiff, 0);
    }
}
