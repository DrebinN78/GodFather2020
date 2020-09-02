using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROT : MonoBehaviour
{
    private ParticleSystem snowPS;
    public float erate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        snowPS = GetComponent<ParticleSystem>();
        StartCoroutine(snowVol());
    }

    // Update is called once per frame
    void Update()
    {
        var emission = snowPS.emission;
        emission.rateOverTime = erate;
    }

    IEnumerator snowVol ()
    {
        yield return new WaitForSeconds(4);
        erate = 0;

        yield return new WaitForSeconds(4);
        erate = 20;

        yield return new WaitForSeconds(4);
        erate = 75;

        yield return new WaitForSeconds(4);
        erate = 100;

        yield return new WaitForSeconds(4);
        erate = 500;
    }

}
