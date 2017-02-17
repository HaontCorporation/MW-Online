using NFSScript.MW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Online
{
    public static class GameDialog
    {
        public static bool shown = false;

        #region Menu
        public static bool isInMn = false;
        public static int MenuIdx = 0;
        public static string[] MenuText;
        public static string MenuResponse = "";
        #endregion

        public static void ShowStableDialog(int id, object text)
        {
            UI.ShowDialogBox(id, text.ToString());
            shown = true;
        }
        public static void ShowMenu()
        {
            isInMn = true;
            StringBuilder builder = new StringBuilder();
            int k = 0;
            while (k != MenuText.Length)
            {
                if (k == MenuIdx) builder.AppendLine("> " + MenuText[k]);
                else builder.AppendLine("  " + MenuText[k]);
                k++;
            }
            ShowStableDialog(0, builder.ToString());
        }
    }
}
