﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(ICommandDispatcher commandDispatcher, IDriverService driverService) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.BrowseAsync();
            return Json(drivers);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = await _driverService.GetAsync(userId);
            if(driver == null)
            {
                return NotFound();
            }

            return Json(driver);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody]UpdateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Post()
        {
            await DispatchAsync(new DeleteDriver());

            return NoContent();
        }
    }
}