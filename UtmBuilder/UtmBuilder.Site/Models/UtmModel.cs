using System.ComponentModel.DataAnnotations;
using UtmBuilder;

namespace UtmBuilder.Site.Models
{
    public class UtmModel
    {
        [Required(ErrorMessage = "URL is required")]
        [DataType(DataType.Url, ErrorMessage = "Invalid URL")]
        [MinLength(11, ErrorMessage = "URL min chars: 11")]
        public string Url { get; set; } = String.Empty;

        public string? Id { get; set; }

        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; } = String.Empty;

        [Required(ErrorMessage = "Medium is required")]
        public string Medium { get; set; } = String.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = String.Empty;

        public string? Term { get; set; }

        public string? Content { get; set; }

        public static implicit operator Utm(UtmModel model) =>
            new Utm(
                model.Url,
                model.Source,
                model.Medium,
                model.Name,
                model.Id,
                model.Term,
                model.Content);
    }
}
