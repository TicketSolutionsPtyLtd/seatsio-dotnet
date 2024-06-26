using System.Threading.Tasks;
using Xunit;

namespace SeatsioDotNet.Test.Events;

public class UpdateChannelTest : SeatsioClientTest
{
    [Fact]
    public async Task UpdateName()
    {
        var event1 = await Client.Events.CreateAsync(CreateTestChart());
        await Client.Events.Channels.AddAsync(event1.Key, "channelKey1", "channel 1", "#FFFF98", 1, new[] {"A-1", "A-2"});

        await Client.Events.Channels.UpdateAsync(event1.Key, "channelKey1", "new channel name", null, null);

        var retrievedEvent = await Client.Events.RetrieveAsync(event1.Key);
        Assert.Equal(1, retrievedEvent.Channels.Count);
        var channel1 = retrievedEvent.Channels[0];
        Assert.Equal("channelKey1", channel1.Key);
        Assert.Equal("new channel name", channel1.Name);
        Assert.Equal("#FFFF98", channel1.Color);
        Assert.Equal(1, channel1.Index);
        Assert.Equal(new[] {"A-1", "A-2"}, channel1.Objects);
    }

    [Fact]
    public async Task UpdateColor()
    {
        var event1 = await Client.Events.CreateAsync(CreateTestChart());
        await Client.Events.Channels.AddAsync(event1.Key, "channelKey1", "channel 1", "#FFFF98", 1, new[] {"A-1", "A-2"});

        await Client.Events.Channels.UpdateAsync(event1.Key, "channelKey1", null, "red", null);

        var retrievedEvent = await Client.Events.RetrieveAsync(event1.Key);
        Assert.Equal(1, retrievedEvent.Channels.Count);
        var channel1 = retrievedEvent.Channels[0];
        Assert.Equal("channelKey1", channel1.Key);
        Assert.Equal("channel 1", channel1.Name);
        Assert.Equal("red", channel1.Color);
        Assert.Equal(1, channel1.Index);
        Assert.Equal(new[] {"A-1", "A-2"}, channel1.Objects);
    }

    [Fact]
    public async Task UpdateObjects()
    {
        var event1 = await Client.Events.CreateAsync(CreateTestChart());
        await Client.Events.Channels.AddAsync(event1.Key, "channelKey1", "channel 1", "#FFFF98", 1, new[] {"A-1", "A-2"});

        await Client.Events.Channels.UpdateAsync(event1.Key, "channelKey1", null, null, new[] {"B-1"});

        var retrievedEvent = await Client.Events.RetrieveAsync(event1.Key);
        Assert.Equal(1, retrievedEvent.Channels.Count);
        var channel1 = retrievedEvent.Channels[0];
        Assert.Equal("channelKey1", channel1.Key);
        Assert.Equal("channel 1", channel1.Name);
        Assert.Equal("#FFFF98", channel1.Color);
        Assert.Equal(1, channel1.Index);
        Assert.Equal(new[] {"B-1"}, channel1.Objects);
    }
}