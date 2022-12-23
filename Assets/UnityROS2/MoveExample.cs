using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;

/// <summary>
///
/// </summary>
public class MoveExample : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "new_position";

    // The game object
    public GameObject cube;
    // // Publish the cube's position and rotation every N seconds
    // public float publishMessageFrequency = 0.1f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<PosRotMsg>(topicName, MoveCube);

    }

    void MoveCube(PosRotMsg msg)
    {
        cube.transform.position = new Vector3(msg.pos_x, msg.pos_y, msg.pos_z);
        cube.transform.rotation = new Quaternion(msg.rot_x, msg.rot_y, msg.rot_z, msg.rot_w);
    }

}
