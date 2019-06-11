using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Facilitates screen shake,
/// is attached to the main camera
/// </summary>
public class CameraShake : MonoBehaviour {
    #region Components
    public static CameraShake Instance;
    #endregion

    #region Attributes
    private const float DURATION = .075f;
    private const float MAGNITUDE = .045f;
    #endregion

    #region States
    private bool shaking;
    #endregion

    public void Awake()
    {
        Instance = this;
    }

    public void Shake()
    {
        if (shaking)
            return;

        StartCoroutine(shake(DURATION, MAGNITUDE));
    }

    //AN overriding function for shake
    public void ShakeByFactor(float factor)
    {
        StopAllCoroutines();
        StartCoroutine(shake(DURATION, MAGNITUDE * factor));
    }

    public bool Shaking
    {
        get
        {
            return shaking;
        }
    }

    IEnumerator shake(float duration, float magnitude)
    {
        shaking = true;

        Vector3 originalPosition = transform.position;
        float elapsed = 0;
        while(elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }


        transform.position = originalPosition;
        shaking = false;
    }
}
