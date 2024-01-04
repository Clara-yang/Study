using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace Plot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class chartCanvas : Window
    {
        private List<Point> dataPoints = new List<Point>();
        private List<Point> dataEastPoints = new List<Point>();
        private List<Point> dataOrigionPoints = new List<Point>();
        private PointCollection coordinateNorthPoints = new PointCollection();
        private PointCollection coordinateEastPoints = new PointCollection();
        private List<Ellipse> pointEllipses = new List<Ellipse>();
        private List<Ellipse> pointEastEllipses = new List<Ellipse>();
        private List<Ellipse> pointOrigionEllipses = new List<Ellipse>();

        private Queue<double> dataQueue = new Queue<double>(300);
        private int num = 2;//每次删除增加几个点

        DispatcherTimer timer;

        public chartCanvas()
        {
            InitializeComponent();
            DrawScale();
            DrawScaleLabel();
            DrawCurve();
            DrawPoint();
            LoadChartHeight();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += new EventHandler(RegularRefresh);
            timer.Start();
        }

        private void LoadChartHeight()
        {
            chartHeight.Series.Clear();
            ChartArea chartHeightArea = new ChartArea("C1");
            chartHeight.ChartAreas.Add(chartHeightArea);
            //ChartHelper.AddSeries(chartHeight, "随时间变化的高度", SeriesChartType.Line, System.Drawing.Color.White, System.Drawing.Color.White, true);
            ChartHelper.CreatSecondaryXY(chartHeight, "随时间变化的高度", SeriesChartType.Point, true);
            ChartHelper.SetTitle(chartHeight, string.Empty, new Font("微软雅黑", 12), Docking.Bottom, System.Drawing.Color.White);
            ChartHelper.SetStyle(chartHeight, System.Drawing.Color.Transparent);
            ChartHelper.SetXY(chartHeight, "时间/min", "高度/m", StringAlignment.Far, 200, 5,
                TickMarkStyle.OutsideArea, "", "", 300, 20, 0, 0, AxisArrowStyle.Triangle, AxisArrowStyle.Triangle, true, true);
            ChartHelper.SetMajorGrid(chartHeight, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent);
        }

        /// <summary>
        /// 作出x轴和y轴的标尺
        /// </summary>
        private void DrawScale()
        {
            #region chartNorth中Y轴的刻度线
            for (int i = 0; i < 7; i++)
            {
                //作出Y轴的刻度
                Line y_scale = new Line();
                y_scale.StrokeEndLineCap = PenLineCap.Triangle;
                y_scale.StrokeThickness = 1;
                y_scale.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));

                Line x_scale = new Line();
                x_scale.X1 = 420;           //原点x=420,只显示Y轴刻度
                x_scale.X2 = x_scale.X1;    //在x轴上的刻度线，起点和终点相同

                y_scale.X1 = 420;            //原点x=420，在y轴上的刻度线的起点与原点相同
                y_scale.X2 = y_scale.X1 + 4;//刻度线长度为4px 

                y_scale.Y1 = 360 - i * 50;  //每50px作一个刻度,从-15开始往上标记刻度
                y_scale.Y2 = y_scale.Y1;    //起点和终点y坐标相同 
                this.chartNorth.Children.Add(y_scale);
            }
            #endregion

            #region chartEast中X轴的刻度线
            for (int i = 0; i < 7; i++)
            {
                //作出Y轴的刻度
                Line x_scale = new Line();
                x_scale.StrokeEndLineCap = PenLineCap.Triangle;
                x_scale.StrokeThickness = 1;
                x_scale.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));

                Line y_scale = new Line();
                y_scale.Y1 = 360;           //原点x=360,只显示x轴刻度
                y_scale.Y2 = y_scale.Y1;    //在y轴上的刻度线，起点和终点相同

                x_scale.Y1 = 360;            //原点x=360，在x轴上的刻度线的起点与原点相同
                x_scale.Y2 = x_scale.Y1 + 4;//刻度线长度为4px 

                x_scale.X1 = 410 - i * 50;  //每50px作一个刻度,从15开始往左标记刻度
                x_scale.X2 = x_scale.X1;    //起点和终点x坐标相同 
                this.chartEast.Children.Add(x_scale);
            }
            #endregion

            #region 左下角chart刻度线
            //for (int i = 0; i < 45; i += 1)//作480个刻度，因为当前x轴长 480px，每10px作一个小刻度，还预留了一些小空间
            //{
            //    //原点 O=(40,280)
            //    Line x_scale = new Line();
            //    x_scale.StrokeEndLineCap = PenLineCap.Triangle;
            //    x_scale.StrokeThickness = 1;
            //    x_scale.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //    x_scale.X1 = 40 + i * 10;   //原点x=40,每10px作1个刻度
            //    x_scale.X2 = x_scale.X1;    //在x轴上的刻度线，起点和终点相同

            //    x_scale.Y1 = 280;           //与原点坐标的y=280，相同 
            //    if (i % 5 == 0)//每5个刻度添加一个大刻度
            //    {
            //        x_scale.StrokeThickness = 3;//把刻度线加粗一点
            //        x_scale.Y2 = x_scale.Y1 - 8;//刻度线长度为8px 
            //    }
            //    else
            //    {
            //        x_scale.Y2 = x_scale.Y1 - 4;//刻度线长度为4px 
            //    }

            //    if (i < 25)//由于y轴短一些，所以在此作出判断，只作25个刻度
            //    {
            //        //作出Y轴的刻度
            //        Line y_scale = new Line();
            //        y_scale.StrokeEndLineCap = PenLineCap.Triangle;
            //        y_scale.StrokeThickness = 1;
            //        y_scale.Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //        y_scale.X1 = 40;            //原点x=40，在y轴上的刻度线的起点与原点相同
            //        if (i % 5 == 0)
            //        {
            //            y_scale.StrokeThickness = 3;
            //            y_scale.X2 = y_scale.X1 + 8;//刻度线长度为8px 
            //        }
            //        else
            //        {
            //            y_scale.X2 = y_scale.X1 + 4;//刻度线长度为4px 
            //        }

            //        y_scale.Y1 = 280 - i * 10;  //每10px作一个刻度 
            //        y_scale.Y2 = y_scale.Y1;    //起点和终点y坐标相同 
            //        this.chartCanvas.Children.Add(y_scale);
            //    }
            //    this.chartCanvas.Children.Add(x_scale);
            //}
            #endregion
        }

        /// <summary>
        /// 添加刻度标签
        /// </summary>
        private void DrawScaleLabel()
        {
            #region chartNorth刻度值
            for (int i = -3; i < 4; i++)
            {
                TextBlock y_ScaleLabel = new TextBlock();

                y_ScaleLabel.Text = (i * 5).ToString();
                Canvas.SetLeft(y_ScaleLabel, 470 - 40); //-35px是字体大小的偏移 
                Canvas.SetTop(y_ScaleLabel, 210 - 5 * 10 * i - 10);  //210px是0的坐标

                this.chartNorth.Children.Add(y_ScaleLabel);
            }
            #endregion

            #region chartEast刻度值
            for (int i = -3; i < 4; i++)
            {
                TextBlock x_ScaleLabel = new TextBlock();

                x_ScaleLabel.Text = (i * 5).ToString();
                Canvas.SetTop(x_ScaleLabel, 360 + 5); //10px是字体大小的偏移 
                Canvas.SetLeft(x_ScaleLabel, 260 + 5 * 10 * i - 10);  //260px是0的坐标

                this.chartEast.Children.Add(x_ScaleLabel);
            }
            #endregion

            #region 左下角chart刻度值
            //for (int i = 1; i < 7; i++)//7 个标签，一共
            //{
            //    TextBlock x_ScaleLabel = new TextBlock();
            //    TextBlock y_ScaleLabel = new TextBlock();

            //    x_ScaleLabel.Text = (i * 50).ToString();//只给大刻度添加标签，每50px添加一个标签

            //    Canvas.SetLeft(x_ScaleLabel, 40 + 5 * 10 * i - 12);//40是原点的坐标，-12是为了让标签看的位置剧中一点
            //    Canvas.SetTop(x_ScaleLabel, 280 + 2);//让标签字往下移一点

            //    y_ScaleLabel.Text = (i * 50).ToString();
            //    Canvas.SetLeft(y_ScaleLabel, 40 - 25);              //-25px是字体大小的偏移
            //    Canvas.SetTop(y_ScaleLabel, 280 - 5 * 10 * i - 6);  //280px是原点的坐标，同样-6是为了让标签不要上坐标轴叠上

            //    this.chartCanvas.Children.Add(x_ScaleLabel);
            //    this.chartCanvas.Children.Add(y_ScaleLabel);
            //}
            #endregion
        }

        /// <summary>
        /// 绘制点
        /// </summary>
        private void DrawPoint()
        {
            //随机生成8个点
            Random rPoint = new Random();
            for (int i = 0; i < 8; i++)
            {
                int x_point = i * 50;
                int y_point = rPoint.Next(200);
                dataPoints.Add(new Point(x_point, y_point));

                int x_eastPoint = rPoint.Next(200);
                int y_eastPoint = i * 50;
                dataEastPoints.Add(new Point(x_eastPoint, y_eastPoint));

                int x_origionPoint = rPoint.Next(50);
                int y_origionPoint = i * 10;
                dataOrigionPoints.Add(new Point(x_eastPoint, y_eastPoint));
            }

            for (int i = 0; i < dataPoints.Count; i++)
            {
                Ellipse dataNorthEllipse = new Ellipse();
                dataNorthEllipse.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0xff));
                dataNorthEllipse.Width = 4;
                dataNorthEllipse.Height = 4;
                Canvas.SetRight(dataNorthEllipse, 70 + dataPoints[i].X - 2);//-2是为了补偿圆点的大小，到精确的位置
                Canvas.SetTop(dataNorthEllipse, 60 + dataPoints[i].Y - 2);
                pointEllipses.Add(dataNorthEllipse);
                chartNorth.Children.Add(dataNorthEllipse);

                Ellipse dataEastEllipse = new Ellipse();
                dataEastEllipse.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0xff));
                dataEastEllipse.Width = 4;
                dataEastEllipse.Height = 4;
                Canvas.SetBottom(dataEastEllipse, 38 + dataEastPoints[i].Y - 2);
                Canvas.SetLeft(dataEastEllipse, 110 + dataEastPoints[i].X - 2);
                pointEastEllipses.Add(dataEastEllipse);
                chartEast.Children.Add(dataEastEllipse);

                Ellipse dataOrigionEllipse = new Ellipse();
                dataOrigionEllipse.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0xff));
                dataOrigionEllipse.Width = 4;
                dataOrigionEllipse.Height = 4;
                Canvas.SetTop(dataOrigionEllipse, 110 + dataEastPoints[i].Y - 2);
                Canvas.SetBottom(dataOrigionEllipse, 110 + dataEastPoints[i].Y - 2);
                Canvas.SetLeft(dataOrigionEllipse, 210 + dataEastPoints[i].X - 2);
                Canvas.SetRight(dataOrigionEllipse, 210 + dataEastPoints[i].X - 2);
                pointOrigionEllipses.Add(dataOrigionEllipse);
                chartOrigion.Children.Add(dataOrigionEllipse);
            }
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private void DrawCurve()
        {
            Polyline curveNorthPolyline = new Polyline();
            curveNorthPolyline.Stroke = Brushes.Green;
            curveNorthPolyline.StrokeThickness = 2;
            curveNorthPolyline.Points = coordinateNorthPoints;
            chartNorth.Children.Add(curveNorthPolyline);

            Polyline curveEastPolyline = new Polyline();
            curveEastPolyline.StrokeThickness = 2;
            curveEastPolyline.Points = coordinateEastPoints;
            chartEast.Children.Add(curveEastPolyline);
        }

        /// <summary>
        /// 绘制曲线点(变化的曲线)北-高度
        /// </summary>
        /// <param name="dataPoint"></param>
        private void AddNorthCurvePoint(Point dataPoint)
        {
            dataPoints.RemoveAt(dataPoints.Count - 1);
            dataPoints.Insert(0, dataPoint);
            for (int i = 0; i < dataPoints.Count; i++)
            {
                dataPoints[i] = new Point(dataPoints[i].X + 1, dataPoints[i].Y);

                Canvas.SetRight(pointEllipses[i], 70 + dataPoints[i].X - 2);//-2是为了补偿圆点的大小，到精确的位置
                Canvas.SetTop(pointEllipses[i], 60 + dataPoints[i].Y - 2);
            }
        }

        /// <summary>
        /// 绘制曲线点(变化的曲线)东-高度
        /// </summary>
        /// <param name="dataPoint"></param>
        private void AddEastCurvePoint(Point dataPoint)
        {
            dataPoints.RemoveAt(dataPoints.Count - 1);
            dataPoints.Insert(0, dataPoint);
            for (int i = 0; i < dataPoints.Count; i++)
            {
                dataPoints[i] = new Point(dataPoints[i].X, dataPoints[i].Y + 1);

                Canvas.SetLeft(pointEastEllipses[i], 110 + dataPoints[i].X - 2);
                Canvas.SetBottom(pointEastEllipses[i], 70 + dataPoints[i].Y - 2);
            }
        }

        /// <summary>
        /// 绘制曲线点(变化的曲线)原点
        /// </summary>
        /// <param name="dataPoint"></param>
        private void AddOrigionCurvePoint(Point dataPoint)
        {
            dataPoints.RemoveAt(dataPoints.Count - 1);
            dataPoints.Insert(0, dataPoint);
            for (int i = 0; i < dataPoints.Count; i++)
            {
                dataPoints[i] = new Point(dataPoints[i].X + 1, dataPoints[i].Y + 1);

                Canvas.SetTop(pointOrigionEllipses[i], 110 + dataPoints[i].Y - 2);
                //Canvas.SetBottom(pointOrigionEllipses[i], 110 + dataEastPoints[i].Y - 2);
                Canvas.SetLeft(pointOrigionEllipses[i], 110 + dataPoints[i].X - 2);
                //Canvas.SetRight(pointOrigionEllipses[i], 210 + dataEastPoints[i].X - 2); 
            }
        }

        /// <summary>
        /// 刷新点位 左下角chart
        /// </summary>
        private void UpdateQueueValue()
        {
            Random r = new Random();
            if (dataQueue.Count > 300)
            {
                for (int i = 0; i < num; i++)
                {
                    dataQueue.Dequeue();
                }
            }
            for (int i = 0; i < num; i++)
            {
                //对象添加到 System.Collections.Generic.Queue<T> 的结尾处。用于存放绘图点(y轴数据点) 随机数
                dataQueue.Enqueue(r.Next(0, 300));
            }
        }

        /// <summary>
        /// 定时刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularRefresh(object sender, EventArgs e)
        {
            //随机生成Y坐标
            Point dataNorthPoint = new Point(new Random().Next(200), new Random().Next(200));
            AddNorthCurvePoint(dataNorthPoint);

            Point dataEastPoint = new Point(new Random().Next(200), new Random().Next(200));
            AddEastCurvePoint(dataEastPoint);

            Point dataOrigionPoint = new Point(new Random().Next(50), new Random().Next(50));
            AddOrigionCurvePoint(dataOrigionPoint);

            UpdateQueueValue();
            this.chartHeight.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue.Count; i++)
            {
                this.chartHeight.Series[0].Points.AddXY((i + 1), dataQueue.ElementAt(i));
            }
        }

        private void chartEast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point dataEastPoint = new Point(new Random().Next(200), new Random().Next(200));
            //AddEastCurvePoint(dataEastPoint);
        }

        private void chartNorth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point dataNorthPoint = new Point(new Random().Next(200), new Random().Next(200));
            //AddNorthCurvePoint(dataNorthPoint);
        }

        private void chartOrigion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point dataOrigionPoint = new Point(new Random().Next(50), new Random().Next(50));
            //AddOrigionCurvePoint(dataOrigionPoint);
        }
    }
}
