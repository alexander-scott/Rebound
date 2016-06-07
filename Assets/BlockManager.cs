using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public int NumOfBlocks;
    public GameObject BlockPrefab;

    private List<Transform> listOfBlocksSides = new List<Transform>();

    private float sideInterval;
    private float topInterval;

    private float currentBlockLength = 13f;
    private float gapBetween = 3f;

    // Use this for initialization
    void Awake()
    {
        RectTransform rt = (RectTransform)BlockPrefab.transform;
        sideInterval = (Camera.main.orthographicSize) / ((rt.rect.height) + gapBetween);
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        float currentYPos = 0f;
        for (int i = 0; i < NumOfBlocks; i++)
        {
            var obj = Instantiate(BlockPrefab) as GameObject;
            //obj.transform.Rotate(0f, 0f, 90f);
            obj.transform.position = new Vector3(obj.transform.position.x, currentYPos, obj.transform.position.z);
            obj.transform.parent = transform;
            currentYPos += sideInterval;
            listOfBlocksSides.Add(obj.transform);
        }
    }
}
