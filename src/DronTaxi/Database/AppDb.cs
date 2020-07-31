using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DronTaxi.Models;
using DronTaxi.Properties;
using DronTaxi.Static;

namespace DronTaxi.Database
{
    public static class AppDb
    {
        public static string ConnectionString => Settings.Default.ConnectionString;

        #region Selects

        public static Order GetOrder(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Id, CreationDateTime, StartDateTime, EndDateTime, " +
                               "StartPointAddress, EndPointAddress, VehicleId, OperatorId, Status from Orders " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Order(
                                reader["Id"].ConvertTo<int>(),
                                reader["Id"].ConvertTo<int>(),
                                reader["CreationDateTime"].ConvertTo<DateTime>(),
                                reader["StartDateTime"].AsNullable<DateTime>(),
                                reader["EndDateTime"].AsNullable<DateTime>(),
                                reader["StartPointAddress"].AsString(),
                                reader["EndPointAddress"].AsString(),
                                reader["VehicleId"].AsNullable<int>(),
                                reader["OperatorId"].AsNullable<int>(),
                                reader["Status"].ConvertTo<int>());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<Order> GetOrders()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<Order> entries = new List<Order>();

                string query = "select Id, Id, CreationDateTime, StartDateTime, EndDateTime, " +
                               "StartPointAddress, EndPointAddress, VehicleId, OperatorId, Status from Orders ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new Order(
                                reader["Id"].ConvertTo<int>(),
                                reader["Id"].ConvertTo<int>(),
                                reader["CreationDateTime"].ConvertTo<DateTime>(),
                                reader["StartDateTime"].AsNullable<DateTime>(),
                                reader["EndDateTime"].AsNullable<DateTime>(),
                                reader["StartPointAddress"].AsString(),
                                reader["EndPointAddress"].AsString(),
                                reader["VehicleId"].AsNullable<int>(),
                                reader["OperatorId"].AsNullable<int>(),
                                reader["Status"].ConvertTo<int>()));
                        }
                    }
                }

                return entries;
            }
        }

        public static SystemFunction GetSystemFunction(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Name, DisplayName from SysFunctions " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new SystemFunction(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<SystemFunction> GetSystemFunctions()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<SystemFunction> entries = new List<SystemFunction>();

                string query = "select Id, Name, DisplayName from SysFunctions ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new SystemFunction(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static IEnumerable<SystemFunction> GetSystemFunctions(UserRole role)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<SystemFunction> entries = new List<SystemFunction>();

                string query = "select Id, Name, DisplayName from SysFunctions " +
                               "inner join RoleFunctions RF on SysFunctions.Id = RF.SysFuncId " +
                               "where RF.RoleId = @RId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@RId", role.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new SystemFunction(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static User GetUser(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Username, PasswordHash from Users " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User(
                                reader["Id"].ConvertTo<int>(),
                                reader["Username"].AsString(),
                                reader["PasswordHash"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static User GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Username, PasswordHash from Users " +
                               "where Username = @EntUsername";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntUsername", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User(
                                reader["Id"].ConvertTo<int>(),
                                reader["Username"].AsString(),
                                reader["PasswordHash"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<User> entries = new List<User>();

                string query = "select Id, Username, PasswordHash from Users ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new User(
                                reader["Id"].ConvertTo<int>(),
                                reader["Username"].AsString(),
                                reader["PasswordHash"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static UserProfile GetUserProfile(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, UserId, LastName, FirstName, MiddleName, BirthDate, Sex, Email, Phone from UserProfiles " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new UserProfile(
                                reader["Id"].ConvertTo<int>(),
                                reader["UserId"].ConvertTo<int>(),
                                reader["LastName"].AsString(),
                                reader["FirstName"].AsString(),
                                reader["MiddleName"].AsString(),
                                reader["BirthDate"].ConvertTo<DateTime>(),
                                reader["Sex"].ConvertTo<int>(),
                                reader["Email"].AsString(),
                                reader["Phone"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static UserProfile GetUserProfile(User user)
        {
            if (user == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, UserId, LastName, FirstName, MiddleName, BirthDate, Sex, Email, Phone from UserProfiles " +
                    "where UserId = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", user.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new UserProfile(
                                reader["Id"].ConvertTo<int>(),
                                reader["UserId"].ConvertTo<int>(),
                                reader["LastName"].AsString(),
                                reader["FirstName"].AsString(),
                                reader["MiddleName"].AsString(),
                                reader["BirthDate"].ConvertTo<DateTime>(),
                                reader["Sex"].ConvertTo<int>(),
                                reader["Email"].AsString(),
                                reader["Phone"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static UserPic GetUserPic(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, UserId, Picture from UserPics " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new UserPic(
                                reader["Id"].ConvertTo<int>(),
                                reader["UserId"].ConvertTo<int>(),
                                reader["Picture"].AsByteArray());
                        }
                    }
                }

                return null;
            }
        }

        public static UserPic GetUserPic(User user)
        {
            if (user == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, UserId, Picture from UserPics " +
                    "where UserId = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", user.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new UserPic(
                                reader["Id"].ConvertTo<int>(),
                                reader["UserId"].ConvertTo<int>(),
                                reader["Picture"].AsByteArray());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<UserProfile> GetUserProfiles()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<UserProfile> entries = new List<UserProfile>();

                string query =
                    "select Id, UserId, LastName, FirstName, MiddleName, BirthDate, Sex, Email, Phone from UserProfiles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new UserProfile(
                                reader["Id"].ConvertTo<int>(),
                                reader["UserId"].ConvertTo<int>(),
                                reader["LastName"].AsString(),
                                reader["FirstName"].AsString(),
                                reader["MiddleName"].AsString(),
                                reader["BirthDate"].ConvertTo<DateTime>(),
                                reader["Sex"].ConvertTo<int>(),
                                reader["Email"].AsString(),
                                reader["Phone"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static UserRole GetUserRole(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Name, DisplayName from Roles " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new UserRole(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<UserRole> GetUserRoles()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<UserRole> entries = new List<UserRole>();

                string query = "select Id, Name, DisplayName from Roles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new UserRole(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static IEnumerable<UserRole> GetUserRoles(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<UserRole> entries = new List<UserRole>();

                string query = "select Id, Name, DisplayName from Roles " +
                               "inner join UserRoles UR on Roles.Id = UR.RoleId " +
                               "where UR.UserId = @UId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UId", user.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new UserRole(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static Vehicle GetVehicle(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, ManufacturerId, ModelName, ProductionDate, RegNumber, StartUsageDate, EndUsageDate, CategoryId from Vehicles " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Vehicle(
                                reader["Id"].ConvertTo<int>(),
                                reader["ManufacturerId"].ConvertTo<int>(),
                                reader["ModelName"].AsString(),
                                reader["ProductionDate"].ConvertTo<DateTime>(),
                                reader["RegNumber"].AsString(),
                                reader["StartUsageDate"].ConvertTo<DateTime>(),
                                reader["EndUsageDate"].AsNullable<DateTime>(),
                                reader["CategoryId"].ConvertTo<int>());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<Vehicle> GetVehicles()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<Vehicle> entries = new List<Vehicle>();

                string query =
                    "select Id, ManufacturerId, ModelName, ProductionDate, RegNumber, StartUsageDate, EndUsageDate, CategoryId from Vehicles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new Vehicle(
                                reader["Id"].ConvertTo<int>(),
                                reader["ManufacturerId"].ConvertTo<int>(),
                                reader["ModelName"].AsString(),
                                reader["ProductionDate"].ConvertTo<DateTime>(),
                                reader["RegNumber"].AsString(),
                                reader["StartUsageDate"].ConvertTo<DateTime>(),
                                reader["EndUsageDate"].AsNullable<DateTime>(),
                                reader["CategoryId"].ConvertTo<int>()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleAttribute GetVehicleAttribute(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Name, DisplayName, Type from VehicleAttributes " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleAttribute(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString(),
                                reader["Type"].ConvertTo<int>());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleAttribute> GetVehicleAttributes()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleAttribute> entries = new List<VehicleAttribute>();

                string query = "select Id, Name, DisplayName, Type from VehicleAttributes";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleAttribute(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString(),
                                reader["Type"].ConvertTo<int>()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleAttributeValue GetVehicleAttributeValue(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, AttributeId, VehicleId, IntValue, FloatValue, StringValue, DateTimeValue from VehicleAttributeValues " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleAttributeValue(
                                reader["Id"].ConvertTo<int>(),
                                reader["AttributeId"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["IntValue"].AsNullable<int>(),
                                reader["FloatValue"].AsNullable<double>(),
                                reader["StringValue"].AsString(),
                                reader["DateTimeValue"].AsNullable<DateTime>());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleAttributeValue> GetVehicleAttributeValues()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleAttributeValue> entries = new List<VehicleAttributeValue>();

                string query =
                    "select Id, AttributeId, VehicleId, IntValue, FloatValue, StringValue, DateTimeValue from VehicleAttributeValues";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleAttributeValue(
                                reader["Id"].ConvertTo<int>(),
                                reader["AttributeId"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["IntValue"].AsNullable<int>(),
                                reader["FloatValue"].AsNullable<double>(),
                                reader["StringValue"].AsString(),
                                reader["DateTimeValue"].AsNullable<DateTime>()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleCategory GetVehicleCategory(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Name, DisplayName from VehicleCategories " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleCategory(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleCategory> GetVehicleCategories()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleCategory> entries = new List<VehicleCategory>();

                string query = "select Id, Name, DisplayName from VehicleCategories";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleCategory(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleCycle GetVehicleCycle(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, VehicleId, InspIntervalTime, CurrInspFlyTime, InspIntervalDist, " +
                               "CurrInspFlyDist, InspIntervalRegType, InspIntervalRegTimeOfDay, InspIntervalRegLastDate, " +
                               "RepIntervalTime, CurrRepFlyTime, RepIntervalDist, CurrRepFlyDist from VehicleCycles " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleCycle(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["InspIntervalTime"].AsNullable<double>(),
                                reader["CurrInspFlyTime"].ConvertTo<double>(),
                                reader["InspIntervalDist"].AsNullable<double>(),
                                reader["CurrInspFlyDist"].ConvertTo<double>(),
                                reader["InspIntervalRegType"].ConvertTo<int>(),
                                reader["InspIntervalRegTimeOfDay"].AsNullable<TimeSpan>(),
                                reader["InspIntervalRegLastDate"].AsNullable<DateTime>(),
                                reader["RepIntervalTime"].AsNullable<double>(),
                                reader["CurrRepFlyTime"].ConvertTo<double>(),
                                reader["RepIntervalDist"].AsNullable<double>(),
                                reader["CurrRepFlyDist"].ConvertTo<int>());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleCycle> GetVehicleCycles()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleCycle> entries = new List<VehicleCycle>();

                string query = "select Id, VehicleId, InspIntervalTime, CurrInspFlyTime, InspIntervalDist, " +
                               "CurrInspFlyDist, InspIntervalRegType, InspIntervalRegTimeOfDay, InspIntervalRegLastDate, " +
                               "RepIntervalTime, CurrRepFlyTime, RepIntervalDist, CurrRepFlyDist from VehicleCycles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleCycle(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["InspIntervalTime"].AsNullable<double>(),
                                reader["CurrInspFlyTime"].ConvertTo<double>(),
                                reader["InspIntervalDist"].AsNullable<double>(),
                                reader["CurrInspFlyDist"].ConvertTo<double>(),
                                reader["InspIntervalRegType"].ConvertTo<int>(),
                                reader["InspIntervalRegTimeOfDay"].AsNullable<TimeSpan>(),
                                reader["InspIntervalRegLastDate"].AsNullable<DateTime>(),
                                reader["RepIntervalTime"].AsNullable<double>(),
                                reader["CurrRepFlyTime"].ConvertTo<double>(),
                                reader["RepIntervalDist"].AsNullable<double>(),
                                reader["CurrRepFlyDist"].ConvertTo<int>()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleInspection GetVehicleInspection(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, VehicleId, StartDate, EndDate, WorkerId, DocumentNumber from VehicleInspections " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleInspection(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["StartDate"].ConvertTo<DateTime>(),
                                reader["EndDate"].AsNullable<DateTime>(),
                                reader["WorkerId"].ConvertTo<int>(),
                                reader["DocumentNumber"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleInspection> GetVehicleInspections()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleInspection> entries = new List<VehicleInspection>();

                string query =
                    "select Id, VehicleId, StartDate, EndDate, WorkerId, DocumentNumber from VehicleInspections";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleInspection(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["StartDate"].ConvertTo<DateTime>(),
                                reader["EndDate"].AsNullable<DateTime>(),
                                reader["WorkerId"].ConvertTo<int>(),
                                reader["DocumentNumber"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleManufacturer GetVehicleManufacturer(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "select Id, Name, DisplayName from VehicleManufacturers " +
                               "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleManufacturer(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleManufacturer> GetVehicleManufacturers()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleManufacturer> entries = new List<VehicleManufacturer>();

                string query = "select Id, Name, DisplayName from VehicleManufacturers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleManufacturer(
                                reader["Id"].ConvertTo<int>(),
                                reader["Name"].AsString(),
                                reader["DisplayName"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        public static VehicleRepair GetVehicleRepair(int? id)
        {
            if (id == null)
                return null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "select Id, VehicleId, Type, StartDate, EndDate, WorkerId, DocumentNumber from VehicleRepairs " +
                    "where Id = @EntId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@EntId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new VehicleRepair(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["Type"].ConvertTo<int>(),
                                reader["StartDate"].ConvertTo<DateTime>(),
                                reader["EndDate"].AsNullable<DateTime>(),
                                reader["WorkerId"].ConvertTo<int>(),
                                reader["DocumentNumber"].AsString());
                        }
                    }
                }

                return null;
            }
        }

        public static IEnumerable<VehicleRepair> GetVehicleRepairs()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VehicleRepair> entries = new List<VehicleRepair>();

                string query =
                    "select Id, VehicleId, Type, StartDate, EndDate, WorkerId, DocumentNumber from VehicleRepairs";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new VehicleRepair(
                                reader["Id"].ConvertTo<int>(),
                                reader["VehicleId"].ConvertTo<int>(),
                                reader["Type"].ConvertTo<int>(),
                                reader["StartDate"].ConvertTo<DateTime>(),
                                reader["EndDate"].AsNullable<DateTime>(),
                                reader["WorkerId"].ConvertTo<int>(),
                                reader["DocumentNumber"].AsString()));
                        }
                    }
                }

                return entries;
            }
        }

        #endregion

        #region Updates

        public static void UpdateUser(int id, string lastName, string firstName, string middleName, DateTime birthDate,
            int sex, string email, string phone, string newPasswordHash, byte[] newPic)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "update UserProfiles " +
                               "set LastName = @LName, FirstName = @FName, MiddleName = @MName, BirthDate = @BDate, " +
                               "Sex = @SexValue, Email = @EMailValue, Phone = @PhoneValue " +
                               "where UserId = @UId; " +
                               "update Users " +
                               "set PasswordHash = @PHash " +
                               "where Id = @Uid; ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UId", id);
                    command.Parameters.AddWithNullCheck("@LName", lastName);
                    command.Parameters.AddWithNullCheck("@FName", firstName);
                    command.Parameters.AddWithNullCheck("@MName", middleName);
                    command.Parameters.AddWithNullCheck("@BDate", birthDate);
                    command.Parameters.AddWithNullCheck("@SexValue", sex);
                    command.Parameters.AddWithNullCheck("@EMailValue", email);
                    command.Parameters.AddWithNullCheck("@PhoneValue", phone);
                    command.Parameters.AddWithNullCheck("@PHash", newPasswordHash);

                    command.ExecuteNonQuery();
                }

                query = "update UserPics " +
                        "set Picture = @Pic " +
                        "where UserId = @UId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UId", id);
                    command.Parameters.AddWithNullCheck("@Pic", newPic);

                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Inserts

        public static User CreateUser(string username, string newPasswordHash, string lastName, string firstName, string middleName, DateTime birthDate,
            int sex, string email, string phone, byte[] pic)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "declare @UId int; " +
                               "insert into Users(Username, PasswordHash) " +
                               "VALUES(@UName, @Pwd); " +
                               "set @UId = SCOPE_IDENTITY();" +
                               "insert into UserProfiles(UserId, LastName, FirstName, MiddleName, BirthDate, Sex, Email, Phone) " +
                               "VALUES(@UId, @LName, @FName, @MName, @BDate, @SexVal, @EMailVal, @PhoneVal); " +
                               "insert into UserPics(UserId, Picture) " +
                               "VALUES(@UId, @Pic); " +
                               "select @UId; ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UName", username);
                    command.Parameters.AddWithNullCheck("@Pwd", newPasswordHash);
                    command.Parameters.AddWithNullCheck("@LName", lastName);
                    command.Parameters.AddWithNullCheck("@FName", firstName);
                    command.Parameters.AddWithNullCheck("@MName", middleName);
                    command.Parameters.AddWithNullCheck("@BDate", birthDate);
                    command.Parameters.AddWithNullCheck("@SexVal", sex);
                    command.Parameters.AddWithNullCheck("@EMailVal", email);
                    command.Parameters.AddWithNullCheck("@PhoneVal", phone);
                    command.Parameters.AddWithNullCheck("@Pic", pic ?? new byte[0]);

                    int id = command.ExecuteScalar().ConvertTo<int>();

                    return GetUser(id);
                }
            }
        }

        public static UserRole CreateRole(string name, string displayName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "insert into Roles(Name, DisplayName) " +
                               "values(@Name, @DName); " +
                               "select SCOPE_IDENTITY(); ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@Name", name);
                    command.Parameters.AddWithNullCheck("@DName", displayName);

                    int id = command.ExecuteScalar().ConvertTo<int>();

                    return GetUserRole(id);
                }
            }
        }

        public static void RemoveRole(UserRole role)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "delete from Roles where Id = @RId;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@RId", role.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddFunctionToRole(UserRole role, SystemFunction function)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "if not exists((select * from RoleFunctions where RoleFunctions.RoleId = @RId and RoleFunctions.SysFuncId = @SId)) " +
                    "begin " +
                    "insert into RoleFunctions(RoleId, SysFuncId) VALUES(@RId, @SId); " +
                    "end";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@RId", role.Id);
                    command.Parameters.AddWithNullCheck("@SId", function.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RemoveFunctionFromRole(UserRole role, SystemFunction function)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "if exists(select * from RoleFunctions where RoleFunctions.RoleId = @RId and RoleFunctions.SysFuncId = @SId) " +
                    "begin " +
                    "delete from RoleFunctions where RoleId = @RId and SysFuncId = @SId; " +
                    "end";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@RId", role.Id);
                    command.Parameters.AddWithNullCheck("@SId", function.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddRoleToUser(User user, UserRole role)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "if not exists(select * from UserRoles where UserRoles.UserId = @UId and UserRoles.RoleId = @RId) " +
                    "begin " +
                    "insert into UserRoles(UserId, RoleId) VALUES(@UId, @RId); " +
                    "end";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UId", user.Id);
                    command.Parameters.AddWithNullCheck("@RId", role.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RemoveRoleFromUser(User user, UserRole role)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query =
                    "if exists(select * from UserRoles where UserRoles.UserId = @UId and UserRoles.RoleId = @RId) " +
                    "begin " +
                    "delete from UserRoles where UserId = @UId and RoleId = @RId; " +
                    "end";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithNullCheck("@UId", user.Id);
                    command.Parameters.AddWithNullCheck("@RId", role.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}