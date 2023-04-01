using MachiniaAPI.Model;
using MachiniaAPI.Service;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MachiniaDatabaseSettings>(builder.Configuration.GetSection("MachiniaDatabaseSettings")); //dependency injector 
builder.Services.AddSingleton<MachiniaService>();
var app = builder.Build();

app.MapGet("/", () => "Machinia Service Running!");

app.MapGet("/trainingcenters", async (MachiniaService service) => await service.Get());

app.MapPost("/trainingcenter", async (MachiniaService service, TrainingCenter center) =>
{
    // Ensure CenterCode does not exist
    var existingCenter = await service.Get(center.CenterCode);
    if (existingCenter != null)
    {
        return Results.BadRequest("Center Code already exists");
    }

    // Validation logic for Center Name
    if (string.IsNullOrWhiteSpace(center.CenterName))
    {
        return Results.BadRequest("Please enter the CenterName.");

    }
    else if(center.CenterName.Length > 40)
    {
        return Results.BadRequest("CenterName should be less than 40 characters");
    }

    // Validation logic for Center Code
    if (string.IsNullOrWhiteSpace(center.CenterCode) || center.CenterCode.Length != 12 || !Regex.IsMatch(center.CenterCode, "^[a-zA-Z0-9]+$"))
    {
        return Results.BadRequest("CenterCode should be exactly 12 character alphanumeric");
    }

    // Validation logic for Address
    if (center.Address == null || string.IsNullOrWhiteSpace(center.Address.DetailedAddress) || string.IsNullOrWhiteSpace(center.Address.City) || string.IsNullOrWhiteSpace(center.Address.State) || string.IsNullOrWhiteSpace(center.Address.Pincode))
    {
        return Results.BadRequest("Address is mandatory with DetailedAddress, City, State and Pincode");
    }

    // Validation logic for Student Capacity
    if (center.StudentCapacity < 0)
    {
        return Results.BadRequest("StudentCapacity should be greater than or equal to 0");
    }

    // Validation logic for Courses Offered
    if (center.CoursesOffered != null && !center.CoursesOffered.Any())
    {
        return Results.BadRequest("CoursesOffered should be a non-empty list of courses");
    }

    // validation logic for Contact Email
    if (!string.IsNullOrEmpty(center.ContactEmail) && !IsValidEmail(center.ContactEmail))
    {
        return Results.BadRequest("Invalid email format");
    }

    // validation logic for Contact Phone
    if (string.IsNullOrEmpty(center.ContactPhone) || !IsValidPhone(center.ContactPhone))
    {
        return Results.BadRequest("Invalid phone number format");
    }

    // set CreatedOn field as per server time
    center.CreatedOn = DateTime.Now.ToString();

    await service.Create(center);

    return Results.Ok(center);
    
});

bool IsValidEmail(string email)
{
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}

bool IsValidPhone(string phone)
{
    return Regex.IsMatch(phone, @"^\+?\d{10,}$");
}


app.MapGet("/trainingcenters/{centercode}", async (MachiniaService service, string centercode) =>
{
    var center = await service.Get(centercode);
    return center is null ? Results.NotFound() : Results.Ok(center);
});

app.Run();
