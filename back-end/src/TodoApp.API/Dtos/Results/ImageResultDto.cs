namespace TodoApp.API.Dtos.Results
{
    public class ImageResultDto
    {
        /// <summary>
        /// Id of the image
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name of the image
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Description of the image
        /// </summary>
        public string Description { get; set; } = null!;

    }
}
