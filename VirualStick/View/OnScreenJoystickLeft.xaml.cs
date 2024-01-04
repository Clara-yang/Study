using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirualStick.View
{
    /// <summary>
    /// OnScreenJoystick.xaml 的交互逻辑
    /// </summary>
    public partial class OnScreenJoystickLeft : UserControl
    {
        // 从0到360的当前角度(以°为单位)
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(OnScreenJoystickLeft), null);
         
        // 多久引发一次StickMove事件（以度为单位）
        public static readonly DependencyProperty AngleStepProperty =
            DependencyProperty.Register("AngleStep", typeof(double), typeof(OnScreenJoystickLeft), new PropertyMetadata(1.0));

        // 当前距离
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(double), typeof(OnScreenJoystickLeft), null);

        // 以距离为单位的StickMove活动应多久举行一次
        public static readonly DependencyProperty DistanceStepProperty =
            DependencyProperty.Register("DistanceStep", typeof(double), typeof(OnScreenJoystickLeft), new PropertyMetadata(1.0));

        // 当前距离
        public static readonly DependencyProperty leftMovedXProperty =
            DependencyProperty.Register("leftMovedX", typeof(double), typeof(OnScreenJoystickLeft), null);

        // 以距离为单位的StickMove活动应多久举行一次
        public static readonly DependencyProperty leftMovedXStepProperty =
            DependencyProperty.Register("leftMovedXStep", typeof(double), typeof(OnScreenJoystickLeft), new PropertyMetadata(1.0));

        // 当前距离
        public static readonly DependencyProperty leftMovedYProperty =
            DependencyProperty.Register("leftMovedY", typeof(double), typeof(OnScreenJoystickLeft), null);

        // 以距离为单位的StickMove活动应多久举行一次
        public static readonly DependencyProperty leftMovedYStepProperty =
            DependencyProperty.Register("leftMovedYStep", typeof(double), typeof(OnScreenJoystickLeft), new PropertyMetadata(1.0));
         
        // 从0到360的当前角度(以°为单位)
        public double Angle
        {
            get { return Convert.ToDouble(GetValue(AngleProperty)); }
            private set { SetValue(AngleProperty, value); }
        } 
        // 多久引发一次StickMove事件（以度为单位）
        public double AngleStep
        {
            get { return Convert.ToDouble(GetValue(AngleStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(AngleStepProperty, Math.Round(value));
            }
        }

        // 当前距离（或“功率”）
        public double Distance
        {
            get { return Convert.ToDouble(GetValue(DistanceProperty)); }
            private set { SetValue(DistanceProperty, value); }
        }
        // 以距离为单位的StickMove活动应多久举行一次
        public double DistanceStep
        {
            get { return Convert.ToDouble(GetValue(DistanceStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(DistanceStepProperty, value);
            }
        }

        // 当前垂直距离
        public double leftMovedY
        {
            get { return Convert.ToDouble(GetValue(leftMovedYProperty)); }
            private set { SetValue(leftMovedYProperty, value); }
        }
        // 以距离为单位的StickMove活动应多久举行一次
        public double leftMovedYStep
        {
            get { return Convert.ToDouble(GetValue(leftMovedYStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(leftMovedYStepProperty, value);
            }
        }

        // 当前水平距离
        public double leftMovedX
        {
            get { return Convert.ToDouble(GetValue(leftMovedXProperty)); }
            private set { SetValue(leftMovedXProperty, value); }
        }  
        // 以距离为单位的StickMove活动应多久举行一次
        public double leftMovedXStep
        {
            get { return Convert.ToDouble(GetValue(leftMovedXStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(leftMovedXStepProperty, value);
            }
        } 

        /// <summary>委托抓住操纵手柄状态更改的数据</summary>
        /// <param name="sender"></param>
        /// <param name="args">保留角度和距离的新值</param>
        public delegate void OnScreenJoystickEventHandler(OnScreenJoystickLeft sender, VirtualJoystickEventArgs args);

        /// <summary>无数据操纵杆事件的委托</summary>
        /// <param name="sender"></param>
        public delegate void EmptyJoystickEventHandler(OnScreenJoystickLeft sender);

        /// <summary>角度和距离的新值</summary>
        public event OnScreenJoystickEventHandler Moved;

        /// <summary>只要操纵手柄移动，就会触发此事件</summary>
        public event EmptyJoystickEventHandler Released;

        /// <summary>一旦松开操纵手柄并重置其位置，就会触发此事件</summary>
        public event EmptyJoystickEventHandler Captured;

        private Point _startPosLeft; //左边摇杆鼠标按下的位置
        private double _prevAngle, _prevDistance;
        private readonly Storyboard centerKnobLeft;
         
        public OnScreenJoystickLeft()
        {
            InitializeComponent();

            KnobLeft.MouseLeftButtonDown += KnobLeft_MouseLeftButtonDown;
            KnobLeft.MouseLeftButtonUp += KnobLeft_MouseLeftButtonUp;
            KnobLeft.MouseMove += KnobLeft_MouseMove; 
            centerKnobLeft = KnobLeft.Resources["CenterKnobLeft"] as Storyboard;
        }

        /// <summary>
        /// 左边摇杆鼠标左键按下时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnobLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_startPosLeft.X == 0 && _startPosLeft.Y == 0)
            {
                _startPosLeft = e.GetPosition(BaseLeft);
            }
            _prevAngle = _prevDistance = 0; 
            Captured?.Invoke(this);
            KnobLeft.CaptureMouse(); // 捕获该元素的鼠标
            centerKnobLeft.Stop();
        }

        // 鼠标移动事件
        private void KnobLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (!KnobLeft.IsMouseCaptured) return; 

            Point newPos = e.GetPosition(BaseLeft); 
            Point deltaPos = new Point(newPos.X - _startPosLeft.X, newPos.Y - _startPosLeft.Y);
             
            double angle = Math.Atan2(deltaPos.Y, deltaPos.X) * 180 / Math.PI;
            if (angle > 0)
                angle += 90;
            else
            {
                angle = 270 + (180 + angle);
                if (angle >= 360) angle -= 360;
            } 
            // Math.Sqrt:平方根；Math.Atan2:反切(正切的倒数)
            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y) / 101 * 100);
            if (distance <= 100)
            {
                Angle = angle;
                Distance = distance;

                knobPositionLeft.X = deltaPos.X;
                knobPositionLeft.Y = deltaPos.Y;

                if (Moved == null ||
                    (!(Math.Abs(_prevAngle - angle) > AngleStep) && !(Math.Abs(_prevDistance - distance) > DistanceStep)))
                    return;

                Moved?.Invoke(this, new VirtualJoystickEventArgs { Angle = Angle, Distance = Distance, leftMovedX = deltaPos.X, leftMovedY = deltaPos.Y });
                _prevAngle = Angle;
                _prevDistance = Distance;
            }
        }

        private void KnobLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            KnobLeft.ReleaseMouseCapture();
            centerKnobLeft.Begin();
        } 

        private void centerKnobLeft_Completed(object sender, EventArgs e) 
        {
            Angle = Distance = _prevAngle = _prevDistance = 0;
            Released?.Invoke(this);
        }
    }
}
