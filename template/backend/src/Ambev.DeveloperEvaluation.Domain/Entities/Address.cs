namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a user's address information.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zip code of the address.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation information of the address.
        /// </summary>
        public Geolocation Geolocation { get; set; } = new Geolocation();
    }
}
