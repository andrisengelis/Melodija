﻿using Microsoft.EntityFrameworkCore;

namespace Melodija.Data
{
  public class MelodijaContext : DbContext
  {
    public MelodijaContext(DbContextOptions options) : base(options)
    {
      
    }
  }
}