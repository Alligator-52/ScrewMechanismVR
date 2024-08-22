using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewYou : MonoBehaviour
{
    public Transform ParentTransform,AttachTransform;
 
    public int numberOfThreads = 10;    

    public float pitch = 0.1f, torqueResistanceFactor = 0.5f,smoothingFactor = 0.1f;
    public float baseRotationSpeed = 100f, currentRotation = 0f,totalRotation;
    private float totalDistance, maxNumOfRotations, currentRotations = 0, smoothedRotDir = 0f;

    public Vector3 axis = Vector3.up;   

    public XRKnob parentKnob, driverKnob;

    public Tool CurrentTool;

    public enum InteractionType
    {
        Hand,
        Tool
    }


    void Start()
    {
        totalRotation = 360f * numberOfThreads;
        totalDistance = pitch * numberOfThreads;
        maxNumOfRotations = totalDistance/pitch;
        driverKnob = parentKnob;
    }


    void Update()
    {
        PerformHandRotation();
    }

    private void PerformHandRotation()
    {
        //float input = Input.GetAxis("Horizontal");
        Debug.Log($"rotDir: {driverKnob.rotDir}");

        smoothedRotDir = Mathf.Lerp(smoothedRotDir, driverKnob.rotDir, smoothingFactor);
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
        if (CurrentTool != null)
            CurrentTool.transform.localPosition += translation;
        currentRotations = Mathf.FloorToInt(currentRotation / 360f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Tool>() != null) 
        {
            CurrentTool = other.GetComponent<Tool>();
            CurrentTool.parentTransform.rotation = ParentTransform.rotation;
            CurrentTool.parentTransform.position = AttachTransform.position;
            CurrentTool.grabInteractable.enabled = false;
            CurrentTool.knob.enabled = false;
            CurrentTool.knob.enabled = true;
            //CurrentTool.transform.SetParent(transform);
            driverKnob = CurrentTool.knob;
            parentKnob.enabled = false;
        }
    }

}
