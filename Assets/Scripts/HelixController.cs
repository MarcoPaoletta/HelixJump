using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startRotation; 
    private List<GameObject> spawnedLevels = new List<GameObject>();

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;
    public List<Stage> allStages = new List<Stage>();
    public float helixDistance;

    void Awake()
    {
        startRotation = transform.localEulerAngles; // start rotation of our helix
        helixDistance = topTransform.localPosition.y - goalTransform.localPosition.y + 0.1f;
        LoadStage(0);
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) // if we press the left button of the mouse or touch
        {
            Vector2 currentTapPosition = Input.mousePosition; // store the mouse pos
            // rotate the helix
            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;
            transform.Rotate(Vector3.up * distance);
        }

        if(Input.GetMouseButtonUp(0)) // if we stop pressing the left button of the mouse or the touch
        {
            lastTapPosition = Vector2.zero; // stop rotating
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)]; // create a new stage between the 0 and the max level we have created
        if(stage == null) // if we have no more levels
        {
            Debug.Log("No more levels");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgorundColor;
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
        transform.localEulerAngles = startRotation;

        foreach(GameObject go in spawnedLevels) // iterate between every level and delete them
        {
            Destroy(go);
        }
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++) // create the platforms
        {
            spawnPosY -= levelDistance; // distance between every platform
            GameObject level = Instantiate(helixLevelPrefab, transform); // instantiate the helix at the current pos
            level.transform.localPosition = new Vector3(0, spawnPosY, 0); // add the corresponding distance
            spawnedLevels.Add(level);

            int partToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partToDisable) // accesing every part
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject; // obtain a random part of the helix

                if(!disabledParts.Contains(randomPart)) // disable an specific part of the helix
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
                List<GameObject> leftParts = new List<GameObject>();

                foreach (Transform t in level.transform) // obtain the transform of every platform
                {
                    t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                    if (t.gameObject.activeInHierarchy)
                    {
                        leftParts.Add(t.gameObject);
                    }
                }
                List<GameObject> deathParts = new List<GameObject>();

                while (deathParts.Count < stage.levels[i].deathPartCount)
                {
                    GameObject randomPart2 = leftParts[(Random.Range(0, leftParts.Count))];

                    if (!deathParts.Contains(randomPart2))
                    {
                        randomPart2.gameObject.AddComponent<DeathPart>();
                        deathParts.Add(randomPart2);
                    }
                }
            }
        }
    }
}
