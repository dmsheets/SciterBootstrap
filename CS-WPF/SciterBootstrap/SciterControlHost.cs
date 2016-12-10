using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using SciterSharp;

namespace SciterBootstrap
{
	class SciterControlHost : HwndHost
	{
		private SciterWindow _wnd;

		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			_wnd = new SciterWindow();
			_wnd.CreateChildWindow(hwndParent.Handle);
			_wnd.LoadHtml("<h1 style=color:blue>Sciter Hello World!</h1>");

			return new HandleRef(this, _wnd._hwnd);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			_wnd.Destroy();
		}
	}
}