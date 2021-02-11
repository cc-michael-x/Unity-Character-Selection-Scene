using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LookingEntry {
    public GameObject item;
    public float maximumAngle = 75f;
    public float turnSpeed = 1f;
    public float maximumRectionDistance = 10f;
    [Range(0.1f, 1.0f)]
    public float turnPercentage = 1f;
}

public class CharacterHeadLookAtCamera : MonoBehaviour
{
    public GameObject target;
    public LookingEntry[] parts;
 
    Quaternion[] rotations;
 
    // Use this for initialization
 
    void Start () {
        rotations = new Quaternion[parts.Length];
 
        for (int i=0; i < parts.Length; i++) {
            rotations[i] = parts[i].item.transform.rotation;
        }
    }
    
    // Update is called once per frame
    void LateUpdate () {
        GameObject item;
        LookingEntry entry;
        for (int i=0; i < parts.Length; i++) {
            entry = parts[i];
            item = entry.item;
            //item.transform.LookAt(target.transform);
            if (item != null) {
                Vector3 angle = (target.transform.position - item.transform.position).normalized;
 
                Quaternion lookRotation;
 
                if ((Vector3.Angle(item.transform.forward, angle) < entry.maximumAngle) && (Vector3.Distance(item.transform.position, target.transform.position) < entry.maximumRectionDistance)) {
                    lookRotation = Quaternion.Slerp(item.transform.rotation, Quaternion.LookRotation(angle), entry.turnPercentage);
                } else {
                    lookRotation = item.transform.rotation;
                }

                rotations[i] = item.transform.rotation = Quaternion.Slerp(rotations[i], lookRotation, Time.deltaTime * entry.turnSpeed);
            }
        }
    }
}
