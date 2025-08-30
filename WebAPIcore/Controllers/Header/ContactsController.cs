using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcore.Data;
using WebAPIcore.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace WebAPIcore.Controllers.Header
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("5.0")]
    [Produces("application/json", "application/xml")]
    public class ContactsController : ControllerBase
    {
        private readonly ClontactsAPIDbContext dbcontext;

        public ContactsController(ClontactsAPIDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbcontext.Contacts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddContacts(Contact addContact)
        {
            addContact.Id = Guid.NewGuid();
            await dbcontext.Contacts.AddAsync(addContact);
            await dbcontext.SaveChangesAsync();
            return Ok(addContact);
            // return Ok(dbcontext.Contacts.ToList());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, Contact updateContact)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContact.FullName;
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;
                contact.Address = updateContact.Address;
                await dbcontext.SaveChangesAsync();
                return Ok(updateContact);
            }
            // await dbcontext.Contacts.AddAsync(addContact);
            // await dbcontext.SaveChangesAsync();
            return NotFound();
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();

        }
    }
}
