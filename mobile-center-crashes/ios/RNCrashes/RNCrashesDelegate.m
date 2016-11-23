#import "RNCrashesDelegate.h"

#import "RCTEventDispatcher.h"
#import "RNCrashesUtils.h"

@import MobileCenterCrashes.MSErrorAttachment;

static NSString *ON_BEFORE_SENDING_EVENT = @"MobileCenterErrorReportOnBeforeSending";
static NSString *ON_SENDING_FAILED_EVENT = @"MobileCenterErrorReportOnSendingFailed";
static NSString *ON_SENDING_SUCCEEDED_EVENT = @"MobileCenterErrorReportOnSendingSucceeded";


@implementation RNCrashesDelegateBase

- (instancetype) init
{
    self.reports = [[NSMutableArray alloc] init];
    return self;
}

- (BOOL) crashes:(MSCrashes *)crashes shouldProcessErrorReport:(MSErrorReport *)errorReport
{
    // By default handle all reports and expose them all to JS.
    [self storeReportForJS: errorReport];
    return YES;
}

- (MSUserConfirmationHandler)shouldAwaitUserConfirmationHandler
{
    // Do not send anything until instructed to by JS
    return ^(NSArray<MSErrorReport *> *errorReports){
        return YES;
    };
}

- (void)storeReportForJS:(MSErrorReport *) report
{
    [self.reports addObject:report];
}

- (void) crashes:(MSCrashes *)crashes willSendErrorReport:(MSErrorReport *)errorReport
{
    [self.bridge.eventDispatcher sendAppEventWithName:ON_BEFORE_SENDING_EVENT body:convertReportToJS(errorReport)];
}

- (void) crashes:(MSCrashes *)crashes didSucceedSendingErrorReport:(MSErrorReport *)errorReport
{
    [self.bridge.eventDispatcher sendAppEventWithName:ON_SENDING_SUCCEEDED_EVENT body:convertReportToJS(errorReport)];
}

- (void) crashes:(MSCrashes *)crashes didFailSendingErrorReport:(MSErrorReport *)errorReport withError:(NSError *)sendError
{
    [self.bridge.eventDispatcher sendAppEventWithName:ON_SENDING_FAILED_EVENT body:convertReportToJS(errorReport)];
}

- (void) provideAttachments: (NSDictionary*) attachments
{
    self.attachments = attachments;
}

- (MSErrorAttachment *)attachmentWithCrashes:(MSCrashes *)crashes forErrorReport:(MSErrorReport *)errorReport
{
    NSObject* attachment = [self.attachments objectForKey:[errorReport incidentIdentifier]];
    if (attachment && [attachment isKindOfClass:[NSString class]]) {
        NSString * stringAttachment = (NSString *)attachment;
        return [MSErrorAttachment attachmentWithText:stringAttachment];
    }

    return nil;
}

- (NSArray<MSErrorReport *>*) getAndClearReports
{
    NSArray<MSErrorReport *>* result = self.reports;
    self.reports = [[NSMutableArray alloc] init];
    return result;
}

@end

@implementation RNCrashesDelegateAlwaysSend
- (BOOL) crashes:(MSCrashes *)crashes shouldProcessErrorReport:(MSErrorReport *)errorReport
{
    // Do not pass the report to JS, but do process them
    return YES;
}

- (MSUserConfirmationHandler)shouldAwaitUserConfirmationHandler
{
    // Do not wait for user confirmation, always send.
    return ^(NSArray<MSErrorReport *> *errorReports){
        return NO;
    };
}

@end