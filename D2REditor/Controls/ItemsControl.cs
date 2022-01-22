using D2SLib;
using D2SLib.Model.Save;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace D2REditor.Controls
{
    public partial class ItemsControl : UserControl
    {
        private Bitmap stashbmp, stashbmp2, charbmp;
        private List<Item> body;
        private List<Item> store;
        private Dictionary<int, List<Item>> stashes = new Dictionary<int, List<Item>>();
        private int currentTab;
        private string[] labels = new String[] { Utils.AllJsons["personal"], Utils.AllJsons["shared"], Utils.AllJsons["shared"], Utils.AllJsons["shared"] };
        private Bitmap transbmpBlue;
        private Bitmap transbmpGreen;
        private Bitmap transbmpRed;
        private Bitmap transbmp;
        private Rectangle currentRectangle = new Rectangle(-1, -1, -1, -1);
        private Dictionary<Rectangle, Item> mappings = new Dictionary<Rectangle, Item>();
        private Dictionary<string, int> locMappings = new Dictionary<string, int>();
        private Dictionary<int, ItemLocation> locMappings2 = new Dictionary<int, ItemLocation>();
        private Bitmap downimg, upimg, goldbmp;
        private Bitmap gembmp;
        private Dictionary<Size, List<List<Point>>> socketMappings = new Dictionary<Size, List<List<Point>>>();

        //        private ButtonEx btnGoldSelf, btnGoldStash;

        private SolidBrush titleBrush = new SolidBrush(Color.FromArgb(199, 179, 119));
        private SolidBrush tooltipBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0));

        private BoxInfo allboxes = new BoxInfo();

        public event EventHandler<ItemSelectedEventArgs> OnItemSelected;

        public void InitData()
        {
            transbmpBlue = MakeTransbmp(Color.FromArgb(170, 9, 3, 29));
            transbmpGreen = MakeTransbmp(Color.FromArgb(170, 11, 30, 10));
            transbmpRed = MakeTransbmp(Color.FromArgb(170, 39, 3, 3));
            transbmp = null;

            currentTab = 0;


            RebuildMappings();
            BuildBoxesInfo();

            var imgname = Helper.GetDefinitionFileName(@"\panel\stash\stash_tabs");
            var img = Helper.Sprite2Png(imgname);
            downimg = Helper.GetImageByFrame(img, 2, 1) as Bitmap;
            upimg = Helper.GetImageByFrame(img, 2, 0) as Bitmap;

            var stashfile = Helper.GetDefinitionFileName(@"\panel\stash\stashpanel_bg");
            stashbmp = Helper.Sprite2Png(stashfile);
            var stashfile2 = Helper.GetDefinitionFileName(@"\panel\stash\stashpanel_bg_big2");
            var stashin = Helper.Sprite2Png(stashfile2);//42,116
            stashbmp2 = new Bitmap(496, 496);
            Graphics g = Graphics.FromImage(stashbmp2);
            g.DrawImage(stashin, new Rectangle(0, 0, stashbmp2.Width, stashbmp2.Height), new Rectangle(0, 0, stashin.Width, stashin.Height), GraphicsUnit.Pixel);
            g.Dispose();

            var charfile = Helper.GetDefinitionFileName(@"\panel\inventory\background_big");
            charbmp = Helper.Sprite2Png(charfile);

            var goldfile = Helper.GetDefinitionFileName(@"\panel\goldbutton");
            goldbmp = Helper.Sprite2Png(goldfile);
            goldbmp = Helper.GetImageByFrame(goldbmp, 4, 0) as Bitmap;

            var gemname = Helper.GetDefinitionFileName(@"\panel\gemsocket");
            gembmp = Helper.Sprite2Png(gemname);
            BuildSocketMappings();

            this.Width = stashbmp.Width + charbmp.Width;
            this.Height = stashbmp.Height;
            //this.Size = new Size((int)(1162*Helper.DisplayRatio), (int)(753 * Helper.DisplayRatio));
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            this.MouseDown += ItemsControl_MouseDown;
        }

        private void BuildSocketMappings()
        {
            int x = (int)(Helper.DefinitionInfo.BoxSize*Helper.DisplayRatio);
            int y = (int)(gembmp.Width);

            socketMappings[new Size(1, 1)] = new List<List<Point>> { new List<Point> { new Point((x - y) / 2, (x - y) / 2) }, null, null, null, null, null };

            socketMappings[new Size(1, 2)] = new List<List<Point>> {
                new List<Point> { new Point((2*x - y) / 2, (x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2), new Point(x+(x - y) / 2, (x - y) / 2) },
                null,null,null,null
            };

            socketMappings[new Size(2, 1)] = new List<List<Point>> {
                new List<Point> { new Point((x - y) / 2, (2*x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2), new Point((x - y) / 2, x + (x - y) / 2) },
                null,null,null,null
            };

            socketMappings[new Size(3, 1)] = new List<List<Point>> {
                new List<Point> { new Point((x - y) / 2,(3*x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (3*x - 2*y)*1 / 3), new Point((x - y) / 2, (3 * x - 2 * y) * 2 / 3+y) },
                new List<Point> { new Point((x - y) / 2, (x - y)/2), new Point((x - y) / 2,x+ (x - y)/2),new Point((x - y) / 2,2* x + (x - y) /2) },
                null,null,null
            };

            socketMappings[new Size(2, 2)] = new List<List<Point>> {
                new List<Point> { new Point((2*x - y) / 2, (2*x - y) / 2) },
                new List<Point> { new Point((2*x - y) / 2, (x - y) / 2), new Point((2*x - y) / 2, x + (x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2),new Point(x+(x - y) / 2, (x - y) / 2),new Point((x - y) / 2, x + (x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2),new Point(x+(x - y) / 2, (x - y) / 2),new Point((x - y) / 2, x + (x - y) / 2),new Point(x+(x - y) / 2, x + (x - y) / 2) },
                null,null
            };

            socketMappings[new Size(4, 1)] = new List<List<Point>> {
                new List<Point> { new Point((x - y) / 2,(4*x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (4*x - 2*y)*1 / 3), new Point((x - y) / 2, (4 * x - 2 * y)*2 / 3+y) },
                new List<Point> { new Point((x - y) / 2, (4*x - 3*y)*1/4), new Point((x - y) / 2, (4 * x - 3 * y) * 2 / 4+y), new Point((x - y) / 2, (4 * x - 3 * y) * 3 / 4+2*y) },
                new List<Point> { new Point((x - y) / 2, (x - y)/2), new Point((x - y) / 2, x + (x - y) / 2), new Point((x - y) / 2, 2*x + (x - y) / 2), new Point((x - y) / 2, 3*x + (x - y) / 2) },
                null,null
            };

            socketMappings[new Size(3, 2)] = new List<List<Point>> {
                new List<Point> { new Point((2*x - y) / 2,(3*x - y) / 2) },
                new List<Point> { new Point((2*x - y) / 2, (3*x - 2*y)*1 / 3), new Point((2*x - y) / 2, (3 * x - 2 * y) * 2 / 3+y) },
                new List<Point> { new Point((2*x - y) / 2, (x - y)/2), new Point((2*x - y) / 2,x+ (x - y)/2),new Point((2*x - y) / 2,2* x + (x - y) /2) },
                new List<Point> { new Point((x - y) / 2, (3*x - 2*y) / 3),new Point(x+(x - y) / 2, (3*x - 2*y) / 3),new Point((x - y) / 2, (3*x - 2*y)*2 / 3+y),new Point(x+(x - y) / 2, (3*x - 2*y)*2 / 3+y)  },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2), new Point(x+(x - y) / 2, (x - y) / 2), new Point((2*x - y) / 2, (3*x - y) / 2), new Point((x - y) / 2, 2*x + (x - y) / 2),new Point(x+(x - y) / 2, 2 * x + (x - y) / 2) },
                new List<Point> { new Point((x - y) / 2, (x - y) / 2), new Point(x+(x - y) / 2, (x - y) / 2), new Point((x - y) / 2, (x - y) / 2+x), new Point(x+(x - y) / 2, (x - y) / 2+x), new Point((x - y) / 2, (x - y) / 2+2*x), new Point(x+(x - y) / 2, (x - y) / 2+2*x)},
            };

            socketMappings[new Size(4, 2)] = new List<List<Point>> {
                new List<Point> { new Point((2*x - y) / 2,(4*x - y) / 2) },
                new List<Point> { new Point((2*x - y) / 2, (4*x - 2*y)*1/3), new Point((2*x - y) / 2, (4 * x - 2 * y)*2 / 3+y) },
                new List<Point> { new Point((2*x - y) / 2, (4*x - 3*y)*1/4), new Point((2*x - y) / 2, (4 * x - 3 * y) * 2 / 4+y), new Point((2*x - y) / 2, (4 * x - 3 * y) * 3 / 4+2*y) },
                new List<Point> { new Point((2*x - y) / 2, (x - y)/2), new Point((2*x - y) / 2,x+ (x - y)/2),new Point((2*x - y) / 2,2* x + (x - y) /2),new Point((2*x - y) / 2,3* x + (x - y) /2) },
                new List<Point> { new Point((x - y) / 2, (4*x - 3*y)*1/4), new Point(x+(x - y) / 2, (4 * x - 3 * y) * 1 / 4), new Point((2*x - y) / 2, (4 * x - 3 * y) * 2 / 4+y),new Point((x - y) / 2, (4*x - 3*y)*3/4+2*y), new Point(x+(x - y) / 2, (4 * x - 3 * y) * 3 / 4 + 2 * y) },
                new List<Point> { new Point((x - y) / 2, (4*x - 3*y)*1/4), new Point(x+(x - y) / 2, (4 * x - 3 * y) * 1 / 4), new Point((x - y) / 2, (4 * x - 3 * y) * 2 / 4+y), new Point(x + (x - y) / 2, (4 * x - 3 * y) * 2 / 4 + y), new Point((x - y) / 2, (4*x - 3*y)*3/4+2*y), new Point(x+(x - y) / 2, (4 * x - 3 * y) * 3 / 4 + 2 * y) },
            };

        }

        private void RebuildMappings()
        {
            mappings.Clear();
            body = Helper.CurrentCharactor.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Equipped" && item.Page == 0)).ToList();
            store = Helper.CurrentCharactor.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Stored" && item.Page == 1)).ToList();

            var mybox = Helper.CurrentCharactor.PlayerItemList.Items.Where(item => (item.Mode.ToString() == "Stored" && item.Page == 5)).ToList();

            int index = 0;
            stashes[index] = mybox;

            foreach (var d2i in Helper.SharedStashes)
            {
                stashes[++index] = d2i.ItemList.Items;
            }

            locMappings["head"] = 0; locMappings["neck"] = 5; locMappings["tors"] = 6; locMappings["rarm"] = 1; locMappings["larm"] = 2;
            locMappings["rrin"] = 4; locMappings["lrin"] = 3; locMappings["belt"] = 9; locMappings["feet"] = 7; locMappings["glov"] = 8;

            locMappings2[0] = ItemLocation.Head; locMappings2[1] = ItemLocation.RightHand; locMappings2[2] = ItemLocation.LeftHand; locMappings2[3] = ItemLocation.LeftFinger; locMappings2[4] = ItemLocation.RightFinger;
            locMappings2[5] = ItemLocation.Neck; locMappings2[6] = ItemLocation.Torso; locMappings2[7] = ItemLocation.Feet; locMappings2[8] = ItemLocation.Gloves; locMappings2[9] = ItemLocation.Waist;

            foreach (var item in body)
            {
                Rectangle r = new Rectangle(-1, -1, -1, -1);

                switch (item.Location)
                {
                    case ItemLocation.Head: r = Helper.DefinitionInfo.EquipedItem[0]; break;
                    case ItemLocation.RightHand: r = Helper.DefinitionInfo.EquipedItem[1]; break;
                    case ItemLocation.LeftHand: r = Helper.DefinitionInfo.EquipedItem[2]; break;
                    case ItemLocation.LeftFinger: r = Helper.DefinitionInfo.EquipedItem[3]; break;
                    case ItemLocation.RightFinger: r = Helper.DefinitionInfo.EquipedItem[4]; break;
                    case ItemLocation.Neck: r = Helper.DefinitionInfo.EquipedItem[5]; break;
                    case ItemLocation.Torso: r = Helper.DefinitionInfo.EquipedItem[6]; break;
                    case ItemLocation.Feet: r = Helper.DefinitionInfo.EquipedItem[7]; break;
                    case ItemLocation.Gloves: r = Helper.DefinitionInfo.EquipedItem[8]; break;
                    case ItemLocation.Waist: r = Helper.DefinitionInfo.EquipedItem[9]; break;
                    case ItemLocation.SwapRight: r = Helper.DefinitionInfo.EquipedItem[10]; break;
                    case ItemLocation.SwapLeft: r = Helper.DefinitionInfo.EquipedItem[11]; break;
                    default:
                        break;
                }

                var eir = new Rectangle((int)(r.X * Helper.DisplayRatio), (int)(r.Y * Helper.DisplayRatio), (int)(r.Width * Helper.DisplayRatio), (int)(r.Height * Helper.DisplayRatio));

                item.Rectangle = eir;
                item.Rectangles.Clear();//要不然，切换一次tab，这个集合就增加了一个，然后就越画颜色越深，特么的。
                item.Rectangles.Add(r);
                item.IconState = ItemIconState.Normal;

                mappings[eir] = item;
            }

            foreach (var item in store)
            {
                item.Rectangles.Clear();
                for (int i = item.Column; i < item.Column + item.Columns; i++)
                {
                    for (int j = item.Row; j < item.Row + item.Rows; j++)
                    {
                        item.Rectangles.Add(new Rectangle((int)(Helper.DefinitionInfo.StoreRangeX[i] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StoreRangeY[j] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio)));
                    }
                }

                item.Rectangle = new Rectangle((int)(Helper.DefinitionInfo.StoreRangeX[item.Column] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StoreRangeY[item.Row] * Helper.DisplayRatio), (int)(item.Columns * Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(item.Rows * Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio));
                item.IconState = ItemIconState.Normal;

                mappings[item.Rectangle] = item;
            }

            foreach (var item in stashes[currentTab])
            {
                item.Rectangles.Clear();
                for (int i = item.Column; i < item.Column + item.Columns; i++)
                {

                    for (int j = item.Row; j < item.Row + item.Rows; j++)
                    {
                        item.Rectangles.Add(new Rectangle((int)(Helper.DefinitionInfo.StashRangeX[i] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashRangeY[j] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio)));
                    }
                }

                item.Rectangle = new Rectangle((int)(Helper.DefinitionInfo.StashRangeX[item.Column] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashRangeY[item.Row] * Helper.DisplayRatio), (int)(item.Columns * Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(item.Rows * Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio));
                item.IconState = ItemIconState.Normal;

                mappings[item.Rectangle] = item;
            }
        }



        private void BuildBoxesInfo()
        {
            allboxes.Equipped = new Box();
            allboxes.Equipped.MaxRows = 4;
            allboxes.Equipped.MaxColumns = 10;
            allboxes.Equipped.UnitList = new List<Unit>();

            for (int i = 0; i < Helper.DefinitionInfo.StoreRangeX.Length; i++)
            {
                for (int j = 0; j < Helper.DefinitionInfo.StoreRangeY.Length; j++)
                {
                    allboxes.Equipped.UnitList.Add(new Unit()
                    {
                        Column = i,
                        Row = j,
                        Rectangle = new Rectangle((int)(Helper.DefinitionInfo.StoreRangeX[i] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StoreRangeY[j] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio))
                    });
                }
            }

            allboxes.Store = new Box();
            allboxes.Store.MaxRows = 8;
            allboxes.Store.MaxColumns = 15;
            allboxes.Store.UnitList = new List<Unit>();

            for (int i = 0; i < Helper.DefinitionInfo.StoreRangeX.Length; i++)
            {
                for (int j = 0; j < Helper.DefinitionInfo.StoreRangeY.Length; j++)
                {
                    allboxes.Store.UnitList.Add(new Unit()
                    {
                        Column = i,
                        Row = j,
                        Rectangle = new Rectangle((int)(Helper.DefinitionInfo.StoreRangeX[i] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StoreRangeY[j] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio))
                    });
                }
            }

            allboxes.Stash = new Box();
            allboxes.Stash.MaxRows = 16;
            allboxes.Stash.MaxColumns = 16;
            allboxes.Stash.UnitList = new List<Unit>();

            for (int i = 0; i < Helper.DefinitionInfo.StashRangeX.Length; i++)
            {
                for (int j = 0; j < Helper.DefinitionInfo.StashRangeY.Length; j++)
                {
                    allboxes.Stash.UnitList.Add(new Unit()
                    {
                        Column = i,
                        Row = j,
                        Rectangle = new Rectangle((int)(Helper.DefinitionInfo.StashRangeX[i] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashRangeY[j] * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio))
                    });
                }
            }

            return;
        }

        public ItemsControl()
        {
            InitializeComponent();
            //this.BackgroundImage = Image.FromFile("d2r2.png");
        }

        private Bitmap MakeTransbmp(Color color)
        {
            var bmp = new Bitmap((int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio));
            Graphics g2 = Graphics.FromImage(bmp);
            g2.FillRectangle(new SolidBrush(color), 0, 0, bmp.Width, bmp.Height);

            return bmp;
        }

        private Point TryGetBestTooltipPosition(Rectangle r, SizeF sf)
        {
            int left = r.X + 2 * (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio) + 5, top = r.Y;


            while (top >= 0)
            {
                if (top + sf.Height < this.Height) break;
                top -= (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio);
            }
            if (top < 0) top = 5;

            while (left >= 0)
            {
                if (left + sf.Width < this.Width) break;
                left -= (int)(Helper.DefinitionInfo.BoxSize * Helper.DisplayRatio);
            }
            if (left < 0) left = 5;


            return new Point(left, top);

        }

        bool isDraging = false;
        Item curDraggingItem;
        Point curDraggingPositon;
        Area fromArea, toArea;

        private enum Area
        {
            None,
            Body,
            Store,
            MyStash,
            SharedStash
        }

        private Item GetItemFromPoint(Point p)
        {
            Item item = null;
            foreach (var r in mappings.Keys)
            {
                if (Helper.IsPointInRange(p, r))
                {
                    item = mappings[r];
                    break;
                }
            }

            return item;
        }

        private DragDropInformation GetItemListFromHoveringPoint(Point p)
        {
            bool canBeDroppped = true;
            Item exchangedItem = null;
            List<Rectangle> list = new List<Rectangle>();
            int startRow = -1;
            int startCol = -1;

            //System.Diagnostics.Debug.WriteLine(String.Format("Row={0},Col={1}, Rows={2}, Columns={3}", curDraggingItem.Row, curDraggingItem.Column, curDraggingItem.Rows, curDraggingItem.Columns));
            Box box = null;
            var area = GetCursorArea(new Point(p.X,p.Y));

            switch (area)
            {
                case Area.Body:
                    box = allboxes.Equipped;
                    break;
                case Area.Store:
                    box = allboxes.Store;
                    break;
                case Area.MyStash:
                case Area.SharedStash:
                    box = allboxes.Stash;
                    break;
                default: break;
            }
            if ((box != null) && (curDraggingItem != null))
            {
                if (area != Area.Body)
                {
                    //foreach (var unit in box.UnitList)
                    {
                        //if (Helper.IsPointInRange(p, unit.Rectangle))
                        var unit = box.UnitList.Where(u => Helper.IsPointInRange(p, u.Rectangle)).FirstOrDefault();
                        if (unit != null)
                        {
                            //System.Diagnostics.Debug.WriteLine(String.Format("StartRow={0},StartCol={1},EndRow={2},EndCol={3}", unit.Row, unit.Column, unit.Row + curDraggingItem.Rows - 1, unit.Column + curDraggingItem.Columns - 1));
                            if (unit.Row + curDraggingItem.Rows - 1 >= box.MaxRows) { /*System.Diagnostics.Debug.WriteLine("Rows exceeded.");*/ canBeDroppped = false;}
                            if (unit.Column + curDraggingItem.Columns - 1 >= box.MaxColumns) { /*System.Diagnostics.Debug.WriteLine("Columnss exceeded.");*/ canBeDroppped = false;}

                            if (canBeDroppped)
                            {
                                int itemsCountFromHoverdRange = 0;
                                List<Item> hoveredItemList = new List<Item>();

                                list.Clear();
                                bool firsttime = true;

                                for (int col = unit.Column; col < unit.Column + curDraggingItem.Columns; col++)
                                {
                                    for (int row = unit.Row; row < unit.Row + curDraggingItem.Rows; row++)
                                    {
                                        if (firsttime)
                                        {
                                            startRow = row;
                                            startCol = col;
                                            firsttime = false;
                                        }
                                        var r = box.UnitList.Where(u => u.Row == row && u.Column == col).First().Rectangle;
                                        var hoveredItem = GetItemFromPoint(new Point(r.X, r.Y));
                                        if (hoveredItem != null)
                                        {
                                            if (!hoveredItemList.Contains(hoveredItem))
                                            {
                                                //System.Diagnostics.Debug.WriteLine("Items hovered name=" + hoveredItem.Name);
                                                itemsCountFromHoverdRange++;
                                                hoveredItemList.Add(hoveredItem);
                                            }
                                        }

                                        list.Add(r);
                                    }
                                }

                                //System.Diagnostics.Debug.WriteLine("Items count=" + itemsCountFromHoverdRange.ToString());

                                if (itemsCountFromHoverdRange == 0)
                                {
                                    canBeDroppped = true;
                                    exchangedItem = curDraggingItem;
                                    //list = exchangedItem.Rectangles;
                                }
                                else if (itemsCountFromHoverdRange == 1)
                                {
                                    canBeDroppped = true;
                                    exchangedItem = hoveredItemList[0];
                                    list = exchangedItem.Rectangles;
                                }

                                else
                                {
                                    canBeDroppped = false;
                                    exchangedItem = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    canBeDroppped = false;
                    for (int i = 0; i < 10; i++)
                    {
                        var ei = Helper.DefinitionInfo.EquipedItem[i];
                        var eir = new Rectangle((int)(ei.X*Helper.DisplayRatio),(int)(ei.Y*Helper.DisplayRatio), (int)(ei.Width*Helper.DisplayRatio), (int)(ei.Height*Helper.DisplayRatio));

                        if (Helper.IsPointInRange(p,eir ))
                        {
                            list = new List<Rectangle>() { eir };

                            if (ExcelTxt.ItemTypesTxt[curDraggingItem.TypeCode]["Body"].ToInt32() == 0)
                            {
                                canBeDroppped = false;
                            }
                            else
                            {
                                var loc1 = locMappings[ExcelTxt.ItemTypesTxt[curDraggingItem.TypeCode]["BodyLoc1"].Value];
                                var loc2 = locMappings[ExcelTxt.ItemTypesTxt[curDraggingItem.TypeCode]["BodyLoc2"].Value];

                                canBeDroppped = (i == loc1 || i == loc2);
                                if (canBeDroppped)
                                {
                                    exchangedItem = GetItemFromPoint(p);
                                    if (exchangedItem == null)
                                    {
                                        exchangedItem = curDraggingItem;
                                    }

                                    curDraggingItem.Location = locMappings2[i];
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return new DragDropInformation() { CanBeDropped = canBeDroppped, ExchangedItem = exchangedItem, Rectangles = list, StartRow = startRow, StartCol = startCol };
        }

        internal class DragDropInformation
        {
            public bool CanBeDropped;
            public Item ExchangedItem;
            public List<Rectangle> Rectangles;
            public int StartRow;
            public int StartCol;
        }

        public void SetCurrentDraggingItem(Item item)
        {
            curDraggingItem = item;
            isDraging = true;
        }
        private void ItemsControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            if (isDraging)
            {
                var hoverinfo = GetItemListFromHoveringPoint(this.curDraggingPositon);
                if (!hoverinfo.CanBeDropped)
                {
                    this.Invalidate();
                    return;
                }

                if (hoverinfo.Rectangles.Count == 0)//拖到store、body、stash之外的地方
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Rectangle stashTabRectangle = new Rectangle((int)(Helper.DefinitionInfo.StashTabStartX*Helper.DisplayRatio) + (int)(Helper.DefinitionInfo.StashTabWidth * i*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabStartY*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabWidth*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabHeight*Helper.DisplayRatio));
                        if (Helper.IsPointInRange(e.Location, stashTabRectangle))
                        {
                            switch (fromArea)
                            {
                                case Area.SharedStash: stashes[currentTab].Remove(curDraggingItem); break;
                                default: Helper.CurrentCharactor.PlayerItemList.Items.Remove(curDraggingItem); break;
                            }

                            currentTab = i;
                            this.RebuildMappings();

                            this.Invalidate();

                            return;
                        }
                    }
                    if (DialogResult.Yes == MessageBox.Show("确实要扔掉这个牛逼的装备吗？", "警告！", MessageBoxButtons.YesNo))
                    {
                        switch (fromArea)
                        {
                            case Area.SharedStash: stashes[currentTab].Remove(curDraggingItem); break;
                            default: Helper.CurrentCharactor.PlayerItemList.Items.Remove(curDraggingItem); break;
                        }

                        isDraging = false;
                        draggingBmp = null;
                        toArea = Area.None;

                        this.RebuildMappings();
                        this.Invalidate();
                        return;
                    }
                }


                if ((hoverinfo.ExchangedItem != null))
                {
                    toArea = GetCursorArea(e.Location);

                    //System.Diagnostics.Debug.WriteLine(String.Format("From {0} to {1}", fromArea, toArea));

                    switch (fromArea)
                    {
                        case Area.SharedStash: stashes[currentTab].Remove(curDraggingItem); break;
                        default: Helper.CurrentCharactor.PlayerItemList.Items.Remove(curDraggingItem); break;
                    }
                    switch (toArea)
                    {
                        case Area.SharedStash: stashes[currentTab].Add(curDraggingItem); break;
                        default: Helper.CurrentCharactor.PlayerItemList.Items.Add(curDraggingItem); break;
                    }

                    //放到空白处
                    if (hoverinfo.ExchangedItem == curDraggingItem)
                    {
                        switch (GetCursorArea(e.Location))
                        {
                            case Area.Body:
                                curDraggingItem.Mode = ItemMode.Equipped; curDraggingItem.Page = 0; break;
                            case Area.MyStash:
                            case Area.SharedStash:
                                curDraggingItem.Mode = ItemMode.Stored; curDraggingItem.Page = 5; break;
                            case Area.Store:
                                curDraggingItem.Mode = ItemMode.Stored; curDraggingItem.Page = 1; break;
                            default: break;
                        }

                        curDraggingItem.Rectangle = new Rectangle(hoverinfo.Rectangles[0].X, hoverinfo.Rectangles[0].Y, (int)(Helper.DefinitionInfo.BoxSize * hoverinfo.ExchangedItem.Columns*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * hoverinfo.ExchangedItem.Rows*Helper.DisplayRatio));
                        curDraggingItem.Rectangles = hoverinfo.Rectangles;
                        curDraggingItem.Row = (byte)hoverinfo.StartRow;
                        curDraggingItem.Column = (byte)hoverinfo.StartCol;
                        curDraggingItem.Rows = hoverinfo.ExchangedItem.Rows;
                        curDraggingItem.Columns = hoverinfo.ExchangedItem.Columns;
                        curDraggingItem.Mode = hoverinfo.ExchangedItem.Mode;
                        curDraggingItem.Page = hoverinfo.ExchangedItem.Page;
                        mappings[curDraggingItem.Rectangle] = curDraggingItem;

                        RebuildMappings();
                        draggingBmp = null;
                        isDraging = false;
                        this.Invalidate();
                    }
                    //替换掉原来位置的item
                    else
                    {

                        var initrec = hoverinfo.Rectangles[0];

                        curDraggingItem.Rectangle = new Rectangle(initrec.X, initrec.Y, (int)(Helper.DefinitionInfo.BoxSize * curDraggingItem.Columns*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize * curDraggingItem.Rows*Helper.DisplayRatio));
                        int recs = curDraggingItem.Rectangles.Count;
                        curDraggingItem.Rectangles.Clear();

                        for (int i = 0; i < curDraggingItem.Columns; i++)
                        {
                            for (int j = 0; j < curDraggingItem.Rows; j++)
                            {
                                curDraggingItem.Rectangles.Add(new Rectangle(initrec.X + (int)(Helper.DefinitionInfo.BoxSize * i*Helper.DisplayRatio), (int)(initrec.Y + Helper.DefinitionInfo.BoxSize * j*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.BoxSize*Helper.DisplayRatio)));
                            }
                        }

                        curDraggingItem.Row = (byte)hoverinfo.StartRow;
                        curDraggingItem.Column = (byte)hoverinfo.StartCol;
                        curDraggingItem.Mode = hoverinfo.ExchangedItem.Mode;
                        curDraggingItem.Page = hoverinfo.ExchangedItem.Page;
                        mappings[curDraggingItem.Rectangle] = curDraggingItem.Clone();

                        curDraggingItem = hoverinfo.ExchangedItem;
                        RebuildMappings();
                        //mappings.Remove(
                        //    mappings.Keys.Where(
                        //        key => (mappings[key] == curDraggingItem)
                        //    )
                        //    .Select(k => k)
                        //    .First()
                        //);
                        draggingBmp = null;
                        isDraging = true;
                        this.Invalidate();
                    }
                }


                return;
            }

            curDraggingItem = GetItemFromPoint(e.Location);
            if (curDraggingItem != null)
            {
                fromArea = GetCursorArea(e.Location);
                //从当前映射表中移除被拖动的item
                mappings.Remove(
                    mappings.Keys.Where(
                        key => (mappings[key] == curDraggingItem)
                    )
                    .Select(k => k)
                    .First()
                );
                isDraging = true;

            }
            else
            {
                //点倒了空白处
                isDraging = false;
            }

        }

        private Area GetCursorArea(Point p)
        {
            Area cursorArea = Area.None;
            if (Helper.IsPointInRange(p, Helper.DefinitionInfo.BodyArea))
            {
                cursorArea = Area.Body;
            }
            else if (Helper.IsPointInRange(p, Helper.DefinitionInfo.StoreArea))
            {
                cursorArea = Area.Store;
            }
            else if (Helper.IsPointInRange(p, Helper.DefinitionInfo.StashArea))
            {
                if (currentTab == 0) cursorArea = Area.MyStash; else cursorArea = Area.SharedStash;
            }
            else
            {
                cursorArea = Area.None;
            }

            return cursorArea;
        }
        private void ItemControl_MouseUp(object sender, MouseEventArgs e)
        {
            //切换大箱子的TAB页
            for (int i = 0; i < 4; i++)
            {
                Rectangle stashTabRectangle = new Rectangle((int)((Helper.DefinitionInfo.StashTabStartX + Helper.DefinitionInfo.StashTabWidth * i)*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabStartY*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabWidth*Helper.DisplayRatio), (int)(Helper.DefinitionInfo.StashTabHeight*Helper.DisplayRatio));
                if (Helper.IsPointInRange(e.Location, stashTabRectangle))
                {
                    currentTab = i;

                    this.RebuildMappings();
                    this.Invalidate();

                    return;
                }
            }

            //右键，进入编辑界面
            if (e.Button == MouseButtons.Right)
            {
                isDraging = false;
                var item = GetItemFromPoint(e.Location);
                if (item == null) return;

                if (!item.IsIdentified)
                {
                    MessageBox.Show(Utils.AllJsons["ItemStats1b"]);
                    return;
                }
                if ((item != null) && (OnItemSelected != null))
                {
                    OnItemSelected(this, new ItemSelectedEventArgs(item,e.Location));
                    //ItemsControl_MouseDown(sender, new MouseEventArgs(MouseButtons.Left,e.Clicks, e.X,e.Y,e.Delta));
                    //Item.UpdateRowsAndColumnsInformation(item);
                    this.Invalidate();
                }
            }
        }

        private void ItemControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraging)
            {
                this.curDraggingPositon = e.Location;
                if (Area.Body == GetCursorArea(e.Location))
                {
                    var item = GetItemFromPoint(e.Location);
                    if (item != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Body now." + item.Name);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Body now.");
                    }
                }
            }
            else
            {
                foreach (var r in mappings.Keys)
                {
                    mappings[r].IconState = ItemIconState.Normal;
                }

                foreach (var r in mappings.Keys)
                {
                    if (Helper.IsPointInRange(e.Location, r))
                    {
                        currentRectangle = r;
                        mappings[r].IconState = ItemIconState.Hovering;//焦点状态

                        break;
                    }
                }
            }

            this.Invalidate();
        }

        private Bitmap draggingBmp;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.DesignMode) return;

            Bitmap background = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(background);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//.AntiAlias;// | System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Black);

            #region 底图
            //左面的大箱子
            g.DrawImage(stashbmp, 0, 0, stashbmp.Width, stashbmp.Height);
            g.DrawImage(stashbmp2, 42, 116, stashbmp2.Width, stashbmp2.Height);
            //右面的装备栏
            g.DrawImage(charbmp, stashbmp.Width, 0, charbmp.Width, charbmp.Height);
            #endregion 底图

            #region 标题栏文字信息
            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize*Helper.DisplayRatio, FontStyle.Bold))
            {
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(Utils.AllJsons["stash"], f, Helper.TextBrush, new RectangleF(0, 36 * Helper.DisplayRatio, stashbmp.Width, 40 * Helper.DisplayRatio), sf);
                    g.DrawString(Utils.AllJsons["strpanel4"], f, Helper.TextBrush, new RectangleF(stashbmp.Width + 0, 36 * Helper.DisplayRatio, charbmp.Width, 40 * Helper.DisplayRatio), sf);
                }
            }

            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTitleFontSize * Helper.DisplayRatio))
            {
                //g.DrawString(Utils.AllJsons["stash"], f, titleBrush, Helper.DefinitionInfo.StashTitleStartX, Helper.DefinitionInfo.StashTitleStartY);
                //g.DrawString(Utils.AllJsons["strpanel4"], f, titleBrush, Helper.DefinitionInfo.InventoryTitleStartX, Helper.DefinitionInfo.InventoryTitleStartY);
                var gold = "0";
                if (Helper.CurrentCharactor.Attributes.Stats.ContainsKey("gold")) gold = Helper.CurrentCharactor.Attributes.Stats["gold"].ToString();
                g.DrawString(gold, f, Helper.TextBrush, 570 * Helper.DisplayRatio, 840 * Helper.DisplayRatio);

                var goldbank = "0";

                if (currentTab == 0)
                {
                    if (Helper.CurrentCharactor.Attributes.Stats.ContainsKey("goldbank"))
                    {
                        goldbank = Helper.CurrentCharactor.Attributes.Stats["goldbank"].ToString();
                    }
                }
                else
                {
                    goldbank = Helper.SharedStashes[currentTab - 1].Gold.ToString();
                }
                g.DrawString(goldbank, f, Helper.TextBrush, 238 * Helper.DisplayRatio, 645 * Helper.DisplayRatio);
            }
            g.DrawImage(goldbmp, 540 * Helper.DisplayRatio, 840 * Helper.DisplayRatio);
            g.DrawImage(goldbmp, 210 * Helper.DisplayRatio, 650 * Helper.DisplayRatio);
            #endregion

            #region 大箱子文字
            using (Font f = new Font("SimSun", Helper.DefinitionInfo.StashTabFontSize * Helper.DisplayRatio, FontStyle.Bold))
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    var sf = g.MeasureString(labels[i], f);
                    if (currentTab == i)
                    {
                        g.DrawImage(downimg, Helper.DefinitionInfo.StashTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.StashTabWidth * i * Helper.DisplayRatio, Helper.DefinitionInfo.StashTabStartY * Helper.DisplayRatio);
                        g.DrawString(labels[i], f, Brushes.White, (Helper.DefinitionInfo.StashTabTitleStartX * Helper.DisplayRatio - sf.Width) / 2 + Helper.DefinitionInfo.StashTabWidth * i * Helper.DisplayRatio, Helper.DefinitionInfo.StashTabTitleStartY * Helper.DisplayRatio);
                    }
                    else
                    {
                        g.DrawImage(upimg, Helper.DefinitionInfo.StashTabStartX * Helper.DisplayRatio + Helper.DefinitionInfo.StashTabWidth * i * Helper.DisplayRatio, Helper.DefinitionInfo.StashTabStartY * Helper.DisplayRatio);
                        g.DrawString(labels[i], f, Brushes.Gray, (Helper.DefinitionInfo.StashTabTitleStartX * Helper.DisplayRatio - sf.Width) / 2 + Helper.DefinitionInfo.StashTabWidth * i * Helper.DisplayRatio, Helper.DefinitionInfo.StashTabTitleStartY * Helper.DisplayRatio);
                    }
                }
            }
            #endregion 大箱子文字

            foreach (var r in mappings.Keys)
            {
                var item = mappings[r];

                #region 背景颜色
                var backbmp = transbmp;//缺省不画背景颜色

                if (item.Mode == ItemMode.Stored)//store或者箱子中
                {
                    backbmp = item.IsIdentified ? transbmpBlue : transbmpRed;
                }

                if (item.IconState == ItemIconState.Hovering)
                {
                    backbmp = transbmpGreen;
                }

                if (backbmp != null)
                {
                    foreach (var box in item.Rectangles)//画对应的蓝色或者红色的小矩形底色
                    {
                        g.DrawImage(backbmp, box);
                    }
                }
                #endregion 背景颜色

                #region 装备图标
                var imgname = Helper.GetDefinitionFileName(@"\items\" + item.Icon);
                var bmp = Helper.Sprite2Png2(imgname);

                g.DrawImage(bmp, r.X + (r.Width - bmp.Width) / 2, r.Y+ (r.Height - bmp.Height) / 2, bmp.Width, bmp.Height);
                #endregion 装备图标


                #region 老方式的图片                
                //D2Palette dp = new D2Palette(item.Icon);
                //var bmp = dp.Transform();
                //g.DrawImage(bmp, r.X + (r.Width - bmp.Width) / 2, r.Y + (r.Height - bmp.Height) / 2, bmp.Width, bmp.Height);
                #endregion 老方式的图片
            }



            #region 所有的都画完了再来画hovering的内容，否则会被其他icon的redraw覆盖掉。
            var hoveringR = mappings.Keys.Where(k => mappings[k].IconState == ItemIconState.Hovering).FirstOrDefault();
            if (mappings.ContainsKey(hoveringR))
            {
                var item = mappings[hoveringR];
                
                var sinfo = this.socketMappings[new Size(item.Rows,item.Columns)];
                
                if (item.TotalNumberOfSockets>0 && sinfo[item.TotalNumberOfSockets-1] != null)
                {
                    int i = item.TotalNumberOfSockets-1;
                    for (int j = 0; j < sinfo[i].Count; j++)
                    {
                        g.DrawImage(gembmp, hoveringR.X + sinfo[i][j].X , hoveringR.Y + sinfo[i][j].Y );
                    }
                    for(int j = 0; j < item.SocketedItems.Count; j++)
                    {
                        var iconname = Helper.GetDefinitionFileName(@"\items\" + item.SocketedItems[j].Icon);
                        var sbmp = Helper.Sprite2Png2(iconname);
                        g.DrawImage(sbmp,new RectangleF(hoveringR.X+ sinfo[i][j].X+10*Helper.DisplayRatio, hoveringR.Y + sinfo[i][j].Y+ 6 * Helper.DisplayRatio, 32 * Helper.DisplayRatio, 32 * Helper.DisplayRatio), new Rectangle(0, 0, sbmp.Width, sbmp.Height), GraphicsUnit.Pixel);
                    }
                }

                var complexRet = MeasureSizeInfo(item);
                var newp = TryGetBestTooltipPosition(hoveringR, complexRet.Item1);

                g.FillRectangle(this.tooltipBrush, new RectangleF(newp.X, newp.Y , complexRet.Item1.Width, complexRet.Item1.Height));
                g.DrawRectangle(Pens.Wheat, new Rectangle((int)(newp.X ), (int)(newp.Y ), (int)(complexRet.Item1.Width), (int)(complexRet.Item1.Height)));
                using (Font f = new Font("SimSun",Helper.DefinitionInfo.TooltipFontSize * Helper.DisplayRatio, FontStyle.Bold))
                {
                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;

                        for (int i = 0; i < complexRet.Item2.Length; i++)
                        {
                            //System.Diagnostics.Debug.WriteLine(tooltips[i]);
                            if (i == 1 && complexRet.Item2[0] == complexRet.Item2[1]) complexRet.Item2[1] = "";

                            var newr = complexRet.Item4[i];
                            g.DrawString(complexRet.Item2[i], f, complexRet.Item3[i], new RectangleF(newp.X, (10 + newp.Y + newr.Y) , complexRet.Item1.Width, complexRet.Item1.Height), format);
                        }
                    }
                }
            }
            #endregion 所有的都画完了再来画hovering的内容，否则会被其他icon的redraw覆盖掉。

            if (isDraging)
            {
                if (draggingBmp == null)
                {
                    var imgname = Helper.GetDefinitionFileName(@"\items\" + this.curDraggingItem.Icon);
                    draggingBmp = Helper.Sprite2Png2(imgname);
                }
                var hoverinfo = GetItemListFromHoveringPoint(this.curDraggingPositon);

                if (hoverinfo.Rectangles.Count > 0 && hoverinfo.ExchangedItem!=null)
                {
                    var r2 = new Rectangle(hoverinfo.Rectangles[0].X, hoverinfo.Rectangles[0].Y, hoverinfo.ExchangedItem.Columns*Helper.DefinitionInfo.BoxSize, hoverinfo.ExchangedItem.Rows * Helper.DefinitionInfo.BoxSize);
                    g.DrawImage(draggingBmp, r2.X + (r2.Width - draggingBmp.Width) / 2, r2.Y + (r2.Height - draggingBmp.Height) / 2, draggingBmp.Width, draggingBmp.Height);// this.curDraggingPositon);

                    var transbmp = hoverinfo.CanBeDropped ? transbmpGreen : transbmpRed;
                    foreach (var r in hoverinfo.Rectangles)
                    {
                        g.DrawImage(transbmp, r);
                    }
                }
            }

            e.Graphics.DrawImage(background, 0, 0);

            g.Dispose();
            background.Dispose();
        }

        public Tuple<Size, string[], Brush[], Rectangle[]> MeasureSizeInfo(Item item)
        {
            Size ret = new Size();

            int margin = 0;
            int left = 0, top = 0, maxw = 0, maxh = top;
            Rectangle[] rectangles = new Rectangle[7];
            string[] tooltips = new string[] {
                item.Name + (item.QualityVersion != "" ? "(" + item.QualityVersion + ")" : "")+(item.WeightDesc!=""?"("+item.WeightDesc+")":""),
                item.SocketItemsName,
                (item.Name==Utils.AllJsons[item.Code.Trim()])?"":Utils.AllJsons[item.Code.Trim()],
                Helper.GetBasicDescription(Helper.CurrentCharactor.Level, item),
                Helper.GetEnhancedDescription(Helper.CurrentCharactor.Level, item),
                item.IsIdentified?"": Utils.AllJsons["ItemStats1b"],//Helper.GetSetDescription(Helper.CurrentCharactor.Level, item),
                Utils.AllJsons["right_click_edit"]
                 };
            Brush[] brushes = new Brush[] { item.NameColor, item.NameColor, item.NameColor, Brushes.White, item.EnhancedColor, Utils.ColorSet, Brushes.Red };

            using (Graphics g = this.CreateGraphics())
            {
                using (Font f = new Font("SimSun", Helper.DefinitionInfo.TooltipFontSize * Helper.DisplayRatio, FontStyle.Bold))
                {
                    for (int i = 0; i < tooltips.Length; i++)
                    {
                        var sf = g.MeasureString(tooltips[i], f);
                        maxw = Math.Max((int)sf.Width, maxw);
                        rectangles[i] = new Rectangle(left, maxh, 0, (int)sf.Height);
                        maxh += (int)sf.Height + margin;
                    }
                }
            }

            ret.Width = maxw + 2 * left + 20;
            ret.Height = maxh + 2 * top + 20;

            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i].Width = ret.Width;
            }

            return new Tuple<Size, string[], Brush[], Rectangle[]>(ret, tooltips, brushes, rectangles);
        }
    }

    public class BoxInfo
    {
        public Box Equipped;
        public Box Store;
        public Box Stash;
    }

    public class Box
    {
        public int MaxRows { get; set; }
        public int MaxColumns { get; set; }
        public List<Unit> UnitList { get; set; }
    }
    public class Unit
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Rectangle Rectangle { get; set; }
    }
}
