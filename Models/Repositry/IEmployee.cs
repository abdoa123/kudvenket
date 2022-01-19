using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kudvenkit.Models.Repositry
{
    public interface IEmployee
    {
       Employee GetEmployee(int Id);
       IEnumerable<Employee> GetAll();
       bool AddEmployee(Employee employee);
       bool update(Employee employee);
       bool Delete(int Id);
        
    }
}
