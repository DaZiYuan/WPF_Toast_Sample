using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Notifications;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            // Obtain the arguments from the notification
            ToastArguments args = ToastArguments.Parse(e.Argument);

            // Obtain any user input (text boxes, menu selections) from the notification
            //ValueSet userInput = toastArgs.UserInput;

            //// Need to dispatch to UI thread if performing UI operations
            //Application.Current.Dispatcher.Invoke(delegate
            //{
            //    // TODO: Show the corresponding content
            //    MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
            //});
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
            {
                new AdaptiveText()
                {
                    Text = "Matt sent you a friend request"
                },
                new AdaptiveText()
                {
                    Text = "Hey, wanna dress up as wizards and ride around on our hoverboards together?"
                }
            },
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "https://unsplash.it/64?image=1005",
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
        {
            new ToastButton("Accept", "action=acceptFriendRequest&userId=49183")
            {
                ActivationType = ToastActivationType.Background
            },
            new ToastButton("Decline", "action=declineFriendRequest&userId=49183")
            {
                ActivationType = ToastActivationType.Background
            }
        }
                },
                Launch = "action=viewFriendRequest&userId=49183"
            };

            // Create the toast notification
            var toastNotif = new ToastNotification(toastContent.GetXml());
            toastNotif.Activated += ToastNotif_Activated;
            toastNotif.Failed += ToastNotif_Failed;
            toastNotif.Dismissed += ToastNotif_Dismissed;
            // And send the notification
            ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotif);
        }

        private void ToastNotif_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
        }

        private void ToastNotif_Failed(ToastNotification sender, ToastFailedEventArgs args)
        {
        }

        private void ToastNotif_Activated(ToastNotification sender, object args)
        {
        }
    }
}
