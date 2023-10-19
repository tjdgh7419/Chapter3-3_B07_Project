using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarphicManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> prefabs;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(prefab);
            prefabs.Add(go);
            go.SetActive(false);
        }
    }

    public void MonsterHit(Vector3 pos)
    {
        prefabs[0].transform.position = pos;
        prefab.SetActive(true);
        prefabs[0].GetComponent<ParticleSystem>().Play();
    }
}
