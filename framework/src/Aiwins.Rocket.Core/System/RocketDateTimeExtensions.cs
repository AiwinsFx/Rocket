namespace System {
    public static class RocketDateTimeExtensions {
        public static DateTime ClearTime (this DateTime dateTime) {
            return dateTime.Subtract (
                new TimeSpan (
                    0,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
                )
            );
        }

        public static DateTimeOffset ClearTime (this DateTimeOffset dateTime) {
            return dateTime.Subtract (
                new TimeSpan (
                    0,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
                )
            );
        }
    }
}