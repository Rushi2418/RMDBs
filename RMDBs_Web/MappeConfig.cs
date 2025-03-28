using AutoMapper;
 using RMDBs_Web.Models.DTO;

namespace RMDBs_Web
{
    public class MappeConfig : Profile
    {
        public MappeConfig()
        {
            CreateMap<ActorDTO, CreateActor>().ReverseMap();
            CreateMap<ActorDTO, UpdatetActor>().ReverseMap();

            CreateMap<GenreDTO, GenreCreateDTO>().ReverseMap();
            CreateMap<GenreDTO, GenreUpdateDTO>().ReverseMap();

            CreateMap<AwardDTO, AwardCreateDTO>().ReverseMap();
            CreateMap<AwardDTO, AwardUpdateDTO>().ReverseMap();

            CreateMap<PositionDTO, PositionCreateDTO>().ReverseMap();
            CreateMap<PositionDTO, PositionUpdateDTO>().ReverseMap();

            CreateMap<MovieDTO, MovieCreateDTO>().ReverseMap();
            CreateMap<MovieDTO, MovieUpdateDTO>().ReverseMap();

            CreateMap<AwardCategoryDTO, AwardCategoryCreateDTO>().ReverseMap();
            CreateMap<AwardCategoryDTO, AwardCategoryUpdateDTO>().ReverseMap();

            CreateMap<UserDTO, UserCreateDTO>().ReverseMap();
            CreateMap<UserDTO, UserUpdateDTO>().ReverseMap();

            CreateMap<ReciverTypeDTO, ReciverTypeCreateDTO>().ReverseMap();
            CreateMap<ReciverTypeDTO, ReciverTypeUpdateDTO>().ReverseMap();

            CreateMap<PopularityDTO, PopularityCreateDTO>().ReverseMap();
            CreateMap<PopularityDTO, PopularityUpdateDTO>().ReverseMap();

            CreateMap<CommentDTO, CommentCreateDTO>().ReverseMap();
            CreateMap<CommentDTO, CommentUpdateDTO>().ReverseMap();

            CreateMap<MovieCastDTO, MovieCastCreateDTO>().ReverseMap();
            CreateMap<MovieCastDTO, MovieCastUpdateDTO>().ReverseMap();

            CreateMap<MovieGenreDTO, MovieGenreCreateDTO>().ReverseMap();
            CreateMap<MovieGenreDTO, MovieGenreUpdateDTO>().ReverseMap();

            CreateMap<MovieMediaDTO, MovieMediaCreateDTO>()
                .ForMember(dest => dest.MediaType,
                    opt => opt.MapFrom(src => src.MediaType.ToString()))
                .ReverseMap();
            CreateMap<MovieMediaDTO, MovieMediaUpdateDTO>().ReverseMap();

            CreateMap<ActorMovieAwardDTO, ActorMovieAwardCreateDTO>().ReverseMap();
            CreateMap<ActorMovieAwardDTO, ActorMovieAwardUpdateDTO>().ReverseMap();

            CreateMap<UserRatingDTO, UserRatingCreateDTO>().ReverseMap();
            CreateMap<UserRatingDTO, UserRatingUpdateDTO>().ReverseMap();
        }
    }
}
