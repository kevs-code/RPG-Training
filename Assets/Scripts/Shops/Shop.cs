using GameDevTV.Inventories;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    public class Shop : MonoBehaviour, IRaycastable
    {
        [SerializeField] string shopName;
        [SerializeField]
        StockItemConfig[] stockConfig;

        [System.Serializable]
        class StockItemConfig
        {
            public InventoryItem item;
            public int initialStock;
            [Range(0, 100)] //could increase price with minus value
            public float buyingDiscountPercentage;
        }

        public event Action onChange;

        public IEnumerable<ShopItem> GetFilteredItems()
        {
            foreach (StockItemConfig config in stockConfig)
            {
                float price = config.item.GetPrice() * (1 - config.buyingDiscountPercentage / 100);
                yield return new ShopItem(config.item, config.initialStock, price, 0);
            }
        }
        public void SelectFilter(ItemCategory category) {}
        public ItemCategory GetFilter() { return ItemCategory.None;  }
        public void SelectMode(bool isBuying) {}
        public bool IsBuyingMode() { return true; }
        public bool CanTransact() { return true; }
        public void ConfirmTransaction() {}
        public float TransactionTotal() { return 0; }

        public string GetShopName()
        {
            return shopName;
        }

        public void AddToTransaction(InventoryItem item, int quantity)
        {
            print($"Added To Transaction: {item.GetDisplayName()} x {quantity}");
        }

        public CursorType GetCursorType()
        {
            return CursorType.Shop;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Shopper>().SetActiveShop(this);
            }
            return true;
        }
    }
}