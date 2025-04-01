using Microsoft.EntityFrameworkCore;
using Practical_17.Data;
using Practical_17.Models;
using Practical_17.Services;
using System.Threading.Tasks;

namespace Practical_17.Services
{
    public class StudentRepository : IStudentRepository
    {
        ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await GetStudentAsync(id);

            if (student != null)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
             _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        internal Student GetStudentAsync(object value)
        {
            throw new NotImplementedException();
        }
    }
}
