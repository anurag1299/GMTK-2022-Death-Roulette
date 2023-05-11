using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFace : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        gameObject.GetComponentInParent<Dice>().CalculateResult(other, gameObject.name);
    }
}
