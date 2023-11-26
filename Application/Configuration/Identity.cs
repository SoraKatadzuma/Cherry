using System.ComponentModel.DataAnnotations;

namespace Application.Configuration; 

public class Identity {
    [Required]
    public string Name { get; set; }
}
