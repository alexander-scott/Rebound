using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float timer;
    private bool offScreen;
    private float timeTilInActive;
    private float leftX;
    private float rightX;
    private float topY;
    private float botY;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        timeTilInActive = GetComponentInChildren<TrailRenderer>().time;

        topY = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
        botY = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        leftX = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        rightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf)
            return;

        if (offScreen)
        {
            if (timer > timeTilInActive)
            {
                gameObject.SetActive(false);
                offScreen = false;
            }
            timer += Time.deltaTime;
        }
        else
        {
            if (transform.position.y > topY ||
                transform.position.y < botY ||
                transform.position.x < leftX ||
                transform.position.x > rightX)
            {
                if (!offScreen)
                {
                    offScreen = true;
                    timer = 0f;
                }
            }
        }
    }
}
