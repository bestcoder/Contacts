using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Api.Models;
using Contacts.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Contacts.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Produces(MediaTypeNames.Application.Json)]
  public class ContactsController : ControllerBase
  {

    private readonly DemoModel _context;

    public ContactsController(DemoModel context)
    {
      _context = context;
    }

    /// <summary>
    /// Gets a list of Contacts for the current user.
    /// </summary>
    [HttpGet]
    [Route("getcontactslist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContacts(string userId)
    {
      var contacts = _context.Contacts.Where(x => x.UserId == userId).Select(x => ContactFrom(x));
      var loadResult = await contacts.ToListAsync();
      return Ok(loadResult);
    }

    /// <summary>
    /// Gets a DevExtreme Contacts response model for a specified user.
    /// </summary>
    [HttpGet]
    [Route("loadcontactsmodel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoadContactsModel(string userId, DataSourceLoadOptions loadOptions)
    {
      var contacts = _context.Contacts
        .Where(x=>x.UserId == userId)
        .Select(x => new {
          x.Id,
          x.UserId,
          x.Name,
          x.Address,
          x.Email,
        });
      loadOptions.PrimaryKey = new[] { "Id" };
      loadOptions.PaginateViaPrimaryKey = true;
      var loadResult = await DataSourceLoader.LoadAsync(contacts, loadOptions);
      return Ok(loadResult);
    }

    [HttpPut]
    [Route("updatecontact")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateContact([FromForm] int key, [FromForm] string values)
    {
      var contact = await _context.Contacts.FindAsync(key);
      if (contact == null)
      {
        return NotFound();
      }
      JsonConvert.PopulateObject(values, contact);
      if (!TryValidateModel(contact))
        return BadRequest(ModelState.GetFullErrorMessage());
      _context.Contacts.Update(contact);
      await _context.SaveChangesAsync();
      return Ok();
    }

    [HttpPost]
    [Route("insertcontact")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> InsertContact([FromForm] string values)
    {
      var contact = new Data.Contacts.Contact();
      JsonConvert.PopulateObject(values, contact);
      if (!TryValidateModel(contact))
        return BadRequest(ModelState.GetFullErrorMessage());
      _context.Contacts.Add(contact);
      await _context.SaveChangesAsync();
      return Ok();
    }

    [HttpDelete]
    [Route("deletecontact")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteContact([FromForm] int key)
    {
      var contact = await _context.Contacts.FindAsync(key);
      if (contact == null)
      {
        return NotFound();
      }
      _context.Contacts.Remove(contact);
      await _context.SaveChangesAsync();
      return Ok();
    }

    /// <summary>
    /// Gets a new Contact Core Api model from a specified Contact DB entity.
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    private static Contact ContactFrom(Data.Contacts.Contact contact)
    {
      return new Contact()
      {
        Id = contact.Id,
        UserId = contact.UserId,
        Name = contact.Name,
        Address = contact.Address,
        Email = contact.Email,
      };
    }

  }
}
