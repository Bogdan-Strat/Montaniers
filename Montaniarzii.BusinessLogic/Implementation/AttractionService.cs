using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Attractions.Models;
using Montaniarzii.Common.DTOs;
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
    public class AttractionService : BaseService
    {
        //private readonly CreateTripValidation CreateTripValidator;
        public AttractionService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            //this.CreateTripValidator = new CreateTripValidation();
        }

        private string GetAttractionNameById(Guid attractionId)
        {
            var attraction = UnitOfWork.Attractions
                .Get()
                .Where(a => a.AttractionId == attractionId)
                .Select(a => a.Name)
                .SingleOrDefault();

            if(attraction == null)
            {
                throw new NotFoundErrorException();
            }

            return attraction;
        }

        public async Task<List<GuidSelectListItemModel<Attraction>>> GetAllAttractionsByPartiallyName(string partOfName)
        {
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionWithoutDiacritics =  attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().Contains(partOfName.ToLower().RemoveDiacritics()))
                .Select(a => new GuidSelectListItemModel<Attraction>()
                {
                    Id = a.AttractionId,
                    Name = a.Name
                })
                .Take(50)
                .ToList();

            return attractionWithoutDiacritics;
        }

        public async Task<List<CoordinatesAttractionModel>> GetAllAttractionsWithCoordinatesByPartiallyName(string partOfName)
        {
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionWithoutDiacritics = attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().Contains(partOfName.ToLower().RemoveDiacritics()))
                .Select(a => new CoordinatesAttractionModel()
                {
                    Id = a.AttractionId,
                    Name = a.Name,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                })
                .Take(30)
                .ToList();

            return attractionWithoutDiacritics;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAllAttractionsPagedByPartiallyName(string partOfName, int count = 0)
        {
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionWithoutDiacritics = attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().Contains(partOfName.ToLower().RemoveDiacritics()))
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToList();

            return attractionWithoutDiacritics;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAllAttractions(int count = 0)
        {
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false)
                .OrderBy(a => a.Name)
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToListAsync();

            return attractions;
        }

        public async Task<AllDetailsAttractionModel> GetAttractionInformation(Guid attractionId)
        {
            var attraction = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.AttractionId == attractionId && a.IsDeleted == false)
                .Select(a => Mapper.Map<Attraction, AllDetailsAttractionModel>(a))
                .SingleOrDefaultAsync();

            if (attraction == null)
            {
                throw new NotFoundErrorException();
            }

            return attraction;
        }

        public async Task<List<string>> GeAllMountains()
        {
            var mountains = await UnitOfWork.Attractions
                .Get()
                .Where(a => a.IsDeleted == false && a.Mountains != null)
                .Select(a => a.Mountains)
                .OrderBy(a =>a)
                .Distinct()
                .ToListAsync();

            return mountains;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAttractionsPagedFilteredByMountains(string mountains, int count = 0)
        {
            var mountainsList = mountains.Split(',');
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false && mountainsList.Contains(a.Mountains))
                .OrderBy(a => a.Name)
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToListAsync();

            return attractions;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAllAttractionsPagedFilteredByPartiallyNameAndMountains(string? partOfName, string? mountains, int count = 0)
        {
            if(partOfName == null)
            {
                partOfName = "";
            }

            var mountainsList = new List<string>();
            if(!(mountains == null))
            {
                mountainsList = mountains.Split(',')
                    .ToList();
            }
            else
            {
                mountainsList = await GeAllMountains();
            }
            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false && mountainsList.Contains(a.Mountains))
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionsWithoutDiacritics = attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().ManualContains(partOfName.ToLower().RemoveDiacritics()))
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToList();

            return attractionsWithoutDiacritics;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAllAttractionsPagedFilteredByPartiallyNameMountainsAndHeight(string? partOfName, string? mountains, int height, int count = 0)
        {
            if (partOfName == null)
            {
                partOfName = "";
            }

            var mountainsList = new List<string>();
            if (!(mountains == null))
            {
                mountainsList = mountains.Split(',')
                    .ToList();
            }
            else
            {
                mountainsList = await GeAllMountains();
            }

            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false && mountainsList.Contains(a.Mountains) && a.Height >= height)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionsWithoutDiacitics = attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().ManualContains(partOfName.ToLower().RemoveDiacritics()))
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToList();

            return attractionsWithoutDiacitics;
        }

        public async  Task<List<GuidSelectListItemModel<Attraction>>> GetTrendingAttractions()
        {
            var attractions = await UnitOfWork.TripsXAttractions
                .Get()
                .Where(txa => txa.Trip.CreateDate >= DateTime.Now.AddMonths(-1) && txa.Trip.TripDate >= DateTime.Now.AddMonths(-1) && txa.Trip.IsDeleted == false)
                .GroupBy(txa => txa.AttractionId)
                .Select(a => new GuidCountSelectListItemModel()
                {
                    Id = a.Key,
                    Count = a.Count()
                })
                .OrderByDescending(a => a.Count)
                .Take(5)
                .ToListAsync();


            var trendingAttractions = attractions
                .Select(a => new GuidSelectListItemModel<Attraction>()
                {
                    Id = a.Id,
                    Name = GetAttractionNameById(a.Id)
                })
                .ToList();

            return trendingAttractions;
        }

        public async Task<int?> GetMaxHeightOfAttraction()
        {
            var maxHeightAttraction = await UnitOfWork.Attractions
                .Get()
                .Where(a => a.IsDeleted == false && a.Height.HasValue)
                .OrderByDescending(a => a.Height)
                .FirstOrDefaultAsync();

            if (maxHeightAttraction == null)
            {
                throw new NotFoundErrorException();
            }

            return maxHeightAttraction.Height;
        }

        public async Task<int?> GetMinHeightOfAttraction()
        {
            var maxHeightAttraction = await UnitOfWork.Attractions
                .Get()
                .Where(a => a.IsDeleted == false && a.Height.HasValue)
                .OrderBy(a => a.Height)
                .FirstOrDefaultAsync();

            if (maxHeightAttraction == null)
            {
                throw new NotFoundErrorException();
            }

            return maxHeightAttraction.Height;
        }

        public async Task<List<FewDetailsAttractionModel>> GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation(string? partOfName, string? mountains, int height, string? location, int count = 0)
        {
            if (partOfName == null)
            {
                partOfName = "";
            }

            if(location == null)
            {
                location = "";
            }

            var mountainsList = new List<string>();
            if (!(mountains == null))
            {
                mountainsList = mountains.Split(',')
                    .ToList();
            }
            else
            {
                mountainsList = await GeAllMountains();
            }

            var attractions = await UnitOfWork.Attractions
                .Get()
                .Include(a => a.TypeAttraction)
                .Where(a => a.IsDeleted == false && mountainsList.Contains(a.Mountains) && a.Height >= height)
                .OrderBy(a => a.Name)
                .ToListAsync();

            var attractionsWithoutDiacitics = attractions
                .Where(a => a.Name.ToLower().RemoveDiacritics().ManualContains(partOfName.ToLower().RemoveDiacritics()) 
                        && a.Location.ToLower().RemoveDiacritics().ManualContains(location.ToLower().RemoveDiacritics()))
                .Skip(count * 6)
                .Take(6)
                .Select(a => Mapper.Map<Attraction, FewDetailsAttractionModel>(a))
                .ToList();

            return attractionsWithoutDiacitics;
        }



    }
}
