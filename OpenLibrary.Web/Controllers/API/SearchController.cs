﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Common.Enums;
using OpenLibrary.Common.Models;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Helpers;

namespace OpenLibrary.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imagesHelper;

        public SearchController(DataContext context,
            IConverterHelper converterHelper,
            IUserHelper userHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _imagesHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

            List<DocumentEntity> documentEntities = await _context.Documents
              .Include(d => d.User)
              .Include(a => a.Author)
              .Include(t => t.TypeOfDocument)
              .Include(l => l.DocumentLanguage)
              .Include(g => g.Gender)
              .Include(r => r.Reviews)
              .Where(u => u.User.UserType == UserType.User)
              .ToListAsync();

            if (documentEntities == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Error"
                });

            }

            return Ok(_converterHelper.ToDocumentResponse2(documentEntities));
        }

        [HttpGet]
        [Route("GetReview")]
        public async Task<IActionResult> GetReviews()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<ReviewEntity> reviewEntities = await _context.Reviews
              .Include(d => d.User)
              .Include(d => d.Document)
              .ToListAsync();

            if (reviewEntities == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Error"
                });

            }

            return Ok(_converterHelper.ToReviewResponse(reviewEntities));
        }


    }
}