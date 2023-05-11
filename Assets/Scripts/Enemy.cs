using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    [SerializeField] private Player player;
    [SerializeField] private Animator chair;
    [SerializeField] private Animator enemy;
    [SerializeField] private ParticleSystem blood;

    private void Start()
    {
        base.Start();
        base.inputManager.onShootPressed += Shoot;
    }

    public override void EquipGun(int result)
    {
        base.result = result;
        gameObject.GetComponent<Animator>().Play("gunEquipEnemy");
        StartCoroutine(waitForAnimation());
    }


    public void Shoot()
    {
        base.Shoot(1);
        if (base.bullets[base.currentBullet].activeInHierarchy)
        {
            player.Die();
        }
        
    }

    public override void Die()
    {
        chair.Play("chairDie");
        //enemy.Play("die");
        blood.Play();
        base.gameManager.audioManager.Play("death");
    }

    public void reset()
    {
        base.reset();
        gameObject.GetComponent<Animator>().Play("dequipGunEnemy");
    }

    private IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        base.RandomizeBulletSelection(-1);
    }
}
