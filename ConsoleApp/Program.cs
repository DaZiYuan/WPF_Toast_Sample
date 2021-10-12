using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Windows.UI.Notifications;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/adaptive-interactive-toasts?tabs=builder-syntax
            Console.WriteLine("1.Text elements");
            Console.WriteLine("2.App logo override");
            Console.WriteLine("3.Hero image");
            Console.WriteLine("4.Inline image");
            Console.WriteLine("5.Attribution text");
            Console.WriteLine("6.Custom timestamp");
            Console.WriteLine("7.Progress bar");
            Console.WriteLine("8.Headers");
            Console.WriteLine("9.Columns and text elements");
            Console.WriteLine("10.Buttons");
            Console.WriteLine("11.Buttons with icons");
            Console.WriteLine("12.Buttons with pending update activation");
            Console.WriteLine("13.Context menu actions");
            Console.WriteLine("14.Inputs");
            Console.WriteLine("15.Quick reply text box");
            Console.WriteLine("16.Inputs with buttons bar");
            Console.WriteLine("17.Selection input");
            Console.WriteLine("18.Snooze/dismiss");
            Console.WriteLine("19.Audio");
            Console.WriteLine("20.Timer");
            var _assembly = System.Reflection.Assembly
                   .GetExecutingAssembly().GetName().CodeBase;

            string _path = AppDomain.CurrentDomain.BaseDirectory;

            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                // Obtain any user input (text boxes, menu selections) from the notification
                //ValueSet userInput = toastArgs.UserInput;

                //// Need to dispatch to UI thread if performing UI operations
                //Application.Current.Dispatcher.Invoke(delegate
                //{
                //    // TODO: Show the corresponding content
                //    MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
                //});
            };
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        new ToastContentBuilder()
                         .AddText("Text elements", hintMaxLines: 1)
                         .AddText("文字2")
                         .AddText("文字3")
                         .Show();
                        break;
                    case "2":
                        string imgPath = Path.Combine(_path, "Assets\\883-48x48.jpg");
                        new ToastContentBuilder()
                          .AddText("App logo override", hintMaxLines: 1)
                          .AddText("文字2")
                         .AddText("文字3")
                          .AddAppLogoOverride(new Uri(imgPath), ToastGenericAppLogoCrop.Circle)
                          .Show();
                        break;
                    case "3":
                        imgPath = Path.Combine(_path, "Assets\\1.gif");
                        new ToastContentBuilder()
                         .AddText("Hero image", hintMaxLines: 1)
                         .AddText("文字2")
                         .AddText("文字3")
                         .AddHeroImage(new Uri(imgPath))
                         .Show();
                        break;
                    case "4":
                        imgPath = Path.Combine(_path, "Assets\\hero.jpg");
                        new ToastContentBuilder()
                         .AddText("Inline image", hintMaxLines: 1)
                         .AddText("文字2")
                         .AddText("文字3")
                         .AddInlineImage(new Uri(imgPath))
                         .Show();
                        break;
                    case "5":
                        new ToastContentBuilder()
                     .AddText("Attribution text", hintMaxLines: 1)
                     .AddText("文字2")
                     .AddText("文字3")
                     .AddAttributionText("看这")
                     .Show();
                        break;
                    case "6":
                        new ToastContentBuilder()
                        .AddText("Custom timestamp", hintMaxLines: 1)
                        .AddText("日期为4月15日")
                        .AddCustomTimeStamp(new DateTime(2021, 04, 15, 19, 45, 00, DateTimeKind.Local))
                        .Show();
                        break;
                    case "7":
                        const string tag = "progressToast";
                        const string group = "progressToastGroup";

                        new ToastContentBuilder()
                                .AddText("Progress bar")
                                .AddVisualChild(new AdaptiveProgressBar()
                                {
                                    Title = "downloading",
                                    Value = new BindableProgressBarValue("progressValue"),
                                    ValueStringOverride = new BindableString("progressValueString"),
                                    Status = new BindableString("progressStatus")
                                }).Show(toast =>
                                {
                                    toast.Tag = tag;
                                    toast.Group = group;

                                    toast.Data = new NotificationData(new Dictionary<string, string>()
                                  {
                                    { "progressValue", "0" },
                                    { "progressValueString", "进度" },
                                    { "progressStatus", "状态" },
                                  });
                                });

                        double progress = 0;

                        while (progress < 1)
                        {
                            Thread.Sleep(new Random().Next(500, 1000));

                            progress += (new Random().NextDouble() * 0.15) + 0.1;

                            ToastNotificationManagerCompat.CreateToastNotifier().Update(
                                new NotificationData(new Dictionary<string, string>()
                                {
                                    { "progressValue",  progress.ToString() },
                                    { "progressValueString", "进度"+ (int)(progress*10) },
                                    { "progressStatus", "状态"+ (int)(progress*10) },
                                }), tag, group);
                        }
                        break;
                    case "8":
                        new ToastContentBuilder()
    .AddHeader("6289", "header", "action=clickHeader&id=6289")
    .AddText("text")
    .Show();
                        break;
                    case "9":
                        break;
                    case "10": break;
                    case "11": break;
                    case "12": break;
                    case "13": break;
                    case "14": break;
                    case "15": break;
                    case "16": break;
                    case "17": break;
                    case "18": break;
                    case "19":
                        string audioPath = Path.Combine(_path, "Assets\\2.m4a");

                        new ToastContentBuilder()
                .AddText("Audio", hintMaxLines: 1)
                .AddAudio(new Uri(audioPath))
                .Show();
                        break;
                    case "20":

                        Thread.Sleep(5000);

                        new ToastContentBuilder()
                 .AddText("Timer", hintMaxLines: 1)
                 .AddText("5s后触发")
                 .Show();
                        break;
                }
            }

        }

        private static void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {

        }
    }
}
