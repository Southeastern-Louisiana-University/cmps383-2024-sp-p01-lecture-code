using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Features.Appointments;

namespace Selu383.SP24.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase
{
    private readonly DataContext dataContext;

    public AppointmentsController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    public ActionResult MakeAppointment(Appointment appointment)
    {

        dataContext.Set<Appointment>().Add(appointment);
        dataContext.SaveChanges();

        return Ok(appointment);
    }

    [HttpGet("recent")]
    public ActionResult<Appointment> GetRecentAppointment()
    {
        var lastAppointment = dataContext.Set<Appointment>()
            .Where(x => x.Occurred.HasValue)
            .OrderByDescending(x => x.Occurred)
            .FirstOrDefault();
        if (lastAppointment == null)
        {
            return NotFound();
        }

        return lastAppointment;
    }

    [HttpGet("all")]
    public List<Appointment> GetAllAppointments()
    {
        return dataContext.Set<Appointment>().ToList();
    }

    [HttpPut("{id}")]
    public ActionResult<Appointment> UpdateAppointment(int id, Appointment appointment)
    {
        var targetAppointment = dataContext.Set<Appointment>().FirstOrDefault(x => x.Id == id);
        if (targetAppointment == null)
        {
            return NotFound();
        }
        targetAppointment.Occurred = appointment.Occurred;
        dataContext.SaveChanges();

        return targetAppointment;
    }

    [HttpDelete("{id}")]
    public ActionResult<Appointment> DeleteAppointment(int id)
    {
        var targetAppointment = dataContext.Set<Appointment>().FirstOrDefault(x => x.Id == id);
        if (targetAppointment == null)
        {
            return NotFound();
        }

        dataContext.Set<Appointment>().Remove(targetAppointment);
        dataContext.SaveChanges();

        return Ok(targetAppointment);
    }

}