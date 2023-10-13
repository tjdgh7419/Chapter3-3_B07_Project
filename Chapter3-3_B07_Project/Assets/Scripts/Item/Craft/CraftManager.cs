using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public ItemData swordCraft;

	private void Start()
	{
		GameManager.Instance.craftManager = this;
	}
}
