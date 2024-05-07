using System.Collections.Generic;
using System.IO; // Added for Path and StreamWriter
using UnityEngine;
using VelUtils;

public class MyLogger : MonoBehaviour
{
    public Rig rig;
    //public Movement m;

    private const string inputsFileName = "inputs";
    //private const string movementFileName = "movement";
    [Space] public float updateRateHz = 4;

    private float nextUpdateTime;

    public bool logInputs;
    bool log = false;
    string playerID="";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("datapath: " + Application.persistentDataPath);
    }
    public void BeginLogging(string pID)
    {
        log = true;
        playerID= pID;
    }

    public void StopLogging()
    {
        log = false;
        playerID = null;
    }
    public void AppendDataFile(List<string> data) // Changed method name to AppendDataFile
    {
        string filePath = Path.Combine(Application.persistentDataPath, "logger.txt");

        // Create or append to the file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Write each string in the list to its own line
        foreach (string line in data)
        {
            writer.WriteLine(line);
        }

        writer.Close();
        Debug.Log("Data appended to text file.");
    }

    void Update()
    {
        if (!log)
        {
            return;
        }

        // if we are due for an update
        if (Time.time < nextUpdateTime) return;

        // set the next update time
        nextUpdateTime += 1f / updateRateHz;

        // if we are still behind, we missed an update - just reset
        if (Time.time > nextUpdateTime)
        {
            Debug.Log("Missed a log cycle", this);
            nextUpdateTime = Time.time + 1f / updateRateHz;
        }

        List<string> positions = new List<string>();
        positions.Add(playerID);
        string timeStamp = System.DateTime.Now.ToString();
        positions.Add(timeStamp);
        // tracking space pos
        Vector3 spacePos = rig.transform.position;

        positions.Add(spacePos.x.ToString());
        positions.Add(spacePos.y.ToString());
        positions.Add(spacePos.z.ToString());
        positions.Add(rig.transform.eulerAngles.y.ToString());

        // local space of head and hands
        Vector3 headPos = rig.head.position;
        Vector3 headForward = rig.head.forward;
        Vector3 headUp = rig.head.up;
        Vector3 headMoment = rig.head.rotation.ToMomentVector();
        positions.Add(headPos.x.ToString());
        positions.Add(headPos.y.ToString());
        positions.Add(headPos.z.ToString());
        positions.Add(headForward.x.ToString());
        positions.Add(headForward.y.ToString());
        positions.Add(headForward.z.ToString());
        positions.Add(headUp.x.ToString());
        positions.Add(headUp.y.ToString());
        positions.Add(headUp.z.ToString());
        positions.Add(headMoment.x.ToString());
        positions.Add(headMoment.y.ToString());
        positions.Add(headMoment.z.ToString());

        Vector3 leftHandPos = rig.leftHand.position;
        Vector3 leftHandForward = rig.leftHand.forward;
        Vector3 leftHandUp = rig.leftHand.up;
        Vector3 leftHandMoment = rig.leftHand.rotation.ToMomentVector();
        positions.Add(leftHandPos.x.ToString());
        positions.Add(leftHandPos.y.ToString());
        positions.Add(leftHandPos.z.ToString());
        positions.Add(leftHandForward.x.ToString());
        positions.Add(leftHandForward.y.ToString());
        positions.Add(leftHandForward.z.ToString());
        positions.Add(leftHandUp.x.ToString());
        positions.Add(leftHandUp.y.ToString());
        positions.Add(leftHandUp.z.ToString());
        positions.Add(leftHandMoment.x.ToString());
        positions.Add(leftHandMoment.y.ToString());
        positions.Add(leftHandMoment.z.ToString());

        Vector3 rightHandPos = rig.rightHand.position;
        Vector3 rightHandForward = rig.rightHand.forward;
        Vector3 rightHandUp = rig.rightHand.up;
        Vector3 rightHandMoment = rig.rightHand.rotation.ToMomentVector();
        positions.Add(rightHandPos.x.ToString());
        positions.Add(rightHandPos.y.ToString());
        positions.Add(rightHandPos.z.ToString());
        positions.Add(rightHandForward.x.ToString());
        positions.Add(rightHandForward.y.ToString());
        positions.Add(rightHandForward.z.ToString());
        positions.Add(rightHandUp.x.ToString());
        positions.Add(rightHandUp.y.ToString());
        positions.Add(rightHandUp.z.ToString());
        positions.Add(rightHandMoment.x.ToString());
        positions.Add(rightHandMoment.y.ToString());
        positions.Add(rightHandMoment.z.ToString());
        Debug.Log(positions);

        
        AppendDataFile(positions);
        
        
    }
}

public static class PlayerLoggerExtensionMethods
{
    public static Vector3 ToMomentVector(this Quaternion value)
    {
        value.ToAngleAxis(out float angle, out Vector3 axis);
        return axis * angle;
    }
}