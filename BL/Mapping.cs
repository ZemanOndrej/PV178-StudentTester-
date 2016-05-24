using System;
using System.Globalization;
using System.Runtime.Remoting;
using AutoMapper;
using BL.DTO;
using DAL.Entities;
using DAL2try.IdentityEntities;

namespace BL
{
	public class Mapping
	{
		public static IMapper Mapper { get; }
		static Mapping()
		{
			var config = new MapperConfiguration(c =>
			{


				c.CreateMap<StudentGroup, StudentGroupDTO>().ReverseMap();
				c.CreateMap<Question, QuestionDTO>().ReverseMap();
				
				c.CreateMap<ThematicArea,ThematicAreaDTO>().ReverseMap();



				c.CreateMap<TestTemplate, TestTemplateDTO>()
					.ForMember(d => d.Date,
						options => options.MapFrom(
							src => DateTime.Parse(src.Date)));
				c.CreateMap<TestTemplateDTO,TestTemplate>()
					.ForMember(d=>d.Date,
						options => options.MapFrom(
							src =>src.Date ));

				c.CreateMap<TestTemplate, TestTemplateDTO>().ReverseMap();
				c.CreateMap<Answer, AnswerDTO>().ReverseMap();
				c.CreateMap<UserDTO, AppUser>().ReverseMap();
			});
			Mapper = config.CreateMapper();
		}
	}
}