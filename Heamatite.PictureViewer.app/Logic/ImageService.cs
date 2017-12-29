using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace Heamatite.PictureViewer.app.Logic
{
	public interface IImageService
	{
		Stream GetThumbnail(string fileName, uint maxHeightAndWidth);
	}
	public class ImageService : IImageService
	{
		public Stream GetThumbnail(string fileName, uint maxHeightAndWidth)
		{
			var stream = new MemoryStream();
			using (Image<Rgba32> image = Image.Load(fileName))
			{
				CalculateThumbnailDimensions(image, maxHeightAndWidth, out var newWidth, out var newHeight);
				//var orientation = image.MetaData.ExifProfile.Values.FirstOrDefault(c => c.Tag == SixLabors.ImageSharp.MetaData.Profiles.Exif.ExifTag.Orientation);
				image.Mutate(x => x
						 .Resize((int)newWidth, (int)newHeight));
				image.Save(stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder()); // automatic encoder selected based on extension.
			}
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		private static void CalculateThumbnailDimensions(Image<Rgba32> image, uint thumbDimension, out uint newWidth, out uint newHeight)
		{
			//use a single thumb dimension so assume square. Just makes things easier

			//TODO this will actually enlarge an image if it is smaller than the thumbnail size. What to do?
			var imageAspectRatio = image.Width / (float)image.Height;
			if (imageAspectRatio > 1)
			{
				newWidth = thumbDimension;
				newHeight = (uint)(newWidth / imageAspectRatio);
			}
			else
			{
				newHeight = thumbDimension;
				newWidth = (uint)(newHeight * imageAspectRatio);
			}
		}
	}
}
