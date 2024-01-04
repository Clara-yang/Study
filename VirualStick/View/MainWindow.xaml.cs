using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirualStick.View;

namespace VirualStick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            onScreenJoystickLeft.Moved += VirtualJoystickLeft_Moved;
            onScreenJoystickRight.Moved += VirtualJoystickRight_Moved;
        }

        private void VirtualJoystickLeft_Moved(object sender, VirualStick.VirtualJoystickEventArgs args)
        {
            leftHorizational.Text = $"leftHorizontal Distance:{(int)args.leftMovedX}, Joystick Angle:{(int)args.Angle}";
            leftVertical.Text = $"leftVertical    Distance:{(int)args.leftMovedY}, Joystick Angle:{(int)args.Angle}";
        }

        private void VirtualJoystickRight_Moved(object sender, VirualStick.VirtualJoystickEventArgs args)
        {
            rightHorizational.Text = $"rightHorizontal Distance:{(int)args.righMovedX}, Joystick Angle:{(int)args.Angle}";
            rightVertical.Text = $"rightVertical    Distance:{(int)args.righMovedY}, Joystick Angle:{(int)args.Angle}";
        }
    }
}
