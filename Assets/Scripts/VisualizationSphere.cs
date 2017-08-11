using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VisualizationSphere : MonoBehaviour {
    
    public Visualizer visualizer;

    public float recoveryRate = 0.1f;
    public float distanceMultiplier = 3;
    public float smoothness = .3f;
    public float distanceOffset = 0.01f;

    private List<float> loudnessValues;
    private MeshRenderer renderer;
   
	void Start ()
    {
        loudnessValues = new List<float>();
        renderer = GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    float distance = 0;
    float currentLoudness;
	void LateUpdate ()
    {
        loudnessValues.Add(visualizer.clipLoudness);

        float averageLoudness = 0;
        foreach (var value in loudnessValues)
        {
            averageLoudness += value;
        }
        averageLoudness /= loudnessValues.Count;
        Color newColor = Color.HSVToRGB(averageLoudness, 1, 1);
        renderer.material.color = newColor;

        if (loudnessValues.Count >= 100)
        {
            loudnessValues.RemoveAt(0);
        }


        currentLoudness = visualizer.clipLoudness;

        if(currentLoudness > distance)
        {
            distance = currentLoudness;
        }
        else
        {
            distance = Mathf.Lerp(distance, distance - recoveryRate, 0.5f);
        }

        distance += distanceOffset;
        
        
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, -transform.forward * distance * distanceMultiplier, smoothness);

        distance -= distanceOffset;
    }
}
