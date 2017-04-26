#import "App.h"

#include "sciter-x.h"
#include "Window.hpp"
#include "Host.hpp"

#include <string>


Window* AppWindow;
Host* AppHost;


@implementation AppDelegate

- (void)applicationDidFinishLaunching:(NSNotification *)aNotification {
	// Create the window
	AppWindow = new Window();
	AppHost = new Host(*AppWindow);
}


- (void)applicationWillTerminate:(NSNotification *)aNotification {
	delete AppHost;
	delete AppWindow;
}

@end



bool ParseArgs(int argc, const char * argv[])
{
	for(int i = 0; i<argc; i++)
	{
		aux::chars wu = aux::chars_of(argv[i]);
	}
	return true;
}
