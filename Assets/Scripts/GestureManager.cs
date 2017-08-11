using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour
{
    public WorldCursor cursor;
    GestureRecognizer recognizer;
    public GameObject FocusedObject { get; private set; }

    private RaycastHit hitInfo;

    public RaycastHit HitInfo
    {
        get { return hitInfo; }
    }

    // Use this for initialization
    void Start () {

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;

        recognizer.StartCapturingGestures();
	}

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if(cursor.HitInfo.collider.gameObject == null)
        {
            return;
        }

        cursor.HitInfo.collider.gameObject.SendMessage("OnSelect");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var headPos = Camera.main.transform.position;
        var headDirection = Camera.main.transform.forward;

        if (Physics.Raycast(new Ray(headPos, headDirection), out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
    }

    private void OnDestroy()
    {
        if(recognizer != null)
        {
            recognizer.StopCapturingGestures();
            recognizer.TappedEvent -= Recognizer_TappedEvent;
        }
    }


}
