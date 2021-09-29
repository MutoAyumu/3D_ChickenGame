using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 1f;
    [SerializeField] string m_collisionTag = " ";
    [SerializeField] string m_playerTag = "Player";
    [SerializeField] AudioSource m_klaxon = default;

    Rigidbody m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        m_rb.velocity = new Vector3(transform.localScale.x * m_moveSpeed, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == m_collisionTag)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == m_playerTag)
        {
            m_klaxon.Play();
        }
    }
}
