## MasterServer
Has methods to query master server.

Valve's master server throttles communication with clients to 30 packets per minute.
If this limit is exceeded,master server wont respond for  few minutes.

1 packet = 1 batch = 231 addressess

So instead of fetching all ips at once(as in earlier version),user can now  choose how many batches to fetch, thus helps in controlling number of request.

A new method(GetNextBatch) is also been added to get rest of the batches.

The IpReceivedCallback has been removed.It now uses BatchInfoReceivedCallback,which provides an instance of BatchInfo class.
BatchInfo class has a flag "IsLastBatch" which tells if the received batch is last or not.
The last ip "0.0.0.0" is also been removed from lastbatch.
thus with this update,there is no need to check if last ip in the received list is "0.0.0.0" or to remove it from the received list.

Retry functionality and callback for each attempt has also been included.
Since there is always a chance that server replied but wasn't received,AttemptCallback helps to keep track of such requests.

IPFilter is also updated with new parameters.


Read documentation for more details.





