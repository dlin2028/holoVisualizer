using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Visualizer : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;

    private Rigidbody body;

    private SphereCollider collider;

    public GameObject visualization;

    public int sphereCount = 5;

    // Use this for initialization
    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        for (int x = 0; x < sphereCount; x++)
        {
            for (int y = 1; y < sphereCount - 1; y++)
            {
                GameObject createdObject = Instantiate(visualization);
                createdObject.transform.position = transform.position;
                createdObject.GetComponent<VisualizationSphere>().visualizer = this;
                createdObject.transform.Rotate(new Vector3(x * 360 / sphereCount + 1, y * 360 / sphereCount + 1, 0));
                createdObject.transform.parent = transform;
                createdObject.transform.localPosition += createdObject.transform.forward;
                createdObject.transform.LookAt(transform);
            }
        }
        collider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        clipSampleData = new float[sampleDataLength];
    }
    private void OnSelect()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {

            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;
        }
        body.AddTorque(Vector3.right * clipLoudness);
        body.AddTorque(Vector3.forward * clipLoudness);
    }
    private void LateUpdate()
    {
        collider.radius = Vector3.Distance(transform.position, transform.GetChild(0).position) * 1/transform.localScale.z + transform.GetChild(0).localScale.z;
    }
}