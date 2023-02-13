using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text score;    
    public Text highScore;
    private bool validScore = true;
    public int scoreCommentary = 5;
    private float wallMoveAmount = 0f;
    
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        score.text = "0";
        FindObjectOfType<AudioManager>().Play("RefereeWhistleBegin");
        FindObjectOfType<AudioManager>().Play("CommentaryStart");
        FindObjectOfType<AudioManager>().Play("CrowdChanting",true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            int scoreNumber = int.Parse(score.text);
            int highScoreNumber = int.Parse(highScore.text);

            //Debug.Log("Ball hitted the object, Name: " + gameObject.name + " Tag: " + gameObject.tag);

            if (gameObject.CompareTag("Ground"))
            {
                if (scoreNumber > highScoreNumber)
                {
                    PlayerPrefs.SetInt("HighScore", scoreNumber);
                    highScore.text = scoreNumber.ToString();
                }
                if (scoreNumber >= 2)
                {
                    FindObjectOfType<AudioManager>().Play("RefereeWhistleFoul");
                }
                GameObject highScoreAlert = GameObject.Find("HighScoreAlert");
                highScoreAlert.GetComponent<Text>().text = "";
                scoreNumber = 0;
                FindObjectOfType<AudioManager>().Play("BallBounceGrass");
                //Debug.Log("Score reseted! ScoreNumber:" + scoreNumber + " Score For Commentary: " + scoreCommentary);
            }
            if (gameObject.CompareTag("Player"))
            {
                //Debug.Log("Score Report! ScoreNumber:" + scoreNumber + " Score For Commentary: " + scoreCommentary);
                if(scoreNumber == 0)
                {
                    scoreCommentary = 5;
                }

                if (validScore == true)
                {
                    scoreNumber++;
                    validScore = false;
                    //Debug.Log("You scored!!");
                }

                if (scoreNumber >= 10 && scoreNumber > highScoreNumber)
                {
                    GameObject highScoreAlert = GameObject.Find("HighScoreAlert");
                    highScoreAlert.GetComponent<Text>().text = "NOVO RECORDE!";
                }
   
                if (scoreNumber < highScoreNumber || highScoreNumber <= 50)
                {
                    if (scoreNumber >= 10 && scoreNumber == scoreCommentary)
                    {
                        scoreCommentary = scoreNumber + 10;
                        FindObjectOfType<AudioManager>().Play("CommentaryScore");
                        FindObjectOfType<AudioManager>().Play("CrowdCheering");
                    }
                    if (scoreNumber < 10 && scoreNumber == scoreCommentary)
                    {
                        scoreCommentary = scoreNumber + 5;
                        FindObjectOfType<AudioManager>().Play("CommentaryLowScore");
                        FindObjectOfType<AudioManager>().Play("CrowdCheering");
                    }
                }
                else if (scoreNumber == scoreCommentary && scoreNumber >= 50)
                {
                    scoreCommentary = scoreNumber + 10;
                    FindObjectOfType<AudioManager>().Play("CommentaryHighScore");
                    FindObjectOfType<AudioManager>().Play("CrowdCheering");
                }

                FindObjectOfType<AudioManager>().Play("BallKick");

            }
            score.text = scoreNumber.ToString();

            if (gameObject.CompareTag("Column"))
            { 
                //Debug.Log("Ball hitted the column");
                bool wallMoved = false;
                //Debug.Log("Collision Magnitude: " + collision.relativeVelocity.magnitude);
                if (collision.relativeVelocity.magnitude > 40.0f && wallMoved == false)
                {
                    wallMoveAmount = -8f;

                    if(collision.relativeVelocity.x > 0)
                    {
                        wallMoveAmount = 8f;
                    }
                    gameObject.transform.Translate(new Vector2(wallMoveAmount, 0f) * Time.deltaTime);
                    wallMoved = true;
                    //Debug.Log("Delta time passed: " + Time.deltaTime);
                    FindObjectOfType<AudioManager>().Play("BallHardWallHit");
                    FindObjectOfType<AudioManager>().Play("CommentaryPostHit");
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("BallWeakWallHit");
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (gameObject.CompareTag("Player"))
            {
                validScore = true;
                //Debug.Log("Now you can score again!!");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (gameObject.CompareTag("Column"))
            {
                //Debug.Log("Wall Movement Value: " + wallMoveAmount);
                if (wallMoveAmount > 0f)
                {
                    gameObject.transform.Translate(new Vector2(-wallMoveAmount, 0f) * Time.deltaTime);
                    //Debug.Log("Wall realocated: "+ -wallMoveAmount);
                    wallMoveAmount = 0f;
                }
                else if(wallMoveAmount < 0f)
                {
                    gameObject.transform.Translate(new Vector2(Mathf.Abs(wallMoveAmount), 0f) * Time.deltaTime);
                    //Debug.Log("Wall realocated: " + Mathf.Abs(wallMoveAmount));
                    wallMoveAmount = 0f;
                }
                
                //Debug.Log("On Collision Exit X magnitude: " + collision.relativeVelocity.x);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (gameObject.CompareTag("Ground"))
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //Debug.Log("Rigidbody X axis velocity of Ball while in collision with ground: " + rb.velocity.x);
                if (rb.velocity.x > 1f || rb.velocity.x < -1f)
                {
                    if (!FindObjectOfType<AudioManager>().isPlaying("BallRollingGrass"))
                    {
                        FindObjectOfType<AudioManager>().Play("BallRollingGrass");
                        //Debug.Log("Play Rolling in grass sound!");
                    }
                }
            }
        }
    }
}
