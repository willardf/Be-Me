">be me" is a small text adventure game in the style of a 4chan greentext, [hosted on Newgrounds](https://www.newgrounds.com/portal/view/704081).

Note that the nature of greentexts is rooted in immature, asinine, sexual, random humour. It often becomes extremely problematic. I tried to capture that without allowing it to become offensive, but just understand that the hard-coded dialogue in this repo is, frankly, a little embarrassing to post.

I'm archiving this code publicly, as-is, including the random psd at the root (idk). 
It was completed in under 24 work-hours, so it's not the prettiest, but hopefully it'll help someone.

People can use it as reference for its architecture (which I think is suitable for small-medium text adventure games).  It is essentially a series of state machines tied together. [GameManager.cs](https://github.com/willardf/Be-Me/blob/master/Assets/Scripts/GameManager.cs) is the most interesting class. If I were to expand it, there would be more classes like [SchoolLocation.cs](https://github.com/willardf/Be-Me/blob/master/Assets/Scripts/SchoolLocation.cs).

The fixed-width bitmap shader is also interesting, albeit not how I would recommend rendering bitmap fonts nowadays.

It also makes use of the [newgrounds.io For Unity](https://bitbucket.org/newgrounds/newgrounds.io-for-unity-c/) Library for achievements.

No attribution necessary, but consider following me on [Twitter](https://twitter.com/forte_bass)