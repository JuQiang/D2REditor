using D2SLib.Model.Save;
using System;
using System.Drawing;

namespace D2REditor
{
    public class ItemSelectedEventArgs : EventArgs
    {
        private Item item;
        private Point point;
        private ItemSelectedEventArgs() { }
        public ItemSelectedEventArgs(Item item,Point point)
        {
            this.item = item;
            this.point = point;
        }
        public Item Item
        {
            get
            {
                return this.item;
            }
        }

        public Point Point { get { return this.point; } }
    }
}
