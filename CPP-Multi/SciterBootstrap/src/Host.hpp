#pragma once

#include "App.h"
#include "BaseHost.hpp"
#include "Consts.h"
#include "sciter-x.h"
#include "aux-slice.h"
#include "aux-cvt.h"
#include <string>


class HostEvh : public SciterEventHandler
{
	virtual bool on_script_call(HELEMENT he, LPCSTR name, UINT argc, const SCITER_VALUE* argv, SCITER_VALUE& retval) override
	{
		if(std::string(name) == "Host_HelloWorld")
		{
			retval = sciter::value(WSTR("Hello World! (from native side)"));
			return true;
		}
		return false;
	}
};


class Host : BaseHost
{
public:
	HostEvh _evh;
	
	Host(SciterWindow& wnd) : BaseHost(wnd)
	{
		AttachEvh(_evh);
		SetupPage(WSTR("index.html"));
	}
};
