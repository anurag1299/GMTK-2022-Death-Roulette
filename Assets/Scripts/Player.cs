using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform ShatterScreen;


    private void Start()
    {
        base.Start();        
        base.inputManager.onShootPressed += Shoot;
    }

    public override void EquipGun(int result)
    {
        base.result = result;
        gameObject.GetComponent<Animator>().SetTrigger("equipGun");
        StartCoroutine(waitForAnimation());
    }


    public void Shoot()
    {
        base.Shoot(0);
        if (base.bullets[base.currentBullet].activeInHierarchy)
        {
            enemy.Die();
        }
    }

    public override void Die()
    {
        Debug.Log("I ded");
        ShatterScreen.gameObject.SetActive(true);
        StartCoroutine(ShatterScreen.GetComponent<ScreenShatterExplosion>().Shatter());
        base.gameManager.audioManager.Play("death");

    }

    public void reset()
    {
        base.reset();
        gameObject.GetComponent<Animator>().Play("dequipGunPlayer");
    }

    private IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        base.RandomizeBulletSelection(-1);
    }
}
