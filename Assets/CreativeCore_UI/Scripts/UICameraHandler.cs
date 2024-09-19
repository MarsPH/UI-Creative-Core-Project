using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraHandler : MonoBehaviour
{

    [SerializeField] Vector3 m_MainVector3;

    [SerializeField] Vector3 m_SettingsVector3;

    [SerializeField] float m_transitionTime = 1.0f;

    [SerializeField] bool SettingsButtonPushed = false;

    private Vector3 m_PositionToMoveTo;
    private Quaternion m_RotationToRotateTo;



    private Quaternion m_RotateStartPos;
    [SerializeField] Quaternion m_RotateEndPos;

    private void Start()
    {
        m_MainVector3 = transform.position;
        m_RotateStartPos = transform.rotation;
    }

    public void MoveCameraToMain()
    {
        gameObject.transform.position = Vector3.Lerp(m_SettingsVector3, m_MainVector3, m_transitionTime);
        gameObject.transform.rotation = Quaternion.Lerp(m_RotateEndPos, m_RotateStartPos, m_transitionTime);
    }

    public void MoveCameraToSettings()
    {
        m_PositionToMoveTo = m_SettingsVector3;
        m_RotateEndPos = m_RotationToRotateTo;
        StartCoroutine(LerpPosition(m_PositionToMoveTo, m_RotationToRotateTo, m_transitionTime));
    }
    public void MakeBoolTrue()
    {
        SettingsButtonPushed = true;
    }
    private void Update()
    {
        if (SettingsButtonPushed)
        {
            MoveCameraToSettings();
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        //captures the current time
        float time = 0;
        //it will assign the current position
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        //it will be true until the time is less than the duration
        // it means it will finish if current time is already the amount of 
        // the setted duration
        while (time < duration)
        {
            //it will lerp between the start to end based on the time
            // if time is 0.01, the lerp will go till 0.01
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);

            //time passes by like this
            time += Time.deltaTime;
            yield return null;
        }
        // transform position afte the duration will be set to the target and voila
        transform.position = targetPosition;
    }
}
