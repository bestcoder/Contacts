namespace Contacts.Api.Models
{
  using Newtonsoft.Json;
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  public class Contact
  {

    public Contact()
    {
    }

    public int Id { get; set; }

    [StringLength(450)]
    public string UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Address { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

  }

}
