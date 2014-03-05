/*
 * Created by SharpDevelop.
 * Author: kmlxk
 * Date: 2013/6/18
 * Time: 15:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CommonLang.UnitTest
{
	public class PathHelperTest
	{
		[Test] public void getFilenameTest()
		{
			string path = "F:\\workspace\\csharp\\CommonLang\\PathHelper.cs";
			Assert.AreEqual("PathHelper.cs", PathHelper.getFilename(path));
			
			path = "F:\\workspace\\csharp\\CommonLang\\";
			Assert.AreEqual(string.Empty, PathHelper.getFilename(path));
			
			path = "CommonLang\\PathHelper.cs";
			Assert.AreEqual("PathHelper.cs", PathHelper.getFilename(path));
			
			path = "PathHelper.cs";
			Assert.AreEqual("PathHelper.cs", PathHelper.getFilename(path));
			
			path = "http://trip.cmbchina.com/cachefile.js";
			Assert.AreEqual("cachefile.js", PathHelper.getFilename(path, '/'));
			
		}
		
		[Test] public void getDirectoryTest()
		{
			string path = "F:\\workspace\\csharp\\CommonLang\\PathHelper.cs";
			Assert.AreEqual("F:\\workspace\\csharp\\CommonLang\\", PathHelper.getDirectory(path));
			
			path = "F:\\workspace\\csharp\\CommonLang\\";
			Assert.AreEqual("F:\\workspace\\csharp\\CommonLang\\", PathHelper.getDirectory(path));
			
			path = "CommonLang\\PathHelper.cs";
			Assert.AreEqual("CommonLang\\", PathHelper.getDirectory(path));
			
			path = "PathHelper.cs";
			Assert.AreEqual(string.Empty, PathHelper.getDirectory(path));
			
			path = "http://trip.cmbchina.com/cachefile.js";
			Assert.AreEqual("http://trip.cmbchina.com/", PathHelper.getDirectory(path, '/'));
			
		}
		
		[Test] public void toPathTest()
		{
			string path = "F:\\workspace\\csharp\\CommonLang\\";
			Assert.AreEqual("F:\\workspace\\csharp\\CommonLang\\", PathHelper.toPath(path));
			
			path = "F:\\workspace\\csharp\\CommonLang";
			Assert.AreEqual("F:\\workspace\\csharp\\CommonLang\\", PathHelper.toPath(path));
		}
	}
}
