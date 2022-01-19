using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kudvenkit.Models.Repositry
{
    public class EmployeeOperation : IEmployee
    {
        List<Employee> employees;
        public EmployeeOperation()
        {
            employees = new List<Employee>()
            {
                new Employee(){Id = 1,email = "asdlsakf" , name = "abdo" },
                new Employee(){Id = 2,email = "abdo_2013131@yahoo.com" , name = "ahmed" },
                new Employee(){Id = 3,email = "asasd@yahoo.com" , name = "tony" }
            };
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                employees.Add(employee);
                return true;
            }catch(Exception err)
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public Employee GetEmployee(int Id)
        {
            
            var employee = employees.FirstOrDefault(x => x.Id == Id);
            return employee;
        }

        public bool update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
