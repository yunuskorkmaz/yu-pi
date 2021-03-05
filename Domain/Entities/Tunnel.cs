using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using yu_pi.Domain.Enums;

namespace yu_pi.Domain.Entities
{
    public class Tunnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Protokol { get; set; }
        public string Port { get; set; }
        public string PublicUrl { get; set; }
        public int Status { get; set; }
    }
}