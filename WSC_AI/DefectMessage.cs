using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DefectMessageNamespace
{
    public class DefectMessage
    {
        [JsonPropertyName("modelId")]
        public string ModelId { get; set; }

        [JsonPropertyName("defects")]
        public List<Defect> Defects { get; set; } = new List<Defect>();

        [JsonPropertyName("totalDefects")]
        public int TotalDefects { get; set; }
    }

    public class Defect
    {
        [JsonPropertyName("coord")]
        public DefectCoordinates DefectCoordinates { get; set; }
        [JsonPropertyName("descriptions")]
        public List<string> Descriptions { get; set; } = new List<string>();
        [JsonPropertyName("image")]
        public string ImageBase64 { get; set; }
        [JsonPropertyName("defectId")]
        public int DefectId { get; set; }
    }

    public class DefectCoordinates
    {
        [JsonPropertyName("x")]
        public double X { get; set; }
        [JsonPropertyName("y")]
        public double Y { get; set; }
        [JsonPropertyName("z")]
        public double Z { get; set; }
        [JsonPropertyName("xr")]
        public double Xr { get; set; }
        [JsonPropertyName("yr")]
        public double Yr { get; set; }
        [JsonPropertyName("zr")]
        public double Zr { get; set; }
    }

}
