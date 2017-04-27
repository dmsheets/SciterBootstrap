#import <Cocoa/Cocoa.h>
#import "App.hpp"

#include "sciter-x.h"


int main(int argc, const char* argv[])
{
	SciterSetOption(nullptr, SCITER_SET_GFX_LAYER, GFX_LAYER_CG);
	
	App::ParseArgs(argc, argv);
	
	NSApplication* application = [NSApplication sharedApplication];
	
	NSArray* tl;
	[[NSBundle mainBundle] loadNibNamed:@"MainMenu" owner:application topLevelObjects:&tl];
	
	AppDelegate* applicationDelegate = [[AppDelegate alloc] init];
	[application setDelegate:applicationDelegate];
	[application run];
}
