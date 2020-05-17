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
        SearchResponse ToDocumentResponse2(DocumentEntity documentEntity);
   
        List<SearchResponse> ToDocumentResponse2(List<DocumentEntity> documentEntities);

        ReviewResponse ToReviewResponse(ReviewEntity reviewEntity);

        List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities);

        Task<DocumentEntity> ToDocumentEntity(DocumentViewModel model, string path, bool isNew);

        DocumentViewModel ToDocumentViewModel(DocumentEntity documentEntity);

        UserResponse ToUserResponse(UserEntity userEntity);

        Task<ReviewEntity> ToReviewEntityAsync(ReviewViewModel model, bool isNew);

        ReviewViewModel ToReviewViewModel(ReviewEntity reviewEntity);

    }
}
