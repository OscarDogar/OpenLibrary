using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Common.Enums;
using OpenLibrary.Common.Models;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Helpers;
using OpenLibrary.Web.Resources;

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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("InsertReview")]
        public async Task<IActionResult> PostReview([FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;


            UserEntity userEntity = await _context.Users.FindAsync(request.User);
            if (userEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.UserDoesntExists
                });
            }

            DocumentEntity documentEntity = await _context.Documents.FindAsync(request.Document);

            if (documentEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.DocumentDoesntexist
                });

            }

            ReviewEntity reviewEntity = await _context.Reviews
                       .FirstOrDefaultAsync(p => p.User.Id == request.User && p.Document.Id == request.Document);
            string message = "";
            if (reviewEntity == null)
            {
                reviewEntity = new ReviewEntity
                {
                    Document=documentEntity,
                    Comment = request.Comment,
                    Rating = request.Rating,
                    Favorite = request.Favorite,
                    User = userEntity
                };
                message = Resource.TheReviewWasMadeCorrectly;
                _context.Reviews.Add(reviewEntity);
            }
            else
            {
                reviewEntity.Comment = request.Comment;
                reviewEntity.Rating = request.Rating;
                reviewEntity.Favorite = request.Favorite;
                message = Resource.TheReviewWasEditedSuccessfully;
                _context.Reviews.Update(reviewEntity);
            }

            await _context.SaveChangesAsync();

            return Ok(new Response
            {
                IsSuccess = true,
                Message = message
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("DeleteReview")]
        public async Task<IActionResult> DeleteReview([FromBody] ReviewDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;


            UserEntity userEntity = await _context.Users.FindAsync(request.User);
            if (userEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.UserDoesntExists
                });
            }

            DocumentEntity documentEntity = await _context.Documents.FindAsync(request.Document);

            if (documentEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.DocumentDoesntexist
                });

            }

            ReviewEntity reviewEntity = await _context.Reviews
                       .FirstOrDefaultAsync(p => p.User.Id == userEntity.Id && p.Document.Id == documentEntity.Id);

            _context.Reviews.Remove(reviewEntity);

            await _context.SaveChangesAsync();

            return Ok(new Response
            {
                IsSuccess = true,
                Message = Resource.TheReviewWasDeletedSuccessfully
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("PutReview")]
        public async Task<IActionResult> PutReview([FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;


            UserEntity userEntity = await _context.Users.FindAsync(request.User);
            if (userEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.UserDoesntExists
                });
            }

            DocumentEntity documentEntity = await _context.Documents.FindAsync(request.Document);

            if (documentEntity == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = Resource.DocumentDoesntexist
                });

            }

            ReviewEntity reviewEntity = await _context.Reviews
                       .FirstOrDefaultAsync(p => p.User.Id == userEntity.Id && p.Document.Id == documentEntity.Id);
            if (reviewEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            reviewEntity.User = userEntity;
            reviewEntity.Document = documentEntity;
            reviewEntity.Comment = request.Comment;
            reviewEntity.Rating = request.Rating;
            reviewEntity.Favorite = request.Favorite;

             _context.Update(reviewEntity);
            await _context.SaveChangesAsync();

            return Ok(new Response
            {
                IsSuccess = true,
                Message = Resource.TheReviewWasEditedSuccessfully
            });
        }


    }
}