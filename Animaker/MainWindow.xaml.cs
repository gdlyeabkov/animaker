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
using System.Speech.Synthesis;
using MaterialDesignThemes.Wpf;

namespace Animaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public SpeechSynthesizer debugger;
        public List<Dictionary<String, Object>> keyframes;
        public List<PointAnimation> animations;
        public int selectedPoint = 1;
        public string mode = "Animation";
        public BezierSegment bezierSegment;
        public int drawCurveStep = 1;
        public List<BezierSegment> bezierSegments;
        public List<Path> paths;
        public List<PathFigure> figures;

        public MainWindow()
        {
            InitializeComponent();

            debugger = new SpeechSynthesizer();

            keyframes = new List<Dictionary<String, Object>>();

            animations = new List<PointAnimation>();

            Dictionary<String, Object> defaultKeyframe = new Dictionary<String, Object>();
            defaultKeyframe.Add("x1", 0);
            defaultKeyframe.Add("y1", 0);
            defaultKeyframe.Add("x2", 35);
            defaultKeyframe.Add("y2", 200);
            defaultKeyframe.Add("x3", 100);
            defaultKeyframe.Add("y3", 0);
            defaultKeyframe.Add("meta", 0);
            keyframes.Add(defaultKeyframe);

            DrawKeyFramesLabels();

            bezierSegment = mainCurve;

            bezierSegments = new List<BezierSegment>()
            {
                mainCurve
            };
            paths = new List<Path>()
            {
                curve
            };
            figures = new List<PathFigure>()
            {
                startFigure
            };

        }

        private void PlayAnimationHandler(object sender, MouseButtonEventArgs e)
        {

            curve.Stroke = Brushes.Black;

            pointSelector.Visibility = Visibility.Collapsed;
            pointOne.Visibility = Visibility.Collapsed;
            pointTwo.Visibility = Visibility.Collapsed;
            pointThree.Visibility = Visibility.Collapsed;

            animations.Clear();
            PointAnimation defautlStateAnimation = new PointAnimation();
            defautlStateAnimation.Duration = new Duration(TimeSpan.FromSeconds(0));
            foreach (Dictionary<String, Object> keyframe in keyframes)
            {
                if (keyframes.IndexOf(keyframe) != keyframes.Count - 1) {
                    if (keyframes.IndexOf(keyframe) == 0)
                    {
                        defautlStateAnimation.From = new Point(((int)(keyframe["x1"])), ((int)(keyframe["y1"])));
                        defautlStateAnimation.To = new Point(((int)(keyframe["x1"])), ((int)(keyframe["y1"])));
                    }
                    PointAnimation curveAnimation = new PointAnimation();
                    curveAnimation.From = new Point(((int)(keyframe["x1"])), ((int)(keyframe["y1"])));
                    curveAnimation.To = new Point(((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["x1"])), ((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["y1"])));
                    curveAnimation.Duration = new Duration(TimeSpan.FromSeconds((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25));
                    debugger.Speak("продолжительность анимации " + ((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25).ToString() + " кадров");
                    if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Синусоидная")
                    {
                        curveAnimation.EasingFunction = new SineEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квинтовая")
                    {
                        curveAnimation.EasingFunction = new QuinticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Четвертичная")
                    {
                        curveAnimation.EasingFunction = new QuarticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квадратичная")
                    {
                        curveAnimation.EasingFunction = new QuadraticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Силовая")
                    {
                        curveAnimation.EasingFunction = new PowerEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Экспоненциальная")
                    {
                        curveAnimation.EasingFunction = new ExponentialEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Эластичная")
                    {
                        curveAnimation.EasingFunction = new ElasticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Кубическая")
                    {
                        curveAnimation.EasingFunction = new CubicEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Круговая")
                    {
                        curveAnimation.EasingFunction = new CircleEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Скачащая")
                    {
                        curveAnimation.EasingFunction = new BounceEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Обратная")
                    {
                        curveAnimation.EasingFunction = new BackEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }

                    animations.Add(curveAnimation);

                    /*
                     *  начало
                     */

                    curveAnimation = new PointAnimation();
                    curveAnimation.From = new Point(((int)(keyframe["x2"])), ((int)(keyframe["y2"])));
                    curveAnimation.To = new Point(((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["x2"])), ((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["y2"])));
                    curveAnimation.Duration = new Duration(TimeSpan.FromSeconds((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25));
                    debugger.Speak("продолжительность анимации " + ((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25).ToString() + " кадров");
                    if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Синусоидная")
                    {
                        curveAnimation.EasingFunction = new SineEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квинтовая")
                    {
                        curveAnimation.EasingFunction = new QuinticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Четвертичная")
                    {
                        curveAnimation.EasingFunction = new QuarticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квадратичная")
                    {
                        curveAnimation.EasingFunction = new QuadraticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Силовая")
                    {
                        curveAnimation.EasingFunction = new PowerEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Экспоненциальная")
                    {
                        curveAnimation.EasingFunction = new ExponentialEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Эластичная")
                    {
                        curveAnimation.EasingFunction = new ElasticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Кубическая")
                    {
                        curveAnimation.EasingFunction = new CubicEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Круговая")
                    {
                        curveAnimation.EasingFunction = new CircleEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Скачащая")
                    {
                        curveAnimation.EasingFunction = new BounceEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Обратная")
                    {
                        curveAnimation.EasingFunction = new BackEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }

                    animations.Add(curveAnimation);
                    //

                    curveAnimation = new PointAnimation();
                    curveAnimation.From = new Point(((int)(keyframe["x3"])), ((int)(keyframe["y3"])));
                    curveAnimation.To = new Point(((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["x3"])), ((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["y3"])));
                    curveAnimation.Duration = new Duration(TimeSpan.FromSeconds((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25));
                    debugger.Speak("продолжительность анимации " + ((((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["meta"])) - ((int)(keyframes[keyframes.IndexOf(keyframe)]["meta"]))) / 25).ToString() + " кадров");
                    if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Синусоидная")
                    {
                        curveAnimation.EasingFunction = new SineEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((SineEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квинтовая")
                    {
                        curveAnimation.EasingFunction = new QuinticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuinticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Четвертичная")
                    {
                        curveAnimation.EasingFunction = new QuarticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuarticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Квадратичная")
                    {
                        curveAnimation.EasingFunction = new QuadraticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((QuadraticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Силовая")
                    {
                        curveAnimation.EasingFunction = new PowerEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((PowerEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Экспоненциальная")
                    {
                        curveAnimation.EasingFunction = new ExponentialEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ExponentialEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Эластичная")
                    {
                        curveAnimation.EasingFunction = new ElasticEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((ElasticEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Кубическая")
                    {
                        curveAnimation.EasingFunction = new CubicEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CubicEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Круговая")
                    {
                        curveAnimation.EasingFunction = new CircleEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((CircleEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Скачащая")
                    {
                        curveAnimation.EasingFunction = new BounceEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BounceEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }
                    else if (((ComboBoxItem)(interpolation.Items[interpolation.SelectedIndex])).Content == "Обратная")
                    {
                        curveAnimation.EasingFunction = new BackEase();
                        if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход значений")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseInOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход из начала")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseOut;
                        }
                        else if (((ComboBoxItem)(interpolationType.Items[interpolationType.SelectedIndex])).Content == "Переход в значении")
                        {
                            ((BackEase)(curveAnimation.EasingFunction)).EasingMode = EasingMode.EaseIn;
                        }
                    }

                    animations.Add(curveAnimation);
                    /*
                     *  конец
                     */

                }
            }
            debugger.Speak("Всего анимаций: " + animations.Count);
            foreach (PointAnimation animation in animations)
            {
                animation.Completed += delegate
                {
                    // debugger.Speak("закончилась анимация");
                    if (animations.IndexOf(animation) < animations.Count - 1)
                    {
                        if ((animations.IndexOf(animation) + 1) % 3 == 0)
                        {
                            // debugger.Speak("дальше анимирую точку x1y1");
                            mainCurve.BeginAnimation(BezierSegment.Point1Property, animations[animations.IndexOf(animation) + 1]);
                        }
                        else if ((animations.IndexOf(animation) + 1) % 2 == 0)
                        {
                            // debugger.Speak("дальше анимирую точку x3y3");
                            mainCurve.BeginAnimation(BezierSegment.Point3Property, animations[animations.IndexOf(animation) + 1]); 
                        }
                        else if ((animations.IndexOf(animation) + 1) % 1 == 0)
                        {
                            // debugger.Speak("дальше анимирую точку x2y2");
                            mainCurve.BeginAnimation(BezierSegment.Point2Property, animations[animations.IndexOf(animation) + 1]);
                            
                        }
                        else
                        {
                            debugger.Speak("не могу анимировать точку");
                        }
                    }
                    else
                    {
                        debugger.Speak("конец анимации");
                        mainCurve.BeginAnimation(BezierSegment.Point1Property, null);
                        mainCurve.BeginAnimation(BezierSegment.Point2Property, null);
                        mainCurve.BeginAnimation(BezierSegment.Point3Property, null);
                        mainCurve.Point1 = new Point(((int)(((Dictionary<String, Object>)(keyframes[0]))["x1"])), ((int)(((Dictionary<String, Object>)(keyframes[0]))["y1"])));
                        mainCurve.Point2 = new Point(((int)(((Dictionary<String, Object>)(keyframes[0]))["x2"])), ((int)(((Dictionary<String, Object>)(keyframes[0]))["y2"])));
                        mainCurve.Point3 = new Point(((int)(((Dictionary<String, Object>)(keyframes[0]))["x3"])), ((int)(((Dictionary<String, Object>)(keyframes[0]))["y3"])));
                        timelineCursor.BeginAnimation(Line.X1Property, null);
                        timelineCursor.BeginAnimation(Line.X2Property, null);
                        xPostiion.Text = ((int)(keyframes[0]["x1"])).ToString();
                        yPostiion.Text = ((int)(keyframes[0]["y1"])).ToString();

                        pointSelector.Visibility = Visibility.Visible;
                        pointOne.Visibility = Visibility.Visible;
                        pointTwo.Visibility = Visibility.Visible;
                        pointThree.Visibility = Visibility.Visible;

                        curve.Stroke = Brushes.Cyan;

                    }
                };
            }
            if (animations.Count >= 1)
            {
                
                mainCurve.BeginAnimation(BezierSegment.Point1Property, animations[0]);
                /*mainCurve.BeginAnimation(BezierSegment.Point2Property, animations[1]);
                mainCurve.BeginAnimation(BezierSegment.Point3Property, animations[2]);*/

                DoubleAnimation cursorAnimation = new DoubleAnimation();
                cursorAnimation.From = ((double)(0));
                cursorAnimation.To = timeline.ActualWidth;
                int totalDuration = 0;
                // cursorAnimation.Duration = new Duration(TimeSpan.FromSeconds(totalDuration));
                timelineCursor.X1 = 0;
                timelineCursor.X2 = 0;
                timelineCursor.BeginAnimation(Line.X1Property, cursorAnimation);
                timelineCursor.BeginAnimation(Line.X2Property, cursorAnimation);

            }

        }

        private void SetTimelineCursorHandler(object sender, RoutedEventArgs e)
        {

            int cursor = ((int)(Mouse.GetPosition(timeline).X));
            timelineCursor.X1 = cursor;
            timelineCursor.X2 = cursor;
            List<Dictionary<String, Object>> possibleKeys = ((List<Dictionary<String, Object>>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()));
            bool isKeyExists = possibleKeys.Count >= 1;
            if (!isKeyExists)
            {
                // добавляем ключ
                debugger.Speak("добавляем ключ");
                Dictionary<String, Object>  keyframe = new Dictionary<String, Object>();
                keyframe.Add("x1", 0);
                keyframe.Add("y1", 0);
                keyframe.Add("x2", 35);
                keyframe.Add("y2", 200);
                keyframe.Add("x3", 100);
                keyframe.Add("y3", 0);
                keyframe.Add("meta", cursor);
                keyframes.Add(keyframe);
                
                if (selectedPoint == 1)
                {
                    xPostiion.Text = "0";
                    yPostiion.Text = "0";
                }
                else if (selectedPoint == 2)
                {
                    xPostiion.Text = "35";
                    yPostiion.Text = "200";
                }
                else if (selectedPoint == 3)
                {
                    xPostiion.Text = "100";
                    yPostiion.Text = "0";
                }
                figures[paths.IndexOf(curve)].StartPoint = new Point(0, 0);
                mainCurve.Point1 = new Point(0, 0);
                mainCurve.Point2 = new Point(35, 200);
                mainCurve.Point3 = new Point(100, 0);

                PackIcon keyFrameIcon = new PackIcon();
                keyFrameIcon.Width = 10;
                keyFrameIcon.Kind = PackIconKind.Key;
                keyFrameIcon.Foreground = Brushes.Yellow;
                ContextMenu keyFrameIconContextMenu = new ContextMenu();
                MenuItem keyFrameIconContextMenuItem = new MenuItem();
                keyFrameIconContextMenuItem.Header = "Удалить ключевой кадр";
                keyFrameIconContextMenuItem.DataContext = ((int)(keyframes.IndexOf(keyframe)));
                keyFrameIconContextMenuItem.Click += RemoveKeyFrameHandler;
                keyFrameIconContextMenu.Items.Add(keyFrameIconContextMenuItem);
                keyFrameIcon.ContextMenu = keyFrameIconContextMenu;
                Canvas.SetLeft(keyFrameIcon, cursor - keyFrameIcon.Width / 2);
                timeline.Children.Add(keyFrameIcon);

                // selectedPoint = 1;
                if (selectedPoint == 1)
                {
                    Canvas.SetLeft(pointSelector, mainCurve.Point1.X - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, mainCurve.Point1.Y - pointSelector.Height / 2);
                }
                else if (selectedPoint == 2)
                {
                    Canvas.SetLeft(pointSelector, mainCurve.Point2.X - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, mainCurve.Point2.Y - pointSelector.Height / 2);
                }
                else if (selectedPoint == 3)
                {
                    Canvas.SetLeft(pointSelector, mainCurve.Point3.X - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, mainCurve.Point3.Y - pointSelector.Height / 2);
                }
                Canvas.SetLeft(pointOne, mainCurve.Point1.X - pointOne.Width / 2);
                Canvas.SetTop(pointOne, mainCurve.Point1.Y - pointOne.Height / 2);
                Canvas.SetLeft(pointTwo, mainCurve.Point2.X - pointTwo.Width / 2);
                Canvas.SetTop(pointTwo, mainCurve.Point2.Y - pointTwo.Height / 2);
                Canvas.SetLeft(pointThree, mainCurve.Point3.X - pointThree.Width / 2);
                Canvas.SetTop(pointThree, mainCurve.Point3.Y - pointThree.Height / 2);

            } else
            {
                debugger.Speak("ключ уже существует");
                Dictionary<String, Object> key = possibleKeys[0];
                if (selectedPoint == 1)
                {
                    xPostiion.Text = ((int)(key["x1"])).ToString();
                    yPostiion.Text = ((int)(key["y1"])).ToString();
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["x1"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["y1"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 2)
                {
                    xPostiion.Text = ((int)(key["x2"])).ToString();
                    yPostiion.Text = ((int)(key["y2"])).ToString();
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["x2"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["y2"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 3)
                {
                    xPostiion.Text = ((int)(key["x3"])).ToString();
                    yPostiion.Text = ((int)(key["y3"])).ToString();
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["x3"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key)]["y3"])) - pointSelector.Height / 2);
                }
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key)]["x1"])), ((int)(keyframes[keyframes.IndexOf(key)]["y1"])));
                mainCurve.Point2 = new Point(((int)(keyframes[keyframes.IndexOf(key)]["x2"])), ((int)(keyframes[keyframes.IndexOf(key)]["y2"])));
                mainCurve.Point3 = new Point(((int)(keyframes[keyframes.IndexOf(key)]["x3"])), ((int)(keyframes[keyframes.IndexOf(key)]["y3"])));

                Canvas.SetLeft(pointOne, ((int)(keyframes[keyframes.IndexOf(key)]["x1"])) - pointOne.Width / 2);
                Canvas.SetTop(pointOne, ((int)(keyframes[keyframes.IndexOf(key)]["y1"])) - pointOne.Height / 2);
                Canvas.SetLeft(pointTwo, ((int)(keyframes[keyframes.IndexOf(key)]["x2"])) - pointTwo.Width / 2);
                Canvas.SetTop(pointTwo, ((int)(keyframes[keyframes.IndexOf(key)]["y2"])) - pointTwo.Height / 2);
                Canvas.SetLeft(pointThree, ((int)(keyframes[keyframes.IndexOf(key)]["x3"])) - pointThree.Width / 2);
                Canvas.SetTop(pointThree, ((int)(keyframes[keyframes.IndexOf(key)]["y3"])) - pointThree.Height / 2);
            
            }

        }

        private void SetKeyXPositionHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int value = Int32.Parse(((TextBox)(sender)).Text);
                int cursor = ((int)(timelineCursor.X1));
                Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
                if (selectedPoint == 1)
                {
                    keyframes[keyframes.IndexOf(key)]["x1"] = ((int)(value));
                    mainCurve.Point1 = new Point(value, mainCurve.Point1.Y);
                    Canvas.SetLeft(pointOne, value - pointOne.Width / 2);
                }
                else if (selectedPoint == 2)
                {
                    keyframes[keyframes.IndexOf(key)]["x2"] = ((int)(value));
                    mainCurve.Point2 = new Point(value, mainCurve.Point2.Y);
                    Canvas.SetLeft(pointTwo, value - pointTwo.Width / 2);
                }
                else if (selectedPoint == 3)
                {
                    keyframes[keyframes.IndexOf(key)]["x3"] = ((int)(value));
                    mainCurve.Point3 = new Point(value, mainCurve.Point3.Y);
                    Canvas.SetLeft(pointThree, value - pointThree.Width / 2);
                }
                Canvas.SetLeft(pointSelector, value - pointSelector.Width / 2);
            }
        }

        private void SetKeyYPositionHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int value = Int32.Parse(((TextBox)(sender)).Text);
                int cursor = ((int)(timelineCursor.X1));
                Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
                if (selectedPoint == 1)
                {
                    keyframes[keyframes.IndexOf(key)]["y1"] = ((int)(value));
                    mainCurve.Point1 = new Point(mainCurve.Point1.X, value);
                    Canvas.SetTop(pointOne, value - pointOne.Height / 2);
                }
                else if (selectedPoint == 2)
                {
                    keyframes[keyframes.IndexOf(key)]["y2"] = ((int)(value));
                    mainCurve.Point2 = new Point(mainCurve.Point2.X, value);
                    Canvas.SetTop(pointTwo, value - pointTwo.Height / 2);
                }
                else if (selectedPoint == 3)
                {
                    keyframes[keyframes.IndexOf(key)]["y3"] = ((int)(value));
                    mainCurve.Point3 = new Point(mainCurve.Point3.X, value);
                    Canvas.SetTop(pointThree, value - pointThree.Height / 2);
                }
                Canvas.SetTop(pointSelector, value - pointSelector.Height / 2);
            }
        }

        private void GlobalHotKeysHandler(object sender, KeyEventArgs e)
        {
            if (!xPostiion.IsFocused && !yPostiion.IsFocused)
            {
                if (e.Key == Key.Left)
                {
                    int cursor = ((int)(timelineCursor.X1)) - 1;
                    if (cursor >= 0)
                    {
                        timelineCursor.X1 = cursor;
                        timelineCursor.X2 = cursor;

                        if (keyframes.Count - 1 < cursor)
                        {
                            // добавляем ключ
                            debugger.Speak("добавляем ключ");
                            Dictionary<String, Object> keyframe = new Dictionary<String, Object>();
                            keyframe.Add("x1", 0);
                            keyframe.Add("y1", 0);
                            keyframe.Add("x2", 35);
                            keyframe.Add("y2", 200);
                            keyframe.Add("x3", 100);
                            keyframe.Add("y3", 0);
                            keyframe.Add("meta", cursor);
                            keyframes.Add(keyframe);
                            xPostiion.Text = "0";
                            yPostiion.Text = "0";

                            Canvas.SetLeft(pointOne, 0 - pointOne.Width / 2);
                            Canvas.SetLeft(pointOne, 0 - pointOne.Height / 2);
                            Canvas.SetLeft(pointTwo, 35 - pointTwo.Width / 2);
                            Canvas.SetLeft(pointTwo, 200 - pointTwo.Height / 2);
                            Canvas.SetLeft(pointThree, 0 - pointThree.Width / 2);
                            Canvas.SetLeft(pointThree, 100 - pointThree.Height / 2);

                        }
                        else
                        {
                            xPostiion.Text = ((int)(keyframes[cursor]["x1"])).ToString();
                            yPostiion.Text = ((int)(keyframes[cursor]["y1"])).ToString();
                        }
                    }
                }
                else if (e.Key == Key.Right)
                {
                    int cursor = ((int)(timelineCursor.X1)) + 1;
                    timelineCursor.X1 = cursor;
                    timelineCursor.X2 = cursor;
                    if (keyframes.Count - 1 < cursor)
                    {
                        // добавляем ключ
                        debugger.Speak("добавляем ключ");
                        Dictionary<String, Object> keyframe = new Dictionary<String, Object>();
                        keyframe.Add("x1", 0);
                        keyframe.Add("y1", 0);
                        keyframe.Add("x2", 0);
                        keyframe.Add("y2", 0);
                        keyframe.Add("x3", 0);
                        keyframe.Add("y3", 0);
                        keyframe.Add("meta", cursor);
                        keyframes.Add(keyframe);
                        xPostiion.Text = "0";
                        yPostiion.Text = "0";
                    }
                    else
                    {
                        xPostiion.Text = ((int)(keyframes[cursor]["x1"])).ToString();
                        yPostiion.Text = ((int)(keyframes[cursor]["y1"])).ToString();
                    }
                }
            }
        }

        private void ClearFocusHandler(object sender, MouseEventArgs e)
        {
            
            if (mode == "Animation")
            {
                Keyboard.ClearFocus();
                Point cursorPosition = e.GetPosition(canvas);
                int selectorThreshold = 0;
                if (curve.RenderedGeometry.StrokeContains(new Pen(), cursorPosition, selectorThreshold, ToleranceType.Absolute))
                {
                    debugger.Speak("animation");
                    if (mainCurve.Point1.X == cursorPosition.X || mainCurve.Point2.X == cursorPosition.X || mainCurve.Point3.X == cursorPosition.X)
                    {
                        Canvas.SetLeft(pointSelector, cursorPosition.X - pointSelector.Width / 2);
                        Canvas.SetTop(pointSelector, cursorPosition.Y - pointSelector.Height / 2);
                        pointSelector.Visibility = Visibility.Visible;
                        if (mainCurve.Point1.X == cursorPosition.X)
                        {
                            debugger.Speak("Точка 1");
                            selectedPoint = 1;
                            xPostiion.Text = mainCurve.Point1.X.ToString();
                            yPostiion.Text = mainCurve.Point1.Y.ToString();
                        }
                        else if (mainCurve.Point2.X == cursorPosition.X)
                        {
                            debugger.Speak("Точка 2");
                            selectedPoint = 2;
                            xPostiion.Text = mainCurve.Point2.X.ToString();
                            yPostiion.Text = mainCurve.Point2.Y.ToString();
                        }
                        else if (mainCurve.Point3.X == cursorPosition.X)
                        {
                            debugger.Speak("Точка 3");
                            selectedPoint = 3;
                            xPostiion.Text = mainCurve.Point3.X.ToString();
                            yPostiion.Text = mainCurve.Point3.Y.ToString();
                        }
                    }
                }
                else if (paths.Where((path) => path.RenderedGeometry.StrokeContains(new Pen(), cursorPosition, selectorThreshold, ToleranceType.Absolute)).ToList().Count() >= 1)
                {
                    curve.Stroke = Brushes.Black;
                    curve = ((Path)(paths.Where((path) => path.RenderedGeometry.StrokeContains(new Pen(), cursorPosition, selectorThreshold, ToleranceType.Absolute)).ToList()[0]));
                    mainCurve = ((BezierSegment)(((BezierSegment)(((PathSegmentCollection)(((PathFigure)(((PathFigureCollection)((PathGeometry)(curve.Data)).Figures))[0])).Segments))[0])));
                    curve.Stroke = Brushes.Cyan;
                    debugger.Speak("сменил кривую");

                    // здесь
                    selectedPoint = 1;
                    Canvas.SetLeft(pointSelector, mainCurve.Point1.X - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, mainCurve.Point1.Y - pointSelector.Height / 2);
                    Canvas.SetLeft(pointOne, mainCurve.Point1.X - pointOne.Width / 2);
                    Canvas.SetTop(pointOne, mainCurve.Point1.Y - pointOne.Height / 2);
                    Canvas.SetLeft(pointTwo, mainCurve.Point2.X - pointTwo.Width / 2);
                    Canvas.SetTop(pointTwo, mainCurve.Point2.Y - pointTwo.Height / 2);
                    Canvas.SetLeft(pointThree, mainCurve.Point3.X - pointThree.Width / 2);
                    Canvas.SetTop(pointThree, mainCurve.Point3.Y - pointThree.Height / 2);

                    xPostiion.Text = (((int)(mainCurve.Point1.X))).ToString();
                    yPostiion.Text = (((int)(mainCurve.Point1.Y))).ToString();

                }
            }
            else if (mode == "Draw")
            {
                if (drawCurveStep == 1)
                {
                    System.Windows.Shapes.Path penCurve = new System.Windows.Shapes.Path();
                    Brush foreGroundColor = Brushes.Black;
                    penCurve.Stroke = foreGroundColor;
                    double brushSizePts = 1;
                    penCurve.StrokeThickness = brushSizePts;
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigureCollection pathFigureCollection = new PathFigureCollection();
                    PathFigure pathFigure = new PathFigure();
                    PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
                    pathFigure.Segments = pathSegmentCollection;
                    pathFigure.StartPoint = Mouse.GetPosition(canvas);
                    pathFigureCollection.Add(pathFigure);
                    pathGeometry.Figures = pathFigureCollection;
                    penCurve.Data = pathGeometry;
                    canvas.Children.Add(penCurve);
                    bezierSegment = new BezierSegment();
                    bezierSegment.Point1 = e.GetPosition(canvas);
                    PathSegment pathSegment = bezierSegment;
                    pathSegmentCollection.Add(pathSegment);
                    drawCurveStep++;

                    curve.Stroke = Brushes.Black;

                    curve = penCurve;
                    curve.Stroke = Brushes.Cyan;
                    mainCurve = bezierSegment;
                    paths.Add(penCurve);
                    figures.Add(pathFigure);
                    bezierSegments.Add(bezierSegment);

                }
                else if (drawCurveStep == 2)
                {
                    bezierSegment.Point2 = e.GetPosition(canvas);
                    drawCurveStep++;
                }
                else if (drawCurveStep == 3)
                {
                    bezierSegment.Point3 = e.GetPosition(canvas);
                    drawCurveStep = 1;
                    // MenuItem currentMode = new MenuItem();
                    /*MenuItem currentModeItem = ((MenuItem)(currentMode.Items[1]));
                    foreach (MenuItem modeItem in currentMode.Items)
                    {
                        modeItem.IsChecked = false;
                    }
                    currentModeItem.IsChecked = true;*/
                    mode = "Animation";

                    pointOne.Visibility = Visibility.Visible;
                    pointTwo.Visibility = Visibility.Visible;
                    pointThree.Visibility = Visibility.Visible;
                    pointSelector.Visibility = Visibility.Visible;

                    Canvas.SetLeft(pointOne, bezierSegment.Point1.X - pointOne.Width / 2);
                    Canvas.SetTop(pointOne, bezierSegment.Point1.Y - pointOne.Height / 2);
                    Canvas.SetLeft(pointTwo, bezierSegment.Point2.X - pointTwo.Width / 2);
                    Canvas.SetTop(pointTwo, bezierSegment.Point2.Y - pointTwo.Height / 2);
                    Canvas.SetLeft(pointThree, bezierSegment.Point3.X - pointThree.Width / 2);
                    Canvas.SetTop(pointThree, bezierSegment.Point3.Y - pointThree.Height / 2);
                    Canvas.SetLeft(pointSelector, bezierSegment.Point1.X - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, bezierSegment.Point1.Y - pointSelector.Height / 2);
                    selectedPoint = 1;

                    xPostiion.Text = (((int)(mainCurve.Point1.X))).ToString();
                    yPostiion.Text = (((int)(mainCurve.Point1.Y))).ToString();
                    
                }
            }

        }

        private void AnimationCompleteHandler (object sender, EventArgs e)
        {

        }

        private void GoToPreviousKeyHandler(object sender, MouseButtonEventArgs e)
        {
            int cursor = ((int)(timelineCursor.X1));
            Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
            if (keyframes.IndexOf(key) != 0)
            {
                timelineCursor.X1 = ((int)(keyframes[keyframes.IndexOf(key) - 1]["meta"]));
                timelineCursor.X2 = ((int)(keyframes[keyframes.IndexOf(key) - 1]["meta"]));
                if (selectedPoint == 1)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["x1"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["y1"])).ToString();
                    // mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x1"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y1"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x1"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y1"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 2)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["x2"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["y2"])).ToString();
                    // mainCurve.Point2 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x2"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y2"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x2"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y2"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 3)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["x3"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["y3"])).ToString();
                    // mainCurve.Point3 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x3"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y3"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x3"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y3"])) - pointSelector.Height / 2);
                }
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x1"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y1"])));
                mainCurve.Point2 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x2"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y2"])));
                mainCurve.Point3 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x3"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y3"])));

                Canvas.SetLeft(pointOne, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x1"])) - pointOne.Width / 2);
                Canvas.SetTop(pointOne, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y1"])) - pointOne.Height / 2);
                Canvas.SetLeft(pointTwo, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x2"])) - pointTwo.Width / 2);
                Canvas.SetTop(pointTwo, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y2"])) - pointTwo.Height / 2);
                Canvas.SetLeft(pointThree, ((int)(keyframes[keyframes.IndexOf(key) - 1]["x3"])) - pointThree.Width / 2);
                Canvas.SetTop(pointThree, ((int)(keyframes[keyframes.IndexOf(key) - 1]["y3"])) - pointThree.Height / 2);

            }
        }

        private void GoToNextKeyHandler(object sender, MouseButtonEventArgs e)
        {
            int cursor = ((int)(timelineCursor.X1));
            Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
            if (keyframes.IndexOf(key) < keyframes.Count - 1)
            {
                timelineCursor.X1 = ((int)(keyframes[keyframes.IndexOf(key) + 1]["meta"]));
                timelineCursor.X2 = ((int)(keyframes[keyframes.IndexOf(key) + 1]["meta"]));
                if (selectedPoint == 1)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["x1"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["y1"])).ToString();
                    // mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x1"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y1"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x1"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y1"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 2)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["x2"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["y2"])).ToString();
                    // mainCurve.Point2 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x2"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y2"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x2"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y2"])) - pointSelector.Height / 2);
                }
                else if (selectedPoint == 3)
                {
                    xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["x3"])).ToString();
                    yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["y3"])).ToString();
                    // mainCurve.Point3 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x3"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y3"])));
                    Canvas.SetLeft(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x3"])) - pointSelector.Width / 2);
                    Canvas.SetTop(pointSelector, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y3"])) - pointSelector.Height / 2);
                }
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x1"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y1"])));
                mainCurve.Point2 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x2"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y2"])));
                mainCurve.Point3 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x3"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y3"])));

                Canvas.SetLeft(pointOne, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x1"])) - pointOne.Width / 2);
                Canvas.SetTop(pointOne, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y1"])) - pointOne.Height / 2);
                Canvas.SetLeft(pointTwo, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x2"])) - pointTwo.Width / 2);
                Canvas.SetTop(pointTwo, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y2"])) - pointTwo.Height / 2);
                Canvas.SetLeft(pointThree, ((int)(keyframes[keyframes.IndexOf(key) + 1]["x3"])) - pointThree.Width / 2);
                Canvas.SetTop(pointThree, ((int)(keyframes[keyframes.IndexOf(key) + 1]["y3"])) - pointThree.Height / 2);

                

            }
        }

        private void ExpandTimelineHandler(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer s = ((ScrollViewer)(sender));
            // debugger.Speak(((int)(s.ScrollableWidth)).ToString());
            if (e.HorizontalOffset > s.ScrollableWidth / 2) {
                timeline.Width += 500;
            }
        }

        private void RemoveKeyFrameHandler(object sender, RoutedEventArgs e)
        {
            debugger.Speak("Удалить ключевой кадр");
            // PackIcon keyFrame = ((PackIcon)(((ContextMenu)(((MenuItem)(sender)).Parent)).Parent));
            MenuItem keyFrame = ((MenuItem)(sender));
            int keyFrameParam = Int32.Parse(keyFrame.DataContext.ToString());
            keyframes.RemoveAt(keyFrameParam);
            timeline.Children.Remove(keyFrame);
        }

        private void DrawKeyFramesLabels()
        {
            for (int keyFrameLabelIdx = 1; keyFrameLabelIdx <= 500; keyFrameLabelIdx++)
            {
                TextBlock keyFrameLabel = new TextBlock();
                keyFrameLabel.Text = (-48 + 48 * keyFrameLabelIdx).ToString();
                keyFrameLabel.Foreground = Brushes.White;
                keyFrameLabel.FontSize = 10;
                timeline.Children.Add(keyFrameLabel);
                Canvas.SetLeft(keyFrameLabel, -48 + 48 * keyFrameLabelIdx);
                Canvas.SetTop(keyFrameLabel, 15);

                TextBlock secondLabel = new TextBlock();
                secondLabel.Text = (-1 + keyFrameLabelIdx).ToString();
                secondLabel.Foreground = Brushes.White;
                secondLabel.FontSize = 10;
                secondLabel.FontWeight = FontWeights.ExtraBlack;
                // secondLabel.Width = 100;
                secondLabel.HorizontalAlignment = HorizontalAlignment.Center;
                timeline.Children.Add(secondLabel);
                int secondIndent = 5;
                if (keyFrameLabel.Text.Length <= 1)
                {
                    secondIndent = 0;
                }
                else if (keyFrameLabel.Text.Length <= 3)
                {
                    secondIndent = 2;
                }
                Canvas.SetLeft(secondLabel, -48 + 48 * keyFrameLabelIdx + secondIndent);
                Canvas.SetTop(secondLabel, 35);
            }
        }

        private void SetDrawModeHandler(object sender, RoutedEventArgs e)
        {
            // MenuItem currentMode = new MenuItem();
            /*MenuItem currentModeItem = ((MenuItem)(sender));
            foreach (MenuItem modeItem in currentMode.Items)
            {
                modeItem.IsChecked = false;
            }
            currentModeItem.IsChecked = true;*/
            mode = "Draw";

            pointOne.Visibility = Visibility.Collapsed;
            pointTwo.Visibility = Visibility.Collapsed;
            pointThree.Visibility = Visibility.Collapsed;
            pointSelector.Visibility = Visibility.Collapsed;

        }

        private void SetAnimationModeHandler(object sender, RoutedEventArgs e)
        {
            // MenuItem currentMode = new MenuItem();
            /*MenuItem currentModeItem = ((MenuItem)(sender));
            foreach (MenuItem modeItem in currentMode.Items)
            {
                modeItem.IsChecked = false;
            }
            currentModeItem.IsChecked = true;*/
            mode = "Animation";

            pointOne.Visibility = Visibility.Visible;
            pointTwo.Visibility = Visibility.Visible;
            pointThree.Visibility = Visibility.Visible;
            pointSelector.Visibility = Visibility.Visible;

            Canvas.SetLeft(pointOne, bezierSegment.Point1.X - pointOne.Width / 2);
            Canvas.SetTop(pointOne, bezierSegment.Point1.Y - pointOne.Height / 2);
            Canvas.SetLeft(pointTwo, bezierSegment.Point2.X - pointTwo.Width / 2);
            Canvas.SetTop(pointTwo, bezierSegment.Point2.Y - pointTwo.Height / 2);
            Canvas.SetLeft(pointThree, bezierSegment.Point3.X - pointThree.Width / 2);
            Canvas.SetTop(pointThree, bezierSegment.Point3.Y - pointThree.Height / 2);
            Canvas.SetLeft(pointSelector, bezierSegment.Point1.X - pointSelector.Width / 2);
            Canvas.SetTop(pointSelector, bezierSegment.Point1.Y - pointSelector.Height / 2);
            selectedPoint = 1;

        }

        private void DrawCurveHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && mode == "Draw")
            {
                
            }
        }

        private void ToggleModeHandler(object sender, RoutedEventArgs e)
        {
            if (mode == "Draw")
            {
                currentMode.Kind = PackIconKind.Animation;
                mode = "Animation";

                pointOne.Visibility = Visibility.Visible;
                pointTwo.Visibility = Visibility.Visible;
                pointThree.Visibility = Visibility.Visible;
                pointSelector.Visibility = Visibility.Visible;

                Canvas.SetLeft(pointOne, bezierSegment.Point1.X - pointOne.Width / 2);
                Canvas.SetTop(pointOne, bezierSegment.Point1.Y - pointOne.Height / 2);
                Canvas.SetLeft(pointTwo, bezierSegment.Point2.X - pointTwo.Width / 2);
                Canvas.SetTop(pointTwo, bezierSegment.Point2.Y - pointTwo.Height / 2);
                Canvas.SetLeft(pointThree, bezierSegment.Point3.X - pointThree.Width / 2);
                Canvas.SetTop(pointThree, bezierSegment.Point3.Y - pointThree.Height / 2);
                Canvas.SetLeft(pointSelector, bezierSegment.Point1.X - pointSelector.Width / 2);
                Canvas.SetTop(pointSelector, bezierSegment.Point1.Y - pointSelector.Height / 2);
                selectedPoint = 1;
            
            }
            else if (mode == "Animation")
            {

                currentMode.Kind = PackIconKind.ModeEdit;
                mode = "Draw";

                pointOne.Visibility = Visibility.Collapsed;
                pointTwo.Visibility = Visibility.Collapsed;
                pointThree.Visibility = Visibility.Collapsed;
                pointSelector.Visibility = Visibility.Collapsed;
            
            }
        }
        
    }
}
