using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteAfterTime());
    }

    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
