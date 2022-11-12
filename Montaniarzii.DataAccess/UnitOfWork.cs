using Montaniarzii.Common;
using Montaniarzii.DataAccess.Context;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.DataAccess
{
    public class UnitOfWork
    {
        private readonly MontaniarziiContext Context;

        public UnitOfWork(MontaniarziiContext context)
        {
            Context = context;
        }

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<Trip> trips { get; set; }
        public IRepository<Trip> Trips => trips ?? (trips = new BaseRepository<Trip>(Context));
        private IRepository<TripXattraction> tripsXAttractions { get; set; }
        public IRepository<TripXattraction> TripsXAttractions => tripsXAttractions ?? (tripsXAttractions = new BaseRepository<TripXattraction>(Context));
        private IRepository<Attraction> attractions { get; set; }
        public IRepository<Attraction> Attractions => attractions ?? (attractions = new BaseRepository<Attraction>(Context));
        private IRepository<Invitation> invitations { get; set; }
        public IRepository<Invitation> Invitations => invitations ?? (invitations = new BaseRepository<Invitation>(Context));
        private IRepository<ParticipationInTrip> participationInTrips { get; set; }
        public IRepository<ParticipationInTrip> ParticipationInTrips => participationInTrips ?? (participationInTrips = new BaseRepository<ParticipationInTrip>(Context));
        private IRepository<Follow> follows { get; set; }
        public IRepository<Follow> Follows => follows ?? (follows = new BaseRepository<Follow>(Context));
        private IRepository<Warning> warnings { get; set; }
        public IRepository<Warning> Warnings => warnings ?? (warnings = new BaseRepository<Warning>(Context));
        private IRepository<SuggestionAttraction> suggestionAttractions { get; set; }
        public IRepository<SuggestionAttraction> SuggestionAttractions => suggestionAttractions ?? (suggestionAttractions = new BaseRepository<SuggestionAttraction>(Context));
        private IRepository<Photo> photos { get; set; }
        public IRepository<Photo> Photos => photos ?? (photos = new BaseRepository<Photo>(Context));
        private IRepository<TrainStation> trainStations { get; set; }
        public IRepository<TrainStation> TrainStations => trainStations ?? (trainStations = new BaseRepository<TrainStation>(Context));
        private IRepository<Like> likes { get; set; }
        public IRepository<Like> Likes => likes ?? (likes = new BaseRepository<Like>(Context));

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
