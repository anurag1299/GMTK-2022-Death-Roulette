using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject diceViewPlayer;
    [SerializeField] private GameObject diceViewEnemy;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;



    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject WonScreen;
    [SerializeField] private GameObject DrawScreen;

    [SerializeField] private Image helperDisplay;
    [SerializeField] private Sprite space;
    [SerializeField] private Sprite leftClick;

    private void Start()
    {
        inputManager.onSpacePressed += ShowDiceView;
        if (!gameManager)
        {
            gameManager = gameObject.GetComponent<GameManager>();
        }

    }

    private void ShowDiceView()
    {
        diceViewPlayer.SetActive(true);
        diceViewEnemy.SetActive(true);
    }

    public void HideDiceView()
    {
        diceViewPlayer.SetActive(false);
        diceViewEnemy.SetActive(false);
    }


    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void ShowWonScreen()
    {
        WonScreen.SetActive(true);
    }

    public void ShowDrawScreen()
    {
        DrawScreen.SetActive(true);
    }

    public void showPressSpace()
    {
        helperDisplay.sprite = space;
        helperDisplay.gameObject.SetActive(true);
    }

    public void showPressLeftClick()
    {
        helperDisplay.sprite = leftClick;
        helperDisplay.gameObject.SetActive(true);
    }

    public void hideHelperDisplay()
    {
        helperDisplay.gameObject.SetActive(false);
    }
}
