// Copyright (c) 2015 SIL International
// This software is licensed under the MIT License (http://opensource.org/licenses/MIT)
using System;
using System.IO;
using System.Threading;
using SIL.CoreImpl;
using SIL.FieldWorks.FDO;
using SIL.Utils;

namespace FwAccess
{
	public class FwAccess
	{
		private readonly IFdoDirectories _fdoDirs;
		private readonly IThreadedProgress _progress = new ThreadedProgress();
		private readonly IFdoUI _fdoUi;

		public FwAccess(string baseDir)
		{
			_fdoDirs = new FdoDirectories(baseDir);
			_fdoUi = new ConsoleFdoUi(_progress.SynchronizeInvoke);
		}

		public FdoCache TryGetFdoCache()
		{
			FdoCache fdoCache = null;
			var path = Path.Combine(_fdoDirs.ProjectsDirectory, "Sena 3",
				"Sena 3" + FdoFileHelper.ksFwDataXmlFileExtension);
			if (!File.Exists(path))
			{
				return null;
			}

			var settings = new FdoSettings {DisableDataMigration = true};

			try
			{
				fdoCache = FdoCache.CreateCacheFromExistingData(
					new ProjectIdentifier(FDOBackendProviderType.kSharedXML, path),
					Thread.CurrentThread.CurrentUICulture.Name, _fdoUi,
					_fdoDirs, settings, _progress);
			}
			catch (FdoDataMigrationForbiddenException)
			{
				Console.WriteLine("Error: Incompatible version");
				return null;
			}
			catch (FdoNewerVersionException)
			{
				Console.WriteLine("Error: Incompatible version");
				return null;
			}
			catch (FdoFileLockedException)
			{
				Console.WriteLine("Error: Access denied");
				return null;
			}
			catch (StartupException)
			{
				Console.WriteLine("Error: Unknown error");
				return null;
			}

//			int langId = 1;
//			if (fdoCache.ServiceLocator.WritingSystems.CurrentVernacularWritingSystems.All(ws => ws.Id != langId))
//			{
//				fdoCache.ServiceLocator.GetInstance<IUndoStackManager>().Save();
//				fdoCache.Dispose();
//				Console.WriteLine("Error: Invalid language");
//				return null;
//			}
//
			return fdoCache;
		}
	}
}

