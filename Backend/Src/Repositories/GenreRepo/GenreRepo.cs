namespace Backend.Src.Repositories;

using Backend.Src.Models;
using Backend.Src.Db;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Src.DTOs;
using Microsoft.EntityFrameworkCore;

public class GenreRepo : BaseRepoName<Genre>
{
    public GenreRepo(AppDbContext context) : base(context)
    {}
}