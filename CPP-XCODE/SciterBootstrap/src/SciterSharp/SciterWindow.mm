//
//  SciterWindow.m
//  SciterBootstrap
//
//  Created by Ramon Mendes on 09/04/17.
//  Copyright © 2017 MI Software. All rights reserved.
//

#import <Cocoa/Cocoa.h>

#include "SciterWindow.hpp"


void SciterWindow::CenterTopLevelWindow()
{
	NSView* nsview = (__bridge NSView*) _hwnd;
	[[nsview window] center];
}

void SciterWindow::Show(bool show)
{
	NSView* nsview = (__bridge NSView*) _hwnd;
	if(show)
	{
		[[nsview window] makeMainWindow];
		//[[nsview window] makeKeyWindow];
		[[nsview window] makeKeyAndOrderFront:nil];
	} else {
		[[nsview window] orderOut:[nsview window]];
	}
}
