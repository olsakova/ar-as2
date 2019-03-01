using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.3f;

    Transform player;
    Transform target1;
    Animation anim;
    public bool firstTarget = false;
    public bool secondTarget = false;
    public bool spinning = false;

    // Time when the movement started.
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        anim = GetComponent<Animation>();
        Input.gyro.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Entered trigger");
        if (!firstTarget)
        {
            TaskOnClick();
        }
        else if (secondTarget)
        {
            spinning = true;
        }
    }

    void onActivityResult(string recognizedText)
    {
        Debug.Log("Listening to speech");
        char[] delimiterChars = { '~' };
        string[] result = recognizedText.Split(delimiterChars);

        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
        if (result[0] == "continue")
        {
            Debug.Log("Got continue back");
            firstTarget = true;
        }
        else
        {
            Debug.Log("got something else back");
        }
    }

    void TaskOnClick()
    {
        AndroidJavaClass pluginClass = new AndroidJavaClass("com.plugin.speech.pluginlibrary.TestPlugin");
        Debug.Log("Call 1 Started");

        // Pass the name of the game object which has the onActivityResult(string recognizedText) attached to it.
        // The speech recognizer intent will return the string result to onActivityResult method of "Main Camera"
        pluginClass.CallStatic("setReturnObject", "MushroomMon");
        Debug.Log("Return Object Set");


        // Setting language is optional. If you don't run this line, it will try to figure out language based on device settings
        pluginClass.CallStatic("setLanguage", "en_US");
        Debug.Log("Language Set");


        // The following line sets the maximum results you want for recognition
        pluginClass.CallStatic("setMaxResults", 3);
        Debug.Log("Max Results Set");

        // The following line sets the question which appears on intent over the microphone icon
        pluginClass.CallStatic("changeQuestion", "Hello, How can I help you???");
        Debug.Log("Question Set");


        Debug.Log("Call 2 Started");

        // Calls the function from the jar file
        pluginClass.CallStatic("promptSpeechInput");

        Debug.Log("Call End");
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        float step = speed * Time.deltaTime; // calculate distance to move
    
        if (!firstTarget)
        {
            // ------- Accelerometer ------------
            Vector3 dir = Vector3.zero;

            // we assume that device is held parallel to the ground
            // and Home button is in the right hand

            // remap device acceleration axis to game coordinates:
            //  1) XY plane of the device is mapped onto XZ plane
            //  2) rotated 90 degrees around Y axis
            dir.x = -Input.acceleration.y;
            dir.z = Input.acceleration.x;

            // clamp acceleration vector to unit sphere
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            // Move object
            transform.position = Vector3.Lerp(transform.position, transform.position + dir, Time.deltaTime * speed);
        }
        else if (!secondTarget)
        {
            target1 = GameObject.FindGameObjectWithTag("target1").transform;

            Vector3 targetDir1 = target1.position - player.position;
            Vector3 newDir1 = Vector3.RotateTowards(player.forward, targetDir1, step, 0.0f);
            player.rotation = Quaternion.LookRotation(newDir1);
            player.position = Vector3.MoveTowards(player.position, target1.position, step);
            if (Vector3.Distance(player.position, target1.position) < 0.01f)
            {
                secondTarget = true;
                spinning = true;
            }
        }
        else if (spinning){
            anim.Play("Spin");
        }

    }
}
