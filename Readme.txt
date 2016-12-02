This implementation was an exercise for a job interview.
The task was to re-write an asset management system to load asset bundles over the internet.


Folder Summary

Content/
	- contains classes representing metadata or specific asset types

Interfaces/
	- contains member declarations of different entities used

Loader/
	- contains implementations that read or process asset metadata source files or metadata objects

Logger/
	- contains logger implementations

Matcher/
	- contains classes that check assets for certain parameters
	
Processors/
	- contains classes that evaluate web streams

Services/
	- contains management classes

Tasks/
	- contains implementations of different asynchronous tasks, e.g. fetching web streams
	

Known Issues
	- no parenting
	- incomplete asset unloading (should it remove all instances?)
	- manual metadata file placement (Webplayer does not allow HDD access)