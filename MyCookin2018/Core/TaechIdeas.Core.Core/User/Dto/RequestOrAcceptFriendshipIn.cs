﻿using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class RequestOrAcceptFriendshipIn
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
    }
}