﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public Guid UniqueId { get; set; }
        public Guid PrivateLabelKey { get; set; }
    }
}