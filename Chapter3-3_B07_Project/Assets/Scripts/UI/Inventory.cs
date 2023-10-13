using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}
public class Inventory : MonoBehaviour
{
	public ItemSlotUI[] uiSlot;
	public ItemSlot[] slots;
	public GameObject inventoryWindow;

	[Header("Selected Item")]
	private ItemSlot selectedItem;
	private int selectedItemIndex;
	public TextMeshProUGUI selectedItemName;
	public TextMeshProUGUI selectedItemDescription;
	public GameObject equipButton;
	public GameObject useButton;
	public GameObject dropButton;
	public GameObject unEquipButton;

	private int curEquipIndex;
	private Dictionary<ItemData, int> ItemTotalCount;

	private void Awake()
	{
		ItemTotalCount = new Dictionary<ItemData, int>();
	}

	private void Start()
	{
		//GameManager.Instance.inventory = this; 게임매니저가 생겼을 때
		inventoryWindow.SetActive(false);
		slots = new ItemSlot[uiSlot.Length];

		for(int i = 0; i < slots.Length; i++)
		{
			slots[i] = new ItemSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();
		}
		ClearSelectedItemWindow();
	}

	public void Toggle()
	{
		if (inventoryWindow.activeInHierarchy)
		{
			inventoryWindow.SetActive(false);
		}
		else
		{
			inventoryWindow.SetActive(true);
		}
	}

	public void OnInventoryButton(InputAction.CallbackContext context)
	{
		if(context.phase == InputActionPhase.Started)
		{
			Toggle();
		}
	}

	public void AddItem(ItemData item)
	{
		if (item.canStack)
		{
			ItemSlot slotInfo = GetItemStack(item);
			if(slotInfo != null)
			{
				slotInfo.quantity++;
				UpdateUI();
				return;
			}
		}

		ItemSlot emptySlotInfo = GetEmptySlot();

		if(emptySlotInfo != null)
		{
			emptySlotInfo.item = item;
			emptySlotInfo.quantity = 1;
			UpdateUI();
			return;
		}
		return;
	}

	private void UpdateUI()
	{
		for(int i = 0; i < slots.Length; i++)
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

	private ItemSlot GetItemStack(ItemData item)
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
			{
				return slots[i];
			}
		}
		return null;
	}

	private ItemSlot GetEmptySlot()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == null)
			{
				return slots[i];
			}
		}
		return null;
	}

	public void SelectItem(int index)
	{
		if (slots[index].item == null)
		{
			return;
		}

		selectedItem = slots[index];
		selectedItemIndex = index;

		selectedItemName.text = selectedItem.item.ItemName;
		selectedItemDescription.text = selectedItem.item.ItemDescription;

		useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlot[index].equipped);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlot[index].equipped);
		dropButton.SetActive(selectedItem.item.type != ItemType.Equipable);
	}

	private void ClearSelectedItemWindow()
	{
		selectedItem = null;
		selectedItemName.text = string.Empty;
		selectedItemDescription.text = string.Empty;

		equipButton.SetActive(false);
		dropButton.SetActive(false);
		unEquipButton.SetActive(false);
		useButton.SetActive(false);
	}
}
