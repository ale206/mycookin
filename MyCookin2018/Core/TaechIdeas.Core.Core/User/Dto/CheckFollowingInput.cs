﻿using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class CheckFollowingInput
    {
        public Guid UserIdFriend1 { get; set; }
        public Guid UserIdFriend2 { get; set; }
    }
}