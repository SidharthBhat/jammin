using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAutomater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject newObject = this.gameObject;
        Destroy(newObject.GetComponent<WallAutomater>());
        GameObject WallCopy = GameObject.Instantiate(newObject);
        WallCopy.transform.parent = transform;
        WallCopy.layer = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
