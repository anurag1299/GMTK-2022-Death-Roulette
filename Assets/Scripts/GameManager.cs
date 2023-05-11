using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Person _Player;
    [SerializeField] private Person _Enemy;
    [SerializeField] private InputManager _InputManager;
    [SerializeField] public UIManager uiManager;

    public AudioManager audioManager;
    private int equipCount = 0;

    

    public int getEquipCount()
    {
        return equipCount;
    }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private bool[] whoShot = { false, false };

    private void Start()
    {

        if (!_InputManager)
        {
            _InputManager = gameObject.GetComponent<InputManager>();
        }

        _InputManager.onSpacePressed += LockInput;

    }


    private void LockInput()
    {
        _InputManager.setInputLock(true);
    }

    private void UnlockInput(int result)
    {
        _InputManager.setInputLock(false);
    }

    public void onEquip()
    {
        equipCount++;
        Debug.Log("equip count " + equipCount);
        if(equipCount == 2)
        {
            uiManager.HideDiceView();
        }
    }

    public void onShoot(int playerIndex, bool isShooting)
    {
        equipCount--;
        whoShot[playerIndex] = isShooting;

        Debug.Log("player " + playerIndex + " shooting " + isShooting);

        if(equipCount == 0)
        {
            if(!whoShot[0] && !whoShot[1])
            {
                StartCoroutine(resetWait());
                
            }else if(!whoShot[0] && whoShot[1])
            {
                StartCoroutine(loseWait());
            }else if(whoShot[0] && !whoShot[1])
            {
                StartCoroutine(wonWait());
            }
            else
            {
                StartCoroutine(drawWait());
            }
        }
    }

    private void reset()
    {
        
        UnlockInput(0);
        _Player.reset();
        _Enemy.reset();
        equipCount = 0;
    }

    private void Lose()
    {
        uiManager.ShowLoseScreen();
    }

    private void Won()
    {
        uiManager.ShowWonScreen();
    }

    private void Draw()
    {
        uiManager.ShowDrawScreen();
    }


    private IEnumerator resetWait()
    {
        yield return new WaitForSeconds(1);
        reset();
    }
    private IEnumerator loseWait()
    {
        yield return new WaitForSeconds(1);
        Lose();
    }
    private IEnumerator wonWait()
    {
        yield return new WaitForSeconds(1);
        Won();
    }
    private IEnumerator drawWait()
    {
        yield return new WaitForSeconds(1);
        Draw();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
