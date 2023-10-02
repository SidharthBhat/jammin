using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject[] goalposts;
    [SerializeField] private GameObject toyItem;
    [SerializeField] private GameObject GameCompleteUI;
    // Start is called before the first frame update
    void Start()
    {
        //Generate at certain points
        Vector3 pos = goalposts[(int)(Random.value * goalposts.Length)].transform.position;
        Instantiate(toyItem,pos, Quaternion.Euler(Vector3.zero));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameComplete()
    {
        GameCompleteUI.SetActive(true);
    }
}
