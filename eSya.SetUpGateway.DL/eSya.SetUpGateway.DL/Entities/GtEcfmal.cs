﻿using System;
using System.Collections.Generic;

namespace eSya.SetUpGateway.DL.Entities
{
    public partial class GtEcfmal
    {
        public int FormId { get; set; }
        public int ActionId { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}