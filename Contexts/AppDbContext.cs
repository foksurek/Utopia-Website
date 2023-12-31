﻿using Microsoft.EntityFrameworkCore;
using Utopia.Dto;
using Utopia.Dto;

namespace Utopia.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SqlUser> SqlUsers { get; set; } = null!;
    public DbSet<Stat> Stats { get; set; } = null!;
}