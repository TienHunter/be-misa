using AutoMapper;
using MISA.WebFresher042023.Demo.Common.DTO.Employee;
using MISA.WebFresher042023.Demo.Common.Entity;
using MISA.WebFresher042023.Demo.Common.Exceptions;
using MISA.WebFresher042023.Demo.Core.Interface.Repositories;
using MISA.WebFresher042023.Demo.Core.Interface.Services;
using MISA.WebFresher042023.Demo.Core.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.UnitTests.Core
{
    [TestFixture]
    public class EmployeeServiceTests
    {

        [Test]
        public async Task GetAsync_NullEntity_ThrowNotFoundException()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea06ea");
            var employeeRepository = new FakeNullEmployeeRepository();
            var departmentRepository = new FakeDepartmentRepository();
            var mapper = new FakeMapper();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            // Act & Assert

            Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.GetAsync(id));


        }
        [Test]
        public async Task GetAsync_ValidInput_ValidEntity()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea06ea");
            var employeeRepository = new FakeEmployeeRepository();
            var departmentRepository = new FakeDepartmentRepository();
            var mapper = new FakeMapper();

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);
            // Act

            var employee = await employeeService.GetAsync(id);
            // Assert
            Assert.That(employeeRepository.ActualId, Is.EqualTo(id));

        }

        [Test]
        public async Task DeleteAsync_NullEntity_ThrowNotFoundException()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            employeeRepository.GetAsync(id).ReturnsNull();
            employeeRepository.DeleteAsync(id).Returns(1);

            var employeeSerive = new EmployeeService(employeeRepository, departmentRepository, mapper);
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await employeeSerive.DeleteAsync(id));
        }

        [Test]
        public async Task DeleteAsync_ValidEntity_DeletedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            var employee = new Employee() { EmployeeId = id };
            employeeRepository.GetAsync(id).Returns(new Employee());
            employeeRepository.DeleteAsync(id).Returns(1);

            var employeeSerive = new EmployeeService(employeeRepository, departmentRepository, mapper);
            // Act & Assert
            var actualResult = await employeeSerive.DeleteAsync(id);
            Assert.That(actualResult, Is.EqualTo(1));
            await employeeRepository.Received(1).DeleteAsync(id);
        }

        public async Task InsertAsync_ValidEntity_CreatedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();
            var mapper = Substitute.For<IMapper>();

            var employeeCreatedDTO = new EmployeeCreateDTO();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            employeeRepository.IsDupEmployeecodeRepositoryAsync(employeeCreatedDTO.EmployeeCode, null).Returns(false);
            departmentRepository.GetAsync(employeeCreatedDTO.DepartmentId).Returns(new Department());
            mapper.Map<Employee>(Arg.Any<EmployeeCreateDTO>()).Returns(employee);
            employeeRepository.InsertAsync(employee).Returns(employee);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            // Act & Assert
            var actualResult = await employeeService.InsertAsync(employeeCreatedDTO);
            Assert.That(actualResult, Is.EqualTo(1));
            await employeeRepository.Received(1).InsertAsync(employee);
        }

        [Test]
        public async Task UpdateAsync_ValidEntity_UpdatedNum()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var departmentId = Guid.Parse("1e5ce342-2eec-79a4-5380-7e19d1ea16ea");

            var employeeUpdateDTO = new EmployeeUpdateDTO();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            employeeRepository.GetAsync(id).Returns(new Employee());
            employeeRepository.IsDupEmployeecodeRepositoryAsync(employeeUpdateDTO.EmployeeCode, null).Returns(null);
            departmentRepository.GetAsync(employeeUpdateDTO.DepartmentId).Returns(new Department());
            mapper.Map<Employee>(Arg.Any<EmployeeUpdateDTO>()).Returns(employee);
            employeeRepository.UpdateAsync(employee).Returns(employee);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.UpdateAsync(id, employeeUpdateDTO);
            // Act & Assert
            Assert.That(actualResult, Is.EqualTo(employee));
            await employeeRepository.Received(1).UpdateAsync(employee);
        }

        [Test]
        public async Task UpdateAsync_InValidDepartment_ThrowNotFoundException()
        {
            // Arrange
            var id = Guid.Parse("1e5ce342-2eec-79a4-5380-7ed9d1ea16ea");
            var departmentId = Guid.Parse("1e5ce342-2eec-79a4-5380-7e19d1ea16ea");

            var employeeUpdateDTO = new EmployeeUpdateDTO();
            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var mapper = Substitute.For<IMapper>();
            var departmentRepository = Substitute.For<IDepartmentRepository>();

            employeeRepository.GetAsync(id).Returns(employee);
            employeeRepository.isDupEmployeecodeRepositoryAsync(employeeUpdateDTO.EmployeeCode,, employeeUpdateDTO.EmployeeId).Returns(null);
            departmentRepository.GetAsync(employeeUpdateDTO.DepartmentId).Returns(new Department());
            mapper.Map<Employee>(Arg.Any<EmployeeUpdateDTO>()).Returns(employee);
            employeeRepository.UpdateAsync(employee).Returns(employee);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);


            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.UpdateAsync(id, employeeUpdateDTO), "id không tồn tại");
        }
    }
}
