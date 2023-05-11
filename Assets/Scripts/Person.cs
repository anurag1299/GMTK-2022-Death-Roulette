using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Person : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] protected GameObject[] bullets;
    private List<int> bulletsCount = new List<int>();    
    [SerializeField] private GameObject GunBarrel;
    public int result = -1;
    [SerializeField]private Dice die;
    [SerializeField] private ParticleSystem muzzleFlash;
    public InputManager inputManager;

    protected int currentBullet = -1;

    private int[] barrelRotationsOnZ = { 0, 60, 120, 180, 240, 300 };

    private delegate void OnGunEquip();
    private event OnGunEquip onGunEquip;

    private delegate void OnGunShoot(int playerIndex,bool isShooting);
    private event OnGunShoot onGunShoot;

    protected void Start()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        die.onDieStopped += EquipGun;
        onGunEquip += gameManager.onEquip;
        
        onGunShoot += gameManager.onShoot;
    }

    public abstract void EquipGun(int result);


    public virtual void Shoot(int personIndex)
    {
        if (bullets[currentBullet].activeInHierarchy)
        {
            //fire vfx,animation and sound
            gameManager.audioManager.Play("shoot");
            
            muzzleFlash.gameObject.SetActive(true);
            muzzleFlash.Play();

            onGunShoot?.Invoke(personIndex, true);
        }
        else
        {
            //fail sound
            gameManager.audioManager.Play("empty_shoot");
            onGunShoot?.Invoke(personIndex,false);
        }
    }

    public abstract void Die();
    

    public void RandomizeBulletSelection(int direction)
    {
        int count = 6-result;
        if(result != -1)
        {
            bulletsCount.Clear();
            for(int i = 0; i < 6; i++)
            {
                bullets[i].SetActive(true);
                bulletsCount.Add(i);
                
            }
            while (count > 0)
            {
                int randomIndex = Random.Range(0, bulletsCount.Count);
                int randomValue = bulletsCount[randomIndex];
                bulletsCount.RemoveAt(randomIndex);
                bullets[randomValue].SetActive(false);
                count--;
            }
        }
        int targetBullet = Random.Range(0, 6);
        currentBullet = targetBullet;
         GunBarrel.transform.DOLocalRotate(new Vector3(0, 0,direction*( 5 * 360 + barrelRotationsOnZ[targetBullet])), 1,RotateMode.FastBeyond360).OnComplete(() => {
            onGunEquip?.Invoke();
        });
    }

    public void reset()
    {
        result = -1;
        currentBullet = -1;
    }

}
