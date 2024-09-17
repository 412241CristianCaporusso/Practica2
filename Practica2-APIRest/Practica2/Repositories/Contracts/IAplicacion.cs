using Practica2.Models;

namespace Practica2.Repositories.Contracts
{
    public interface IAplicacion

    {
        bool Add(Article article);
        List<Article> GetAll();
        bool Edit(Article article);
        bool Delete(int id);


    }
}
