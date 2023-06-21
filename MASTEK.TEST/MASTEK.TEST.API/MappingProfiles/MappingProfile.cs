using System;
using AutoMapper;
using MASTEK.TEST.API.Models;
using MASTEK.TEST.ENTITY;

namespace MASTEK.TEST.API.MappingProfiles
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Beer, BeerModel>();
			CreateMap<BeerModel,Beer>();

        }
	}
}

