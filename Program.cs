using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    class Flight
    {
        public int depertureAirportId;
        public int arivalAirportId;
        public DateTime departureDate;
        public DateTime arrivalDate;

        public Flight(int depertureAirportId, int arivalAirportId, DateTime departureDate, DateTime arrivalDate)
        {
            this.depertureAirportId = depertureAirportId;
            this.arivalAirportId = arivalAirportId;
            this.departureDate = departureDate;
            this.arrivalDate = arrivalDate;
        }

    }

    class ShortPath
    {
        public List<List<int>> paths;
        public List<int> airports;
        public List<int> depAirports;
        public List<int> arrAirports;
        public List<Flight> flights;
        public DateTime depDate;
        public int k = 5;

        public ShortPath(List<int> airports, List<Flight> flights, List<int> depAirports, List<int> arrAirports, DateTime depDate)
        {
            this.airports = airports;
            this.depAirports = depAirports;
            this.arrAirports = arrAirports;
            this.depDate = depDate;
            this.flights = flights;
        }

        public List<List<Flight>> getShortPath()
        {
            List<List<Flight>> paths = new List<List<Flight>>();
            for (int i = 0; i < depAirports.Count; ++i)
            {
                for (int j = 0; j < flights.Count; ++j)
                {
                    if (flights[j].depertureAirportId == depAirports[i])
                    {
                        List<List<Flight>> newPaths = getShortPath(flights[j]);
                        paths.AddRange(newPaths);
                    }
                }
            }
            List<List<Flight>> filteredPaths = paths.Where(x => arrAirports.Contains(x.Last().arivalAirportId)).ToList();
            filteredPaths.OrderBy(x => x.Last().arrivalDate);
            return filteredPaths;
        }

        List<List<Flight>> getShortPath(Flight flight)
        {
            List<List<Flight>> paths = new List<List<Flight>>();
            Flight currFlight = flight;
            if (arrAirports.Contains(currFlight.arivalAirportId))
            {
                List<Flight> path = new List<Flight>() { currFlight };
                paths.Insert(0, path);
                return paths;
            }
            for (int i = 0; i < flights.Count; ++i)
            {
                if(flights[i].depertureAirportId == currFlight.arivalAirportId && flights[i].departureDate > currFlight.arrivalDate)
                {
                    List<List<Flight>> nextFlights = getShortPath(flights[i]);
                    paths.AddRange(nextFlights);
                }
            }
            for (int i = 0; i < paths.Count; ++i)
            {
                paths[i].Insert(0, currFlight);
            }
            if (paths.Count == 0)
            {
                List<Flight> path = new List<Flight>() { currFlight };
                paths.Insert(0, path);
            }
            return paths;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Flight flight1 = new Flight(1, 2, DateTime.Parse("Jan 1, 2009 03:00"), DateTime.Parse("Jan 1, 2009 10:00"));
            Flight flight2 = new Flight(1, 3, DateTime.Parse("Jan 1, 2009 05:00"), DateTime.Parse("Jan 1, 2009 17:00"));
            Flight flight3 = new Flight(2, 3, DateTime.Parse("Jan 1, 2009 11:00"), DateTime.Parse("Jan 1, 2009 13:00"));
            Flight flight4 = new Flight(2, 3, DateTime.Parse("Jan 1, 2009 01:00"), DateTime.Parse("Jan 1, 2009 20:30"));
            Flight flight5 = new Flight(3, 2, DateTime.Parse("Jan 2, 2009 07:00"), DateTime.Parse("Jan 2, 2009 10:00"));

            List<int> dep = new List<int>() { 1 };
            List<int> arr = new List<int>() { 2 };
            List<Flight> flights = new List<Flight>();
            flights.Add(flight1);
            flights.Add(flight2);
            flights.Add(flight3);
            flights.Add(flight4);
            flights.Add(flight5);

            var newDate = DateTime.Parse("Jan 1, 2009 00:00");
            List<int> airports = new List<int>() { 1, 2, 3 };

            var algorithm = new ShortPath(airports, flights, dep, arr, newDate);
            var paths = algorithm.getShortPath();
            foreach(List<Flight> path in paths)
            {
                foreach (Flight flight in path)
                {
                    Console.WriteLine(flight.depertureAirportId);
                    Console.WriteLine(flight.arivalAirportId);
                    Console.WriteLine(flight.departureDate);
                    Console.WriteLine("--------------");


                }
                Console.WriteLine("---------------------------------------");
            }


            Console.ReadLine();
        }
    }
}
