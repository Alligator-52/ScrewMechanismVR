using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VR_ScrewOrBottleCap : MonoBehaviour
{
    public int totalThreads = 5;
    public float maxTightenDistance = 1f;
    public Vector3 rotationAxis = Vector3.up;
    public Vector3 downwardDirection = Vector3.down;
    public float rotationSpeed = 1f;

    private XRBaseInteractor interactor;
    private Vector3 initialInteractorPosition;
    private float currentRotation = 0f;
    private float rotationPerThread;
    private Vector3 initialPosition;
    private float maxRotation;
    private bool isBeingInteracted = false;

    public TextMeshProUGUI text;
    void Start()
    {
        rotationAxis.Normalize();
        downwardDirection.Normalize();

        rotationPerThread = 360f / totalThreads;
        maxRotation = rotationPerThread * totalThreads;

        initialPosition = transform.position;

        var interactable = gameObject.GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(OnGrabbed);
        interactable.hoverExited.AddListener(OnReleased);

    }
    int rotDir = 1;

    void Update()
    {
        //float input = Input.GetAxis("Horizontal");
        //Debug.Log($"input: {input}");
        if (isBeingInteracted)
        {
            float rotationAmount = rotDir * rotationSpeed * Time.deltaTime;

            currentRotation = Mathf.Clamp(currentRotation + rotationAmount, 0, maxRotation);
            //currentRotation = Mathf.Clamp(currentRotation + rotationAmount, -maxRotation, maxRotation);

            float completedRotationPercentage = currentRotation / maxRotation;
            float movementDistance = completedRotationPercentage * maxTightenDistance;

            transform.rotation = Quaternion.AngleAxis(currentRotation, rotationAxis);

            transform.position = initialPosition + downwardDirection * movementDistance;
        }
        
    }

    void OnGrabbed(HoverEnterEventArgs args)
    {
        interactor = args.interactor;
        initialInteractorPosition = interactor.transform.position;
        isBeingInteracted = true;
        rotDir = interactor.transform.parent.CompareTag("LeftController") ? -1 : 1;
        //if (interactor.CompareTag("LeftController"))
        //{
        //    rotDir = -1;
        //}
        //else if (interactor.CompareTag("RightController"))
        //{
        //    rotDir = 1;
        //}

        text.text = $"interactor: {interactor.name}, rotdir: {rotDir}";
    }

    void OnReleased(HoverExitEventArgs args)
    {
        interactor = null;
        isBeingInteracted = false;
    }
}
