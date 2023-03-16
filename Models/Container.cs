using System.ComponentModel.DataAnnotations;

namespace AzureBlobStorage.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
