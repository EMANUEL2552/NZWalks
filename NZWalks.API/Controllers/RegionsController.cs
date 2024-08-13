using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //GET ALL REGIONS
        [HttpGet]
		public IActionResult GetAll()
		{
			var regionsDomain = dbContext.Regions.ToList();
			

			//MAP DOMAIN MODELS TO DTOS
			var regionsDto = new List<RegionDTO>();
			foreach (var regionDomain in regionsDomain) 
			{
				regionsDto.Add(new RegionDTO()
				{
					Id = regionDomain.Id,
					Code = regionDomain.Code,
					Name = regionDomain.Name,
					RegionImageUrl = regionDomain.RegionImageUrl

				});
			}


			return Ok(regionsDto);
		}

		//GET REGION BY ID
		[HttpGet]
		[Route("{id:guid}")]
		public IActionResult GetById([FromRoute]Guid id) 
		{

          //GET REGION DOMAIN MODEL FROM DATABASE
		  var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if(regionDomain == null) 
			{
			  return NotFound();
			}

			//Map/Convert Region Domain Model to region DTO
			//
			var regionDto = new RegionDTO
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl

			};

			//return DTO back to client
			return Ok(regionDto);
		}

		//POST TO CREATE A NEW REGION
		[HttpPost]
		public IActionResult Create([FromBody] AddegionRequestDto addegionRequestDto)
		{
			//MAP OR CONVERT DTO TO DOMAIN MODEL
			var regionDomainModel = new Region
			{
				Code = addegionRequestDto.Code,
				Name = addegionRequestDto.Name,
				RegionImageUrl= addegionRequestDto.RegionImageUrl
			};

			//USE DOMAIN MODEL TO CREATE REGION
			dbContext.Regions.Add(regionDomainModel);
			dbContext.SaveChanges();

			//map domain model back to DTO
			var regionDto = new RegionDTO
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		//UPDATE REGION PUT
		[HttpPut]
		[Route("{id:guid}")]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
		{
			//CHECK IF REGION EXISTS
			var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if (regionDomainModel == null) 
			{
			  return NotFound();
			}

			// MAP DTO TO DOMAIN MODEL
			regionDomainModel.Code = updateRegionRequestDTO.Code;
			regionDomainModel.Name = updateRegionRequestDTO.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

			dbContext.SaveChanges();

			//CONVERT DOMAIN MODEL TO DTO
			var regionDto = new RegionDTO
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl


			};

			return Ok(regionDto);
		}
	}
}
