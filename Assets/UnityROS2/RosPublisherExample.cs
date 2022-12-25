using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.UnityRoboticsDemo;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

/// <summary>
///
/// </summary>
public class RosPublisherExample : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "pos_rot";

    // The game object
    public GameObject cube;
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.1f;
    // rotation degrees per second
    float degreesPerSecond = 25;
    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;
    float metersPerSecond = 2.5f;

    Vector3 currentPosition;
    Vector3 rotationVector;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PosRotMsg>(topicName);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            // cube.transform.rotation = Random.rotation;
            //https://gamedevbeginner.com/how-to-rotate-in-unity-complete-beginners-guide/

            //move cube
            //cube.transform.Translate( metersPerSecond * Time.deltaTime, 0, 0 );

            //Rotate the cube by converting the angles into a quaternion.
            //cube.transform.Rotate(degreesPerSecond * Time.deltaTime, 0 , 0);
            
            // rotationVector = new Vector3(0, 0, 1); 
            // cube.transform.RotateAround(cube.transform.parent.position, 
            // rotationVector, degreesPerSecond * Time.deltaTime);

            //convert Unity coordinate frame to ROS coordinate frame
            Vector3<FLU> rosPos = cube.transform.position.To<FLU>();
        	Quaternion<FLU> myorientation = cube.transform.rotation.To<FLU>();            

            PosRotMsg cubePos = new PosRotMsg(
                rosPos.x,
                rosPos.y,
                rosPos.z,
                myorientation.x,
                myorientation.y,
                myorientation.z,
                myorientation.w
            );

            // PosRotMsg cubePos = new PosRotMsg(
            //     cube.transform.position.x,
            //     cube.transform.position.y,
            //     cube.transform.position.z,
            //     cube.transform.rotation.x,
            //     cube.transform.rotation.y,
            //     cube.transform.rotation.z,
            //     cube.transform.rotation.w
            // );
 

            // Finally send the message to server_endpoint.py running in ROS
            ros.Publish(topicName, cubePos);

            timeElapsed = 0;
        }
    }
}
