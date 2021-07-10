using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    public GameObject[] enemyA1;
    public GameObject[] enemyA2;
    public GameObject keyFeatherA1;
    public GameObject keyFeatherA2;
    public GameObject keyKey;
    public GameObject keyFilter;

    private int RandomOptionA1;
    private int RandomOptionA2;
    private bool A1Spawned;
    private bool A2Spawned;
    private bool filterSpawned;


    // Start is called before the first frame update
    void Start()
    {
        RandomOptionA1 = Random.Range(0, enemyA1.Length);
        RandomOptionA2 = Random.Range(0, enemyA2.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyA1.Length != 0 && !A1Spawned){
            if(!enemyA1[RandomOptionA1].activeInHierarchy){
                keyFeatherA1.transform.position = enemyA1[RandomOptionA1].transform.position;
                //Instantiate(keyFeatherA1, enemyA1[RandomOptionA1].transform.position, Quaternion.identity);
                keyFeatherA1.SetActive(true);
                A1Spawned = true;
            }
        }

        if(enemyA2.Length != 0 && !A2Spawned){
            if(!enemyA2[RandomOptionA2].activeInHierarchy){
                keyFeatherA2.transform.position = enemyA2[RandomOptionA2].transform.position;
                //Instantiate(keyFeatherA2, enemyA2[RandomOptionA2].transform.position, Quaternion.identity);
                keyFeatherA2.SetActive(true);
                A2Spawned = true;
            }
        }

        if(!filterSpawned &&
           keyFeatherA1 == null &&
           keyFeatherA2 == null &&
           keyKey == null)
           {
               keyFilter.SetActive(true);
               filterSpawned = true;
           }
    }
}
