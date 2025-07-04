using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ChangeTrolls : MonoBehaviour
{
    public GameObject text;
    public GameObject[] trolls;
    int[] newCords = new int[3]{1, 0, -1};
    bool unblock = true;

    public void left()
    {
        if (unblock)
        {
            GameObject tmp = trolls[0];
            for (int i = 0; i < 3; i++)
            {
                newCords[i] = (int)math.round(trolls[i].transform.position.x) - 1;
                if (newCords[i] < -1)
                {
                    trolls[i].transform.position = new Vector3(1, trolls[i].transform.position.y, trolls[i].transform.position.z);
                    newCords[i] = 1;
                }
                else if (newCords[i] == 0)
                    tmp = trolls[i];
            }
            text.GetComponent<Text>().text = tmp.name;
            StartCoroutine(blockMovement());
        }
    }

    public void right()
    {
        if (unblock)
        {
            GameObject tmp = trolls[0];
            for (int i = 0; i < 3; i++)
            {
                newCords[i] = (int)math.round(trolls[i].transform.position.x) + 1;
                if (newCords[i] > 1)
                {
                    trolls[i].transform.position = new Vector3(-1, trolls[i].transform.position.y, trolls[i].transform.position.z);
                    newCords[i] = -1;
                }
                else if (newCords[i] == 0)
                    tmp = trolls[i];
            }
            text.GetComponent<Text>().text = tmp.name;
            StartCoroutine(blockMovement());
        }
    }

    IEnumerator blockMovement()
    {
        unblock = false;
        yield return new WaitForSeconds(0.5f);
        unblock = true;
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 tmp = new Vector3(newCords[i], trolls[i].transform.position.y, trolls[i].transform.position.z);
            trolls[i].transform.position = Vector3.Lerp(trolls[i].transform.position, tmp, 4*Time.deltaTime);
        }
    }
}
