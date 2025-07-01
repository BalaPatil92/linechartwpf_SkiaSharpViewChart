# linechartwpf_SkiaSharpViewChart

# This libraries we have used for wpf 
using LiveChartsCore.SkiaSharpView;

6 Dataset Loaded At Initial
----------------------

X-Axis (Horizontal Axis):
Represents Time, aggregated per minute.

The labels (e.g., 06-29 15:47, 06-30 11:07, etc.) are timestamps.

These timestamps are generated from raw data spanning approximately 3 days (260,000 seconds ≈ 72 hours), then grouped by minute (resulting in ~4320 data points per dataset).

Y-Axis (Vertical Axis):
Represents the numeric values generated randomly (rand.NextDouble() * 100) for each timestamp.

Since you're aggregating per minute using Average, each Y value is the average of 60 raw points (1 per second) over each minute.

Chart Series:
You have 6 datasets, each plotted as a separate LineSeries<double> with different colors.

Each dataset simulates different streams or sources of data, all plotted over the same time range.

The orange series (Dataset 6) seems to have larger geometry (dots) or overlap, which might cause the cluttered effect you're seeing in the middle.



Real Time simulation
----------------------
X-Axis (Horizontal):

Shows timestamps (formatted as HH:mm:ss) that are added once per second.

This is implemented using:

csharp
Copy
Edit
var timestamp = DateTime.Now.ToString("HH:mm:ss");
_labels.Add(timestamp);
Y-Axis (Vertical):

Shows the random numeric values between 0 and 100 generated every second.

These simulate a live data feed (e.g., from a sensor, stock ticker, etc.).

csharp
Copy
Edit
var value = rand.NextDouble() * 100;
Data Points:

Each blue circle represents a single data point.

The blue line connects those points in time order.

Tooltip is enabled and shows:

The time the data was added.

The corresponding value.
