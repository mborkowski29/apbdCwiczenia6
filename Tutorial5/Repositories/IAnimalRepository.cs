using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    void AddAnimal(AddAnimal animal);
    void UpdateAnimal(int idAnimal, AddAnimal animal);
    void DeleteAnimal(int idAnimal);
}