using System;
using TechShopApp.Models;
using DatabaseConnection;

namespace TechShopApp.Managers
{
    public class InventoryManager
    {
        public void AddInventory(DatabaseConnector dbConnector, Inventory inventory)
        {
            Inventory.AddInventory(dbConnector, inventory);
        }

        public void UpdateInventory(DatabaseConnector dbConnector, Inventory inventory)
        {
            Inventory.UpdateInventory(dbConnector, inventory);
        }

        public Inventory? GetInventoryByProductId(DatabaseConnector dbConnector, int productId)
        {
            return Inventory.GetInventoryByProductId(dbConnector, productId);
        }
    }
}
