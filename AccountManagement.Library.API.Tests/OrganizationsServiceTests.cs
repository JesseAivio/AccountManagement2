using System;
using System.Collections.Generic;
using System.Text;
using AccountManagement.Library.API.Data;
using AccountManagement.Library.API.Models;
using Xunit;

namespace AccountManagement.Library.API.Tests
{
    public class OrganizationsServiceTests
    {
        OrganizationsService OrganizationsService = new OrganizationsService();
        [Fact]
        public async void Should_AddOrganizationAsync()
        {
            // Arrange
            string Expected = "Organization added";

            // Act
            Organization organization = new Organization
            {
                Name = "Unit",
                BusinessId = "1234567-0",
                Email = "jesse@jesse.com",
                Info = "unit testing"
            };
            string Actual = await OrganizationsService.AddOrganizationAsync(organization);

            // Assert
            Assert.Equal(Expected, Actual);
        }
    }
}
