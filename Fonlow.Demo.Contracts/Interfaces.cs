

namespace Fonlow.Demo.Contracts
{
    public interface IPeopleManager
    {
        string Create(Person p);

        Person Read(string id);

        bool Update(Person p);

        bool Delete(string id);
    }


    public interface IEntityManager
    {
        string Create(Entity p);

        Entity Read(string id);

        bool Update(Entity p);

        bool Delete(string id);
    }

    public interface ICompaniesManager
    {
        string Create(Company p);

        Company Read(string id);

        bool Update(Company p);

        bool Delete(string id);
    }


    public interface IFilesManager
    {
        string Create(MediaFile file);

        MediaFile Read(string id);

        bool Update(MediaFile p);

        bool Delete(string id);
    }


}
