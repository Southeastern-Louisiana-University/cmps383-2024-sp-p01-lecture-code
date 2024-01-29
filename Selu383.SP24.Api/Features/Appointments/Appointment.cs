namespace Selu383.SP24.Api.Features.Appointments;

public class Appointment
{
    public int Id { get; set; }

    public DateTimeOffset? Occurred { get; set; }

    public string Name { get; set; }
}