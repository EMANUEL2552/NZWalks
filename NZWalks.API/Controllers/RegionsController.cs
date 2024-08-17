using AutoMapper;
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
		private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
			IMapper mapper)
        {
            this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
        }


        //GET ALL REGIONS
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//get data from database - domain models
			var regionsDomain = await regionRepository.GetAllAsync();
			

			
			//map domain model to dto
			var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

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
			

			//return DTO back to client
			return Ok(mapper.Map<RegionDTO>(regionDomain));
		}

		//POST TO CREATE A NEW REGION
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddegionRequestDto addegionRequestDto)
		{
			//MAP OR CONVERT DTO TO DOMAIN MODEL
			var regionDomainModel = mapper.Map<Region>(addegionRequestDto);

			//USE DOMAIN MODEL TO CREATE REGION
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
			

			//map domain model back to DTO
			var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		//UPDATE REGION PUT
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
		{
			//map DTO  to domain model
			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

			//CHECK IF REGION EXISTS
			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

			if (regionDomainModel == null) 
			{
			  return NotFound();
			}

			

			//CONVERT DOMAIN MODEL TO DTO
			var regionDto = mapper.Map<RegionDTO> (regionDomainModel);

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

			var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
			
			return Ok(regionDTO);
		}
	}
}
