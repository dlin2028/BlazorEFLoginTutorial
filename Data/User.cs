using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOKTutorial.Data;

public class User
{    

    /// <summary>
    /// Unique identifier.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
    public string? Username { get; set; }  
    
    [Required]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string? Password { get; set; }
}
