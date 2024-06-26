using System.Linq;
using System.Threading.Tasks;
using SeatsioDotNet.Reports.Usage;
using SeatsioDotNet.Reports.Usage.DetailsForEventInMonth;
using Xunit;
using Xunit.Abstractions;

namespace SeatsioDotNet.Test.Reports.Usage;

public class UsageReportTest : SeatsioClientTest
{
    private readonly ITestOutputHelper TestOutputHelper;

    public UsageReportTest(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task TestUsageReportForAllMonths()
    {
        if (!DemoCompanySecretKeySet())
        {
            warnAboutDemoCompanySecretKeyNotSet();
            return;
        }

        var client = CreateSeatsioClient(DemoCompanySecretKey());

        var report = await client.UsageReports.SummaryForAllMonthsAsync();

        Assert.True(report.UsageCutoffDate.Year > 2000);
        Assert.True(report.Usage.Any());
        Assert.Equal(2, report.Usage.ElementAt(0).Month.Month);
        Assert.Equal(2014, report.Usage.ElementAt(0).Month.Year);
    }

    [Fact]
    public async Task TestUsageReportForMonth()
    {
        if (!DemoCompanySecretKeySet())
        {
            warnAboutDemoCompanySecretKeyNotSet();
            return;
        }

        var client = CreateSeatsioClient(DemoCompanySecretKey());

        var report = await client.UsageReports.DetailsForMonthAsync(new UsageMonth(2021, 11));

        Assert.True(report.Any());
        Assert.True(report.ElementAt(0).UsageByChart.Any());
        Assert.Equal(143, report.ElementAt(0).UsageByChart.ElementAt(0).UsageByEvent.ElementAt(0).NumUsedObjects);
    }

    [Fact]
    public async Task TestUsageReportForEventInMonth()
    {
        if (!DemoCompanySecretKeySet())
        {
            warnAboutDemoCompanySecretKeyNotSet();
            return;
        }

        var client = CreateSeatsioClient(DemoCompanySecretKey());

        var report = await client.UsageReports.DetailsForEventInMonthAsync(580293, new UsageMonth(2021, 11));

        Assert.True(report.Any());
        Assert.Equal(1, ((UsageForObjectV1) report.ElementAt(0)).NumFirstSelections);
    }

    private void warnAboutDemoCompanySecretKeyNotSet()
    {
        TestOutputHelper.WriteLine("DEMO_COMPANY_SECRET_KEY environment variable not set. Skipping test.");
    }
}