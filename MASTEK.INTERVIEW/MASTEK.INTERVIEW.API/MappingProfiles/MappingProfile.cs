using System;
using AutoMapper;
using MASTEK.INTERVIEW.API.Models;
using MASTEK.INTERVIEW.ENTITY;

namespace MASTEK.INTERVIEW.API.MappingProfiles
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Beer, BeerModel>();

			CreateMap<Brewery, BreweryModel>();
			CreateMap<Brewery, BreweryWithBeerModel>();

            CreateMap<Bar, BarModel>();
			CreateMap<Bar, BarWithBeerModel>();
			//.ForMember(d => d.Beers, o => o.MapFrom(s => s.Beers));

            CreateMap<BeerModel, Beer>();

            CreateMap<BreweryModel, Brewery>();
            CreateMap<BreweryBeerMappingModel, BreweryBeersMapping>();

            CreateMap<BarModel, Bar>();
            CreateMap<BarBeersMappingModel, BarBeersMapping>();

        }
	}
}

