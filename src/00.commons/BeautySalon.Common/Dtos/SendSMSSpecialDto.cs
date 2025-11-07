using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Common.Dtos;
public class SendSMSSpecialDto
{
    [Required]
    public string Number { get; set; } = default!;

    public string BodyName { get; set; } = default!;

    public List<string> Args { get; set; } = new();
}
