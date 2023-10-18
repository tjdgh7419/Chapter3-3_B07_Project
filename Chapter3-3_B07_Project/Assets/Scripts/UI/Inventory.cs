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
	public TextMeshProUGUI selectedItemTypeTitle;
	public TextMeshProUGUI selectedItemTypeDescription;
	public TextMeshProUGUI selectedItemStatTitle;
	public TextMeshProUGUI selectedItemStatType;
	public TextMeshProUGUI selectedItemStatDescription;
	public GameObject equipButton;
	public GameObject useButton;
	public GameObject dropButton;
	public GameObject unEquipButton;

	private int curEquipIndex;
	public Dictionary<ItemData, int> ItemTotalCount;

	private void Awake()
	{
		ItemTotalCount = new Dictionary<ItemData, int>();
	}

	private void Start()
	{
		GameManager.Instance.inventory = this;
		inventoryWindow.SetActive(false);
		slots = new ItemSlot[uiSlot.Length];

		for (int i = 0; i < slots.Length; i++)
		{
			slots[i] = new ItemSlot();
			uiSlot[i].index = i;
			uiSlot[i].Clear();
		}
		ClearSelectedItemWindow();

		AddItem(GameManager.Instance.itemManager.crystal); // 테스트
		AddItem(GameManager.Instance.itemManager.Gold);
		AddItem(GameManager.Instance.itemManager.HpPotion);
		AddItem(GameManager.Instance.itemManager.Sword);
	}

	public void Toggle()
	{
		if (inventoryWindow.activeInHierarchy)
		{
			Cursor.lockState = CursorLockMode.None;
			inventoryWindow.SetActive(false);
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			inventoryWindow.SetActive(true);
		}
	}

	public void OnInventoryButton(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			Toggle();
		}
	}

	public void AddItem(ItemData item)
	{
		if (item.canStack)
		{
			ItemSlot slotInfo = GetItemStack(item);
			if (slotInfo != null)
			{				
				slotInfo.quantity++;
				UpdateUI();
				return;
			}
		}

		ItemSlot emptySlotInfo = GetEmptySlot();

		if (emptySlotInfo != null)
		{
		
			emptySlotInfo.item = item;
			if (emptySlotInfo.item.type == ItemType.Equipable) //테스트
			{
				emptySlotInfo.quantity = 1;
			}
			else
			{
				emptySlotInfo.quantity = 100;
			}
			
			ItemTotalCount.Add(emptySlotInfo.item, emptySlotInfo.quantity);
						
			UpdateUI();
			return;
		}
		return;
	}

	public void UpdateUI()
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

	private ItemSlot GetItemStack(ItemData item)
	{
		for (int i = 0; i < slots.Length; i++)
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
		for (int i = 0; i < slots.Length; i++)
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

		switch (selectedItem.item.type)
		{
			case ItemType.Consumable:
				{
					selectedItemTypeDescription.text = "소모품";
					if (selectedItem.item.Ctype == ConsumableType.HP)
					{
						selectedItemStatType.text = selectedItem.item.Ctype.ToString() + " " + "+";
						selectedItemStatDescription.text = selectedItem.item.consumableDatas[0].value.ToString();
					}
					else if (selectedItem.item.Ctype == ConsumableType.MP)
					{
						selectedItemStatType.text = selectedItem.item.Ctype.ToString() + " " + "+";
						selectedItemStatDescription.text = selectedItem.item.consumableDatas[0].value.ToString();

					}
					else
					{
						selectedItemStatType.text = selectedItem.item.Ctype.ToString() + " " + "+";
						selectedItemStatDescription.text = selectedItem.item.consumableDatas[0].value.ToString();
					}

					break;
				}
			case ItemType.Equipable:
				{
					selectedItemTypeDescription.text = "장비";
					selectedItemStatType.text = "공격력" + " " + "+";
					selectedItemStatDescription.text = selectedItem.item.AttackPower.ToString();
					break;
				}
		}

		selectedItemName.text = selectedItem.item.ItemName;
		selectedItemDescription.text = selectedItem.item.ItemDescription;
		selectedItemStatTitle.text = "아이템 효과";
		selectedItemTypeTitle.text = "아이템 타입";
		selectedItemStatTitle.gameObject.SetActive(selectedItem.item.type == ItemType.Consumable || selectedItem.item.type == ItemType.Equipable);
		selectedItemTypeTitle.gameObject.SetActive(selectedItem.item.type == ItemType.Consumable || selectedItem.item.type == ItemType.Equipable);
		useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
		equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlot[index].equipped);
		unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlot[index].equipped);
		dropButton.SetActive(true);
	}

	public void OnUseButton()
	{
		if (selectedItem.item.type == ItemType.Consumable)
		{
			for (int i = 0; i < selectedItem.item.consumableDatas.Length; i++)
			{
				switch (selectedItem.item.consumableDatas[i].type)
				{
					case ConsumableType.HP:
						break; // 플레이어 컨디션 매니저가 생겼을 때
					case ConsumableType.MP:
						break;
				}
			}
		}
		RemoveSelectedItem();
	}

	public void OnDropButton()
	{
		RemoveSelectedItem();
	}

	public void OnEquipButton()
	{
		if (uiSlot[curEquipIndex].equipped)
		{
			UnEquip(curEquipIndex);
		}

		uiSlot[selectedItemIndex].equipped = true;

		GameManager.Instance.equipManager.EquipNew(selectedItem.item);
		UpdateUI();
		SelectItem(selectedItemIndex);
	}

	public void UnEquipButton()
	{
		UnEquip(selectedItemIndex);
	}

	public void RemoveSelectedItem()
	{
		selectedItem.quantity--;

		if (selectedItem.quantity <= 0)
		{
			if (uiSlot[selectedItemIndex].equipped)
			{
				UnEquip(selectedItemIndex);
			}
			selectedItem.item = null;
			ClearSelectedItemWindow();
		}
		UpdateUI();
	}

	private void UnEquip(int index)
	{
		uiSlot[index].equipped = false;
		GameManager.Instance.equipManager.UnEquip();
		UpdateUI();

		if (selectedItemIndex == index)
		{
			SelectItem(index);
		}
	}

	public void ClearSelectedItemWindow()
	{
		selectedItem = null;
		selectedItemName.text = string.Empty;
		selectedItemDescription.text = string.Empty;
		selectedItemStatDescription.text = string.Empty;
		selectedItemStatTitle.text = string.Empty;
		selectedItemTypeDescription.text = string.Empty;
		selectedItemTypeTitle.text = string.Empty;
		selectedItemStatType.text = string.Empty;
		equipButton.SetActive(false);
		dropButton.SetActive(false);
		unEquipButton.SetActive(false);
		useButton.SetActive(false);
	}
}
