using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        /// <summary>
        /// 員工代碼
        /// </summary>
        [Key]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        /// <summary>
        /// 部門編號
        /// </summary>
        [Column(TypeName = "NVARCHAR(100)")]
        public string Dept { get; set; }
    }
}