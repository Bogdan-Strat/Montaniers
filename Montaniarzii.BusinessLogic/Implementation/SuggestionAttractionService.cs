using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models;
using Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Validation;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Common.Extensions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class SuggestionAttractionService : BaseService
    {
        private readonly CreateSuggestionAttractionValidation CreateSuggestionAttractionValidation;
        private readonly ApproveSuggestionModelValidation ApproveSuggestionModelValidation;
        public SuggestionAttractionService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            CreateSuggestionAttractionValidation = new CreateSuggestionAttractionValidation();
            ApproveSuggestionModelValidation = new ApproveSuggestionModelValidation();
        }

        public async Task CreateSuggestionAttraction(CreateSuggestionAttractionModel model)
        {
            CreateSuggestionAttractionValidation.Validate(model).ThenThrow(model);

            var suggestionAttraction = Mapper.Map<CreateSuggestionAttractionModel, SuggestionAttraction>(model);

            suggestionAttraction.SuggestionAttractionId = Guid.NewGuid();
            suggestionAttraction.CreatedByUserId = CurrentUser.Id;
            suggestionAttraction.CreateDate = DateTime.Now;
            suggestionAttraction.TypeAttractionId = model.TypeAttractionId;
            if(model.Height != "")
            {
                suggestionAttraction.Height = int.Parse(model.Height);
            }

            UnitOfWork.SuggestionAttractions.Insert(suggestionAttraction);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<SuggestionAttractionFewDetailsModel>> GetAllYourSuggestionAttraction()
        {
            return await UnitOfWork.SuggestionAttractions
                .Get()
                .Where(sa => sa.CreatedByUserId == CurrentUser.Id)
                .OrderByDescending(sa => sa.CreateDate)
                .Select(sa => new SuggestionAttractionFewDetailsModel()
                {
                    SuggestionAttractionId = sa.SuggestionAttractionId,
                    AttractionName = sa.AttractionName,
                    TypeAttraction = sa.TypeAttraction.TypaAttractionName,
                    CretedDate = sa.CreateDate,
                    IsAccepted = sa.IsApproved
                })
                .ToListAsync();
        }

        public async Task<SuggestionAttractionAllDetailsModel> GetSuggestionAttractionById(Guid suggestionId)
        {
            var suggestionAttraction = await UnitOfWork.SuggestionAttractions
                .Get()
                .Where(sa => sa.SuggestionAttractionId == suggestionId)
                .Include(sa => sa.TypeAttraction)
                .Select(sa => Mapper.Map<SuggestionAttraction, SuggestionAttractionAllDetailsModel>(sa))
                .SingleOrDefaultAsync();

            if(suggestionAttraction == null)
            {
                throw new NotFoundErrorException();
            }

            return suggestionAttraction;
        }


        public async Task<SuggestionAttractionAllDetailsModelAsAdmin> GetSuggestionAttractionByIdAsAdmin(Guid suggestionId)
        {
            var suggestionAttraction = await UnitOfWork.SuggestionAttractions
                .Get()
                .Where(sa => sa.SuggestionAttractionId == suggestionId)
                .Include(sa => sa.TypeAttraction)
                .Select(sa => Mapper.Map<SuggestionAttraction, SuggestionAttractionAllDetailsModelAsAdmin>(sa))
                .SingleOrDefaultAsync();

            if (suggestionAttraction == null)
            {
                throw new NotFoundErrorException();
            }

            return suggestionAttraction;
        }

        public async Task<GetSuggestionsPagedModel> GetAllSuggestionAttractions(int pageNumber = 0)
        {
            if (pageNumber > 0)
            {
                pageNumber--;
            }

            var model = new GetSuggestionsPagedModel()
            {
                NumberOfSuggestions = await UnitOfWork.SuggestionAttractions
                    .Get()
                    .CountAsync(),
                ActualPageNumber = pageNumber + 1
            };
            model.ListOfSuggestions = await UnitOfWork.SuggestionAttractions
                .Get()
                .Include(sa => sa.User)
                .Include(sa => sa.TypeAttraction)
                .OrderByDescending(sa => sa.CreateDate)
                .OrderBy(sa => sa.IsApproved)
                .Skip(pageNumber * 5)
                .Take(5)
                .Select(sa => Mapper.Map<SuggestionAttraction, SuggestionAttractionAllDetailsModelAsAdmin>(sa))
                .ToListAsync();


            return model;
        }


        public async Task RejectSuggestionAttraction(Guid suggestionId)
        {
            var suggestion = await GetSuggestionById(suggestionId);

            if(suggestion == null)
            {
                throw new NotFoundErrorException();
            }

            suggestion.IsApproved = false;

            UnitOfWork.SuggestionAttractions.Update(suggestion);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task ApproveSuggestion(ApproveSuggestionModel model)
        {
            ApproveSuggestionModelValidation.Validate(model).ThenThrow(model);

            await ExecuteInTransactionAsync(async unitOfWork =>
            {
                var suggestion = await GetSuggestionById(model.SuggestionAttractionId);

                if (suggestion == null)
                {
                    throw new NotFoundErrorException();
                }

                suggestion.IsApproved = true;
                UnitOfWork.SuggestionAttractions.Update(suggestion);
                await UnitOfWork.SaveChangesAsync();

                var newAttraction = new Attraction()
                {
                    AttractionId = Guid.NewGuid(),
                    Name = model.AttractionName,
                    TypeAttractionId = model.TypeAttractionId,
                    Location = model.Location,
                    Height = int.Parse(model.Height),
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Description = model.Description,
                    IsDeleted = false

                };

                UnitOfWork.Attractions.Insert(newAttraction);
                await UnitOfWork.SaveChangesAsync();
            });
            
        }

        private async Task<SuggestionAttraction> GetSuggestionById(Guid suggestionId)
        {
            var suggestion = await UnitOfWork.SuggestionAttractions
                .Get()
                .SingleOrDefaultAsync(sa => sa.SuggestionAttractionId == suggestionId);

            if(suggestion == null)
            {
                throw new NotFoundErrorException();
            }

            return suggestion;
        }
    }
}
