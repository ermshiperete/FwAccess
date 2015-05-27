// Copyright (c) 2015 SIL International
// This software is licensed under the MIT License (http://opensource.org/licenses/MIT)
using System;
using System.IO;
using SIL.FieldWorks.FDO;

namespace FwAccess
{
	public class FdoDirectories: IFdoDirectories
	{
		private readonly string _baseDir;

		public FdoDirectories(string baseDir)
		{
			_baseDir = baseDir;
		}

		#region IFdoDirectories implementation

		public string ProjectsDirectory
		{
			get
			{
				return Path.Combine(_baseDir, "ReleaseData");
			}
		}

		public string DefaultProjectsDirectory
		{
			get
			{
				return ProjectsDirectory;
			}
		}

		public string TemplateDirectory
		{
			get
			{
				return Path.Combine(_baseDir, "Templates");
			}
		}

		#endregion
	}
}

