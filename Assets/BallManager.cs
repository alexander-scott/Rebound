using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public int NumOfBalls;
    public GameObject BallPrefab;

    private List<Transform> listOfBalls = new List<Transform>();
    private int currentBallIndex = 0;

    // Use this for initialization
    void Awake()
    {
        SpawnBalls();

        var recognizer = new TKSwipeRecognizer();
        recognizer.boundaryFrame = new TKRect(0, 0, Screen.width, Screen.height);
        recognizer.timeToSwipe = 0f;
        recognizer.gestureRecognizedEvent += (r) =>
        {
            var vel = r.swipeVelocity;
            if (vel > 99)
                vel = 99;
            Swiped((r.endPoint - r.startPoint) / ((100 - vel) / 30));
        };
        TouchKit.addGestureRecognizer(recognizer);
    }

    void SpawnBalls()
    {
        for (int i = 0; i < NumOfBalls; i++)
        {
            var obj = Instantiate(BallPrefab) as GameObject;
            obj.transform.position = transform.position;
            obj.transform.parent = transform;
            if (i != 1)
                obj.gameObject.SetActive(false);    

            listOfBalls.Add(obj.transform);
        }
    }

    void Swiped(Vector2 velocity)
    {
        if (velocity.y > 100f)
        {
            velocity = new Vector2(velocity.x, 100f);
        }
        if (velocity.x > 100f)
        {
            velocity = new Vector2(100f, velocity.y);
        }

        listOfBalls[currentBallIndex].gameObject.GetComponent<Rigidbody2D>().velocity = velocity;

        currentBallIndex++;
        if (currentBallIndex >= NumOfBalls)
            currentBallIndex = 0;

        listOfBalls[currentBallIndex].gameObject.SetActive(true);
        listOfBalls[currentBallIndex].transform.position = transform.position;
        listOfBalls[currentBallIndex].gameObject.SetActive(true);
    }
}
