using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentApi.Models;
using StudentApi.Repositories;

[TestClass]
public class StudentControllerTests
{
    private Mock<IStudentRepository> _mockRepository;
    private StudentController _controller;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<IStudentRepository>();
        _controller = new StudentController(_mockRepository.Object);
    }

    [TestMethod]
    public async Task GetStudents_ReturnsOkResult()
    {
        // Arrange
        var students = new List<Student> { new Student { ID = 1, Name = "John" }, new Student { ID = 2, Name = "Jane" } };
        _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(students);

        // Act
        var result = await _controller.GetStudents();

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = result.Result as OkObjectResult;
        Assert.AreEqual(students.Count, (okResult.Value as List<Student>).Count);
    }

    [TestMethod]
    public async Task GetStudent_ReturnsOkResult_ForExistingStudent()
    {
        // Arrange
        var student = new Student { ID = 1, Name = "John" };
        _mockRepository.Setup(x => x.GetById(student.ID)).ReturnsAsync(student);

        // Act
        var result = await _controller.GetStudent(student.ID);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        var okResult = result.Result as OkObjectResult;
        Assert.AreEqual(student.ID, (okResult.Value as Student).ID);
    }

    [TestMethod]
    public async Task GetStudent_ReturnsNotFoundResult_ForNonExistingStudent()
    {
        // Arrange
        int nonExistingId = 1;
        _mockRepository.Setup(x => x.GetById(nonExistingId)).ReturnsAsync(null as Student);

        // Act
        var result = await _controller.GetStudent(nonExistingId);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task PostStudent_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newStudent = new Student { ID = 1, Name = "John" };
        _mockRepository.Setup(x => x.Create(newStudent)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PostStudent(newStudent);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.AreEqual(nameof(_controller.GetStudent), createdResult.ActionName);
        Assert.AreEqual(newStudent.ID, createdResult.RouteValues["id"]);
        Assert.AreEqual(newStudent, createdResult.Value);
    }

    [TestMethod]
    public async Task PutStudent_ReturnsOkResult()
    {
        // Arrange
        var student = new Student { ID = 1, Name = "John" };
        _mockRepository.Setup(x => x.Update(student)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.PutStudent(student.ID, student);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DeleteStudent_ReturnsOkResult()
    {
        // Arrange
        var idToDelete = 1;
        _mockRepository.Setup(x => x.Delete(idToDelete)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteStudent(idToDelete);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }
}
