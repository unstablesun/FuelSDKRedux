//
//  fuel.h
//  fuel
//
//  Created by Alan Price on 2015-07-31.
//  Copyright (c) 2015 Fuel. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UILocalNotification.h>
#import "fueltypes.h"

@interface fuel : NSObject

+ (void)setup:(NSString*)gameID gameSecret:(NSString*)gameSecret gameHasLogin:(BOOL)gameHasLogin gameHasInvite:(BOOL)gameHasInvite gameHasShare:(BOOL)gameHasShare;
+ (fuel*)instance;

+ (void)setNotificationToken:(NSString*)token;
+ (BOOL)handleRemoteNotification:(NSDictionary*)userInfo newLaunch:(BOOL)bNewLaunch;
+ (BOOL)handleLocalNotification:(UILocalNotification*)notification newLaunch:(BOOL)bNewLaunch;
+ (void)cancelAllNotifications;
+ (void)restoreAllLocalNotifications;

+ (BOOL)enableNotification:(fuelnotificationtype)notificationType;
+ (BOOL)disableNotification:(fuelnotificationtype)notificationType;
+ (BOOL)isNotificationEnabled:(fuelnotificationtype)notificationType;
+ (BOOL)setLocalNotificationMessage:(fuellocalnotificationtype*)type message:(NSString*)message;
+ (BOOL)setLocalNotificationActive:(fuellocalnotificationtype*)type active:(BOOL)active;
+ (BOOL)isLocalNotificationActive:(fuellocalnotificationtype*)type;

- (void)setLanguageCode:(NSString*)languageCode;

- (BOOL)syncVirtualGoods;
- (BOOL)acknowledgeVirtualGoods:(NSString*)transactionID consumed:(BOOL)consumed;
- (BOOL)acknowledgeVirtualGoods:(NSString*)transactionID acknowledgementTokens:(NSArray*)acknowledgementTokens consumed:(BOOL)consumed;

- (BOOL)sdkSocialLoginCompleted:(NSDictionary*)loginData;
- (BOOL)sdkSocialInviteCompleted;
- (BOOL)sdkSocialShareCompleted;
- (BOOL)getLocalizationFile;

- (BOOL)requestUpdateUserInfo:(NSDictionary*)userDetails;
- (BOOL)requestUserAvatars;
- (BOOL)requestUserInfo;

@end
