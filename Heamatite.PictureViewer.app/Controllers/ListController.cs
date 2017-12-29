using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heamatite.PictureViewer.app.Models;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using SixLabors.ImageSharp;
using Heamatite.PictureViewer.app.Logic;
using System.Text;

namespace Heamatite.PictureViewer.app.Controllers
{
	[Route("List")]
	public partial class ListController : Controller
	{
		private readonly string FileLocation = @"C:\ImageSamples";
		private readonly IImageService imageService;

		public ListController(IMemoryCache cache, IImageService imageService)
		{
			this.Cache = cache;
			this.imageService = imageService;
		}

		public IMemoryCache Cache { get; }

		//[Route("Index")]
		[HttpGet]
		[Route("Index")]
		public IActionResult Index(string currentFolder)
		{
			if (currentFolder != null)
			{
				currentFolder = Decode(currentFolder);
			}

			var allFolders = Directory.GetDirectories(FileLocation, "*", SearchOption.AllDirectories);
			
			var model = new ListModel
			{
				Folders = allFolders.ToDictionary(
					c => Path.GetFileName(c), 
					c=> Encode(c)),
				Images = GetAndCacheImages(currentFolder)
			};

			return View(model);
		}

		private IEnumerable<string> GetAndCacheImages(string folder = null)
		{
			folder = folder ?? FileLocation;

			var files = Directory.GetFiles(folder).Take(5);

			return files.Select(c => Encode(c));
		}

		[HttpGet]
		[Route("thumb/{file}")]
		public IActionResult Thumb(string file)
		{
			file = Decode(file);
			var stream = imageService.GetThumbnail(file, 200);
			return this.File(stream, "image/jpeg");
		}

		private string Encode(string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));

		private string Decode(string str)
		{
			var encodedBytes = Convert.FromBase64String(str);
			return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Count());

		}
	}
}
