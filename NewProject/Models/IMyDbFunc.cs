using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
   interface IMyDbFunc<T>
   {
       Task<int> addDataAsync();
       Task<int> deleteDataByIdAsync(int id);
    //    Task<int> alterDataByIdAsync(int id);
    //    Task<T> selectDataByIdAsync(int id);
   }
}
