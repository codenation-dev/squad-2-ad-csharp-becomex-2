using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.Models
{
    public class ErrorLogData
    {
        [Key]
        public int Id { get; set; }
        public Guid? Token { get; set; }
        public int? Level { get; set; }
        [StringLength(100)]
        public string Application { get; set; }
        [StringLength(300)]
        public string Title { get; set; }
        [StringLength(3000)]
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime TimeStamp { get; private set; }
    }
}
