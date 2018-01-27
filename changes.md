*This document is a work in progress...*

## General
- Properly implemented the `Optional<T>` class for entity models.
- Changed entity helper class access to public.
- Added automatic pagination where possible.
- Added REST ratelimit handling

#### Breaking Changes: 
- The `Stream` entity type has been renamed to `Broadcast`, along with all accompanying types.

## Kraken -> Helix Changes:
##### Users
- No longer able to get token information to check for required scopes.
- No longer able to check if a user is verified, partnered, or twitter connected via REST.
- No longer able to view a user's notification settings.
- No longer able to check if a user is subscribed to a channel.
##### Clips
- Added ability to create a clip during a broadcast.
- Creator, Broadcaster, and Video entity information reduced to only their respective Ids.
- No longer able to view a clip's duration.
##### Broadcasts
- Channel and Game information reduced to their respective Ids.
- No longer able to view a broadcast's delay, fps value, or video height.
##### Channels
- This entity type no longer exists (split into User and Broadcast).