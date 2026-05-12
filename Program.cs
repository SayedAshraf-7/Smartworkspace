// ================================================
// FILE: Program.cs
// ================================================
using SmartWorkSpaceVS;
using System;
using System.Windows.Forms;

namespace SmartWorkspace
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
