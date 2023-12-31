﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcore.Data;
using WebAPIcore.Models;

namespace WebAPIcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
