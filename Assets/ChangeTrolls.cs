using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using UnityEngine.Rendering;

public class ChangeTrolls : MonoBehaviour
{
    class Troll
    {
        public GameObject trollObj;
        public Vector3 newPosition;
        public Troll(GameObject trollObj, bool isActive, int offsetX)
        {
            this.trollObj = trollObj;
            this.trollObj.SetActive(isActive);
            this.trollObj.transform.position = new Vector3(offsetX, 0, 0);
            newPosition = this.trollObj.transform.position;
        }
        public void setNewPosition(int sign)
        {
            trollObj.transform.position = new Vector3(sign * math.abs(trollObj.transform.position.x), 0, 0);
            newPosition = new Vector3(trollObj.transform.position.x - sign*2, 0, 0);
            trollObj.SetActive(true);
        }
    }

    public GameObject text;
    public GameObject[] trollObjects;
    List<Troll> trolls = new List<Troll>();
    int currentTrollIndex = 0;
    bool unblock = true;

    IEnumerator SmoothMove(GameObject trollObj, Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0;
        while (t <= 1){
            t += Time.deltaTime/seconds;
            trollObj.transform.position = Vector3.Lerp(startpos, endpos, -(math.cos(math.PI * Mathf.SmoothStep(0.0f, 1.0f, t)) - 1) / 2);
            yield return new WaitForEndOfFrame();
        }
        trollObj.SetActive(endpos.x == 0 ? true : false);
    }
    IEnumerator blockInput()
    {
        unblock = false;
        yield return new WaitForSeconds(0.5f);
        unblock = true;
    }

    void iterate(int sign)
    {
        if (unblock)
        {
            StartCoroutine(blockInput());
            trolls[currentTrollIndex].setNewPosition(sign);
            StartCoroutine(SmoothMove(trolls[currentTrollIndex].trollObj, trolls[currentTrollIndex].trollObj.transform.position, trolls[currentTrollIndex].newPosition, 0.5f));

            currentTrollIndex += sign;
            if(currentTrollIndex > trolls.Count-1)
                currentTrollIndex=0;
            if(currentTrollIndex < 0)
                currentTrollIndex=trolls.Count-1;

            trolls[currentTrollIndex].setNewPosition(sign);
            StartCoroutine(SmoothMove(trolls[currentTrollIndex].trollObj, trolls[currentTrollIndex].trollObj.transform.position, trolls[currentTrollIndex].newPosition, 0.5f));
            text.GetComponent<Text>().text = trolls[currentTrollIndex].trollObj.name;
        }
    }

    public void left()
    {
        iterate(1);
    }

    public void right()
    {
        iterate(-1);
    }

    void Start()
    {
        trolls.Add(new Troll(trollObjects[0], true, 0));
        text.GetComponent<Text>().text = trollObjects[0].name;
        foreach(GameObject troll in trollObjects.Skip(1))
        {
            trolls.Add(new Troll(troll, true, 2));
        }
    }
}
