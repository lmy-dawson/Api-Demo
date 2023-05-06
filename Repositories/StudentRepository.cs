using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using StudentApi.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentContext _dbContext;

    public StudentRepository(StudentContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        return await _dbContext.Students.ToListAsync();
    }

    public async Task<Student> GetById(int id)
    {
        return await _dbContext.Students.FindAsync(id);
    }

    public async Task Create(Student student)
    {
        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Student student)
    {
        _dbContext.Entry(student).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var student = await GetById(id);
        if (student == null)
        {
            return;
        }

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
    }
}
