#import <UIKit/UIKit.h>

void OpenURL(const char* url) {
    NSString* urlString = [NSString stringWithUTF8String:url];
    NSURL* nsUrl = [NSURL URLWithString:urlString];

    if ([[UIApplication sharedApplication] canOpenURL:nsUrl]) {
        if (@available(iOS 10.0, *)) {
            [[UIApplication sharedApplication] openURL:nsUrl options:@{} completionHandler:^(BOOL success) {
                if (success) {
                    NSLog(@"URL opened successfully");
                } else {
                    NSLog(@"Failed to open URL");
                }
            }];
        } else {
            [[UIApplication sharedApplication] openURL:nsUrl];
        }
    }
}