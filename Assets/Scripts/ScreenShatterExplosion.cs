using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShatterExplosion : MonoBehaviour
{
    public Transform explosionPosition;
    


    public  IEnumerator Shatter()
    {

        yield return new WaitForSeconds(1);
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRB))
            {
                childRB.AddExplosionForce(150f, explosionPosition.position, 50f);
            }
        }
    }
}
