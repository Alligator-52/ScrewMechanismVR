//using Oculus.Interaction;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
////using Oculus.Interaction;

////public enum AxisSelector
////{
////    X,
////    Y,
////    Z,
////    XY,
////    XYZ
////}

////public enum MotionDirection
////{
////    None,
////    Forward,
////    Backward
////}



//public class DynamicRotationController : MonoBehaviour
//{
//    //[SerializeField]
//    //private Quaternion currentRotation;

//    //[SerializeField]
//    //private Quaternion previousRotationAllen;

//    //[SerializeField]
//    //private float angleChange;

//    //public ManifoldScriptManager ManifoldScriptManager;
//    public StepManager stepmanager;

//    public float currentAngle = 0;
//    public int number = 0;
//    public Quaternion previousRotation;
//    public Quaternion InitRot;
//    public float newPositionX;
//    public float newPositionY;
//    public float newPositionZ;
//    public float offset = 0.1714f;
//    public float offsetY = 0;
//    public float offsetZ = 0;
//    public float threshold = 0.096f;
//    public Vector3 currentPosition;
//    public float lim;
//    public float LimOfZ;

//    public bool absoulteZero = false, doUlta = false;

//    public bool needMotionSupport
//    {
//        get; private set;

//    }
//    [SerializeField]
//    private MotionDirection direction;


//    public AxisSelector SelectAxis;



//    void Start()
//    {
//        InitRot = previousRotation = transform.localRotation;
//        //newPositionZ = 0.2473f;
//        //previousRotationAllen = transform.rotation;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        RotationManager();
//        currentPosition = transform.localPosition;
//        if (SelectAxis == AxisSelector.X)
//        {
//            transform.localPosition = new Vector3(newPositionX - offset, currentPosition.y, currentPosition.z);
//            newPositionX = number * -0.003f;
//        }
//        else if (SelectAxis == AxisSelector.Y)
//        {
//            transform.localPosition = new Vector3(currentPosition.x, newPositionY - offsetY, currentPosition.z);
//            newPositionY = number * -0.003f;
//        }

//        else if (SelectAxis == AxisSelector.Z)
//        {
//            transform.localPosition = new Vector3(currentPosition.x, currentPosition.y, newPositionZ - offsetZ);
//            newPositionZ = number * -0.003f;
//        }

//        //var greaterThanEqualTo = newPositionY >= threshold;
//        //var lessThanEqualTo = newPositionY <= threshold;

//        if (newPositionY >= 0.096f && isoneGrabAttached)
//        {
//            //Translator();
//        }

//        if (newPositionY <= -0.096f && isoneGrabAttached)
//        {

//        }

//        if (newPositionX <= -0.066 && stepmanager.stepCounter == 1)  //to do - make it dynamic - Lim, ><, index value. 
//        {
//            stepmanager.Steper[0].checkMark.SetActive(true);
//            stepmanager.Steper[0].snapObjects["rotatableObject"].SetActive(false);
//            stepmanager.Animator.Play("Step1a_BreakAssembly");
//        }

//        if (newPositionZ <= LimOfZ)
//        {
//            var trans = stepmanager.Steper[1].snapObjects["rotatableObject"].transform;
//            stepmanager.Steper[1].snapObjects["freeObject"].transform.position = trans.position;
//            stepmanager.Steper[1].snapObjects["freeObject"].transform.rotation = trans.rotation;
//            stepmanager.Steper[1].snapObjects["rotatableObject"].SetActive(false);
//            stepmanager.Steper[1].snapObjects["freeObject"].SetActive(true);
//            stepmanager.Steper[1].miscellaneous[0].SetActive(false);
//            stepmanager.Steper[1].miscellaneous[1].SetActive(true);
//            stepmanager.Steper[1].miscellaneous[2].SetActive(true);
//            stepmanager.Steper[1].miscellaneous[3].SetActive(false);
//            stepmanager.Steper[4].miscellaneous[4].SetActive(true);

//            //stepmanager.Steper[1].checkMark.SetActive(true);

//            //stepmanager.ManifoldAnimator.Play("Step1a_BreakAssembly");
//        }

//        if (newPositionX <= -0.042f && stepmanager.stepCounter == 4)
//        {
//            stepmanager.Steper[3].checkMark.SetActive(true);
//            stepmanager.Animator.Play("Step4b_BreakAssembly");
//            //stepmanager.Steper[3].snapObjects["Spanner_Rotatable"].SetActive(false);
//            stepmanager.Steper[3].miscellaneous[0].SetActive(false);
//        }

//        if (newPositionX >= 0.036 && stepmanager.stepCounter == 7)
//        {
//            stepmanager.Steper[6].checkMark.SetActive(true);
//            stepmanager.Animator.Play("Step7b_BreakAssembly");
//            stepmanager.Steper[6].miscellaneous[0].SetActive(false);
//            stepmanager.Steper[6].miscellaneous[1].SetActive(true);
//            //stepmanager.Steper[6].snapObjects["Spanner_Rotatable2"].SetActive(false);
//        }

//        //if (stepmanager != null)
//        //{
//        //    if (newPositionX <= lim)
//        //    {
//        //        stepmanager.Steper[0].checkMark.SetActive(true);
//        //    }

//        //    if (newPositionZ <= LimOfZ)
//        //    {
//        //        stepmanager.Steper[1].checkMark.SetActive(true);
//        //    }
//        //}

//        //currentRotation = transform.rotation;
//        //angleChange = Quaternion.Angle(previousRotationAllen, currentRotation);

//        //Debug.Log(angleChange);
//    }


//    public void RotationManager()
//    {
//        Quaternion currentRotation = transform.localRotation;
//        float angleChange = Quaternion.Angle(previousRotation, currentRotation);

//        Debug.Log($"Current rotation: {currentRotation}");
//        Debug.Log($"Angle change: {angleChange}");

//        Vector3 axis = Vector3.zero;
//        float angle = 0f;
//        Quaternion.FromToRotation(previousRotation * Vector3.forward, currentRotation * Vector3.forward).ToAngleAxis(out angle, out axis);

//        int sign = (int)Mathf.Sign(Vector3.Dot(axis, Vector3.up));

//        if (sign == 0)
//        {
//            currentAngle += 0f;
//        }
//        else if (sign > 0)
//        {
//            Debug.Log("Clockwise");
//            if (direction == MotionDirection.Forward)
//            {
//            }
//            else
//            {
//                if (doUlta)
//                    currentAngle -= angle;
//                else
//                    currentAngle += angle;
//            }
//        }
//        else if (sign < 0)
//        {

//            Debug.Log("Counter Clockwise");
//            if (direction == MotionDirection.Backward)
//            {
//            }
//            else
//            {
//                if (doUlta)
//                    currentAngle += angle;
//                else
//                    currentAngle -= angle;
//            }
//        }

//        number = Mathf.FloorToInt(currentAngle / 90f);

//        if (absoulteZero)
//        {
//            number = Mathf.Abs(number);
//        }

//        Debug.Log($"Current angle: {currentAngle}, number of rotations: {number}");
//        previousRotation = currentRotation;
//    }

//    bool isoneGrabAttached = true;

//    //public void Translator()
//    //{
//    //    ManifoldScriptManager.TrainingAnimator.Play("empty");
//    //    // ITransformer former =  gameObject.GetComponent<Grabbable>().InjectOptionalOneGrabTransformer(trans);
//    //    Destroy(gameObject.GetComponent<OneGrabRotateTransformer>());
//    //    if (isoneGrabAttached == true)
//    //    {
//    //        ManifoldScriptManager.Steps[2].transform.GetChildWithName("VisualAllenRotatable").gameObject.transform.GetChildWithName("HandGrabInteractable").gameObject.SetActive(false);
//    //        ManifoldScriptManager.Steps[2].transform.GetChildWithName("VisualAllenRotatable").gameObject.SetActive(false);
//    //        ManifoldScriptManager.TrainingAnimator.Play("Step3a_Manifold");
//    //        ManifoldScriptManager.Steps[2].transform.GetChildWithName("checkmarkThree").gameObject.SetActive(true);
//    //        ManifoldScriptManager.Buttons[2].gameObject.transform.GetComponent<PokeInteractable>().enabled = true;
//    //        ManifoldScriptManager.Buttons[2].gameObject.transform.GetChildWithName("ButtonVisual").gameObject.transform.GetComponent<Image>().color = Color.green;
//    //        //StartCoroutine(ScrewRemovalAnimation());
//    //        isoneGrabAttached = false;
//    //        ManifoldScriptManager.StepManager.setNextButtonActive = true;
//    //        //ManifoldScriptManager.Animations[2].SetActive(false);

//    //        //gameObject.AddComponent<OneGrabFreeTransformer>();
//    //        //gameObject.GetComponent<Grabbable>().InjectOptionalOneGrabTransformer(gameObject.GetComponent<OneGrabFreeTransformer>());

//    //    }

//    //}


//}