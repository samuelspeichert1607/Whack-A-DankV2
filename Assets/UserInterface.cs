using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    private float statingTime;
    private float currentTime = 0f;
    private Text time;


    // Start is called before the first frame update
    private void Awake()
    {
        currentTime = statingTime;
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        time.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true;
            foreach(GameObject boomer in GameObject.FindGameObjectsWithTag("Boomer"))
            {
                GameOver(boomer);
            }

        }
    }


    private void GameOver(GameObject boomer)
    {
        boomer.GetComponent<Move>().GameOver = true;
    }
}
