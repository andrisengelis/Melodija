﻿using System;

namespace Melodija.Domain
{
  public class ReleaseDto
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string SortTitle { get; set; }
  }
}