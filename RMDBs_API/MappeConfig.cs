using AutoMapper;
using RMDBs_API.Model.DTO;
using RMDBs_API.Model;
using RMDBs_API.DTOs;

namespace RMDBs_API
{
    public class MappeConfig : Profile
    {
        public MappeConfig() 
        
        {
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, CreateActor>().ReverseMap();
            CreateMap<Actor, UpdatetActor>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreUpdateDTO>().ReverseMap();
            CreateMap<Genre, GenreCreateDTO>().ReverseMap();

            CreateMap<Award, AwardDTO>().ReverseMap();
            CreateMap<AwardCreateDTO, Award>();
            CreateMap<AwardUpdateDTO, Award>();

            CreateMap<PositionCreateDTO, Position>().ReverseMap();
            CreateMap<PositionUpdateDTO, Position>().ReverseMap();
            CreateMap<Position, PositionDTO>().ReverseMap();

            CreateMap<Movie, MovieDTO>()
             .ForMember(dest => dest.ProductionStatus,
                        opt => opt.MapFrom(src => src.productionStatus.ToString())); 
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();


            CreateMap<AwardCategory, AwardCategoryDTO>();
            CreateMap<AwardCategoryCreateDTO, AwardCategory>();
            CreateMap<AwardCategoryUpdateDTO, AwardCategory>();


            CreateMap<User, UserDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

           


            CreateMap<ReceiverType, ReciverTypeDTO>();
            CreateMap<ReciverTypeCreateDTO, ReceiverType>();
            CreateMap<ReciverTypeUpdateDTO, ReceiverType>();

            CreateMap<Popularity, PopularityDTO>().ReverseMap();
            CreateMap<Popularity, PopularityCreateDTO>().ReverseMap();
            CreateMap<Popularity, PopularityUpdateDTO>().ReverseMap();

            CreateMap<Comment, CommentDTO>();          
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();

            CreateMap<MovieCast, MovieCastDTO>().ReverseMap();
            CreateMap<MovieCastCreateDTO, MovieCast>().ReverseMap();
            CreateMap<MovieCastUpdateDTO, MovieCast>().ReverseMap();


            CreateMap<MovieGenre, MovieGenreDTO>().ReverseMap();
            CreateMap<MovieGenreCreateDTO, MovieGenre>().ReverseMap();
            CreateMap<MovieGenreUpdateDTO, MovieGenre>().ReverseMap();
            CreateMap<Position, PositionDTO>();


            CreateMap<MovieMedia1, MovieMedia1DTO>()
        .ForMember(dest => dest.ReceiverType, opt => opt.MapFrom(src => src.ReceiverType.ToString()));

            CreateMap<MovieMedia1CreateDTO, MovieMedia1>();
            CreateMap<MovieMedia1UpdateDTO, MovieMedia1>();



            CreateMap<ActorMovieAward, ActorMovieAwardDTO>().ReverseMap();
            CreateMap<ActorMovieAwardCreateDTO, ActorMovieAward>().ReverseMap();
            CreateMap<ActorMovieAwardUpdateDTO, ActorMovieAward>().ReverseMap();

            CreateMap<UserRating, UserRatingDTO>().ReverseMap();
            CreateMap<UserRating, UserRatingCreateDTO>().ReverseMap();
            CreateMap<UserRating, UserRatingUpdateDTO>().ReverseMap();



        }
    }
}
