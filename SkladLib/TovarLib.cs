using UserStory_Men_Cher.Models;

namespace SkladLib
{
    public class TovarLib
    {
        private List<Tovar> tovars = new List<Tovar>();

        public Tovar GetTypeOf()
        {
            return new Tovar();
        }

        public List<Tovar> GetTovar()
        {
            return tovars;
        }

        public Tovar AddTovar(Tovar tovar)
        {
            tovars.Add(tovar);
            return tovar;
        }

        public void ChangeTovar(Tovar oldValue, Tovar newValue)
        {
            tovars[tovars.IndexOf(oldValue)] = newValue;
        }

        public void RemoveTovar(Tovar oldValue)
        {
            tovars.Remove(oldValue);
        }

        public int GetCountTovar()
        {
            return tovars.Count();
        }

        public double GetAllPriceTovar()
        {
            return tovars.Select(p => p.AllPrice).Sum();
        }

        public double GetAllPriceNDSTovar()
        {
            return tovars.Select(p => p.AllPrice).Sum() + tovars.Select(p => p.AllPrice).Sum() * 0.2;
        }
    }
}