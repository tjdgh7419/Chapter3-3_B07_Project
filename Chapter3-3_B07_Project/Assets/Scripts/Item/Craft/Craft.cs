using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Craft : MonoBehaviour
{
	public CraftSlotUI[] uiSlot;
	public CraftData[] slots;
	public GameObject CraftWindow;

	[Header("Selected CraftItem")]
	private CraftData selectedCraftItem;
	private int selectedCraftItemIndex;
	public Image icon;
	public TextMeshProUGUI matarialQuantity;
	public GameObject craftButton;

	private void Start()
	{
		GameManager.Instance.craft = this;
		CraftWindow.SetActive(false);
		slots = new CraftData[uiSlot.Length];

		for(int i = 0; i < slots.Length; i++)
		{
			slots[i] = new CraftData();
			uiSlot[i].index = i;
		}
	}
	
	public void AddItem(CraftData recipe)
	{
		CraftData emptySlot = GetEmptySlot();
		if(emptySlot != null)
		{
			emptySlot = recipe;
			UpdateCraftUI();
			return;
		}
	}

	CraftData GetEmptySlot()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i] == null)
			{
				return slots[i];
			}
		}
		return null;
	}

	void UpdateCraftUI()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i] != null)
			{
				uiSlot[i].Set(slots[i]);
			}
			else
			{
				return;
			}
		}
	}
}
