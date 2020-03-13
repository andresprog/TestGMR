using System;

namespace Cofradinn.Modules.Utilities
{
    public static class Haversine 
    {
        //public float ToRadian(float angle)
        //{
        //    return (float)(Math.PI * angle / 180.0);
        //}
        const float kEarthRadiusKms = 6371.16f;
        //public float HaversineDistance(Point a, Point b)
        //{
        //    const float radius = 6371.16f; 
        //    float latitude = ToRadian(b.Latitude - a.Latitude) / 2;
        //    float longitude = ToRadian(b.Longitude - a.Longitude) / 2;

        //    float x = (float)(Math.Sin(latitude) * Math.Sin(latitude) + 
        //               Math.Sin(longitude) * Math.Sin(longitude) *
        //               Math.Cos(ToRadian(a.Latitude)) * Math.Cos(ToRadian(b.Latitude)));

        //    float y = (float)(2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x)));

        //    return radius * y;
        //}

        public static float Calc(double Lat1, double Long1, double Lat2, double Long2)
        {
            /*
                The Haversine formula according to Dr. Math.
                http://mathforum.org/library/drmath/view/51879.html

                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c

                Where
                    * dlon is the change in longitude
                    * dlat is the change in latitude
                    * c is the great circle distance in Radians.
                    * R is the radius of a spherical Earth.
                    * The locations of the two points in 
                        spherical coordinates (longitude and 
                        latitude) are lon1,lat1 and lon2, lat2.
            */

            float dLat1InRad = (float)(Lat1 * (Math.PI / 180.0));
            float dLong1InRad = (float)(Long1 * (Math.PI / 180.0));
            float dLat2InRad = (float)(Lat2 * (Math.PI / 180.0));
            float dLong2InRad = (float)(Long2 * (Math.PI / 180.0));

            float dLongitude = dLong2InRad - dLong1InRad;
            float dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            float a = (float)(Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0));

            // Intermediate result c (great circle distance in Radians).
            float c = (float)(2.0 * Math.Asin(Math.Sqrt(a)));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            //const float kEarthRadiusKms = 6376.5f;
            
            float dDistance = kEarthRadiusKms * c;

            return dDistance;


        }
    }
}

