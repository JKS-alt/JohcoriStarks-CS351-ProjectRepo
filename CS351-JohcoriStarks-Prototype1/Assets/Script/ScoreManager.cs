using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// add this to work with text mesh pro
using TMPro;
// add to work with scene manager
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    //notice public static varibale can be
    //acess by any script but cant be seen in inspector panel
    public static bool gameOver;
    public static bool won;
    public static int score;

    public TMP_Text textbox;

    public static int ScoreToWin;

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

        if (score >= ScoreToWin)
        {
            won = true;
            gameOver = true;


        }

        if (gameOver)
        {
            if (won)
            {

                textbox.text = ("You win \n Press R to try again");

            }
            else
            {
                textbox.text = "You lose \n Press R to try again";
            }

            // finish this
            // if (Input.GetKeyDown(KeyCode.R))

        }


    }
}
