using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneTracker : MonoBehaviour
{
    public Transform[] allBonesToCheck;
    public Transform characterRoot;
    public Transform furthestBone;
    public Transform highestBone;
    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {

        FindFurthestBone(allBonesToCheck);
        FindHighestBone(allBonesToCheck);
        if (furthestBone != null && highestBone != null)
        {
            thisTransform.position = new Vector3(thisTransform.position.x, highestBone.position.y, furthestBone.position.z);
        }
    }

    public void FindFurthestBone(Transform[] allBonesToCheck)
    {
        float FurthestDistance = 0;
        Transform furthestBoneTemp = null;

        foreach (Transform boneTransform in allBonesToCheck)
        {
            float checkedDistance = Mathf.Abs(boneTransform.position.z - characterRoot.position.z);
            //Debug.Log(checkedDistance);

            if (checkedDistance > FurthestDistance)
            {
                furthestBoneTemp = boneTransform;
                FurthestDistance = checkedDistance;
                //Debug.Log(furthestBoneTemp);
                //Debug.Log(FurthestDistance);
            }
        }

        furthestBone = furthestBoneTemp;
    }

    public void FindHighestBone(Transform[] allBonesToCheck)
    {
        float HighestDistance = 0;
        Transform highestBoneTemp = null;

        foreach (Transform boneTransform in allBonesToCheck)
        {
            float checkedDistance = boneTransform.position.y - characterRoot.position.y;

            //Debug.Log(checkedDistance);

            if (checkedDistance > HighestDistance)
            {
                highestBoneTemp = boneTransform;
                HighestDistance = checkedDistance;
                Debug.Log(highestBoneTemp);
                //Debug.Log(HighestDistance);
            }
        }

        highestBone = highestBoneTemp;
    }
}