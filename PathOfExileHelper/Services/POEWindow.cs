using System;
using System.Runtime.InteropServices;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace PathOfExileHelper.Services
{
    public class POEWindow
    {
        private static readonly string WindowClass = "POEWindowClass";
        private static readonly string WindowName = "Path of Exile";

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern IntPtr GetForegroundWindow();


        public static bool IsWindowActive()
        {
            return FindWindow(WindowClass, WindowName) == GetForegroundWindow();
        }

        public static void MakeActive(IntPtr? PoeHandle = null)
        {
            if (PoeHandle == null)
            {
                PoeHandle = FindPoeWindow();
            }

            // Need to press ALT because the SetForegroundWindow sometimes does not work
            //iSim.Keyboard.KeyPress(VirtualKeyCode.MENU);

            SetForegroundWindow((IntPtr)PoeHandle);
        }

        private static IntPtr FindPoeWindow()
        {
            IntPtr PoeHandle = FindWindow(WindowClass, WindowName);
            if (PoeHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(WindowName + " is not Running");
            }

            return PoeHandle;
        }

        public static void SendInputToPoe(string input)
        {
            IntPtr PoeHandle = FindPoeWindow();

            InputSimulator iSim = new InputSimulator();

            MakeActive(PoeHandle);

            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            iSim.Keyboard.TextEntry(input);

            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            iSim = null;
        }

        public static void SendInputToPoeNoSubmit(string input)
        {
            IntPtr PoeHandle = FindPoeWindow();

            InputSimulator iSim = new InputSimulator();

            MakeActive(PoeHandle);

            iSim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            iSim.Keyboard.TextEntry(input);

            iSim = null;
        }
    }
}
