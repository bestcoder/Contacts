using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Pages
{
  [Authorize]
  public class ContactListModel : PageModel
  {
    private readonly ILogger<ContactListModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    [BindProperty]
    public string UserId { get; set; }

    public ContactListModel(ILogger<ContactListModel> logger, 
      UserManager<IdentityUser> userManager)
    {
      _logger = logger;
      _userManager = userManager;
    }

    public void OnGet()
    {
      UserId = _userManager.GetUserId(User);
    }

  }
}
