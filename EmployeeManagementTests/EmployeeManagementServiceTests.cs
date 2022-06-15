using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using EmployeeManagement;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Services;
using EmployeeManagement.Services.Database;
using Moq;
using Xunit;
using FluentAssertions;
using EmployeeManagement.Models;

namespace EmployeeManagementTests
{
    public class EmployeeManagementServiceTests
    {
        #region Get
        [Fact]
        public async void GetAllEmployees_CallGet_Sucess_ReturnListOfEmployees()
        {
            //Arrange
            var mockData = new Mock<IDataAccess>();
            //Create a mock for auto mapper - use the AutoMapperProfile from EmployeeManagement project
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var expectedResult = GetSampleEmployees();
            mockData.Setup(d => d.GetAllData("Employee")).Returns(GenerateMockEmployeeTable());
            var empService = new EmployeeManagerService(mapper, mockData.Object);
            //Act
            var result = await empService.GetAllEmployees();
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void GetEmployeeById_CallGet_Success_ReturnSelEmployee()
        {
            //Arrange
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var expectedResult = GetSampleEmployees()[0];
            mockData.Setup(d => d.GetSelectedData("Employee", "Id", "0")).Returns(GenerateMockEmployeeTable().Rows[0]);
            var empService = new EmployeeManagerService(mapper, mockData.Object);
            //Act
            var result = await empService.GetEmployeeById(0);
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void GetEmployeeById_CallGet_Failure_NotReturnSelEmployee()
        {
            //Arrange
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var expectedResult = GetSampleEmployees()[0];
            mockData.Setup(d => d.GetSelectedData("Employee", "Id", "0")).Returns(GenerateMockEmployeeTable().Rows[1]);
            var empService = new EmployeeManagerService(mapper, mockData.Object);
            //Act
            var result = await empService.GetEmployeeById(0);
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region Post
        [Fact]
        public async void AddEmployee_CallPost_Success_ReturnAllEmployee()
        {
            //Arrange
            //Setting up mock for DataAccess and Mapper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Creating new employee to be added
            var newEmployee = new AddEmployeeDTO()
            {
                Name = "Shiva",
                MailId = "shiva@gmail.com",
                JobTitle = EmployeeManagement.Models.JobTitle.ProjectEngineer,
                Mission = EmployeeManagement.Models.Mission.GUI,
                ProjectName = "MNO",
                ReportsTo = "Jim"
            };

            //Creating a mock employee
            var newMockEmployee = new GetEmployeeDTO()
            {
                Id = 3,
                Name = newEmployee.Name,
                MailId = newEmployee.MailId,
                JobTitle = newEmployee.JobTitle,
                Mission = newEmployee.Mission,
                ProjectName = newEmployee.ProjectName,
                ReportsTo = newEmployee.ReportsTo
            };

            //Set expected result
            var expectedResult = GetSampleEmployees();
            expectedResult.Add(newMockEmployee);

            //Get list of prop names and values for mock
            List<string> mockPropNames = new();
            List<string> mockPropValues = new();
            foreach (var prop in typeof(AddEmployeeDTO).GetProperties())
            {
                mockPropNames.Add(prop.Name);
                mockPropValues.Add(prop.GetValue(newEmployee, null).ToString());
            }
            DataTable mockTable = AddDataToTable(GenerateMockEmployeeTable(), newMockEmployee);
            //Setup mock method
            mockData.Setup(d => d.InsertData("Employee", mockPropNames, mockPropValues)).Returns(mockTable);
            var empService = new EmployeeManagerService(mapper, mockData.Object);

            //Act
            var result = await empService.AddEmployee(newEmployee);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void AddEmployee_CallPost_Failure_ReturnAllEmployee()
        {
            //Arrange
            //Setting up mock for DataAccess and Mapper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Creating new employee to be added
            var newEmployee = new AddEmployeeDTO()
            {
                Name = "Shiva",
                MailId = "shiva@gmail.com",
                JobTitle = EmployeeManagement.Models.JobTitle.ProjectEngineer,
                Mission = EmployeeManagement.Models.Mission.GUI,
                ProjectName = "MNO",
                ReportsTo = "Jim"
            };

            //Creating a mock employee
            var newMockEmployee = new GetEmployeeDTO()
            {
                Id = 3,
                Name = newEmployee.Name,
                MailId = newEmployee.MailId,
                JobTitle = newEmployee.JobTitle,
                Mission = newEmployee.Mission,
                ProjectName = newEmployee.ProjectName,
                ReportsTo = newEmployee.ReportsTo
            };

            //Set expected result
            var expectedResult = GetSampleEmployees();
            expectedResult.Add(newMockEmployee);

            //Get list of prop names and values for mock
            List<string> mockPropNames = new();
            List<string> mockPropValues = new();
            foreach (var prop in typeof(AddEmployeeDTO).GetProperties())
            {
                mockPropNames.Add(prop.Name);
                mockPropValues.Add(prop.GetValue(newEmployee, null).ToString());
            }
            //DataTable mockTable = AddDataToTable(GenerateMockEmployeeTable(), newMockEmployee);
            //Setup mock method
            mockData.Setup(d => d.InsertData("Employee", mockPropNames, mockPropValues)).Returns(GenerateMockEmployeeTable());
            var empService = new EmployeeManagerService(mapper, mockData.Object);

            //Act
            var result = await empService.AddEmployee(newEmployee);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region Delete
        [Fact]
        public async void DeleteEmployee_CallDelete_Success_ReturnAllEmployee()
        {
            //Arrange
            //Setup mock IDataAccess and Mappper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Preparing expected data and mock data
            var expectedResult = GetSampleEmployees();
            expectedResult.RemoveAt(0);
            mockData.Setup(d => d.DeleteSelectedData("Employee", "Id", "0")).Returns(() =>
            {
                var mockDt = GenerateMockEmployeeTable();
                mockDt.Rows.RemoveAt(0);
                return mockDt;
            });
            var empService = new EmployeeManagerService(mapper, mockData.Object);

            //Act
            var result = await empService.DeleteEmployee(0);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void DeleteEmployee_CallDelete_Failure_ReturnAllEmployee()
        {
            //Arrange
            //Setup mock IDataAccess and Mappper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Preparing sample data
            var expectedResult = GetSampleEmployees();
            expectedResult.RemoveAt(0);
            mockData.Setup(d => d.DeleteSelectedData("Employee", "Id", "0")).Returns(() =>
            {
                var mockDt = GenerateMockEmployeeTable();
                //mockDt.Rows.RemoveAt(0);
                return mockDt;
            });
            var empService = new EmployeeManagerService(mapper, mockData.Object);
            //Act
            var result = await empService.DeleteEmployee(0);
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region Put
        [Fact]
        public async void UpdateEmployee_CallPut_Success_ReturnUpdatedEmployee()
        {
            //Arrange
            //Setup mock IDataAccess and Mappper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Preparing sample data
            var expectedResult = GetSampleEmployees()[0];
            expectedResult.JobTitle = JobTitle.ProjectManager;
            expectedResult.Mission = Mission.GUI;

            var newEmployee = new UpdateEmployeeDTO()
            {
                Name = expectedResult.Name,
                MailId = expectedResult.MailId,
                JobTitle = expectedResult.JobTitle,
                Mission = expectedResult.Mission,
                ProjectName = expectedResult.ProjectName,
                ReportsTo = expectedResult.ReportsTo
            };

            //Get list of prop names and values for mock
            List<string> mockPropNames = new();
            List<string> mockPropValues = new();
            foreach (var prop in typeof(UpdateEmployeeDTO).GetProperties())
            {
                mockPropNames.Add(prop.Name);
                mockPropValues.Add(prop.GetValue(newEmployee, null).ToString());
            }

            mockData.Setup(d => d.UpdateData("Employee", "Id", "1", mockPropNames, mockPropValues)).Returns(() =>
            {
                var mockDt = GenerateMockEmployeeTable().Rows[0];
                mockDt["JobTitle"] = "ProjectManager";
                mockDt["Mission"] = "GUI";
                return mockDt;
            });


            var empService = new EmployeeManagerService(mapper, mockData.Object);

            //Act
            var result = await empService.UpdateEmployee(1, newEmployee);
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void UpdateEmployee_CallPut_Failure_ReturnEmployeeWitoutUpdate()
        {
            //Arrange
            //Setup mock IDataAccess and Mappper
            var mockData = new Mock<IDataAccess>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            //Preparing sample data
            var expectedResult = GetSampleEmployees()[0];
            expectedResult.JobTitle = JobTitle.ProjectManager;
            expectedResult.Mission = Mission.GUI;

            var newEmployee = new UpdateEmployeeDTO()
            {
                Name = expectedResult.Name,
                MailId = expectedResult.MailId,
                JobTitle = expectedResult.JobTitle,
                Mission = expectedResult.Mission,
                ProjectName = expectedResult.ProjectName,
                ReportsTo = expectedResult.ReportsTo
            };

            //Get list of prop names and values for mock
            List<string> mockPropNames = new();
            List<string> mockPropValues = new();
            foreach (var prop in typeof(UpdateEmployeeDTO).GetProperties())
            {
                mockPropNames.Add(prop.Name);
                mockPropValues.Add(prop.GetValue(newEmployee, null).ToString());
            }

            mockData.Setup(d => d.UpdateData("Employee", "Id", "1", mockPropNames, mockPropValues)).Returns(() =>
            {
                var mockDt = GenerateMockEmployeeTable().Rows[0];
                // mockDt["JobTitle"] = "ProjectManager";
                // mockDt["Mission"] = "GUI";
                return mockDt;
            });


            var empService = new EmployeeManagerService(mapper, mockData.Object);

            //Act
            var result = await empService.UpdateEmployee(1, newEmployee);
            //Assert
            //Assert.Same(result.Data, expectedResult);
            result.Data.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region HelperMethods
        private DataTable GenerateMockEmployeeTable()
        {
            DataTable mockEmployeeTable = new DataTable();
            mockEmployeeTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("MailId", typeof(string)),
                new DataColumn("JobTitle", typeof(string)),
                new DataColumn("Mission", typeof(string)),
                new DataColumn("ProjectName", typeof(string)),
                new DataColumn("ReportsTo", typeof(string))
            });
            mockEmployeeTable.Rows.Add(
                1,
                "Sree",
                "sree@gmail.com",
                "ProjectLead",
                "SCV",
                "XYZ",
                "Jack");
            mockEmployeeTable.Rows.Add(
                2,
                "Karthik",
                "karthik@gmail.com",
                "ProjectManager",
                "D2T",
                "ABC",
                "Jill");
            return mockEmployeeTable;
        }

        private DataTable AddDataToTable(DataTable dt, GetEmployeeDTO newSampleEmployee)
        {
            DataRow dr = dt.NewRow();
            List<string> propNames = new();
            List<string> propValues = new();
            foreach (var prop in typeof(GetEmployeeDTO).GetProperties())
            {
                propNames.Add(prop.Name);
                propValues.Add(prop.GetValue(newSampleEmployee, null).ToString());
            }
            for (int i = 0; i < propNames.Count; i++)
            {
                string colType = dt.Columns[i].DataType.Name.ToString();
                if (colType == "Int32")
                {
                    dr[propNames[i]] = Int32.Parse(propValues[i]);
                }
                else
                {
                    dr[propNames[i]] = propValues[i];
                }

            }
            dt.Rows.Add(dr);
            return dt;
        }

        private List<GetEmployeeDTO> GetSampleEmployees()
        {
            var employees = new List<GetEmployeeDTO>(){
               new GetEmployeeDTO()
               {
                Id = 1,
                Name = "Sree",
                MailId = "sree@gmail.com",
                JobTitle = EmployeeManagement.Models.JobTitle.ProjectLead,
                Mission = EmployeeManagement.Models.Mission.SCV,
                ProjectName = "XYZ",
                ReportsTo = "Jack"
               },
               new GetEmployeeDTO()
               {
                Id = 2,
                Name = "Karthik",
                MailId = "karthik@gmail.com",
                JobTitle = EmployeeManagement.Models.JobTitle.ProjectManager,
                Mission = EmployeeManagement.Models.Mission.D2T,
                ProjectName = "ABC",
                ReportsTo = "Jill"
               }
            };

            return employees;
        }
        #endregion
    }
}
