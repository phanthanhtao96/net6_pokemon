﻿using AutoMapper;
using Ecm.Dto;
using Ecm.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mappper;
        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mappper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwners()
        {
            var owners = _mappper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerById(int ownerId) 
        {
            if (!_ownerRepository.OwnerExists(ownerId)) return NotFound();

            var owner = _mappper.Map<OwnerDto>(_ownerRepository.GetOwnerById(ownerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId) 
        {
            if (!_ownerRepository.OwnerExists(ownerId)) return NotFound();
            
            var pokemons = _mappper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(pokemons);
        }

        //[HttpGet("owner/pokemon/{pokemonId}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        //[ProducesResponseType(400)]
        //public IActionResult GetOwnerOfPokemon

    }
}
