using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class APICaller : MonoBehaviour
{
    private string apiUrl = "https://localhost:7004/Predict";

    void Start()
    {
        // Create a HandGestureNumber object with the example data
        HandGestureNumber data = new HandGestureNumber
        {
            Wrist = new Vector3Data(0.4105914f, 1.026155f, 0.245f),
            ThumbMetacarpalJoint = new Vector3Data(0.4338585f, 0.9586738f, -0.00685771f),
            ThumbProximalInterphalangealJoint = new Vector3Data(0.4248339f, 0.9005846f, -0.01735616f),
            ThumbDistalInterphalangealJoint = new Vector3Data(0.4036435f, 0.8747644f, -0.02756958f),
            ThumbTip = new Vector3Data(0.3794498f, 0.8661107f, -0.03893784f),
            IndexFingerMetacarpalJoint = new Vector3Data(0.3990996f, 0.8483842f, -0.02935667f),
            IndexFingerProximalInterphalangealJoint = new Vector3Data(0.3894559f, 0.7602079f, -0.04186963f),
            IndexFingerDistalInterphalangealJoint = new Vector3Data(0.3833727f, 0.7002555f, -0.04957679f),
            IndexFingerTip = new Vector3Data(0.3777024f, 0.6548913f, -0.05584303f),
            MiddleFingerMetacarpalJoint = new Vector3Data(0.3680162f, 0.872964f, -0.03136414f),
            MiddleFingerProximalInterphalangealJoint = new Vector3Data(0.3666555f, 0.8446578f, -0.0397642f),
            MiddleFingerDistalInterphalangealJoint = new Vector3Data(0.3792623f, 0.8731467f, -0.03675493f),
            MiddleFingerTip = new Vector3Data(0.3885958f, 0.8969937f, -0.03595158f),
            RingFingerMetacarpalJoint = new Vector3Data(0.3467827f, 0.9154005f, -0.03271836f),
            RingFingerProximalInterphalangealJoint = new Vector3Data(0.3574339f, 0.9044529f, -0.03456492f),
            RingFingerDistalInterphalangealJoint = new Vector3Data(0.3691992f, 0.9259737f, -0.02417508f),
            RingFingerTip = new Vector3Data(0.3735565f, 0.9447212f, -0.02018719f),
            LittleFingerMetacarpalJoint = new Vector3Data(0.3298681f, 0.9600306f, -0.03528517f),
            LittleFingerProximalInterphalangealJoint = new Vector3Data(0.344176f, 0.9523773f, -0.03479016f),
            LittleFingerDistalInterphalangealJoint = new Vector3Data(0.3538409f, 0.9702536f, -0.02667142f),
            LittleFingerTip = new Vector3Data(0.3561259f, 0.9834204f, -0.02189351f),

        };

        // Call the API with the data
        CallAPI(data);
    }

    public void CallAPI(HandGestureNumber data)
    {
        StartCoroutine(PostRequest(data));
    }

    IEnumerator PostRequest(HandGestureNumber data)
    {
        string json = JsonConvert.SerializeObject(data); ;

        
        var request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
