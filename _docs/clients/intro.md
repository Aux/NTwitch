---
layout: doc_page
permalink: docs/clients/
title: Intro
description: An intro into using, and the similarities between, the clients provided by NTwitch
last_modified_at: 2017-08-03 07:30 -0600
---

Every accessible feature of NTwitch is tied strongly around the use of clients. Each client has a configuration class that can be used to change certain behaviors as the developer sees fit. One common example is the `LogLevel` property, which changes the amount of information that is provided to the `Log` event. 

### [Rest](/clients/rest)

The `TwitchRestClient` found in the `NTwitch.Rest` package is used for making http requests to Twitch's REST API. This client allows the developer to to make "unauthenticated" requests with the application's Client Id (where applicable), and authenticated requests with a user's OAuth Token. 

### [Pubsub](/clients/pubsub)

The `TwitchPubsubClient` found in the `NTwitch.Pubsub` package is used for connecting to Twitch's PubSub service via a WebSocket connection. This client implements a basic version of the `TwitchRestClient` allowing the developer access to REST endpoints while including methods to subscribe and unsubscribe to event topics.

### [Chat](/clients/chat)

The `TwitchChatClient` found in the `NTwitch.Chat` package is used for connecting to Twitch's pseudo-irc service via either a TCP or WebSocket connection. This client also implements a basic version of the `TwitchRestClient` and includes several methods to manage the user's chat connection.