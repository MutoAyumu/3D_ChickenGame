using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text m_timerText = default;
    [SerializeField] float m_timeLimit = 60f;
    [SerializeField] Text m_movingDistanceText = default;
    float m_movingDistance;
    [SerializeField] PlayeMoveController m_playerPos = default;
    Vector3 m_startPos;
    [SerializeField] float m_countTime = 3.5f;
    [SerializeField] Text m_countTimerText = default;
    [SerializeField] UnityEvent m_startEvent = default;
    [SerializeField] UnityEvent m_endEvent = default;
    bool isOn;
    bool isEnd;

    private void Start()
    {
        m_startPos = m_playerPos.transform.position;
    }

    private void Update()
    {
        if(!isOn)
        Count();

        if (isOn && !isEnd)
        {
            m_timeLimit -= Time.deltaTime;
            m_timerText.text = m_timeLimit.ToString("F0") + "s";

            m_movingDistance = m_playerPos.transform.position.z - m_startPos.z;
            m_movingDistanceText.text = m_movingDistance.ToString("F0") + "m";
        }
        
        if(m_timeLimit <= 0 && !isEnd)
        {
            m_endEvent.Invoke();
            isEnd = true;
        }
    }
    public void Reload(string name)
    {
        SceneManager.LoadScene(name);
    }
    void Count()
    {
        if (m_countTime >= 0)
        {
            m_countTime -= Time.deltaTime;
            m_countTimerText.text = m_countTime.ToString("F0");
        }
        else
        {
            m_startEvent.Invoke();
            isOn = true;
        }
    }
}
