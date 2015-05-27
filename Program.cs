// Copyright (c) 2015 SIL International
// This software is licensed under the MIT License (http://opensource.org/licenses/MIT)
using System;
using System.IO;
using SIL.FieldWorks.FDO;

namespace FwAccess
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{
			string baseDir;
			if (args != null && args.Length > 0)
				baseDir = args[0];
			else
				baseDir = Path.Combine(Environment.GetEnvironmentVariable("HOME"), "fwrepo/fw/DistFiles");
			var fw = new FwAccess(baseDir);
			using (var fdoCache = fw.TryGetFdoCache())
			{
				Console.WriteLine("Ethnologue Code: {0}", fdoCache.LangProject.EthnologueCode);
				Console.WriteLine("Interlinear texts:");
				foreach (var t in fdoCache.LangProject.InterlinearTexts)
				{
					Console.WriteLine("{0:D6}: title: {1} (comment: {2})", t.Hvo,
						t.Title.BestVernacularAlternative.Text,
						t.Comment.BestVernacularAnalysisAlternative.Text);
				}
			}
		}
	}
}
