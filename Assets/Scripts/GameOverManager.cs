using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI enemyKilledLabel;
    public TextMeshProUGUI timeLeftLabel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int enemyLeft = GameManager.instance.enemyLeft;
        float timeLeft = GameManager.instance.timeLeft;

        if (enemyLeft <= 0)
        {
            titleLabel.text = "Cleard!";

        }
        else
        {
            titleLabel.text = "GameOver..";
        }
        enemyKilledLabel.text = "Enemy Killed : " + (10 - enemyLeft);
        timeLeftLabel.text = "Time Left : " + timeLeft.ToString("#.##");

        Destroy(GameManager.instance.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAgainPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
