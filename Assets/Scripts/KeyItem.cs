using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyItem : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject keyFeather;
    private int RandomOption;
    private bool isSpawned = false;

    void Start(){
        RandomOption = Random.Range(0, enemy.Length);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
    // Update is called once per frame
    void Update()
    {
        if(enemy.Length != 0 && !isSpawned){
            if(!enemy[RandomOption].activeInHierarchy){
                Instantiate(keyFeather, enemy[RandomOption].transform.position, Quaternion.identity);
                isSpawned = true;
            }
        }
        
        var eufuipego = Physics2D.CircleCast(transform.position, .5f, Vector2.zero);
        try
        {
            if (eufuipego.collider.CompareTag("Player"))
                Destroy(gameObject);
        }
        catch (System.Exception ex)
        {
            //do nothing
        }

    }
}
