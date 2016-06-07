using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float timer;
    private bool offScreen;
    private float timeTilInActive;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        timeTilInActive = GetComponentInChildren<TrailRenderer>().time;
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
            Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
            if (transform.position.y > wrld.y)
            {
                if (_rigidbody.velocity.y > 0f)
                {
                    //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * -0.3f);
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * -1f);
                }

            }

            Vector3 wrld2 = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            if (transform.position.y < wrld2.y)
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
