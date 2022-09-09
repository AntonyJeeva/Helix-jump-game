using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class HelixController : MonoBehaviour
{

    private Vector2 lastTapPos;
    private Vector3 startRotation;

    public Transform topTransform; 
    public Transform goalTransform;
    // to get the position of the starting and ending helix ^ 

    public GameObject helixPlatformPrefab;

    public List<Stage> allStages = new List<Stage>();

    public float helixDistance;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    

    void Awake()
    {

        //Instantiate(LoadStage);
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if(lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if(Input.GetMouseButtonDown(0))
        {
            lastTapPos = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if(stage == null)
        {
            Debug.LogError("No Stage" + stageNumber + "found in allStages List. Are all stages assigned in the List ?");
            return;
        }
        
        //changing background color
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        // change ball color in respective stages 
        FindObjectOfType<PlayerController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        //reset helix rotation
        transform.localEulerAngles = startRotation;

        // destroy old platforms 
        foreach (GameObject go in spawnedPlatforms)
            Destroy(go);

        //create new platforms 
        float platformDistance = helixDistance / stage._platforms.Count;
       
        float spawnPosY = topTransform.localPosition.y;


        //this is where all the shit begins
        for (int i = 0; i < stage._platforms.Count; i++)
        {
            spawnPosY -= platformDistance;
            //create platforms with in the scene
            GameObject platform = Instantiate(helixPlatformPrefab, transform);
            Debug.Log("Platforms spawned");


            platform.transform.localPosition = new Vector3(0, spawnPosY, 0.286f);
            spawnedPlatforms.Add(platform);


            //creating gaps or empty space in the platforms 
            int partsToDisable = 12 - stage._platforms[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while(disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = platform.transform.GetChild(Random.Range(0, platform.transform.childCount)).gameObject;
                if(!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                } 

            }

            //changine color to the left parts
            // left parts - platforms which are in game after disabling some parts

            List<GameObject> leftParts = new List<GameObject>();

            foreach ( Transform t in platform.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stagePlatformColor;

                if (t.gameObject.activeInHierarchy)
                    leftParts.Add(t.gameObject);

            }


            // creating the obstacle - the red platform 
            List<GameObject> obstacleParts = new List<GameObject>();
            while(obstacleParts.Count < stage._platforms[i].obstaclePartCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if(!obstacleParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<Obstacle>();
                    obstacleParts.Add(randomPart);
                }
            }
          



        }

    }


}
  