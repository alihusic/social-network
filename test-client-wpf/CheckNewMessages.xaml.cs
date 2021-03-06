﻿using Newtonsoft.Json;
using SocialNetwork2.Request;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using testClientWPF;

namespace SocialNetwork
{
    /// <summary>
    /// Interaction logic for CheckNewMessages.xaml
    /// </summary>
    public partial class CheckNewMessages : Page
    {
        public CheckNewMessages()
        {
            InitializeComponent();
        }

        private void loadNewMessages(object sender, RoutedEventArgs e)
        {
            if (ClientInfo.Instance.SessionToken == null) return;
            try
            {
                ConfidentialRequest query = new ConfidentialRequest
                {
                    userToken = ClientInfo.Instance.SessionToken
                };

                ClientInfo.Instance.UnreadMessagesList = new ServiceConnector().checkNewMessages(query);

                foreach (var message in ClientInfo.Instance.UnreadMessagesList)
                {
                    newMessageContent.Text += "\n";
                    newMessageContent.Text += message.senderId;
                    newMessageContent.Text += "\n";
                    newMessageContent.Text += message.messageText;
                }



            }
            catch(Exception ex)
            {

            }
        }
    }
}
