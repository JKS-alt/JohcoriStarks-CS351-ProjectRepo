using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;// load or reload scene


public class ScoreScript : MonoBehaviour
{
    // Public static variables can be accessed from any script
    public static bool gameOver;
    public static bool won;
    public static int score;

    // Reference to a TextMeshPro UI element
    public TMP_Text textbox;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        won = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            textbox.text = "Score: " + score;
        }

        if (score >= 10)
        {
            won = true;
            gameOver = true;
        }

        if (gameOver)
        {
            if (won)
            {
                textbox.text = "You win!\nPress R to Try Again!";
            }
            else
            {
                textbox.text = "You lose!\nPress R to Try Again!";
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }
}
