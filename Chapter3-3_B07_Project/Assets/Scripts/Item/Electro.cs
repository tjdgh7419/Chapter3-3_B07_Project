using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;

    private Action effactStart;

    public float timer;

    public void Start()
    {
        UseSkill();
    }

    public void UseSkill()
    {
        effactStart?.Invoke();
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
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(10,0,0));
        if (effact)
        {
            GameObject go = Instantiate(prefabs, this.transform);
            effactStart = () => go.GetComponent<ParticleSystem>().Play();
        }
    }
}
