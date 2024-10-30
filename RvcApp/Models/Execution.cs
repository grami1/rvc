using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RvcApp.Models;

public class Execution
{
    [Key]
    public int Id { get; set; }
    [Column]
    public string Timestamp { get; set; }
    [Column]
    public int Commands { get; set; }
    [Column]
    public int Result { get; set; }
    [Column]
    public double Duration { get; set; }
}
