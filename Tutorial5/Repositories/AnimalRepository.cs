using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Animal> GetAnimals(string orderBy = "Name")
        {
            var animals = new List<Animal>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM Animal ORDER BY {orderBy};";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                animals.Add(new Animal()
                                {
                                    IdAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Category = reader.GetString(reader.GetOrdinal("Category")),
                                    Area = reader.GetString(reader.GetOrdinal("Area"))
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"błąd podczas pobierania: {ex.Message}");
                throw; // Re-throw the exception to propagate it to the caller
            }

            return animals;
        }

        public void AddAnimal(AddAnimal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Animal (Name, Description, Category, Area) VALUES (@animalName, @description, @category, @area);", connection))
                    {
                        command.Parameters.AddWithValue("@animalName", animal.Name);
                        command.Parameters.AddWithValue("@description", animal.Description ?? "");
                        command.Parameters.AddWithValue("@category", animal.Category ?? "");
                        command.Parameters.AddWithValue("@area", animal.Area ?? "");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"błąd przy dodawaniu: {ex.Message}");
                throw;
            }
        }
        public void UpdateAnimal(int idAnimal, AddAnimal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE Animal SET Name = @animalName, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @idAnimal;", connection))
                    {
                        command.Parameters.AddWithValue("@idAnimal", idAnimal);
                        command.Parameters.AddWithValue("@animalName", animal.Name);
                        command.Parameters.AddWithValue("@description", animal.Description ?? "");
                        command.Parameters.AddWithValue("@category", animal.Category ?? "");
                        command.Parameters.AddWithValue("@area", animal.Area ?? "");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"błąd przy aktualizowaniu: {ex.Message}");
                throw;
            }
        }
        public void DeleteAnimal(int idAnimal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @idAnimal;", connection))
                    {
                        command.Parameters.AddWithValue("@idAnimal", idAnimal);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"bład przy usuwaniu: {ex.Message}");
                throw;
            }
        }
}

