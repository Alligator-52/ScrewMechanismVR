using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Content.Interaction;

public class ScrewYou : MonoBehaviour
{
    public int numberOfThreads = 10;    
    public float pitch = 0.1f, torqueResistanceFactor = 0.5f,smoothingFactor = 0.1f;
    public float baseRotationSpeed = 100f; 
    public float currentRotation = 0f; 
    public float totalRotation;
    public Vector3 axis = Vector3.up;   
    private float totalDistance, maxNumOfRotations, currentRotations = 0, smoothedRotDir = 0f;


    public XRKnob knob;

    public enum InteractionType
    {
        Hand,
        ScrewDriver
    }


    void Start()
    {
        totalRotation = 360f * numberOfThreads;
        totalDistance = pitch * numberOfThreads;
        maxNumOfRotations = totalDistance/pitch;
    }


    void Update()
    {
        PerformHandRotation();
    }

    private void PerformHandRotation()
    {
        //float input = Input.GetAxis("Horizontal");
        Debug.Log($"rotDir: {knob.rotDir}");

        smoothedRotDir = Mathf.Lerp(smoothedRotDir, knob.rotDir, smoothingFactor);
        float rotationSpeed = baseRotationSpeed + (1f - torqueResistanceFactor * (currentRotation / totalRotation));
        //float rotationThisFrame = knob.rotDir * rotationSpeed * Time.deltaTime;
        float rotationThisFrame = smoothedRotDir * rotationSpeed * Time.deltaTime;


        if (currentRotation + rotationThisFrame > totalRotation)
        {
            rotationThisFrame = totalRotation - currentRotation;
        }
        else if (currentRotation + rotationThisFrame < 0f)
        {
            rotationThisFrame = -currentRotation;
        }

        currentRotation += rotationThisFrame;

        Quaternion rotation = Quaternion.AngleAxis(rotationThisFrame, axis);
        transform.rotation *= rotation;

        Vector3 translation = pitch * (rotationThisFrame / 360f) * axis.normalized;
        transform.localPosition += translation;
        currentRotations = Mathf.FloorToInt(currentRotation / 360f);
    }


}
