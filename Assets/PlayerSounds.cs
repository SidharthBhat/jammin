using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip[] box;
    [SerializeField] private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FootStep()
    {
        int i = (int)(Random.value * footsteps.Length);
        audio.PlayOneShot(footsteps[i]);
    }

}
