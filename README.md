# D2REditor
D2R offline file editor, which includes d2s &amp; d2i and maybe something else.

这是一个暗黑2重制版的离线存档修改器，参考了很多有用的链接，当然首先是D2SLib。链接都放在下面，感谢他们，让我对D2S的file format有了一些了解。

---
**关于txt文件的获取**
- 所有的txt文件，都是用最新版本的CascView打开的，选择data/data文件夹即可
- 中文的信息，可以用/data/locales/data/zhtw/data/local/lng/chi/english.txt这个文件

---
**特别感谢**
- 特别感谢D2SLib，节省了全部的对于file format包括但不限于item format的解析，所有的byte2bit的操作，以及回写的bit2byte的操作。遗憾一点的是，源代码是NET Core的，我这里用着不方便，稍微做了一些改动，主要是Json库的使用，移到了NET Framework上。**

---
**特殊说明**
- 第一个有用的链接，https://github.com/krisives/d2s-format#items
- 项目中引用的D2SLib，在我代码库里面，https://github.com/dschu012/D2SLib

---
**如果要深入理解文件及最复杂的装备格式，必须要看这四个**
- 对于D2S文件格式最清晰最全面的链接，http://user.xmission.com/~trevin/DiabloIIv1.09_File_Format.shtml
- 装备部分，最清晰最全面的链接，http://user.xmission.com/~trevin/DiabloIIv1.09_Item_Format.shtml
- 所有txt文件格式的解释，https://d2mods.info/forum/viewtopic.php?t=34455
- Documents/Sheets/Excel是0x61这个版本中所有的txt文件，其中的mhtml必须要读，包括了所有的属性的解释
- 163官网的基础知识：https://wiki.d.163.com/index.php?title=Items_(Diablo2)
---

**UI有关**
- dc6文件的介绍，https://www.91d2.cn/MODxiugai/2018-08-21/180.html
- dc6格式的介绍，尤其是dat、pal2，需要了解，https://d2mods.info/forum/viewtopic.php?t=724#p148076
- 重制版的高清素材，已经不是dc6了，而是sprite格式，转换sprite到png，参考这里：https://github.com/eezstreet/D2RModding-SpriteEdit
- 高清的sprite的对应关心，在items.json里面，通过code来映射sprite文件
- 高清路径：hd\data\data\hd\
- 攻击计算：http://www.diablofans.com.cn/jisuanqi/d2/damtool.html
- 编辑器：https://d2.maxroll.gg/d2planner/
- 词缀及装备导入：https://tieba.baidu.com/p/6635347764

---

**所有装备的模板**
- he-yaowen的资料，https://github.com/he-yaowen/DiabloII_Item_Warehouse_1.0.11.46

---
**其他普通参考链接继续如下**
- 很好的cheatsheet，https://htmlpreview.github.io/?https://github.com/Michaelangel007/d2_cheat_sheet/blob/master/index.html
- https://github.com/HarpyWar/d2s-character-editor/tree/master/tools/esCharView/CharacterEditor
- https://github.com/fabd/diablo2/blob/master/code/itemswith.pl
- https://github.com/d07RiV/d07riv.github.io/blob/master/d2r.html
- https://github.com/nokka/d2s/blob/master/item.go
- https://github.com/fabd/diablo2/blob/master/code/itemswith.pl
- http://user.xmission.com/~trevin/images/d2sEdit.pdf
- 中文版的BBS，mod居多，文件格式很少，http://bbs.anhei2.com/forum-6-1.html

---
- 这个链接不错，可以将来中文用，https://www.docin.com/p-693430572.html
---

flippyfile - Controls which DC6 file to use for displaying the item in the game world when it is dropped on the ground (uses the file name as the input)
invfile - Controls which DC6 file to use for displaying the item graphics in the inventory (uses the file name as the input)

uniqueinvfile - Controls which DC6 file to use for displaying the item graphics in the inventory when it is a Unique quality item (uses the file name as the input)

setinvfile - Controls which DC6 file to use for displaying the item graphics in the inventory when it is a Set quality item (uses the file name as the input)


另外，我第一次使用markdown方式来写，所以先把简书这个链接放在这里，感谢作者！https://www.jianshu.com/p/191d1e21f7ed
