//
//  fuelignite.h
//  fuelsdk
//
//  Created by Alan Price on 2015-07-31.
//  Copyright (c) 2015 Fuel. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "fueltypes.h"

@interface fuelignite : NSObject

+ (void)setup;
+ (fuelignite*)instance;


- (BOOL)execMethod:(NSString *)method params:(NSArray *)params;

- (BOOL)sendProgress:(NSDictionary *)progress ruleTags:(NSArray *)ruleTags;
- (BOOL)getEvents:(NSArray *)eventTags;
- (BOOL)getSampleEvents:(NSArray *)eventTags;
- (BOOL)joinEvent:(NSString *)eventID;
- (BOOL)getLeaderBoard:(NSString *)boardID;
- (BOOL)getMission:(NSString *)missionID;
- (BOOL)getQuest:(NSString *)questID;
- (BOOL)getOffer:(NSString *)offerID;
- (BOOL)acceptOffer:(NSString *)offerID;

@end
