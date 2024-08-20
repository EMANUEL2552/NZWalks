using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalksController(IMapper mapper, IWalkRepository walkRepository) 
		{
		   this.mapper = mapper;
			this.walkRepository = walkRepository;
		}

		//CREATE WALK POST
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
		{

			//map DTO to domain model
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
			await walkRepository.CreateAsync(walkDomainModel);

			//map domain model to DTO
			return Ok(mapper.Map<WalkDTO>(walkDomainModel));

		}

		//GET WALKS
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
		  var walksDomainModel = 	await walkRepository.GetAllAsync();

			//map domain to dto
			return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
		}

		//GET WALK BY ID
		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIdAsync(id);
			if(walkDomainModel == null) 
			{
			  return NotFound();
			}

			return Ok (mapper.Map<WalkDTO>(walkDomainModel));
		}


	}
}
