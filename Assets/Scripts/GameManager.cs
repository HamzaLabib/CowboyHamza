using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public MeshRenderer ball;

    public AudioSource startingBell;
    public AudioSource gun;
    public AudioSource loseSound;

    public float timeDelayToShot = 3f;
    public float timeDelayToReset = 5f;
    int shooterOrder = 0;

    bool isRoundFinished = false;
    bool isTimeToShot = false;

    //List<Transform> players;


    void Start()
    {
        ball.material.color = Color.red;
        //players.Add(player1);
        //players.Add(player2);
    }


    void Update()
    {
        if (!isRoundFinished)
        {
            CountDown();
            ReadyToShot();
            GetController();
        }
        if (isRoundFinished)
        {
            CountDown();
            ResetTheGame();
        }
    }

    void CountDown()
    {
        if (isRoundFinished)
            timeDelayToReset -= Time.deltaTime;
        else
            timeDelayToShot -= Time.deltaTime;
    }

    void ReadyToShot()
    {
        if (timeDelayToShot < 0 && !isRoundFinished)
        {
            if (!isTimeToShot)
                startingBell.Play();

            ball.material.color = Color.green;
            isTimeToShot = true;
        }
    }

    void GetController()
    {
        if (!isRoundFinished)
        {
            if (Input.GetKey(KeyCode.A))
            {
                shooterOrder = 1;
                CheckTimeOfShot();
            }
            if (Input.GetKey(KeyCode.L))
            {
                shooterOrder = 2;
                CheckTimeOfShot();
            }
        }
    }

    void CheckTimeOfShot()
    {
        isRoundFinished = true;
        gun.Play();
        loseSound.Play();
        if (isTimeToShot)
            PlayerWon(shooterOrder);
        else
            PlayerLost(shooterOrder);
    }

    void PlayerWon(int playerOrder)
    {
        switch (playerOrder)
        {
            case 1:
                player2.Rotate(0, 0, 90);
                break;
            case 2:
                player1.Rotate(0, 0, -90);
                break;
            default:
                break;
        }
    }

    void PlayerLost(int playerOrder)
    {
        switch (playerOrder)
        {
            case 1:
                player1.Rotate(0, 0, -90);
                break;
            case 2:
                player2.Rotate(0, 0, 90);
                break;
            default:
                break;
        }
    }

    void ResetTheGame()
    {
        if (timeDelayToReset < 0)
        {
            player1.rotation = new Quaternion(0, 0, 0, 0);
            player2.rotation = new Quaternion(0, 0, 0, 0);
            ball.material.color = Color.red;

            timeDelayToShot = 3f;
            timeDelayToReset = 5f;

            isTimeToShot = false;
            isRoundFinished = false;
        }
    }
}
