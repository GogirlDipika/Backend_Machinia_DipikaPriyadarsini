using MongoDB.Bson.Serialization.Attributes;

namespace MachiniaAPI.Model
{
    public class TrainingCenter
    {
        /// <summary>
        /// ID from MongoDB
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] 
        public string Id { get; set; }

        /// <summary>
        /// Center Name - Mandatory
        /// </summary>
        public string CenterName { get; set; }

        /// <summary>
        /// Center Code - Mandatory
        /// </summary>
        public string CenterCode { get; set; }

        /// <summary>
        /// Address - Mandatory
        /// </summary>
        public Address Address { get; set; }

        public int StudentCapacity { get; set; }

        public List<string> CoursesOffered { get; set; }

        public string CreatedOn { get; set; }

        public string ContactEmail { get; set; }

        /// <summary>
        /// Contact Phone - Mandatory
        /// </summary>
        public string ContactPhone { get; set; }

    }
}
