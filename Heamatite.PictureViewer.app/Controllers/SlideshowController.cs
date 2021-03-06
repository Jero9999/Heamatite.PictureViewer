﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Heamatite.PictureViewer.app.Models;
using System.IO;
using System.Text;

namespace Heamatite.PictureViewer.app.Controllers
{
	public class SlideshowController : Controller
	{
		private readonly string FileLocation = @"C:\camera photos";


		public async Task<IActionResult> NextImage(string fileName)
		{
			fileName = Decode(fileName);
			var currentFolder = Path.GetDirectoryName(fileName);
			var files = Directory.GetFiles(currentFolder);
			var position = Array.IndexOf(files, fileName, 0);
			return this.RedirectToAction("Next", new { position, currentFolder });
		}


		public IActionResult Next(int position = 0, string currentFolder = null)
		{
			return View("NextVirtualSlide", new SlideshowModel
			{
				Position = position,
				CurrentFolder = Encode(currentFolder),
				NumberOfImages = Directory.GetFiles(currentFolder).Count()
			});
		}

		public IActionResult Image(int position, string currentFolder = null)
		{
			currentFolder = Decode(currentFolder);

			var files = Directory.GetFiles(currentFolder);
			var numFiles = files.Count();
			position = (position + numFiles) % numFiles;
			//position = position < 0 ? numFiles + position : position;
			Debug.WriteLine($"********{position}");
			var fileToShow = files.ElementAt(position);

			var stream = new FileInfo(fileToShow).OpenRead();
			{
				return this.File(stream, "image/jpeg");
			}
		}

		[Route("Image/Shuffle")]
		public IActionResult ImageShuffle(string currentFolder = null)
		{
			currentFolder = Decode(currentFolder);
			var shuffler = new Random();
			var files = Directory.GetFiles(currentFolder);
			var numFiles = files.Count();

			var position = shuffler.Next(numFiles);
			position = position < 0 ? numFiles + position : position;

			var fileToShow = files.ElementAt(position);

			var stream = new FileInfo(fileToShow).OpenRead();
			{
				return this.File(stream, "image/jpeg");
			}
		}

		private string Encode(string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));

		private string Decode(string str)
		{
			var encodedBytes = Convert.FromBase64String(str);
			return Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Count());
		}
	}
}
