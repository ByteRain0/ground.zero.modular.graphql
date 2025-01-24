Ideas to test event sourcing on:

We are going to play around inside the Rating.Api with event sourcing.
Events:
- RatingForAnimeSubmitted
- RatingForMangaSubmitted
- RatingSuspiciousActivityDetected
- RatingPopularityChaged

Community Metrics:
- Calculate and display community-wide metrics (e.g., most-rated genres, average rating by studio/author).
- Generate leaderboards for top-rated animes/mangas. 
- Show rankings such as "Top Rated Animes of the Month" or "Most Read Mangas This Week."
Personal Metrics:
- Use rating data to suggest similar highly-rated animes/mangas to users.
- Track user rating history with events like RatingAdded or RatingUpdated.
- Send notifications like "You've rated 10 animes this month!"
Fraud Detection:
- Trigger RatingSuspiciousActivityDetected if a single user or group inflates ratings for an entity abnormally.
Payload: { entityId, anomalyType, details, timestamp }