using Microsoft.CodeAnalysis;
using OpenLibrary.Common.Models;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public ConverterHelper(DataContext context,ICombosHelper combosHelper,IUserHelper userHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public ReviewResponse ToReviewResponse(ReviewEntity reviewEntity)
        {
            return new ReviewResponse
            {
                Comment = reviewEntity.Comment,
                Favorite = reviewEntity.Favorite,
                Id = reviewEntity.Id,
                Rating= reviewEntity.Rating,
                User= ToUser2Response(reviewEntity.User),
                Document = ToDocumentResponse(reviewEntity.Document)

            };
        }

        public List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities)
        {
            List<ReviewResponse> list = new List<ReviewResponse>();
            foreach (ReviewEntity reviewEntity in reviewEntities)
            {
                list.Add(ToReviewResponse(reviewEntity));
            }

            return list;
        }

        public SearchResponse ToDocumentResponse2(DocumentEntity documentEntity)
        {
            return new SearchResponse
            {

                    Id = documentEntity.Id,
                    Title= documentEntity.Title,
                    DocumentPath = documentEntity.DocumentPath,
                    Date = documentEntity.Date,
                    Summary = documentEntity.Summary,
                    PagesNumber= documentEntity.PagesNumber,
                    Accepted= documentEntity.Accepted,
                    Gender = ToGenderResponse(documentEntity?.Gender),
                    Author = ToAuthorResponse(documentEntity?.Author),
                    DocumentLanguage = ToLanguageResponse(documentEntity?.DocumentLanguage),
                    TypeOfDocument = ToTypeResponse(documentEntity?.TypeOfDocument),
                    User = ToUserResponse(documentEntity?.User),
                    Reviews = documentEntity.Reviews?.Select(gd => new ReviewResponse
                    {
                        Id = gd.Id,
                        Comment = gd.Comment,
                        Rating = gd.Rating,
                        Favorite = gd.Favorite,
                    }).ToList(),
                
            };
        }

        public List<SearchResponse> ToDocumentResponse2(List<DocumentEntity> documentEntities)
        {
            List<SearchResponse> list = new List<SearchResponse>();
            foreach (DocumentEntity documentEntity in documentEntities)
            {
                list.Add(ToDocumentResponse2(documentEntity));
            }

            return list;
        }

        public UserResponse ToUser2Response(UserEntity userEntity)
        {
            if (userEntity == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = userEntity.Id,
                Email= userEntity.Email,
                FirstName=userEntity.FirstName,
                LastName=userEntity.LastName
            };
        }

        public SearchResponse ToDocumentResponse(DocumentEntity documentEntity)
        {
            if (documentEntity == null)
            {
                return null;
            }

            return new SearchResponse
            {
                Id = documentEntity.Id,
                Title=documentEntity.Title
            };
        }

        public UserResponse ToUserResponse(UserEntity userEntity)
        {
            if (userEntity == null)
            {
                return null;
            }

            return new UserResponse
            {
                DocumentId = userEntity.DocumentId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Address = userEntity.Address,
                PicturePath = userEntity.PicturePath,
                UserType = userEntity.UserType,
                PhoneNumber = userEntity.PhoneNumber,
                Id = userEntity.Id,
                Email = userEntity.Email
            };
        }

        public TypeOfDocumentResponse ToTypeResponse(TypeOfDocumentEntity typeOfDocumentEntity)
        {
            if (typeOfDocumentEntity == null)
            {
                return null;
            }

            return new TypeOfDocumentResponse
            {
                Id = typeOfDocumentEntity.Id,
                Name = typeOfDocumentEntity.Name
            };
        }

        public DocumentLanguageResponse ToLanguageResponse(DocumentLanguageEntity documentLanguageEntity)
        {
            if (documentLanguageEntity == null)
            {
                return null;
            }

            return new DocumentLanguageResponse
            {
                Id = documentLanguageEntity.Id,
                Name = documentLanguageEntity.Name
            };
        }

        public AuthorResponse ToAuthorResponse(AuthorEntity authorEntity)
        {
            if (authorEntity == null)
            {
                return null;
            }

            return new AuthorResponse
            {
                Id = authorEntity.Id,
                Name = authorEntity.Name
            };
        }

        public GenderResponse ToGenderResponse(GenderEntity genderEntity)
        {
            if (genderEntity == null)
            {
                return null;
            }

            return new GenderResponse
            {
                Id = genderEntity.Id,
                Name = genderEntity.Name
            };
        }

        public async Task<ReviewEntity> ToReviewEntityAsync(ReviewViewModel model, bool isNew)
        {
            return new ReviewEntity
            {
                Id = isNew ? 0 : model.Id,
                Comment = model.Comment,
                Favorite = model.Favorite,
                Rating = model.Rating,
                User = model.User,
                Document = await _context.Documents.FindAsync(model.DocumentId)
            };
        }

        public ReviewViewModel ToReviewViewModel(ReviewEntity reviewEntity)
        {
            return new ReviewViewModel
            {
                Id = reviewEntity.Id,
                Comment = reviewEntity.Comment,
                Favorite = reviewEntity.Favorite,
                User = reviewEntity.User,
                Rating = reviewEntity.Rating,
                Document = reviewEntity.Document,
                DocumentId = reviewEntity.Document.Id
            };
        }

        public async Task<DocumentEntity> ToDocumentEntity(DocumentViewModel model, string path, bool isNew)
        {
            return new DocumentEntity
            {
                Id = isNew ? 0 : model.Id,
                DocumentPath = path,
                Title = model.Title,
                Date = model.Date.ToUniversalTime(),
                PagesNumber = model.PagesNumber,
                Summary = model.Summary,
                Accepted = model.Accepted,
                Gender = await _context.Genders.FindAsync(model.GenderId),
                Author = await _context.Authors.FindAsync(model.AuthorId),
                TypeOfDocument = await _context.TypeOfDocuments.FindAsync(model.TypeOfDocumentId),
                DocumentLanguage = await _context.DocumentLanguages.FindAsync(model.DocumentLanguageId),
            };
        }

        public DocumentViewModel ToDocumentViewModel(DocumentEntity documentEntity)
        {
            return new DocumentViewModel
            {
                Id = documentEntity.Id,
                DocumentPath = documentEntity.DocumentPath,
                Title = documentEntity.Title,
                Date = documentEntity.Date.ToLocalTime(),
                PagesNumber = documentEntity.PagesNumber,
                Summary = documentEntity.Summary,
                Gender = documentEntity.Gender,
                GenderId = documentEntity.Gender.Id,
                Accepted = documentEntity.Accepted,
                Genders = _combosHelper.GetComboGenders(),
                Author = documentEntity.Author,
                AuthorId = documentEntity.Author.Id,
                Authors = _combosHelper.GetComboAuthors(),
                TypeOfDocument = documentEntity.TypeOfDocument,
                TypeOfDocumentId = documentEntity.TypeOfDocument.Id,
                TypeOfDocuments = _combosHelper.GetComboTypeOfDocuments(),
                DocumentLanguage = documentEntity.DocumentLanguage,
                DocumentLanguageId = documentEntity.DocumentLanguage.Id,
                DocumentLanguages = _combosHelper.GetComboDocumentLanguages(),
            };
        }

    }
}    

