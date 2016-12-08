using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;

class Script
{
	static readonly string INIT_CWD = Environment.CurrentDirectory + '\\';

	static public void Main(string[] args)
	{
		// Clone templates
		if(false)
		{
			SpawnProcess("git", "clone -b master --single-branch https://github.com/MISoftware/SciterBootstrap-CSharp.git SciterBootstrap-CSharp-master");
			SpawnProcess("git", "clone -b TemplateWebcam --single-branch https://github.com/MISoftware/SciterBootstrap-CSharp.git SciterBootstrap-CSharp-TemplateWebcam");
			SpawnProcess("git", "clone -b TemplateNCRenderer --single-branch https://github.com/MISoftware/SciterBootstrap-CSharp.git SciterBootstrap-CSharp-TemplateNCRenderer");
			SpawnProcess("git", "clone -b TemplateMultiPlatform --single-branch https://github.com/MISoftware/SciterBootstrap-CSharp.git SciterBootstrap-CSharp-TemplateMultiPlatform");
			SpawnProcess("git", "clone -b TemplateAeroTabs --single-branch https://github.com/MISoftware/SciterBootstrap-CSharp.git SciterBootstrap-CSharp-TemplateAeroTabs");

			SpawnProcess("git", "clone -b master --single-branch https://github.com/MISoftware/SciterBootstrap-D.git SciterBootstrap-D-master");
			SpawnProcess("git", "clone -b TemplateAeroTabs --single-branch https://github.com/MISoftware/SciterBootstrap-D.git SciterBootstrap-D-TemplateAeroTabs");
			SpawnProcess("git", "clone -b Template-AeroOrb --single-branch https://github.com/MISoftware/SciterBootstrap-D.git SciterBootstrap-D-Template-AeroOrb");

			SpawnProcess("git", "clone -b master --single-branch https://github.com/MISoftware/SciterBootstrap-CPP.git SciterBootstrap-CPP-master");
			SpawnProcess("git", "clone -b TemplateOpenCV --single-branch https://github.com/MISoftware/SciterBootstrap-CPP.git SciterBootstrap-CPP-TemplateOpenCV");
		}

		// Update C# project (Nuget, dll)
		if(true)
		{
			File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin.osx\sciter-osx-32.dylib", INIT_CWD + @"SciterBootstrap-CSharp-TemplateMultiPlatform\SciterBootstrap\sciter-osx-32.dylib", true);

			var dirs_csharp = Directory.EnumerateDirectories(INIT_CWD).Where(dir => dir.Contains("SciterBootstrap-CSharp-"));
			foreach(var dir_path in dirs_csharp)
			{
				// Nuget update (does not work for TemplateMultiPlatform)
				SpawnProcess("nuget", "restore", dir_path);
				SpawnProcess("nuget", "update SciterBootstrap.sln", dir_path);

				// Copy DLLs
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter32.dll", dir_path + @"\SciterBootstrap\sciter32.dll", true);
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter64.dll", dir_path + @"\SciterBootstrap\sciter64.dll", true);
			}
		}

		// Update D projects (GIT, dll's)
		if(true)
		{
			var dirs_d = Directory.EnumerateDirectories(INIT_CWD).Where(dir => dir.Contains("SciterBootstrap-D"));
			foreach(var dir_path in dirs_d)
			{
				// GIT update
				SpawnProcess("cmd", "/C call copy-vendor.bat", dir_path + @"\vendor");

				// Copy DLLs
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter32.dll", dir_path + @"\Debug\sciter32.dll", true);
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter32.dll", dir_path + @"\Release\sciter32.dll", true);
			}
		}

		// Update C++ projects (GIT, dll's)
		if(true)
		{
			var dirs_cpp = Directory.EnumerateDirectories(INIT_CWD).Where(dir => dir.Contains("SciterBootstrap-CPP"));
			foreach(var dir_path in dirs_cpp)
			{
				// Copy \include
				var files = Directory.EnumerateFiles(@"D:\Projetos\Libs Shared\sciter-sdk-3\include", "*", SearchOption.AllDirectories);
				foreach(var file in files)
				{
					string subfile = file.Substring(@"D:\Projetos\Libs Shared\sciter-sdk-3\include".Length);
					File.Copy(file, dir_path + @"\vendor\sciter-sdk-3\include\" + subfile, true);
				}

				// Copy lib
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\lib\sciter32.lib", dir_path + @"\vendor\sciter-sdk-3\lib\sciter32.lib", true);
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\lib\sciter64.lib", dir_path + @"\vendor\sciter-sdk-3\lib\sciter64.lib", true);

				// Copy DLLs
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter32.dll", dir_path + @"\vendor\sciter-sdk-3\bin\sciter32.dll", true);
				File.Copy(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter64.dll", dir_path + @"\vendor\sciter-sdk-3\bin\sciter64.dll", true);
			}
		}

		// All: git add/commit/push
		if(true)
		{
			var sciter_ver = FileVersionInfo.GetVersionInfo(@"D:\Projetos\Libs Shared\sciter-sdk-3\bin\sciter32.dll");
			string sciter_ver_str = string.Format("{0}.{1}.{2}.{3}", sciter_ver.FileMajorPart, sciter_ver.FileMinorPart, sciter_ver.FileBuildPart, sciter_ver.FilePrivatePart);
			string commit_msg = string.Format("Update Boot -- Sciter version " + sciter_ver_str);

			var dirs = Directory.EnumerateDirectories(INIT_CWD);
			foreach(var dir_path in dirs)
			{
				Console.WriteLine();
				Console.WriteLine("#####################################################");
				Console.WriteLine("# PUSH repo: " + dir_path);
				Console.WriteLine("#####################################################");

				SpawnProcess("git", "add .", dir_path);
				SpawnProcess("git", "commit -m\"" + commit_msg + "\"", dir_path, true);
				SpawnProcess("git", "push origin", dir_path);
			}
		}
	}

	static public void SpawnProcess(string exe, string args, string cwd = null, bool ignore_error = false)
	{
		var startInfo = new ProcessStartInfo(exe, args)
		{
			FileName = exe,
			Arguments = args,
			UseShellExecute = false,
			WorkingDirectory = cwd ?? INIT_CWD
		};

		var p = Process.Start(startInfo);
		p.WaitForExit();

		if(p.ExitCode != 0)
		{
			Console.ForegroundColor = ignore_error ? ConsoleColor.Yellow : ConsoleColor.Red;

			string msg = exe + ' ' + args;
			Console.WriteLine();
			Console.WriteLine("-------------------------");
			Console.WriteLine("FAILED: " + msg);
			Console.WriteLine("EXIT CODE: " + p.ExitCode);
			if(!ignore_error)
				Console.WriteLine("Press ENTER to exit");
			Console.WriteLine("-------------------------");
			Console.ResetColor();

			if(!ignore_error)
			{
				Console.ReadLine();
				Environment.Exit(0);
			}
		}

		Console.WriteLine();
	}
}