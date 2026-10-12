using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Persistence.DbContexts;

namespace EduFlowHub.Api.Seeders.TestDataSeeder;

public class TestDataSeeder
    : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    private const string TeacherName = "teacher";
    private const string StudentName = "student";
    private const string TeacherPassword = "Teacher123";
    private const string StudentPassword = "Student123";

    public TestDataSeeder(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
 
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
 
        if (await unitOfWork.UserRepository.GetByNameAsync(TeacherName, cancellationToken) is not null)
        {
            return;
        }

        var teacher = new User(
            TeacherName,
            hasher.HashPassword(TeacherPassword),
            UserRole.Teacher,
            "John",
            "Smith");
 
        var student = new User(
            StudentName,
            hasher.HashPassword(StudentPassword),
            UserRole.Student,
            "Emily",
            "Johnson");
 
        var programming = new CourseCategory("Programming");
        var design = new CourseCategory("Design");
        var languages = new CourseCategory("Languages");
        var mathematics = new CourseCategory("Mathematics");
 
        await unitOfWork.UserRepository.InsertAsync(teacher, cancellationToken);
        await unitOfWork.UserRepository.InsertAsync(student, cancellationToken);
 
        await unitOfWork.CourseCategoryRepository.InsertAsync(programming, cancellationToken);
        await unitOfWork.CourseCategoryRepository.InsertAsync(design, cancellationToken);
        await unitOfWork.CourseCategoryRepository.InsertAsync(languages, cancellationToken);
        await unitOfWork.CourseCategoryRepository.InsertAsync(mathematics, cancellationToken);
 
        await unitOfWork.SaveAsync(cancellationToken);
 
        var courses = new[]
        {
            new Course(
                "C# for Beginners",
                "C# fundamentals: data types, control flow, classes and collections.",
                19.99m, teacher.Id, programming.Id),
 
            new Course(
                "ASP.NET Core: Building Web APIs",
                "Building REST APIs: controllers, authentication and Entity Framework Core.",
                29.99m, teacher.Id, programming.Id),
 
            new Course(
                "UI/UX Design Basics",
                "Interface design principles, prototyping and working with mockups.",
                14.99m, teacher.Id, design.Id),
 
            new Course(
                "English Language: A2 Level",
                "Basic grammar, vocabulary and speaking practice for the Elementary level.",
                9.99m, teacher.Id, languages.Id),
 
            new Course(
                "Linear Algebra",
                "Vectors, matrices, systems of linear equations and linear transformations.",
                12.99m, teacher.Id, mathematics.Id)
        };
 
        foreach (var course in courses)
        {
            await unitOfWork.CourseRepository.InsertAsync(course, cancellationToken);
        }
 
        await unitOfWork.SaveAsync(cancellationToken);
    }
 
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
