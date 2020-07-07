﻿using Passenger.Infrastructure.Commands.Models;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle { get; set; }
    }
  
}
