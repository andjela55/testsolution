﻿using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
