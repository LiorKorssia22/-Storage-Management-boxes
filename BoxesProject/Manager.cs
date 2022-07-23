using _1060DS;
using System;
using System.Collections.Generic;
using System.Text;
using static BoxesProject.Boxes;
using System.Threading;
using LinkedListAssignment;

namespace BoxesProject
{
    public class Manager
    {
        readonly BST<BoxBase> BstBase = new BST<BoxBase>();
        readonly DoubleLinkedList<TimeData> timeList = new DoubleLinkedList<TimeData>();

        Timer timer;
        INotifier notifier;
        TimeSpan invocation;
        TimeSpan chchPeriod;
        double expiration;
        int maximum;
        int minAmount;

        public Manager(INotifier notifier, TimeSpan spanChek, TimeSpan spanInvoke, double spanExep, int maxAmount, int minAmount)
        {
            invocation = spanInvoke;
            expiration = spanExep;
            chchPeriod = spanChek;
            maximum = maxAmount;
            this.minAmount = minAmount;
            this.notifier = notifier;
            timer = new Timer(CheckExpireredBoxes, timeList, invocation, chchPeriod);
        }

        private void CheckExpireredBoxes(object obj)
        {
            if (timeList.Last == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                notifier.OnInfo($"Everything in your stock has been deleted!");
                Console.ForegroundColor = ConsoleColor.White;
                timer.Dispose();
                return;
            }
            foreach (var timeData in timeList)
            {
                BoxBase boxBase;
                BoxHeight boxHeight;

                bool searchedBase = BstBase.Search(timeData.BoxBaseNodeProp, out boxBase);
                bool searchedHeight = boxBase.BstHeight.Search(timeData.BoxHeightNodeProp, out boxHeight);
                if (timeList.Last != null && timeData.LastTimeSold.Date <= DateTime.Now.Date)
                {
                    notifier.OnInfo($"expiered removed box info: Bottom {timeData.X} Height {timeData.Y}");
                    boxBase.BstHeight.Remove(boxHeight);
                    if (!searchedHeight)
                    {
                        BstBase.Remove(boxBase);
                    }
                    timeList.DeleteNode(timeData);
                }
                else
                    break;
            }
            if (BstBase.RootProp == null)
            {
                notifier.OnInfo($"Everything in your stock has been deleted !");
                timer.Dispose();
                return;
            }
        }

        public void Supply(double buttomSize, double heightSize, int amount)
        {
            BoxBase boxBasetmp = new BoxBase(buttomSize);
            TimeData data = new TimeData(buttomSize, heightSize);
            BoxBase boxBase;
            if (amount > maximum)
            {
                amount = maximum;
                Console.ForegroundColor = ConsoleColor.Red;
                notifier.OnError("Cannot supply above the maximum amount!");
                Console.ForegroundColor = ConsoleColor.White;

            }
            BoxHeight boxHeighttmp = new BoxHeight(heightSize, amount);
            if (BstBase.Search(new BoxBase(buttomSize), out boxBase))
            {
                BoxHeight boxHeight;
                if (boxBase.BstHeight.Search(boxHeighttmp, out boxHeight))
                {
                    if (boxHeight.Amount + amount < maximum)
                        boxHeight.Amount += amount;
                    else
                    {
                        notifier.OnError("Cannot surpass the amount of max crates allowed!");
                        int maxCratesAllowed = maximum - boxHeight.Amount;
                        boxHeight.Amount += maxCratesAllowed;
                    }
                }
                else
                {
                    data = new TimeData(buttomSize, heightSize);
                    timeList.AddFirst(data);
                    boxHeighttmp.NodeBox = timeList.First;
                    boxHeighttmp.TimeData = data;
                    data.BoxBaseNodeProp = boxBasetmp;
                    data.BoxHeightNodeProp = boxHeighttmp;
                    boxBase.BstHeight.Add(boxHeighttmp);
                    timeList.AddFirst(data);
                }
            }
            else
            {
                BoxBase box = new BoxBase(buttomSize);
                box.BstHeight.Add(boxHeighttmp);
                data = new TimeData(buttomSize, heightSize);
                boxHeighttmp.TimeData = data;
                boxHeighttmp.NodeBox = timeList.First;
                data.BoxBaseNodeProp = box;
                data.BoxHeightNodeProp = boxHeighttmp;
                timeList.AddFirst(data);
                BstBase.Add(box);
            }
        }

        public bool Show(out string shop)
        {
            if (BstBase.RootProp == null)
            {
                shop = "";
                return false;
            }
            if (notifier.On("Welcome to 'The BOXES' would You want to see our box collection?"))
            {
                shop = PrintStorage();
                return true;
            }
            shop = "OK, too bad Bey Bey!";
            return false;
        }


        public string ShowList() => timeList.ToString();

        public string PrintStorage()
        {
            string shop;
            StringBuilder sb = new StringBuilder();
            if (BstBase.RootProp == null)
            {
                return "";
            }
            foreach (BoxBase box in BstBase)
            {
                if (box.BstHeight.RootProp == null)
                {
                    return "";
                }
                sb.Append(box.ToString());
                foreach (BoxHeight boxheight in box.BstHeight)
                {
                    sb = sb.Append(boxheight.ToString());
                }
            }
            shop = sb.ToString();
            return shop;
        }

        public bool Purchase(double width, double height, int amount)
        {
            const int splits = 3;
            if (BstBase.RootProp == null)
            {
                notifier.OnError("Sorry we ran out of stock!");
                return false;
            }
            int amountCounter = 0;
            int splitCounter = 0;
            List<BoxDataInfo> buyBoxes = new List<BoxDataInfo>();
            BoxBase tmpBase = new BoxBase(width);
            BoxBase tmpMatchBase;
            BoxHeight tmpHeight = new BoxHeight(height, amount);
            BoxHeight tmpMatchHeight;
            while (amountCounter < amount && splitCounter < splits)
            {
                bool matchBox = BstBase.SearchBestMatch(tmpBase, out tmpMatchBase);
                if (!matchBox) break;
                else
                {
                    matchBox = tmpMatchBase.BstHeight.SearchBestMatch(tmpHeight, out tmpMatchHeight);
                    if (!matchBox && amountCounter > 0)
                    {
                        tmpBase = new BoxBase(tmpMatchBase.Width + 1);
                    }
                    else if (!matchBox) break;
                    else
                    {
                        if (tmpMatchHeight.Amount <= (amount - amountCounter))
                        {
                            buyBoxes.Add(new BoxDataInfo(tmpMatchBase, tmpMatchHeight, false, tmpMatchHeight.Amount));
                            amountCounter += tmpMatchHeight.Amount;
                            splitCounter++;
                            tmpHeight = new BoxHeight(tmpMatchHeight.Height + 1, amount);
                        }
                        else
                        {
                            buyBoxes.Add(new BoxDataInfo(tmpMatchBase, tmpMatchHeight, true, amount - amountCounter));
                            splitCounter = splits;
                        }
                    }
                }
            }

            string listPrint = PrintList(buyBoxes);
            if (buyBoxes.Count == 0)
            {
                notifier.OnInfo("We did not find what you were looking for in our box inventory!");
                return false;
            }
            bool answer = notifier.Purchase($"Would you like to buy {listPrint}?");
            if (answer)
            {
                foreach (var box in buyBoxes)
                {
                    if (!box.Blank) OutOfStock(box.BoxHeights, box.BoxBase, box.BoxHeight);
                    else
                    {
                        box.BoxHeight.Amount -= box.Amount;
                        if (box.BoxHeight.Amount < minAmount)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            notifier.OnInfo("The number of boxes has dropped below the set minimum!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        timeList.ChangeNodeLocation(box.BoxHeight.NodeBox);
                        box.BoxHeight.TimeData.LastTimeSold = DateTime.Now;
                    }
                }
                return true;
            }
            return false;
        }

        private string PrintList(List<BoxDataInfo> boxInfo)
        {
            StringBuilder s = new StringBuilder();
            s.Append("What We have got- ");
            foreach (BoxDataInfo box in boxInfo)
            {
                s.Append($"width: {box.X} X {box.Y} :height\namount: {box.Amount} \nIf there is box in stock: {box.Blank}.");
            }
            return s.ToString();
        }

        private void OutOfStock(BST<BoxHeight> heightTree, BoxBase width, BoxHeight height)
        {
            if (height.NodeBox.Value == null)
            {
                notifier.OnInfo($"Deleted from stock : {width.Width}, {height.Height}\nStock is empty!");
                return;
            }
            foreach (var item in heightTree)
            {
                if (width.BstHeight.RootProp == null)
                    BstBase.Remove(width);
                notifier.OnInfo($"Deleted from stock : {width.Width}, {height.Height}");
                timeList.DeleteNode(height.NodeBox.Value);
                heightTree.Remove(height);
            }
        }
    }
}
