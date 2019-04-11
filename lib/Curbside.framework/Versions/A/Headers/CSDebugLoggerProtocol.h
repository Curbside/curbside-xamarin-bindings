//
//  CSDebugLoggerProtocol.h
//  Curbside
//
//  Created by Hon Chen on 1/25/18.
//  Copyright © 2018 Curbside. All rights reserved.
//

#import <Foundation/Foundation.h>
@class CSDebugLogger;

@protocol CSDebugLoggerProtocol <NSObject>

+(CSDebugLogger *)sharedLogger;
- (void)classInfo:className log:(NSString *)format, ...;

@end
