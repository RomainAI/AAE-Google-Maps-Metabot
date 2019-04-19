using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Google_Maps_API
{
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast2 northeast { get; set; }
        public Southwest2 southwest { get; set; }
    }

    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> postcode_localities { get; set; }
        public List<string> types { get; set; }
    }

    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

    public class APICalls
    {

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted street number
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>Street number or "unknown"</returns>
        public static string GetStreetNumber(string jsonContent)
        {
            string returnValue = "unknown";

            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
            {
                foreach (AddressComponent objAddress in rootObject.results[0].address_components)
                {
                    if (objAddress.types.Contains("street_number"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted route
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>Route or "unknown"</returns>
        public static string GetRoute(string jsonContent)
        {
            string returnValue = "unknown";

            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
            {
                foreach (AddressComponent objAddress in rootObject.results[0].address_components)
                {
                    if (objAddress.types.Contains("route"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted city
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>city name or "unknown"</returns>
        public static string GetCity(string jsonContent)
        {
            string returnValue = "unknown"; 

            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
            {
                foreach (AddressComponent objAddress in rootObject.results[0].address_components)
                {
                    if (objAddress.types.Contains("postal_town"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                    else if (objAddress.types.Contains("locality"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                    else if (objAddress.types.Contains("administrative_area_level_3"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                    else if (objAddress.types.Contains("administrative_area_level_2"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                    else if (objAddress.types.Contains("administrative_area_level_1"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted postal code
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>postal code or "unknown"</returns>
        public static string GetPostalCode(string jsonContent)
        {
            string returnValue = "unknown";

            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
            {
                foreach (AddressComponent objAddress in rootObject.results[0].address_components)
                {
                    if (objAddress.types.Contains("postal_code"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted country
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>Country or "unknown"</returns>
        public static string GetCountry(string jsonContent)
        {
            string returnValue = "unknown";
            
            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
            {
                foreach (AddressComponent objAddress in rootObject.results[0].address_components)
                {
                    if (objAddress.types.Contains("country"))
                    {
                        returnValue = objAddress.long_name;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Deserialize a Google Maps API Json value to get formatted address
        /// </summary>
        /// <param name="jsonContent">Google Maps API Json result</param>
        /// <returns>Formatted address or "unknown"</returns>
        public static string GetFormattedAddress(string jsonContent)
        {
            string returnValue = "unknown";

            RootObject rootObject = new JavaScriptSerializer().Deserialize<RootObject>(jsonContent);

            if (rootObject.status.ToLower() == "ok")
                returnValue = rootObject.results[0].formatted_address;

            return returnValue;
        } 


        /// <summary>
        /// Get JSON response from Google geocode API call
        /// </summary>
        /// <param name="address">Address to send to Google API</param>
        /// <param name="key">Google API account key</param>
        /// <returns>JSON response</returns>
        public static string GetJsonResultFromCall(string address, string key)
        {
            string jsonResult = string.Empty;
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", Uri.EscapeDataString(address), Uri.EscapeDataString(key));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    jsonResult = reader.ReadToEnd();
                }
            }
            catch
            {
                jsonResult = string.Empty;
            }

            return jsonResult;
        }

    }

}
