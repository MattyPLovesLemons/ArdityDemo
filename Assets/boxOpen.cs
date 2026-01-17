using UnityEngine;
using System.Collections;


public class boxOpen : MonoBehaviour
{

    public int clicksUntilOpen = 10;
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.1f;

    public Transform cameraTransform;
    public float cameraStepForward = 0.1f;

    private int clickCount = 0;
    private Vector3 originalPosition;
    private bool isOpen = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            clickCount++;

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

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offset = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPosition + new Vector3(offset, 0f, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
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


        Destroy(gameObject, 1f);

        Debug.Log("Box opened!");
    }
}
