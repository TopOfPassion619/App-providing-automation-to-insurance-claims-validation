﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarController : ControllerBase
    {

        public class CarPhoto
        {
            public IFormFile photoFile { get; set; }
        }

        [HttpPost]
        public IActionResult CarPhotoUpload([FromForm] CarPhoto carPhoto)
        {
            if (carPhoto.photoFile.ContentType.ToLower() != "image/jpeg" &&
                carPhoto.photoFile.ContentType.ToLower() != "image/jpg" &&
                carPhoto.photoFile.ContentType.ToLower() != "image/png")
            {
                return BadRequest("You dont upload photo");
            }
            else
            {
                long photoLength = carPhoto.photoFile.Length;
                return Ok(photoLength);
            }
        }
    }
}