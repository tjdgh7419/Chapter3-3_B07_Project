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

	public Inventory inventory;
	public Craft craft;
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
	}

}