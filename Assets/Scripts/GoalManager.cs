using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalManager : MonoBehaviour {

    public GameObject goalPrefab;

    private Rect screen;
    private List<Transform> listOfGoals = new List<Transform>();
    private float timer = 0f;
    private int currentGoalIndex = 1;
    private int maxGoals = 15;

    // Use this for initialization
    void Start () {
        var origin = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        var extent = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        screen = new Rect(origin, extent);
        SpawnGoals();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > 5f)
        {
            var x = Random.Range(screen.min.x, screen.max.x);
            var y = Random.Range(screen.min.y, screen.max.y);
            listOfGoals[currentGoalIndex].position = new Vector3(x, y);
            listOfGoals[currentGoalIndex].gameObject.SetActive(true);
            currentGoalIndex++;
            if (currentGoalIndex > maxGoals)
                currentGoalIndex = 0;
            timer = 0f;
        }
	}

    void SpawnGoals()
    {
        for (int i = 0; i < maxGoals; i++)
        {
            var obj = Instantiate(goalPrefab) as GameObject;
            obj.transform.position = transform.position;
            obj.transform.parent = transform;
            if (i != 0)
                obj.gameObject.SetActive(false);

            listOfGoals.Add(obj.transform);
        }
    }

}
