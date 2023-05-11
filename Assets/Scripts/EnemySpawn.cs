using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private Animator ChairAnimator;

    public void animateChair()
    {
        ChairAnimator.Play("chairSpawn");
    }
}
