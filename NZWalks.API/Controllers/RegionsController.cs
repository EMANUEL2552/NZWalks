using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
		private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
			this.regionRepository = regionRepository;
        }


        //GET ALL REGIONS
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//get data from database - domain models
			var regionsDomain = await regionRepository.GetAllAsync();
			

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
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute]Guid id) 
		{

          //GET REGION DOMAIN MODEL FROM DATABASE
		  var regionDomain = await regionRepository.GetByIdAsync(id);

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
		public async Task<IActionResult> Create([FromBody] AddegionRequestDto addegionRequestDto)
		{
			//MAP OR CONVERT DTO TO DOMAIN MODEL
			var regionDomainModel = new Region
			{
				Code = addegionRequestDto.Code,
				Name = addegionRequestDto.Name,
				RegionImageUrl= addegionRequestDto.RegionImageUrl
			};

			//USE DOMAIN MODEL TO CREATE REGION
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
			

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
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
		{
			//map DTO  to domain model
			var regionDomainModel = new Region
			{
				Code = updateRegionRequestDTO.Code,
				Name = updateRegionRequestDTO.Name,
				RegionImageUrl = updateRegionRequestDTO.RegionImageUrl

			};

			//CHECK IF REGION EXISTS
			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

			if (regionDomainModel == null) 
			{
			  return NotFound();
			}

			

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

		//DELETE REGION
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id) 
		{
			var regionDomainModel = await regionRepository.DeleteAsync(id);

			if (regionDomainModel == null) 
			{
				return NotFound();
			}

			//map domain model to Dto

			var regionDTO = new RegionDTO
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			
			return Ok(regionDTO);
		}
	}
}
