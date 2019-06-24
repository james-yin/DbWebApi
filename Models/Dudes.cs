using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbWebApi.Models
{
    public partial class Dude
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
