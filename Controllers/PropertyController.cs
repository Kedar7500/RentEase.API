using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentEase.API.Models.Domain;
using RentEase.API.Models.DTOs;
using RentEase.API.Services.Interfaces;

namespace RentEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                return NotFound($"key with id {id} not existed");
               
            }
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromForm] AddPropertyDto addPropertyDto)
        {
            ValidateFileUpload(addPropertyDto);

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

        private void ValidateFileUpload(AddPropertyDto addProperty)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(addProperty == null || addProperty.Images.Count == 0)
            {
                ModelState.AddModelError("file","at least one image must to have");
            }

            foreach(var file in addProperty.Images)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName.ToLowerInvariant())))
                {
                    ModelState.AddModelError("file", "UnSupported File Extension");
                }

                if (file.Length > 10485760)
                {
                    ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
                }
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
