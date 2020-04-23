using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "La Mama", Location = "Sos. Stefan cel Mare", Cuisine = CuisineType.Romanian },
                new Restaurant { Id = 2, Name = "Pizza Fabio", Location = "Grivita", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 3, Name = "Los Pollos Hermanos", Location = "Albequerque", Cuisine = CuisineType.Mexican }
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var rest = restaurants.SingleOrDefault(r => r.Id == restaurant.Id);
            if (rest != null)
            {
                rest.Name = restaurant.Name;
                rest.Location = restaurant.Location;
                rest.Cuisine = restaurant.Cuisine;
            }
            return rest;
        }
    }
}
