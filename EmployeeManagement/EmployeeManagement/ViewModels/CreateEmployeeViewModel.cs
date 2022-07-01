using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CreateEmployeeViewModel
    {
        /// <summary>
        /// 員工代碼
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部門編號
        /// </summary>
        public string Dept { get; set; }
    }
}