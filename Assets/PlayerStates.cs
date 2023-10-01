using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public bool InBox = false;
    public int CymbalMonkeys = 3;

    private bool InBoxDebounce = false;
    private bool CMDebounce = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleBox() {
        if (Input.GetButtonDown("Jump")) {
            if (InBoxDebounce) {
                // do nothing
            } else {
                InBox = !InBox;
                InBoxDebounce = true;
            }
        } else {
            InBoxDebounce = false;
        }

        // TODO: we still need to actually change the player sprite, and probably movement speed
    }

    void HandleMonkeys() {
        if (CymbalMonkeys < 1) {
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
            if (CMDebounce) {
                // do nothing
            } else {
                CymbalMonkeys -= 1;
                // TODO: we still need to actually fire a cymbal mokey
                CMDebounce = true;
            }
        } else {
            CMDebounce = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleBox();
        HandleMonkeys();
    }
}
