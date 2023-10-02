using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField] private Vector2 Startpos;
    [SerializeField] private Vector2 Endpos;

    [SerializeField] private float ThrowSpeed;
    [SerializeField] private float ArcHeight;
    [SerializeField] private GameObject Sprite;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Throw()
    {
        
        float ThrowDistance = Vector2.Distance(Startpos, Endpos);
        while (Vector2.Distance(this.gameObject.transform.position,Endpos) > 0.01)
        {
            float Progress = Vector2.Distance(this.gameObject.transform.position, Startpos) / ThrowDistance;
            Sprite.transform.localPosition = new Vector3(0, Mathf.Sin(Progress * Mathf.PI)*ArcHeight);
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, Endpos, ThrowSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void ThrowStart()
    {

        StartCoroutine(Throw());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.GetComponent<Patrol>().enabled = false;
        if(collision.GetComponent<Patrol>() != null) { 
            collision.GetComponent<Patrol>().Stun();
            Destroy(this.gameObject);
        }
    }

    public void setPos(Vector2 start, Vector2 end)
    {
        Startpos = start;
        this.gameObject.transform.position = start;
        Endpos = end;
    }
}
