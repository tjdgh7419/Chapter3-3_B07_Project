using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    public static GraphicManager Instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private List<GameObject> prefabs;

    [Header("Shadow")]
    public static bool Effact = true;
    public static bool Shadow = true;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

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

    public void SetGraphicSetting(bool _effact, bool _shadow)
    {
        Effact = _effact;
        Shadow = _shadow;
    }

    //0¹ø ÀÌÆåÆ® 1¹ø ½¦µµ¿ì
    public bool[] GetGraphicSetting()
    {
        return new bool[] { Effact, Shadow };
    }
}
