﻿Create View [dbo].[MouseCaughtEvents] as
 select ClearedEvent.TrapId, ClearedEvent.building, ClearedEvent.location, ClearedEvent.eventDate as ClearedDate, TrippedEvent.eventDate as TrippedDate,
  DATEDIFF(minute, ClearedEvent.eventDate , TrippedEvent.eventDate) as MinutesToCatch

 from Trapevents ClearedEvent
	 Left Outer Join Trapevents TrippedEvent
	 On ClearedEvent.TrapId = TrippedEvent.TrapId
	and TrippedEvent.EventType = 2
	and TrippedEvent.eventDate = (select min(eventDate) 
									from Trapevents TrippedEvents
									where TrippedEvents.TrapId = ClearedEvent.TrapId
										and TrippedEvents.EventType = 2
										and TrippedEvents.eventDate > ClearedEvent.eventDate
									)
where ClearedEvent.EventType = 1

