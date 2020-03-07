using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCount : MonoBehaviour
{
    private int points = 0;
    private Text pointage;

    public int Points { get => points; set => points = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        pointage = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        pointage.text = Points.ToString();
    }

    public void AddPoint()
    {
        Points++;
    }
}
