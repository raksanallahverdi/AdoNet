using Ado.net.Constants;
using System;
using System.Data.SqlClient;

namespace Ado.net.Services
{
    internal class CountryService
    {
        public static void GetAllCountries()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.Default))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Countries", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string name = Convert.ToString(reader["Name"]);
                        string area = Convert.ToString(reader["Area"]);

                        Console.WriteLine($"{id}, {name}, {area}");
                    }
                }
            }
        }

        public static void AddCountry()
        {
            Messages.InputMessage("country name");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                Messages.InputMessage("country area");
                string areaInput = Console.ReadLine();
                decimal area;
                bool isSucceeded = decimal.TryParse(areaInput, out area);
                if (isSucceeded)
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionStrings.Default))
                    {
                        connection.Open();
                        var command = new SqlCommand("INSERT INTO Countries VALUES(@name, @area)", connection);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@area", area);

                        try
                        {
                            var affectedRows = command.ExecuteNonQuery();
                            if (affectedRows > 0)
                            {
                                Messages.SuccessAddMessage("country", name);
                            }
                            else
                            {
                                Messages.ErrorOccuredMessage();
                            }
                        }
                        catch (Exception)
                        {
                            Messages.ErrorOccuredMessage();
                        }
                    }
                }
                else
                {
                    Messages.InvalidInputMessage("Country area");
                }
            }
            else
            {
                Messages.InvalidInputMessage("Country Name");
            }
        }

        public static void UpdateCountry()
        {
            GetAllCountries();
            Messages.InputMessage("Country Name");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.Default))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM COUNTRIES WHERE Name=@name", connection);
                    command.Parameters.AddWithValue("@name", name);

                    try
                    {
                        int id = Convert.ToInt32(command.ExecuteScalar());
                        if (id > 0)
                        {
                        NameWantToChangeSection: Messages.PrintWantToChangeMessage("name");
                            var choiceForName = Console.ReadLine();
                            char choice;
                            bool isSucceeded = char.TryParse(choiceForName, out choice);
                            if (isSucceeded && choice == 'y')
                            {
                                string newName = string.Empty;
                                if (choice.Equals('y'))
                                {
                                InputNewNameSection: Messages.PrintWantToChangeMessage("New name");
                                    newName = Console.ReadLine();

                                    if (string.IsNullOrWhiteSpace(newName))
                                    {
                                        var alreadyExistedCommand = new SqlCommand("SELECT * FROM COUNTRIES WHERE Name=@name", connection);
                                        alreadyExistedCommand.Parameters.AddWithValue("@name", newName);
                                        alreadyExistedCommand.Parameters.AddWithValue("@id", id);

                                        int existId = Convert.ToInt32(alreadyExistedCommand.ExecuteScalar());
                                        if (existId > 0)
                                        {
                                            Messages.AlreadyExistMessage("country", newName);
                                            goto NameWantToChangeSection;
                                        }
                                    }
                                    else
                                    {
                                        Messages.InvalidInputMessage("New Name");
                                        goto InputNewNameSection;
                                    }
                                }

                            AreaWantToChangeSection: Messages.PrintWantToChangeMessage("area");
                                var choiceForArea = Console.ReadLine();
                                isSucceeded = char.TryParse(choiceForArea, out choice);
                                decimal newArea = default;
                                if (isSucceeded && choice == 'y')
                                {
                                InputNewArea: Messages.PrintWantToChangeMessage("New Area");
                                    var newAreaInput = Console.ReadLine();
                                    isSucceeded = decimal.TryParse(newAreaInput, out newArea);
                                    if (!isSucceeded)
                                    {
                                        Messages.InvalidInputMessage("New Area");
                                        goto InputNewArea;
                                    }
                                }
                                else
                                {
                                    Messages.InvalidInputMessage("New Area");
                                    goto AreaWantToChangeSection;
                                }

                                var updateCommand = new SqlCommand("UPDATE COUNTRIES SET", connection);
                                if (!string.IsNullOrEmpty(newName) || newArea != default)
                                {
                                    bool isRequiredComma = false;
                                    if (!string.IsNullOrEmpty(newName))
                                    {
                                        isRequiredComma = true;
                                        updateCommand.CommandText = updateCommand.CommandText + " Name=@name";
                                        updateCommand.Parameters.AddWithValue("@name", newName);
                                    }
                                    if (newArea != default)
                                    {
                                        string commaText = isRequiredComma ? "," : "";
                                        updateCommand.CommandText = updateCommand.CommandText + " Area=@area";
                                        updateCommand.Parameters.AddWithValue("@area", newArea);
                                    }
                                    updateCommand.CommandText = updateCommand.CommandText + " WHERE id=@id";
                                    updateCommand.Parameters.AddWithValue("@id", id);
                                    int affectedRows = Convert.ToInt32(updateCommand.ExecuteNonQuery());
                                    if (affectedRows > 0)
                                    {
                                        Messages.SuccessUpdateMessage("country", newName);
                                    }
                                    else
                                    {
                                        Messages.ErrorOccuredMessage();
                                    }
                                }
                                else
                                {
                                    Messages.InvalidInputMessage("choice");
                                }
                            }
                            else
                            {
                                Messages.NotFoundMessage("country", name);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Messages.ErrorOccuredMessage();
                    }
                }
            }
            else
            {
                Messages.InvalidInputMessage("country name");
            }
        }

        public static void DeleteCountry()
        {
            Messages.InputMessage("country ID");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings.Default))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Countries WHERE Id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        var affectedRows = command.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            Messages.SuccessDeleteMessage("country", id.ToString());
                        }
                        else
                        {
                            Messages.ErrorOccuredMessage();
                        }
                    }
                    catch (Exception)
                    {
                        Messages.ErrorOccuredMessage();
                    }
                }
            }
            else
            {
                Messages.InvalidInputMessage("Country ID");
            }
        }

        public static void GetDetailsOfCountry()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.Default))
            {
                connection.Open();
                Messages.InputMessage("country ID");
                int id;
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    var command = new SqlCommand("SELECT * FROM Countries WHERE Id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = Convert.ToString(reader["Name"]);
                            string area = Convert.ToString(reader["Area"]);
                            Console.WriteLine($"ID: {id}, Name: {name}, Area: {area}");
                        }
                        else
                        {
                            Messages.NotFoundMessage("country", id.ToString());
                        }
                    }
                }
                else
                {
                    Messages.InvalidInputMessage("id");
                }
            }
        }
    }
}
