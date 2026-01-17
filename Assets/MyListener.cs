using System.Collections;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    public int clicksUntilOpen = 10;
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.1f;

    public Transform cameraTransform;
    public float cameraStepForward = 0.1f;
    public GameObject prize;
    public GameObject box;

    private int clickCount = 0;
    private Vector3 originalPosition;
    private bool isOpen = false;

    void Start()
    {
        prize.gameObject.SetActive(false);
        originalPosition = box.transform.position;
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);

        if (msg == "1" && !isOpen)
        {
            clickCount++;
            Debug.Log("Click count: " + clickCount);

            if (clickCount < clicksUntilOpen)
            {
                StartCoroutine(Shake());
                MoveCameraCloser();
            }
            else
            {
                OpenBox();
            }
        }
    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    IEnumerator Shake()
    {
        Debug.Log("SHAKE");
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offset = Random.Range(-shakeAmount, shakeAmount);
            box.transform.position = originalPosition + new Vector3(offset, 0f, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        box.transform.position = originalPosition;
    }

    void MoveCameraCloser()
    {
        if (cameraTransform != null)
        {
            cameraTransform.position += cameraTransform.forward * cameraStepForward;
        }
    }

    void OpenBox()
    {
        isOpen = true;


        Destroy(box.gameObject);
        //do something here
        prize.gameObject.SetActive(true);
        Debug.Log("Box opened!");
    }
}