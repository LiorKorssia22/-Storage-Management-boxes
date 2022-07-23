using BoxesProject;
using System;
using System.Windows.Forms;

namespace Porgram
{
    internal class NotificationImplementation : INotifier
    {
        public void NotificationMessageBox(string message)
        {
            MessageBox.Show(message, "Notice!");
        }
        public bool On(string question)
        {
            if (MessageBox.Show
                       (question, "",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question)
                       == DialogResult.Yes)
            {
                return true;
            }
            else
                return false;
        }
        public void OnError(string error)
        {
            Console.WriteLine($"An error has been found: {error}");
        }

        public void OnInfo(string info)
        {
            Console.WriteLine(info);
        }

        public void OnOption(string optionMessage)
        {
            Console.WriteLine($"We could'nt find the box you were looking for ,but a suitable suggestion has been found: {optionMessage}"); 
        }
        public bool Purchase(string question)
        {
            if (MessageBox.Show
                      (question, "",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question)
                      == DialogResult.Yes)
            {
                return true;
            }
            else
                return false;
        }
    }
}
