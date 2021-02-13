using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class experimentalObjectField : MonoBehaviour
{
    public GameObject originalObject;
    private List<GameObject> cloneList = new List<GameObject>();
    private List<Transform> originalPosList = new List<Transform>();
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = -10; x <= 10; x++)
        {
            for (int z = -10; z <= 10; z++)
            {
                GameObject clonedObject = Instantiate(originalObject, new Vector3(x, 0, z), Quaternion.identity);
                clonedObject.transform.parent = this.transform;
                clonedObject.SetActive(true);
                cloneList.Add(clonedObject);
                originalPosList.Add(clonedObject.transform);
            }
        }

        for (int i = 0; i < cloneList.Count; i++)
        {
            originalPosList[i].position = cloneList[i].transform.position;
            Debug.Log(originalPosList[i].position);
        }

        Debug.Log(originalPosList.Count);
        Debug.Log(originalPosList[220].position);
        cloneList[220].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float waveSize = MidiMaster.GetKnob(0x14, 0) * 10 + 1;
        float waveHeight = MidiMaster.GetKnob(0x13, 0) * 2;
        float speed = MidiMaster.GetKnob(0x12, 0) / 10;
        float displace = MidiMaster.GetKnob(0x11, 0);
        time += speed;

        for (int j = 0; j < cloneList.Count; j++)
        {
            cloneList[j].transform.position = originalPosList[j].position;
            float wobble = Random.Range(-displace, displace);
            cloneList[j].transform.position = new Vector3(originalPosList[j].transform.position.x + wobble, originalPosList[j].position.y, originalPosList[j].position.z);
        }

        if (Input.anyKey)
        {
            Debug.Log(originalPosList[220].position);
        }
    }
}
