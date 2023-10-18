using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot
{
	public CraftData item;
	public int idx;
}
public class Craft : MonoBehaviour
{
	public CraftSlotUI[] uiSlot;
	private CraftSlot[] slots;
	public GameObject CraftWindow;
	public MaterialSlotUI[] mUislot;

	[Header("Selected CraftItem")]
	private CraftSlot selectedCraftItem;
	private int selectedCraftItemIndex;
	public TextMeshProUGUI CraftItemName;
	public TextMeshProUGUI CraftItemDescription;
	public GameObject craftButton;
	private CraftManager data;
	private void Start()
	{
		GameManager.Instance.craft = this;
		data = GameManager.Instance.craftManager;
		slots = new CraftSlot[uiSlot.Length];

		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new CraftSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();
		}
		ClearCraftUI();
		CraftItemList();
	}

	public void AddItem(CraftData recipe)
	{
		CraftSlot emptySlot = GetEmptySlot();
		if (emptySlot != null)
		{
			emptySlot.item = recipe;
			emptySlot.idx = 1;
			UpdateCraftUI();
			return;
		}
	}

	public void SelectCraftItem(int index)
	{
		if (slots[index].item == null)
		{
			return;
		}
		ClearCraftUI();
		selectedCraftItem = slots[index];
		selectedCraftItemIndex = index;
		for (int i = 0; i < selectedCraftItem.item.resources.Length; i++)
		{
			mUislot[i].mSlot.SetActive(true);
			mUislot[i].mIcon.sprite = selectedCraftItem.item.resources[i].icon;
			mUislot[i].mQuantitiy.text = selectedCraftItem.item.resourceCount[i].ToString();
		}
		CraftItemName.text = selectedCraftItem.item.Result.ItemName;
		CraftItemDescription.text = selectedCraftItem.item.Result.ItemDescription;

		craftButton.SetActive(true);
	}

	private CraftSlot GetEmptySlot()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				return slots[i];
			}
		}
		return null;
	}

	void UpdateCraftUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item != null)
			{
				uiSlot[i].Set(slots[i]);
			}
			else
			{
				uiSlot[i].Clear();
			}
		}
	}

	private void ClearCraftUI()
	{
		CraftItemName.text = string.Empty;
		CraftItemDescription.text = string.Empty;
		craftButton.SetActive(false);
		for (int i = 0; i < mUislot.Length; i++)
		{
			mUislot[i].mSlot.SetActive(false);
		}
	}

	private void CraftItemList()
	{
		for (int i = 0; i < data.craftItem.Length; i++)
		{
			AddItem(data.craftItem[i]);
		}
	}

	public void OnCraftButton()
	{
		Inventory inventoryData = GameManager.Instance.inventory;
		if (MakableChk())
		{
			return;
		}
		else
		{
			for (int i = 0; i < selectedCraftItem.item.resources.Length; i++)
			{
				for (int j = 0; j < inventoryData.slots.Length; j++)
				{
					if (selectedCraftItem.item.resources[i] == inventoryData.slots[j].item)
					{
						inventoryData.slots[j].quantity -= selectedCraftItem.item.resourceCount[i];
						if (inventoryData.slots[j].quantity <= 0)
						{
							inventoryData.uiSlot[j].CraftSet(inventoryData.slots[j]);
							inventoryData.slots[j].item = null;
							inventoryData.ClearSelectedItemWindow();
							inventoryData.UpdateUI();
							break;
						}
						inventoryData.uiSlot[j].CraftSet(inventoryData.slots[j]);
						break;
					}

				}
			}
			inventoryData.AddItem(selectedCraftItem.item.Result);
		}
	}

	public bool MakableChk()
	{		
		Inventory inventoryData = GameManager.Instance.inventory;
		bool chk = false;
		for (int i = 0; i < selectedCraftItem.item.resources.Length; i++)
		{
			chk = false;
			for (int j = 0; j < inventoryData.slots.Length; j++)
			{
				if (selectedCraftItem.item.resources[i] == inventoryData.slots[j].item)
				{
					if (selectedCraftItem.item.resourceCount[i] <= inventoryData.slots[j].quantity)
					{
						chk = true;
						break;					
					}
					else
					{
						return true;
					}
				}			
			}
		}
		if (!chk) return true;
		else return false;
	}
}
