using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;
using EXE201_BE_ThrivoHR.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace EXE201_BE_ThrivoHR.Infrastructure.Repositories;
public class FaceDetectionRepository : IFaceDetectionRepository
{
    public async Task<string> DetectFaceFromImage(IFormFile image, string[] directories)
    {
        var cascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml");
        CascadeClassifier faceClassifier = new(cascadePath);
        var recognizedNames = "";
        List<Image<Gray, byte>> trainingImages = [];
        List<string> labels = [];
        var ContTrain = 0;

        try
        {
            // Load previously trained faces and labels for each image
            foreach (var dir in directories)
            {
                var label = Path.GetFileNameWithoutExtension(dir); // Get the label from the directory name
                var files = Directory.GetFiles(dir, "*.bmp");

                foreach (var file in files)
                {
                    trainingImages.Add(new Image<Gray, byte>(file));
                    labels.Add(label);
                }
            }

            ContTrain = labels.Count;
            var filePath = await SaveImage(image, "UnknownEmployee", "UnknownEmployeeFolder");

            // Take the latest image from the UnknowEmployeeFolder
            var grayImage = new Image<Bgr, byte>(filePath).Convert<Gray, byte>();
            // Convert the IFormFile to Image<Bgr, Byte>


            // Face Detector
            var facesDetected = faceClassifier.DetectMultiScale(
                grayImage,
                1.2,
                10,
                new Size(20, 20),
                Size.Empty);

            foreach (var faceRect in facesDetected)
            {
                var result = grayImage.Copy(faceRect).Resize(100, 100, Inter.Cubic);

                if (trainingImages.Count == 0)
                {
                    continue;
                }

                // Create the Eigen face recognizer
                EigenFaceRecognizer recognizer = new();
                await using (VectorOfMat imagesVector = new(trainingImages.Select(img => img.Mat).ToArray()))
                await using (VectorOfInt labelsVector = new(Enumerable.Range(0, labels.Count).ToArray()))
                {
                    recognizer.Train(imagesVector, labelsVector);
                }

                var resultRecognized = recognizer.Predict(result);
                if (resultRecognized.Label == -1)
                {
                    continue;
                }

                var name = labels[resultRecognized.Label];
                recognizedNames = name;
            }

            // Print recognized names
            if (string.IsNullOrEmpty(recognizedNames))
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

        return recognizedNames;
    }

    public async Task<string> SaveImage(IFormFile image, string employeeId, string employeeFolderPath,
        CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(employeeFolderPath))
        {
            _ = Directory.CreateDirectory(employeeFolderPath);
        }

        // Check if the image is not empty
        if (image.Length == 0)
        {
            throw new NotFoundException("Image is empty");
        }

        // Construct the path to save the image inside the employee's folder
        var uniqueFileName = $"{Guid.NewGuid()}.bmp";
        var filePath = Path.Combine(employeeFolderPath, uniqueFileName);

        try
        {
            await using FileStream stream = new(filePath, FileMode.Create);
            await image.CopyToAsync(stream, cancellationToken);
            return filePath;
        }
        catch 
        {
            return "Fail";
        }
    }
}