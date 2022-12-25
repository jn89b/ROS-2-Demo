using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

/// <summary>
///
/// </summary>
public class MoveExample : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "uav_position";

    // The game object
    public GameObject cube;
    // // Publish the cube's position and rotation every N seconds
    // public float publishMessageFrequency = 0.1f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    Vector3 UnityPosition; //position of the cube in Unity coordinate frame
    Quaternion UnityRotation; //rotation of the cube in Unity coordinate frame
    Quaternion NewRotation;


    PointMsg rosPoint;
    QuaternionMsg rosRotation;


    //for unity coordinates
    // unity y axis is aircraft z axis
    // unity z axis is -aircraft x axis
    // unity x axis is -aircraft y axis
    

    //unity rotation
    // rotation about x axis is roll 
    // rotation about y axis is yaw
    // rotation about z axis is pitch


    // I need to flip the unity z axis and x axis 

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<PosRotMsg>(topicName, MoveCube);

    }

    void MoveCube(PosRotMsg msg)
    {
        //convert ROS coordinate frame to Unity coordinate frame
        rosPoint = new PointMsg(msg.pos_x, msg.pos_y, msg.pos_z);
        UnityPosition = rosPoint.From<FLU>();

        rosRotation = new QuaternionMsg(msg.rot_x, msg.rot_y, msg.rot_z, msg.rot_w);
        UnityRotation = rosRotation.From<FLU>();

        // rosRotation = (msg.rot_x, msg.rot_y, msg.rot_z, msg.rot_w);

        //log the unity 
        // Debug.Log("Unity Position: " + UnityPosition);

        cube.transform.position = UnityPosition;
        //cube.transform.Rotate(UnityRotation.eulerAngles);
        cube.transform.rotation = UnityRotation;

        //get euler angles of unity rotation
        Vector3 euler = UnityRotation.eulerAngles;
        // cube.transform.Rotate(euler.z, euler.y, euler.z);



    }

}
