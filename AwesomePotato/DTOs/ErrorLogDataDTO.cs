using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.DTOs
{
    public class ErrorLogDataDTO
    {
        public int Id { get; private set; }
        public Guid? Token { get; set; }
        public int? Level { get; set; }
        public string Application { get; set; }
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime TimeStamp { get; private set; }
    }
}
