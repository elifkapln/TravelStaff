using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using EntityLayer.DTOs.TravelDTOs;

namespace TravelStaffAPI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateStaffDto, Staff>().ReverseMap();
			CreateMap<StaffListDto, Staff>().ReverseMap();
			CreateMap<AdminListDto, Staff>().ReverseMap();

			CreateMap<CreateStatusDto, Status>().ReverseMap();
			CreateMap<StatusListDto, Status>().ReverseMap();

			CreateMap<CreateTravelDto, Travel>().ReverseMap();
            CreateMap<TravelListDto, Travel>().ReverseMap();
            CreateMap<AdminTravelListDto, Travel>().ReverseMap();
            CreateMap<AdminUpdateTravelDto, Travel>().ReverseMap();

			CreateMap<CreateMessageDto, Message>().ReverseMap();
			CreateMap<GetMessageDto, Message>().ReverseMap();
			CreateMap<UpdateMessageDto, Message>().ReverseMap();

		}
	}
}
