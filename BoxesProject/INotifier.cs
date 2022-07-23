using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public interface INotifier
    {
        void OnInfo(string info);
        void OnError(string error);
        bool On(string question);
        void OnOption(string optionMessage);
        bool Purchase(string question);
        void NotificationMessageBox(string message);
    }
}
