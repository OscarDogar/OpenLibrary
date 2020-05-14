using Microsoft.CodeAnalysis;
using OpenLibrary.Common.Models;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity userEntity);
   
        List<UserResponse> ToUserResponse(List<UserEntity> userEntities);
        ReviewResponse ToReviewResponse(ReviewEntity reviewEntity);

        List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities);

        Task<DocumentEntity> ToDocumentEntity(DocumentViewModel model, string path, bool isNew);

        DocumentViewModel ToDocumentViewModel(DocumentEntity documentEntity);

        Task<ReviewEntity> ToReviewEntityAsync(ReviewViewModel model, bool isNew);

        ReviewViewModel ToReviewViewModel(ReviewEntity reviewEntity);

    }
}
