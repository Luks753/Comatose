using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
    // Update is called once per frame
    void Update()
    {
        var eufuipego = Physics2D.CircleCast(transform.position, .5f, Vector2.zero);
        try
        {
            if (eufuipego.collider.CompareTag("Player"))
                Destroy(gameObject);
        }
        catch (System.Exception ex)
        {
            
        }

    }
}
