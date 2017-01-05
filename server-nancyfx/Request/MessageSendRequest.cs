﻿namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles message sending.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class MessageSendRequest : ConfidentialRequest
    {
        public int receiverId { get; set; }
        public string messageText { get; set; }
    }
}