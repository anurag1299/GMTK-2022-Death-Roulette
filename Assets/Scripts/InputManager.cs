using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManager gameManager;
    private bool _InputLocked = false;
    public void setInputLock(bool locked)
    {
        _InputLocked = locked;
    }

    public delegate void OnSpacePressed();
    public  event OnSpacePressed onSpacePressed;

    public delegate void OnShootPressed();
    public  event OnShootPressed onShootPressed;

    private void Start()
    {
        if (!gameManager)
        {
            gameManager = gameObject.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_InputLocked)
        {
            gameManager.uiManager.showPressSpace();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onSpacePressed?.Invoke();
                gameManager.uiManager.hideHelperDisplay();
            }
        }
        if(gameManager.getEquipCount() == 2)
        {
            gameManager.uiManager.showPressLeftClick();
            if (Input.GetMouseButtonDown(0)) {
                onShootPressed?.Invoke();
                gameManager.uiManager.hideHelperDisplay();

            }
        }
    }



}
