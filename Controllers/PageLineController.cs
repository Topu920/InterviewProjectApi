using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewProjectApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PageLineController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public PageLineController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<string>> Get()
        {
            var lines=new List<string>();
            var model = await _dbContext.Lines.ToListAsync();
            foreach (var VARIABLE in model)
            {
                lines.Add(VARIABLE.LineofString);
            }
            return lines;
        }
        [HttpPost]
        [ActionName("page-size")]
        public async Task<IActionResult> pagesize([FromHeader] int n, [FromBody] List<string>words)
        {
            try
            {
                if (words != null)
                {

                    string line="";
                    List<string> lines = new List<string>();
                    bool isLimitCross = false;
                   
                    for (int i=0;i<words.Count;i++)
                    {
                        
                        if (line == "")
                        {
                            line += words[i];
                        }
                        else if (line.Length + words[i].Length +1<=n)
                        {
                            line=line+" " + words[i];
                        }
                        else if(line.Length + words[i].Length + 1 > n)
                        {
                            i--;
                            isLimitCross = true;
                        }

                        if (isLimitCross)
                        {
                           
                         lines.Add(line);
                            line = "";
                          isLimitCross=false;
                        }
                      
                    }
                    lines.Add(line);
                    foreach (var str in lines)
                    {
                        var model = new Line
                        {
                            Id = 0,
                            LineofString = str
                        };
                        await _dbContext.Lines.AddAsync(model);
                        await _dbContext.SaveChangesAsync();
                    }
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return StatusCode(201);

        }
    }
}
