using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayeMoveController : MonoBehaviour
{
    Rigidbody m_rb;
    Animator m_anim;
    AudioSource m_audio;

    Vector3 m_movePos;

    float m_v;

    bool isJump = true;
    bool isOn;

    [SerializeField] float m_movePower = 5f;
    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] string m_ground = "Ground";
    [SerializeField] StageManager m_stageManager = default;
    [SerializeField] string m_stageTag = "StageGenerator";
    [SerializeField] Slider m_jumpSlider = default;
    [SerializeField] float m_jumpInterval = 5f;
    [SerializeField] Image m_fillImage = default;
    [SerializeField] Color m_maxJumpColor = default;
    Color m_standardColor;
    [SerializeField] UnityEvent m_end;
    [SerializeField] string m_endTag = "Car";

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
        m_jumpSlider.value = 1;
        m_standardColor = m_fillImage.color;
    }
    private void Update()
    {
        InputMove();
        UpdateMove();

        if(m_jumpSlider.value != 1)
        {
            m_jumpSlider.value += 1 / m_jumpInterval * Time.deltaTime;
        }
        else
        {
            m_fillImage.color = m_maxJumpColor;
        }
    }
    void InputMove()
    {
        m_v = Input.GetAxisRaw("Fire1");

        m_movePos = new Vector3(0, 0, m_v).normalized;

        if (Input.GetButtonDown("Jump") && isJump)
        {
            UpdateJump();
        }
    }
    void UpdateMove()
    {
        if (m_movePos.sqrMagnitude > 0 && isJump)
        {
            m_rb.velocity = transform.forward * m_movePower;
            m_anim.SetBool("Walk", true);
        }
        else
        {
            m_anim.SetBool("Walk", false);
        }
    }
    public void UpdateJump()
    {
        if (!isOn && m_jumpSlider.value == 1)
        {
            m_rb.AddForce(new Vector3(0, m_jumpPower, 0), ForceMode.VelocityChange);
            isJump = false;
            isOn = true;
            m_anim.SetBool("Run", true);
            m_audio.Play();
            m_jumpSlider.value = 0;
            m_fillImage.color = m_standardColor;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == m_ground)
        {
            isJump = true;
            isOn = false;
            m_anim.SetBool("Run", false);
        }
        if(other.gameObject.tag == m_stageTag)
        {
            m_stageManager.CreateStage();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == m_endTag)
        {
            m_end.Invoke();
        }
    }
}
