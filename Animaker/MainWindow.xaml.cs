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

        public MainWindow()
        {
            InitializeComponent();

            debugger = new SpeechSynthesizer();

            keyframes = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> defaultKeyframe = new Dictionary<String, Object>();
            /*defaultKeyframe.Add("x1", 0);
            defaultKeyframe.Add("y1", 0);
            defaultKeyframe.Add("x2", 0);
            defaultKeyframe.Add("y2", 0);*/
            defaultKeyframe.Add("x", 0);
            defaultKeyframe.Add("y", 0);
            keyframes.Add(defaultKeyframe);

        }

        private void PlayAnimationHandler(object sender, MouseButtonEventArgs e)
        {
            PointAnimation curveAnimation = new PointAnimation();
            /*curveAnimation.From = new Point(0, 0);
            curveAnimation.To = new Point(35, 300);*/
            foreach (Dictionary<String, Object> keyframe in keyframes)
            {
                /*curveAnimation.From = new Point(((int)(keyframe["x1"])), ((int)(keyframe["y1"])));
                curveAnimation.To = new Point(((int)(keyframe["x2"])), ((int)(keyframe["y2"])));*/
                if (keyframes.IndexOf(keyframe) == 0)
                {
                    curveAnimation.From = new Point(((int)(keyframe["x"])), ((int)(keyframe["y"])));
                } else if (keyframes.IndexOf(keyframe) == keyframes.Count - 1)
                {
                    curveAnimation.From = new Point(((int)(keyframe["x"])), ((int)(keyframe["y"])));
                }
            }
            mainCurve.BeginAnimation(BezierSegment.Point1Property, curveAnimation);
        }

        private void SetTimelineCursorHandler(object sender, MouseButtonEventArgs e)
        {

            int cursor = ((int)(e.GetPosition(timeline).X));
            timelineCursor.X1 = cursor;
            timelineCursor.X2 = cursor;

            if (keyframes.Count -1 < cursor)
            {
                // добавляем ключ
                debugger.Speak("добавляем ключ");
                Dictionary<String, Object>  keyframe = new Dictionary<String, Object>();
                /*keyframe.Add("x1", 0);
                keyframe.Add("y1", 0);
                keyframe.Add("x1", 0);
                keyframe.Add("y2", 0);*/
                keyframe.Add("x", 0);
                keyframe.Add("y", 0);
                keyframes.Add(keyframe);
                xPostiion.Text = "0";
                yPostiion.Text = "0";
            } else
            {
                xPostiion.Text = ((int)(keyframes[cursor]["x"])).ToString();
                yPostiion.Text = ((int)(keyframes[cursor]["y"])).ToString();
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
                if (keyframes.Count - 1 < cursor) {
                    keyframes[keyframes.Count - 1]["x"] = ((int)(value));
                } else
                {
                    keyframes[cursor]["x"] = ((int)(value));
                }
            }
        }

        private void SetKeyYPositionHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int value = Int32.Parse(((TextBox)(sender)).Text);
                int cursor = ((int)(timelineCursor.X1));
                // keyframes[cursor]["y1"] = ((int)(0));
                if (keyframes.Count - 1 < cursor)
                {
                    keyframes[keyframes.Count - 1]["y"] = ((int)(value));
                }
                else
                {
                    keyframes[cursor]["y"] = ((int)(value));
                }
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

    }
}
