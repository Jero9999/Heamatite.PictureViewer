using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heamatite.PictureViewer.app.Models;
using System.IO;

namespace Heamatite.PictureViewer.app.Controllers
{
	public class SlideshowController : Controller
	{
		private readonly string FileLocation = @"C:\camera photos";

		public IActionResult Next(int position = 0)
		{
			var newPosition = position + 1;
			return View(newPosition);
		}

		public async Task<IActionResult> Image(int position)
		{
			var files = Directory.GetFiles(FileLocation);
			var numFiles = files.Count();
			position %= numFiles;
			position = position < 0 ? numFiles + position : position;

			var fileToShow = files.ElementAt(position);

			var stream = new FileInfo(fileToShow).OpenRead();
			{
				return this.File(stream, "image/jpeg");
			}
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
