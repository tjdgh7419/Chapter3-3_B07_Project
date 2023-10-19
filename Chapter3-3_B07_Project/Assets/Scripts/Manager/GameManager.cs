using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	public ItemManager itemManager;
	public EquipManager equipManager;
	public CraftManager craftManager;
	public UIManager uiManager;
	public InteractionManager interactionManager;
	public RoundManager roundManager;
	public MonsterManager monsterManager;
	public Inventory inventory;
	public CraftPanel craft;
	public PlayerConditionManager playerConditionManager;
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	
	}

	private void Start()
	{
		uiManager.OpenUI<PlayerUI>();
		SoundManager.Instance.BackMusic.WaveOff();
	}

}
