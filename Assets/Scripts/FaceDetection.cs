using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class FaceDetection : MonoBehaviour
{
    WebCamTexture webCamTexture;
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webCamTexture = new WebCamTexture(devices[0].name);
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(webCamTexture);
        
        findNewFace(frame);
    }

    void findNewFace( Mat frame)
    {
        CascadeClassifier cascadeClassifier = new CascadeClassifier(Application.dataPath + "/haarcascade_frontalcatface.xml");
        // Load the pre-trained face detection model

        Mat gray = new Mat();
        Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
        // Convert the frame to grayscale, as the face detection algorithm works on grayscale images

        OpenCvSharp.Rect[] faces = cascadeClassifier.DetectMultiScale(gray, scaleFactor: 1.1, minNeighbors: 3);
        // Detect faces in the current frame using the pre-trained model

        foreach (var face in faces)
        {
            // Do something with the detected
            // 
            Debug.Log(face);
            Debug.Log($"Face {face}");
        }
    }


}
