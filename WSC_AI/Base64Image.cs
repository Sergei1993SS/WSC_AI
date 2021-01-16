using OpenCvSharp;

namespace WSC_AI
{
    class Base64Image
    {
        public static string Base64Encode(Mat Image)
        {
            byte[] originalBytes = Image.ToBytes(".jpg");
            
            return System.Convert.ToBase64String(originalBytes);
        }

        public static Mat Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return Cv2.ImDecode(base64EncodedBytes, ImreadModes.Color);
        }
    }
}
