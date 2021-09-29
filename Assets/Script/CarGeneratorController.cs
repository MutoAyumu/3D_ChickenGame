using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGeneratorController : MonoBehaviour
{
    [SerializeField] CarController[] m_carPrefab = default;
    [SerializeField] float m_max = 7f;
    [SerializeField] float m_min = 4f;
    [SerializeField, Range(90f, -90f)] float m_ratate;

    float m_instanceLimit;
    float m_timer = 0;

    private void Update()
    {
        if (m_timer == 0)
            m_instanceLimit = Random.Range(m_max, m_min);

        m_timer += Time.deltaTime;

        if (m_timer > m_instanceLimit)
        {
            var num = Random.Range(0, m_carPrefab.Length);
            var car = Instantiate(m_carPrefab[num], this.transform.position, Quaternion.identity);
            car.transform.Rotate(new Vector3(0, m_ratate, 0));
            car.transform.parent = this.transform;

            if(m_ratate >= 0)
            {
                
            }
            else
            {
                car.transform.localScale = (new Vector3(-1, 1, 1));
            }
            m_timer = 0;
        }
    }
}
