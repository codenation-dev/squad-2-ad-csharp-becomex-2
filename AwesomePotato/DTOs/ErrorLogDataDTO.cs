using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.DTOs
{
    public class ErrorLogDataDTO
    {
        public int Id { get; set; }
        public int? Level { get; set; }
        [StringLength(100)]
        public string Application { get; set; }
        [StringLength(300)]
        public string Title { get; set; }
        [StringLength(3000)]
        [Required]
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
