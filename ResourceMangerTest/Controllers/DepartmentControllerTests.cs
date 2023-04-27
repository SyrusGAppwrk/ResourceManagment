using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using ResourceManagment.Controllers;
using ResourceManagment.Models;
using ResourceManagment.Repository;
using ResourceMangerTest.MockData;

namespace ResourceMangerTest.Controllers
{  
    public class DepartmentControllerTests
    {
        [Fact]
        public async Task GetDepartmentListTest()
        {
            //Arange 
            var depart = new Mock<IDepartmentRepository>();
            depart.Setup(_ => _.GetDepartments()).ReturnsAsync(DepartmentListMockData.GetDepartment());
            //Act
            var sut=new DepartmentController(depart.Object);
            var result=sut.GetDepartments();
            var list = result.Result as OkObjectResult;
            //Assert
            Assert.Equal(200, list.StatusCode);
        }
    }
}
