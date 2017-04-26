#pragma once

#include "App.h"
#include "SciterHost.hpp"
#include "SciterArchive.hpp"

#include "aux-slice.h"
#include <string>

#include "../ArchiveResource.cpp"


class BaseHost : public SciterHost
{
public:
	SciterArchive _archive;
	
	BaseHost(SciterWindow& wnd) : SciterHost(wnd)
	{
		_archive.Open(aux::make_slice(resources));
	}
	
	void SetupPage(LPCWSTR page_from_res_folder)
	{
		std::ustring path;
		path = WSTR("archive://app/");
		path += page_from_res_folder;
		_wnd.LoadPage(path.c_str());
	}
	
	virtual LRESULT OnLoadData(LPSCN_LOAD_DATA pnmld) override
	{
		aux::wchars wu = aux::chars_of(pnmld->uri);
		if(wu.like(WSTR("archive://app/*")))
		{
			// load resource from SciterArchive
			auto data = _archive.Get(wu.start + 14);
			if(data.length != 0)
				::SciterDataReady(_wnd.GetHWND(), pnmld->uri, data.start, data.length);
		}
		else if(wu.like(WSTR("sciter:debug-peer.tis")))
		{
			auto data = _archive.Get(WSTR("LibConsole/console.tis"));
			::SciterDataReady(_wnd.GetHWND(), pnmld->uri, data.start, data.length);
		}
		else if(wu.like(WSTR("sciter:utils.tis")))
		{
			auto data = _archive.Get(WSTR("LibConsole/utils.tis"));
			::SciterDataReady(_wnd.GetHWND(), pnmld->uri, data.start, data.length);
		}
		return LOAD_OK;
	}
};
