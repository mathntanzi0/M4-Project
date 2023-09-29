using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models
{
    /// <summary>
    /// Represents an address with latitude and longitude coordinates.
    /// </summary>
    public class Address
    {
        private string addressName;
        private readonly double latitude;
        private readonly double longitude;



        /// <summary>
        /// Creates an address with latitude and longitude coordinates.
        /// </summary>
        public Address()
        {

        }
        /// <summary>
        /// Creates an address with latitude and longitude coordinates.
        /// </summary>
        /// <param name="latitude">The latitude coordinate of the address.</param>
        /// <param name="longitude">The longitude coordinate of the address.</param>
        public Address(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        /// <summary>
        /// Creates an address with an address name, latitude, and longitude coordinates.
        /// </summary>
        /// <param name="addressName">The name of the address.</param>
        /// <param name="latitude">The latitude coordinate of the address.</param>
        /// <param name="longitude">The longitude coordinate of the address.</param>
        public Address(string addressName, double latitude, double longitude)
        {
            this.addressName = addressName;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Checks if given coordinates are within a specified boundary radius.
        /// </summary>
        /// <param name="latitude">The latitude of the coordinates.</param>
        /// <param name="longitude">The longitude of the coordinates.</param>
        /// <param name="boundaryRadiusKm">The radius of the boundary in kilometers.</param>
        /// <returns>True if the coordinates are within the boundary, otherwise false.</returns>
        public static bool AreCoordinatesWithinBoundary(double latitude, double longitude, double boundaryRadiusKm)
        {
            double centerLatitude = 0.0;
            double centerLongitude = 0.0;

            double earthRadiusKm = 6371;
            double dLat = DegreeToRadian(latitude - centerLatitude);
            double dLon = DegreeToRadian(longitude - centerLongitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreeToRadian(centerLatitude)) * Math.Cos(DegreeToRadian(latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadiusKm * c;

            return distance <= boundaryRadiusKm;
        }

        private static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }


        /// <summary>
        /// Gets the latitude coordinate of the address.
        /// </summary>
        public double Latitude { get => latitude; }

        /// <summary>
        /// Gets the longitude coordinate of the address.
        /// </summary>
        public double Longitude { get => longitude; }
        /// <summary>
        /// Gets the name of the address.
        /// </summary>
        public string AddressName { get => addressName; set => addressName = value; }
        public string AddressName_Short { get => Utilities.TextManager.ShortString(addressName, 25); }
    }
}