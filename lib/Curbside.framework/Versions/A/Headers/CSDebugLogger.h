//
//  CSDebugLogger.h
//  Curbside
//
//  Created by Radwar on 4/11/14.
//
//

#import <Foundation/Foundation.h>
#import "CSDebugLoggerProtocol.h"

#define VERBOSE_LOGGING 1

#   define DLog(fmt, ...) [[CSDebugLogger sharedLogger] classInfo:[self class] log:fmt, ##__VA_ARGS__]
#   define FLog(fmt, ...) [[CSDebugLogger sharedLogger] classInfo:[NSObject class] log:fmt, ##__VA_ARGS__]
#   define LLog(fmt, ...) [[CSDebugLogger sharedLogger] classInfo:[self class] log:fmt, ##__VA_ARGS__]
#   define DFLog(name, fmt, ...) [[CSDebugLogger sharedLogger] classInfo:name log:fmt, ##__VA_ARGS__]

#if VERBOSE_LOGGING == 1
#   define VLog(fmt, ...) [[CSDebugLogger sharedLogger] classInfo:[self class] log:fmt, ##__VA_ARGS__]
#else
#   define VLog(fmt, ...)
#endif

@interface CSDebugLogger : NSObject<CSDebugLoggerProtocol>

+(CSDebugLogger *)sharedLogger;
+(void)enableLogger;
+(void)disableLogger;
+ (BOOL)isLoggingEnabled;
+(void)enableWriteToFile;
+(void)enableConsoleLogging;
+(void)disableWriteToFile;

+(void)enableCustomerLoggingMode;
+(void)disableCustomerLoggingMode;

- (void)classInfo:className log:(NSString *)format, ...;
- (void)clearLogs;
- (void)dumpLogs;
- (void)flushLogs;
- (NSString *)logString;
- (void)uploadLogsIfNeeded;

- (void)retryUploadLogs;

@end
