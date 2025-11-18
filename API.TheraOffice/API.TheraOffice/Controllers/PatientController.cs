using Microsoft.AspNetCore.Mvc;
using Library.TheraOffice.Models;
using Library.TheraOffice.Data;
using API.TheraOffice.Enterprise;

namespace API.TheraOffice.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;

    public PatientController(ILogger<PatientController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public Patient? GetById(int id)
    {
        return new PatientEC().GetById(id);
    }
    
    [HttpGet]
    public IEnumerable<Patient> Get()
    {
        return new PatientEC().GetPatients();
    }

    [HttpPost]
    public Patient? Create([FromBody] Patient patient)
    {
        return new PatientEC().Create(patient);
    }

    [HttpDelete("{id}")]
    public Patient? Delete(int id)
    {
        return new PatientEC().Delete(id);
    }

    [HttpPost("Search")]
    public IEnumerable<Patient> Search([FromBody] QueryRequest query)
    {
        return new PatientEC().Search(query.Content);
    }
}