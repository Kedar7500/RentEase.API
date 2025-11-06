using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;
using RentEase.API.Services.Interfaces;

namespace RentEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
           var properties = await propertyService.GetAllProperties();

           if(properties == null) return NotFound();
            
           return Ok(properties);
        }

        [HttpGet]
        [Route("{id:int?}")]
        public async Task<IActionResult> GetPropertiesById([FromRoute] int id)
        { 
            var property = await propertyService.GetPropertyById(id);

            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] AddPropertyDto addPropertyDto)
        {
            if (ModelState.IsValid)
            {
                var created = await propertyService.CreateProperty(addPropertyDto);
                return CreatedAtAction(nameof(GetPropertiesById), new { id = created.Id }, created);
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpPut("{id:int?}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] int id, [FromBody] UpdatePropertyDto updateProperty)
        {
            if (ModelState.IsValid)
            {
                var updated = await propertyService.UpdateProperty(id, updateProperty);
                return Ok(updated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int?}")]
        public async Task<IActionResult> DeleteProperty([FromRoute]int id)
        {
            var deletedProperty = await propertyService.DeleteProperty(id);

            if(deletedProperty == null)
            { 
                return NotFound(); 
            }

            return Ok(deletedProperty);
        }
    }
}
