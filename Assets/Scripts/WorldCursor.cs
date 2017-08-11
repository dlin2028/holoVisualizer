﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WorldCursor : MonoBehaviour {

    private MeshRenderer mesh;


    private RaycastHit hitInfo;

    public RaycastHit HitInfo
    {
        get { return hitInfo; }
    }


    // Use this for initialization
    void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var headPos = Camera.main.transform.position;
        var headDirection = Camera.main.transform.forward;
        
        if (Physics.Raycast(new Ray(headPos, headDirection), out hitInfo))
        {
            mesh.enabled = true;
            transform.position = hitInfo.point;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            mesh.enabled = false;
        }
	}
}
