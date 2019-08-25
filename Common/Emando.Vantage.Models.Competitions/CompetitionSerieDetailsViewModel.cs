﻿using System;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitionSerieDetailsViewModel
    {
        public Guid Id { get; set; }

        public string LicenseIssuerId { get; set; }

        public string Discipline { get; set; }

        public int Season { get; set; }

        public string Name { get; set; }

        public CompetitionViewModel[] Competitions { get; set; }
    }
}