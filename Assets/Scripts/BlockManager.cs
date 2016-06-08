using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public int NumOfBlocks;
    public GameObject BlockPrefab;

    private List<Transform> listOfBlocksLeftSide = new List<Transform>();
    private List<Transform> listOfBlocksRightSide = new List<Transform>();
    private List<Transform> listOfBlocksTopSide = new List<Transform>();

    private float sideInterval;
    private float topInterval;

    private float currentBlockLength = 13f;
    private float gapBetween = 6f;
    private float currentYPos = 0f;
    private float currentXPos = 0f;

    private float maxY;
    private float maxX;
    private Vector3 originPoint;

    // Use this for initialization
    void Awake()
    {
        RectTransform rt = (RectTransform)BlockPrefab.transform;
        sideInterval = (Camera.main.orthographicSize) / ((rt.rect.height) + gapBetween);
        currentYPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y +5f;
        maxY = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        originPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        currentXPos = originPoint.x + 2f;

        SpawnBlocks();
    }

    void SpawnBlocks()
    { 
        while (currentYPos < maxY -1f)
        {
            var obj = Instantiate(BlockPrefab) as GameObject;
            //obj.transform.Rotate(0f, 0f, 90f);
            obj.transform.position = new Vector3(originPoint.x + 1f, currentYPos, obj.transform.position.z);
            obj.transform.parent = transform;
            currentYPos += sideInterval;
            listOfBlocksLeftSide.Add(obj.transform);
        }

        currentYPos = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y + 5f;

        while (currentYPos < maxY -1f)
        {
            var obj = Instantiate(BlockPrefab) as GameObject;
            obj.transform.position = new Vector3(maxX - 1f, currentYPos, obj.transform.position.z);
            obj.transform.parent = transform;
            currentYPos += sideInterval;
            listOfBlocksRightSide.Add(obj.transform);
        }

        while (currentXPos < maxX - 1f)
        {
            var obj = Instantiate(BlockPrefab) as GameObject;
            obj.transform.position = new Vector3(currentXPos, maxY - 1f, obj.transform.position.z);
            obj.transform.Rotate(0f, 0f, 90f);
            obj.transform.parent = transform;
            currentXPos += sideInterval;
            listOfBlocksTopSide.Add(obj.transform);
        }
    }
}
