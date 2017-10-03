﻿namespace Vostok.Metrics
{
    public interface IMetricEventReporter
    {
        void SendEvent(MetricEvent metricEvent);
        void SendMetric(MetricEvent metricEvent);
    }
}