using System.Collections.Generic;
using System.Linq;
using DronTaxi.Database;
using DronTaxi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DronTaxi.UnitTests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestOrders()
        {
            List<Order> orders = AppDb.GetOrders().ToList();

            if (orders.Any())
            {
                Order first = orders.First();

                Order response = AppDb.GetOrder(first.Id);
            }
        }

        [TestMethod]
        public void TestSystemFunctions()
        {
            List<SystemFunction> sysFunctions = AppDb.GetSystemFunctions().ToList();

            if (sysFunctions.Any())
            {
                SystemFunction first = sysFunctions.First();

                SystemFunction response = AppDb.GetSystemFunction(first.Id);
            }
        }

        [TestMethod]
        public void TestUsers()
        {
            List<User> users = AppDb.GetUsers().ToList();

            if (users.Any())
            {
                User first = users.First();

                User response = AppDb.GetUser(first.Id);
            }
        }

        [TestMethod]
        public void TestUserProfiles()
        {
            List<UserProfile> userProfiles = AppDb.GetUserProfiles().ToList();

            if (userProfiles.Any())
            {
                UserProfile first = userProfiles.First();

                UserProfile response = AppDb.GetUserProfile(first.Id);
            }
        }

        [TestMethod]
        public void TestUserRoles()
        {
            List<UserRole> userRoles = AppDb.GetUserRoles().ToList();

            if (userRoles.Any())
            {
                UserRole first = userRoles.First();

                UserRole response = AppDb.GetUserRole(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicles()
        {
            List<Vehicle> vehicles = AppDb.GetVehicles().ToList();

            if (vehicles.Any())
            {
                Vehicle first = vehicles.First();

                Vehicle response = AppDb.GetVehicle(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleAttributes()
        {
            List<VehicleAttribute> vehicleAttributes = AppDb.GetVehicleAttributes().ToList();

            if (vehicleAttributes.Any())
            {
                VehicleAttribute first = vehicleAttributes.First();

                VehicleAttribute response = AppDb.GetVehicleAttribute(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleAttributeValues()
        {
            List<VehicleAttributeValue> vehicleAttributeValues = AppDb.GetVehicleAttributeValues().ToList();

            if (vehicleAttributeValues.Any())
            {
                VehicleAttributeValue first = vehicleAttributeValues.First();

                VehicleAttributeValue response = AppDb.GetVehicleAttributeValue(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleCategories()
        {
            List<VehicleCategory> vehicleCategories = AppDb.GetVehicleCategories().ToList();

            if (vehicleCategories.Any())
            {
                VehicleCategory first = vehicleCategories.First();

                VehicleCategory response = AppDb.GetVehicleCategory(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleCycles()
        {
            List<VehicleCycle> vehicleCycles = AppDb.GetVehicleCycles().ToList();

            if (vehicleCycles.Any())
            {
                VehicleCycle first = vehicleCycles.First();

                VehicleCycle response = AppDb.GetVehicleCycle(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleInspections()
        {
            List<VehicleInspection> vehicleInspections = AppDb.GetVehicleInspections().ToList();

            if (vehicleInspections.Any())
            {
                VehicleInspection first = vehicleInspections.First();

                VehicleInspection response = AppDb.GetVehicleInspection(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleManufacturers()
        {
            List<VehicleManufacturer> vehicleManufacturers = AppDb.GetVehicleManufacturers().ToList();

            if (vehicleManufacturers.Any())
            {
                VehicleManufacturer first = vehicleManufacturers.First();

                VehicleManufacturer response = AppDb.GetVehicleManufacturer(first.Id);
            }
        }

        [TestMethod]
        public void TestVehicleRepairs()
        {
            List<VehicleRepair> vehicleRepairs = AppDb.GetVehicleRepairs().ToList();

            if (vehicleRepairs.Any())
            {
                VehicleRepair first = vehicleRepairs.First();

                VehicleRepair response = AppDb.GetVehicleRepair(first.Id);
            }
        }
    }
}
