﻿namespace Ecm.Dto
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gym { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
    }
}
