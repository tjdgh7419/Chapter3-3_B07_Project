using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;

    private Action effactStart;

    public float timer;

    public void UseSkill(Vector3 dir)
    {
        gameObject.SetActive(true);
        effactStart?.Invoke();
        gameObject.GetComponent<Rigidbody>().AddForce(dir*1000);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if(timer > 5)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    public void EffactSettingHow(bool effact)
    {
        if (effact)
        {
            GameObject go = Instantiate(prefabs, this.transform);
            effactStart = () => go.GetComponent<ParticleSystem>().Play();
        }
        gameObject.SetActive(false);
    }
}
