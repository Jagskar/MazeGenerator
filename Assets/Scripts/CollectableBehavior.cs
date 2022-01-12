using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehavior : MonoBehaviour
{
    private float rotSpeed = 10f;
    GameManager gm;

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.CurrCollectables++;
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    //gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    //    //gm.CurrCollectables++;
    //    //Debug.Log(gm.CurrCollectables);
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * rotSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "robot")
        {
            Destroy(gameObject);
            gm.CurrCollectables--;
            gm.UpdateUI();
        }
        //Debug.Log(gm.CurrCollectables);
    }

    
}
