using D2SLib.Model.Save;
using System;

namespace D2REditor
{
    public class ItemSelectedEventArgs : EventArgs
    {
        private Item item;
        private ItemSelectedEventArgs() { }
        public ItemSelectedEventArgs(Item item)
        {
            this.item = item;
        }
        public Item Item
        {
            get
            {
                return this.item;
            }
        }
    }
}
