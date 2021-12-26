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

        public MainWindow()
        {
            InitializeComponent();

            debugger = new SpeechSynthesizer();

            keyframes = new List<Dictionary<String, Object>>();

            animations = new List<PointAnimation>();

            Dictionary<String, Object> defaultKeyframe = new Dictionary<String, Object>();
            defaultKeyframe.Add("x", 0);
            defaultKeyframe.Add("y", 0);
            defaultKeyframe.Add("meta", 0);
            keyframes.Add(defaultKeyframe);

        }

        private void PlayAnimationHandler(object sender, MouseButtonEventArgs e)
        {
            animations.Clear();
            PointAnimation defautlStateAnimation = new PointAnimation();
            defautlStateAnimation.Duration = new Duration(TimeSpan.FromSeconds(0));
            foreach (Dictionary<String, Object> keyframe in keyframes)
            {
                if (keyframes.IndexOf(keyframe) != keyframes.Count - 1) {
                    if (keyframes.IndexOf(keyframe) == 0)
                    {
                        defautlStateAnimation.From = new Point(((int)(keyframe["x"])), ((int)(keyframe["y"])));
                        defautlStateAnimation.To = new Point(((int)(keyframe["x"])), ((int)(keyframe["y"])));
                    }
                    PointAnimation curveAnimation = new PointAnimation();
                    curveAnimation.From = new Point(((int)(keyframe["x"])), ((int)(keyframe["y"])));
                    curveAnimation.To = new Point(((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["x"])), ((int)(keyframes[keyframes.IndexOf(keyframe) + 1]["y"])));
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
                    curveAnimation.Completed += delegate
                    {
                        if (animations.IndexOf(curveAnimation) < animations.Count - 1)
                        {
                            mainCurve.BeginAnimation(BezierSegment.Point1Property, animations[animations.IndexOf(curveAnimation) + 1]);
                        } else
                        {
                            debugger.Speak("конец анимации");
                            mainCurve.BeginAnimation(BezierSegment.Point1Property, null);
                            mainCurve.Point1 = ((Point)(defautlStateAnimation.To));
                            timelineCursor.BeginAnimation(Line.X1Property, null);
                            timelineCursor.BeginAnimation(Line.X2Property, null);
                            xPostiion.Text = ((int)(keyframes[0]["x"])).ToString();
                            yPostiion.Text = ((int)(keyframes[0]["y"])).ToString();

                        }
                    };
                }
            }
            if (animations.Count >= 1)
            {
                
                mainCurve.BeginAnimation(BezierSegment.Point1Property, animations[0]);

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
                keyframe.Add("x", 0);
                keyframe.Add("y", 0);
                keyframe.Add("meta", cursor);
                keyframes.Add(keyframe);
                xPostiion.Text = "0";
                yPostiion.Text = "0";
                mainCurve.Point1 = new Point(0, 0);
            } else
            {
                debugger.Speak("ключ уже существует");
                Dictionary<String, Object> key = possibleKeys[0];
                xPostiion.Text = ((int)(key["x"])).ToString();
                yPostiion.Text = ((int)(key["y"])).ToString();
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key)]["x"])), ((int)(keyframes[keyframes.IndexOf(key)]["y"])));
            }
            PackIcon keyFrameIcon = new PackIcon();
            keyFrameIcon.Width = 10;
            keyFrameIcon.Kind = PackIconKind.Key;
            keyFrameIcon.Foreground = Brushes.Yellow;
            Canvas.SetLeft(keyFrameIcon, cursor - keyFrameIcon.Width / 2);
            timeline.Children.Add(keyFrameIcon);

        }

        private void SetKeyXPositionHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int value = Int32.Parse(((TextBox)(sender)).Text);
                int cursor = ((int)(timelineCursor.X1));
                Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
                keyframes[keyframes.IndexOf(key)]["x"] = ((int)(value));
                mainCurve.Point1 = new Point(value, mainCurve.Point1.Y);
            }
        }

        private void SetKeyYPositionHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int value = Int32.Parse(((TextBox)(sender)).Text);
                int cursor = ((int)(timelineCursor.X1));
                Dictionary<String, Object> key = ((Dictionary<String, Object>)(keyframes.Where((keyframe) => cursor == ((int)(keyframe["meta"]))).ToList()[0]));
                keyframes[keyframes.IndexOf(key)]["y"] = ((int)(value));
                mainCurve.Point1 = new Point(mainCurve.Point1.X, value);
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
                            keyframe.Add("x", 0);
                            keyframe.Add("y", 0);
                            keyframe.Add("meta", cursor);
                            keyframes.Add(keyframe);
                            xPostiion.Text = "0";
                            yPostiion.Text = "0";
                        }
                        else
                        {
                            xPostiion.Text = ((int)(keyframes[cursor]["x"])).ToString();
                            yPostiion.Text = ((int)(keyframes[cursor]["y"])).ToString();
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
                        keyframe.Add("x", 0);
                        keyframe.Add("y", 0);
                        keyframe.Add("meta", cursor);
                        keyframes.Add(keyframe);
                        xPostiion.Text = "0";
                        yPostiion.Text = "0";
                    }
                    else
                    {
                        xPostiion.Text = ((int)(keyframes[cursor]["x"])).ToString();
                        yPostiion.Text = ((int)(keyframes[cursor]["y"])).ToString();
                    }
                }
            }
        }

        private void ClearFocusHandler(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
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
                xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["x"])).ToString();
                yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) - 1]["y"])).ToString();
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) - 1]["x"])), ((int)(keyframes[keyframes.IndexOf(key) - 1]["y"])));
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
                xPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["x"])).ToString();
                yPostiion.Text = ((int)(keyframes[keyframes.IndexOf(key) + 1]["y"])).ToString();
                mainCurve.Point1 = new Point(((int)(keyframes[keyframes.IndexOf(key) + 1]["x"])), ((int)(keyframes[keyframes.IndexOf(key) + 1]["y"])));
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
    }
}
