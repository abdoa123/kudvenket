using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kudvenkit.Models.Repositry
{
    public class SQLEmployeeRepository : IEmployee
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public bool AddEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int Id)
        {
            Employee employee = _context.employees.Find(Id);
            if (employee != null)
            {
                _context.employees.Remove(employee);
                _context.SaveChanges();
            }
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.employees;
        }

        public Employee GetEmployee(int Id)
        {
            return _context.employees.Find(Id);
        }

        public bool update(Employee employeeChange)
        {
            var employee = _context.employees.Attach(employeeChange);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
    }
}
