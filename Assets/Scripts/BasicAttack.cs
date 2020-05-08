using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject subject;
    Vector3 subjectOriginalPosition;
    Vector3 windupPosition;
    Vector3 forwardPosition;

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Basic attacking");
        subjectOriginalPosition = subject.transform.position;
        windupPosition = new Vector3(subjectOriginalPosition.x, subjectOriginalPosition.y, subjectOriginalPosition.z - 1);
        forwardPosition = new Vector3(subjectOriginalPosition.x, subjectOriginalPosition.y, subjectOriginalPosition.z + 1);

    }

    public void Awake()
    {
        Debug.Log("Moving unit from " + subjectOriginalPosition + " to " + windupPosition);
        subject.transform.position = Vector3.Lerp(subjectOriginalPosition, windupPosition, 1.0f);
        StartCoroutine(OneSecondPause());
        subject.transform.position = Vector3.Lerp(windupPosition, forwardPosition, .75f);
        StartCoroutine(ThreeQuarterPause());
        subject.transform.position = Vector3.Lerp(forwardPosition, subjectOriginalPosition, .25f);
    }

    IEnumerator OneSecondPause()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator ThreeQuarterPause()
    {
        yield return new WaitForSeconds(.75f);
    }
}
