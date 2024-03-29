﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Models.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
