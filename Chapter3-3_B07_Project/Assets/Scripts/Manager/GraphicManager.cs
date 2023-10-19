using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphicManager : MonoBehaviour
{
    public static GraphicManager Instance;

    [SerializeField] private GameObject hitEffatPrefab;
    [SerializeField] private GameObject electroPrefab;
    [SerializeField] private List<GameObject> prefabs;

    [Header("Shadow")]
    public static bool Effact = true;
    public static bool Shadow = true;

    [SerializeField] private Light mainLight;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (Effact)
        {
            prefabs.Add(Instantiate(hitEffatPrefab));
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(electroPrefab);
                prefabs.Add(go);
            }
        }
        if (!Shadow && mainLight != null)
        {
            mainLight.shadows = LightShadows.None;
        }
    }

    public void MonsterHit(Vector3 pos)
    {
        prefabs[0].transform.position = pos;
        prefabs[0].GetComponent<ParticleSystem>().Play();
    }

    public void SetGraphicSetting(bool _effact, bool _shadow)
    {
        Effact = _effact;
        Shadow = _shadow;
    }

    //0�� ����Ʈ 1�� ������
    public bool[] GetGraphicSetting()
    {
        return new bool[] { Effact, Shadow };
    }
}