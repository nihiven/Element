using Element.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Element.Classes
{
    interface IInventory
    {
        int InventorySelected { get; }
        int InventoryMaxItems { get; }
        bool ShowInventory { get; set; }
        double InventoryTimeout { get; set; }
    }

    class Inventory : IInventory
    {
        private List<IItem> _inventory { get; }
        public int InventorySelected { get; }
        public int InventoryMaxItems { get; }
        public bool ShowInventory { get; set; }
        public double InventoryTimeout { get; set; }

        public Inventory()
        {
            this._inventory = new List<IItem>(32);
            this.ShowInventory = false;
            this.InventoryTimeout = 5;
            this.InventorySelected = 0;
            this.InventoryMaxItems = 5;
        }
    }
}
