using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D _rigidBody2d;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private Random _random = new Random();
    private int _randomColor = 0;

    private float statingTime = 3f;
    private float currentTime = 0f;

    private float[] delaysToRespawn;

    private bool gameOver = false;

    public State CurrentState { get; set; } = State.Hidden;
    public bool GameOver { get => gameOver; set => gameOver = value; }

    // Awake is called before the first frame update
    private void Awake()
    {
        currentTime = statingTime;
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _randomColor = Random.Range(1, 4);
        CurrentState = (State)_randomColor;
        delaysToRespawn = new float[4] { 2f, 3f, 4f, 5f };
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameOver == false)
        {
            currentTime -= 1 * Time.deltaTime;

            SwitchColorAccordingToState();

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.name.Contains("Square"))
                    {
                        if (hit.transform.gameObject.GetComponent<Move>().CurrentState == State.Outside)
                        {
                            hit.transform.gameObject.GetComponent<Move>().CurrentState = State.Crushed;
                            GetPoint();
                        }
                    }
                }
            }

            if (currentTime <= 0)
            {
                SwitchToNextState();
            }
        }
    }

    private void GetPoint()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<PointCount>().AddPoint();
    }

    private void SwitchToNextState()
    {
        switch (CurrentState)
        {
            case State.Hidden:
                CurrentState = State.Mid;
                // Time it takes to go outside
                currentTime = 1f; // delaysToRespawn[Random.Range(1, 4)];
                break;
            case State.Mid:
                CurrentState = State.Outside;
                currentTime = delaysToRespawn[Random.Range(1, 4)];
                break;
            case State.Outside:
                CurrentState = State.Hidden;
                // Time it takes to hide
                currentTime = delaysToRespawn[Random.Range(1, 4)];
                break;
            case State.Crushed:
                CurrentState = State.Hidden;
                currentTime = 5f;
                break;
        }
    }

    private void SwitchColorAccordingToState()
    {
        switch (CurrentState)
        {
            case State.Hidden:
                ChangeColor(Color.red, gameObject);
                break;
            case State.Mid:
                ChangeColor(Color.yellow, gameObject);
                break;
            case State.Outside:
                ChangeColor(Color.green, gameObject);
                break;
            case State.Crushed:
                ChangeColor(Color.magenta, gameObject);
                break;
        }
    }

    private void ChangeColor(Color color, GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
