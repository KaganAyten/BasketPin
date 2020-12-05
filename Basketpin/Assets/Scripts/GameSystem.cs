using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [Header("Game Elements")]
    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject rightArmReflect;
    public GameObject leftArmReflect;
    public GameObject ball;
    public GameObject hoop;

    [Header("UI Elements")]
    public Button AudioButton;
    public Button SFXButton;
    public GameObject Retry;
    public GameObject StaticButtons;
    public GameObject Play;
    public GameObject HowToPlay;
    public Text scoreText;
    public Text scoreFinalText;
    public Text bestScoreText;

    [Header("Sprites")]
    public Sprite AudioOn;
    public Sprite AudioOff;
    public Sprite SFXOn;
    public Sprite SFXOff;


    [Header("Sound Elements")]
    public AudioClip kick;
    public AudioClip gameOver;
    public AudioClip dunk;
    public AudioSource musicControl;
    public AudioSource SFXControl;

    int isMutedAudio;
    int isMutedSFX;
    int bestScore;

    int retryCounter;

    [Header("Game Settings")]
    [SerializeField]
    [Range(500,2500)]
    private float throwForce;

    [SerializeField]
    [Range(-50, 50)]
    private float minY,maxY;

    [SerializeField]
    [Range(-50, 50)]
    private float minX,maxX;

    private Rigidbody2D rbRight;
    private Rigidbody2D rbLeft;
    private Rigidbody2D rbRightReflect;
    private Rigidbody2D rbLeftReflect;

    private Collider2D hoopColl;
    private BallControl ballData;

    private Vector3 spawnPos;
    void Start()
    {
        rbRight = rightArm.GetComponent<Rigidbody2D>();
        rbLeft = leftArm.GetComponent<Rigidbody2D>();
        rbRightReflect = rightArmReflect.GetComponent<Rigidbody2D>();
        rbLeftReflect = leftArmReflect.GetComponent<Rigidbody2D>();
        hoopColl = hoop.GetComponent<BoxCollider2D>();
        ballData = ball.GetComponent<BallControl>();
        spawnPos = new Vector3(-1.5f, 4, 0);

        ball.transform.position = spawnPos;
        isMutedAudio = 0;
        isMutedSFX = 0;
        Time.timeScale = 0;
        scoreText.enabled = false;
        bestScore= PlayerPrefs.GetInt("Best", 0);
        isMutedAudio = PlayerPrefs.GetInt("Audio", 0);
        isMutedSFX= PlayerPrefs.GetInt("SFX", 0);
        retryCounter = 0;
    }

    void Update()
    {
        if (retryCounter >= 5)
        {
            //show ad
            retryCounter = 0;
        }
        if (isMutedAudio == 1)
        {
            musicControl.volume = 0;
            AudioButton.image.sprite = AudioOff;
        }
        else
        {
            musicControl.volume = 1;
            AudioButton.image.sprite = AudioOn;
        }
        if (isMutedSFX == 1)
        {

            SFXControl.volume = 0;
            SFXButton.image.sprite = SFXOff;
        }
        else
        {
            SFXControl.volume = 1;
            SFXButton.image.sprite = SFXOn;
        }
        if (!ballData.GetIsDead())
        {
            Time.timeScale = 1;
            scoreText.text = ballData.GetScore().ToString();
            if (Input.GetMouseButtonDown(0))
            {
                SFXControl.PlayOneShot(kick);
                Vector3 inputPos = Input.mousePosition;
                if (inputPos.x >= Screen.width / 2f)
                {
                    Throw(rbRight, -throwForce);
                    Throw(rbRightReflect, -throwForce);
                }
                if (inputPos.x < Screen.width / 2f)
                {
                    Throw(rbLeft, throwForce);
                    Throw(rbLeftReflect, throwForce);
                }
            }
            if (ballData.GetIsHooped())
            {
                hoopColl.enabled = false;
            }
            else
            {
                hoopColl.enabled = true;
            }
        }
        else
        {
            if (ballData.GetScore() > bestScore)
            {
                bestScore = ballData.GetScore();
                PlayerPrefs.SetInt("Best", bestScore);
            }
            scoreFinalText.text = "SCORE:" + ballData.GetScore();
            bestScoreText.text = "BEST: " + bestScore;
            scoreText.enabled = false;
            StaticButtons.SetActive(true);
            ball.SetActive(false);
            hoop.SetActive(false);
            Retry.SetActive(true);
            musicControl.volume = 0;
            Time.timeScale = 0;
        }
        
    }
    public void StartGame()
    {
        StaticButtons.SetActive(false);
        Play.SetActive(false);
        RestartGame();
    }
    public void HowToPlayButton()
    {
        HowToPlay.SetActive(true);
    }
    public void HowToPlayButtonClose()
    {
        HowToPlay.SetActive(false);
    }
    public void RateUs()
    {
        Application.OpenURL("market://details?id=com.PixelPearl.Reflecks");
    }
    public void SFXGameOver()
    {
        SFXControl.PlayOneShot(gameOver);
    }
    public void SFXDunk()
    {
        SFXControl.PlayOneShot(dunk);
    }
    public void MuteAudio()
    {
        if (isMutedAudio==0)
        {
            isMutedAudio = 1;
            PlayerPrefs.SetInt("Audio", isMutedAudio);
        }
        else
        {
            isMutedAudio = 0;
            PlayerPrefs.SetInt("Audio", isMutedAudio);
        }
    }
    public void MuteSFX()
    {
        if (isMutedSFX == 0)
        {
            isMutedSFX = 1;
            PlayerPrefs.SetInt("SFX", isMutedSFX);
        }
        else
        {
            isMutedSFX = 0;
            PlayerPrefs.SetInt("SFX", isMutedSFX);
        }
    }
    public void ChangeHoopPosition()
    {
        hoop.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY),0);
    }
    void Throw(Rigidbody2D rb,float force)
    {
        rb.AddTorque(force);
    }
    public void RestartGame()
    {
        retryCounter++;
        scoreText.enabled = true;
        StaticButtons.SetActive(false);
        ball.SetActive(true);
        hoop.SetActive(true);
        ballData.SetScore(0);
        ballData.SetIsDead(false);
        ball.transform.position = spawnPos;
        Retry.SetActive(false);
        musicControl.volume = 1;
        ChangeHoopPosition();
    }
}
