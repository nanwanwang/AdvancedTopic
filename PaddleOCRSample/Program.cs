using System.Threading.Channels;
using OpenCvSharp;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.KnownModels;

OCRModel model = KnownOCRModel.PPOcrV2;
await  model.EnsureAll();

byte[] sampleImageData;
string sampleImageUrl = @"https://img0.baidu.com/it/u=781212115,4208908823&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=375";
using var httpclient =new HttpClient();
Console.WriteLine("Download sample image from: " + sampleImageUrl);
sampleImageData = await httpclient.GetByteArrayAsync(sampleImageUrl);

using PaddleOcrAll all= new PaddleOcrAll(model.RootDirectory,model.KeyPath)
{
    AllowRotateDetection = false,
    Enable180Classification = false
};

using Mat src = Cv2.ImDecode(sampleImageData, ImreadModes.Color);
PaddleOcrResult result = all.Run(src);
Console.WriteLine("Detected all texts: \n" + result.Text);
foreach (PaddleOcrResultRegion region in result.Regions)
{
    Console.WriteLine($"Text:{region.Text},Score:{region.Score},RectCenter:{region.Rect.Center},RectSize:{region.Rect.Size},Angle:{region.Rect.Angle}");
}
