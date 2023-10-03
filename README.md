This program was created in the process of recruitation to show skills of the author.
The program implements requirements stated in "Santander - Developer Coding Test" doc.

#API:

There is one endpoint:

[GET] /BestStories
param: int count - number of requested Best Stories details
example query: http://localhost:5000/BestStories?count=4
example response content:
[
  {
    "title": "What every software developer must know about Unicode in 2023",
    "uri": "https://tonsky.me/blog/unicode/",
    "postedBy": null,
    "time": "2023-10-02T09:22:05\u002B00:00",
    "score": 891,
    "commentCount": 0
  },
  {
    "title": "Return to Office Is Bullshit and Everyone Knows It",
    "uri": "https://soatok.blog/2023/10/02/return-to-office-is-bullshit-and-everyone-knows-it/",
    "postedBy": null,
    "time": "2023-10-02T15:28:10\u002B00:00",
    "score": 879,
    "commentCount": 0
  },
  {
    "title": "Goodbye integers, hello UUIDv7",
    "uri": "https://buildkite.com/blog/goodbye-integers-hello-uuids",
    "postedBy": null,
    "time": "2023-10-02T01:44:59\u002B00:00",
    "score": 706,
    "commentCount": 0
  },
  {
    "title": "Tire dust makes up the majority of ocean microplastics",
    "uri": "https://www.thedrive.com/news/tire-dust-makes-up-the-majority-of-ocean-microplastics-study-finds",
    "postedBy": null,
    "time": "2023-10-01T15:01:01\u002B00:00",
    "score": 693,
    "commentCount": 0
  }
]

#Assumptions and possible enhancements
It was observed and assumed that https://hacker-news.firebaseio.com returns constant list of stories IDs. In order not to overload the server, the list is downloaded once at the startup and used for all further client queries.
However, if the list changes from time to time, often than the the app is planned to be restarted, the list should be downloaded repeatedly with apropriate period.
The risk of overloading the Hacker News API can be diminish by creating a cache in the app (as it was observed that the details of Best Stories don't change either) and serve data from it. But for the purpose of demostration of asynchronous querying of remote server, the API is queried for the requested content once per every the app's client request.

author: Lukasz J.